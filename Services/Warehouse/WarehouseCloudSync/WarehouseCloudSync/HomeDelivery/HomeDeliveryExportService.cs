using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WarehouseCloudSync.DropboxUtility;

namespace WarehouseCloudSync.HomeDelivery
{
    public class HomeDeliveryExportService
    {
        private readonly HomeDeliveryExportRepository repository;
        private readonly CsvExportService csvExportService;

        public HomeDeliveryExportService()
            : this(
                new HomeDeliveryExportRepository(),
                new CsvExportService())
        {
        }

        public HomeDeliveryExportService(
            HomeDeliveryExportRepository repository,
            CsvExportService csvExportService)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (csvExportService == null) throw new ArgumentNullException(nameof(csvExportService));

            this.repository = repository;
            this.csvExportService = csvExportService;
        }

        public async Task<IList<HomeDeliveryExportResult>> ExecuteAsync(HomeDeliveryExportOptions options, CancellationToken cancellationToken)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            Stopwatch stopwatch = Stopwatch.StartNew();
            SyncData.WriteHomeDeliveryLine("Home Delivery export started.");

            IList<HomeDeliveryExportResult> results = new List<HomeDeliveryExportResult>();
            IList<HomeDeliveryExportDefinition> definitions;

            try
            {
                SyncData.WriteHomeDeliveryLine($"Step 2/5: Executing main export definition proc: {options.MainProcedureName}");
                definitions = repository.GetExportDefinitions(options);
                SyncData.WriteHomeDeliveryLine($"Step 2/5 completed. Enabled export definitions loaded: {definitions.Count}");
            }
            catch (Exception ex)
            {
                SyncData.WriteHomeDeliveryLine($"Home Delivery export failed during LoadDefinitions: {ex.Message}");
                results.Add(new HomeDeliveryExportResult
                {
                    Success = false,
                    Stage = "LoadDefinitions",
                    ErrorMessage = ex.Message,
                    StartedAt = DateTime.Now,
                    CompletedAt = DateTime.Now
                });
                return results;
            }

            foreach (HomeDeliveryExportDefinition definition in definitions)
            {
                cancellationToken.ThrowIfCancellationRequested();
                SyncData.WriteHomeDeliveryLine($"Step 3/5: Processing export {definition.ExecutionOrder} - {definition.ExportCode}");
                HomeDeliveryExportResult result = ExecuteDefinitionCsv(definition, options, cancellationToken);
                results.Add(result);
            }

            IList<HomeDeliveryExportResult> generatedResults = new List<HomeDeliveryExportResult>();
            foreach (HomeDeliveryExportResult result in results)
            {
                if (result.Success)
                {
                    generatedResults.Add(result);
                }
            }

            if (generatedResults.Count > 0)
            {
                SyncData.WriteHomeDeliveryLine($"Step 4/5: Loading Dropbox configuration and uploading {generatedResults.Count} generated file(s).");
                await UploadGeneratedFilesAsync(generatedResults, options, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                SyncData.WriteHomeDeliveryLine("Step 4/5 skipped. No CSV files were generated successfully.");
            }

            stopwatch.Stop();
            SyncData.WriteHomeDeliveryLine($"Step 5/5: Home Delivery cycle completed. Duration: {stopwatch.Elapsed}");
            return results;
        }

        private HomeDeliveryExportResult ExecuteDefinitionCsv(HomeDeliveryExportDefinition definition, HomeDeliveryExportOptions options, CancellationToken cancellationToken)
        {
            HomeDeliveryExportResult result = new HomeDeliveryExportResult
            {
                ExportCode = definition.ExportCode,
                FileName = definition.FileName,
                DropboxTargetFolder = definition.DropboxTargetFolder,
                StartedAt = DateTime.Now
            };

            try
            {
                SyncData.WriteHomeDeliveryLine($"  Export {definition.ExportCode}: executing data proc {definition.ExportProcedureName}");
                result.Stage = "ExecuteProcedure";
                DataTable data = repository.ExecuteExportProcedure(definition.ExportProcedureName, options.CommandTimeoutSeconds);
                result.RowCount = data.Rows.Count;
                SyncData.WriteHomeDeliveryLine($"  Export {definition.ExportCode}: data proc completed. Rows: {data.Rows.Count}, Columns: {data.Columns.Count}");

                result.Stage = "GenerateCsv";
                string outputFilePath = BuildOutputFilePath(options, definition.FileName);
                SyncData.WriteHomeDeliveryLine($"  Export {definition.ExportCode}: generating CSV {outputFilePath}");
                CsvExportResult csvResult = csvExportService.Write(data, outputFilePath, cancellationToken);
                if (!csvResult.Success)
                {
                    throw new InvalidOperationException(csvResult.ErrorMessage, csvResult.Exception);
                }

                SyncData.WriteHomeDeliveryLine($"  Export {definition.ExportCode}: CSV generated successfully. Rows: {csvResult.RowCount}, Columns: {csvResult.ColumnCount}, Size: {csvResult.FileSizeBytes} bytes");
                result.Success = true;
                result.Stage = "CsvGenerated";
                result.OutputFilePath = outputFilePath;
                result.CompletedAt = DateTime.Now;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.CompletedAt = DateTime.Now;
                SyncData.WriteHomeDeliveryLine($"Home Delivery export failed. Export: {definition.ExportCode}, Stage: {result.Stage}, Error: {ex.Message}");
                return result;
            }
        }

        private async Task UploadGeneratedFilesAsync(IList<HomeDeliveryExportResult> generatedResults, HomeDeliveryExportOptions options, CancellationToken cancellationToken)
        {
            DropboxUploadConfig dropboxOptions;

            try
            {
                SyncData.WriteHomeDeliveryLine("  Dropbox: loading generic Dropbox config using USP_R_DROPBOX_CONFIG.");
                dropboxOptions = repository.GetDropboxConfig(options.CommandTimeoutSeconds);
                SyncData.WriteHomeDeliveryLine("  Dropbox: configuration loaded.");
            }
            catch (Exception ex)
            {
                SyncData.WriteHomeDeliveryLine($"Home Delivery export failed during DropboxConfig: {ex.Message}");
                MarkDropboxFailure(generatedResults, "DropboxConfig", ex.Message);
                return;
            }

            IDropboxFileUploader dropboxFileUploader = new DropboxFileUploader(dropboxOptions.ToDropboxOptions());

            foreach (HomeDeliveryExportResult result in generatedResults)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    result.Stage = "DropboxUpload";
                    string dropboxPath = BuildDropboxPath(options, result);
                    SyncData.WriteHomeDeliveryLine($"  Dropbox: uploading {Path.GetFileName(result.OutputFilePath)} to {dropboxPath}");
                    DropboxUploadResult uploadResult = await dropboxFileUploader.UploadFileAsync(result.OutputFilePath, dropboxPath, cancellationToken).ConfigureAwait(false);
                    if (!uploadResult.Success)
                    {
                        throw new InvalidOperationException(uploadResult.ErrorMessage, uploadResult.Exception);
                    }

                    SyncData.WriteHomeDeliveryLine($"  Dropbox: upload completed for {result.ExportCode}. Bytes: {uploadResult.UploadedBytes}, Rev: {uploadResult.DropboxRevision}, ServerModified: {uploadResult.DropboxServerModified}");
                    result.Stage = "Completed";
                    result.CompletedAt = DateTime.Now;
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.ErrorMessage = ex.Message;
                    result.CompletedAt = DateTime.Now;
                    SyncData.WriteHomeDeliveryLine($"Home Delivery upload failed. Export: {result.ExportCode}, Error: {ex.Message}");
                }
            }
        }

        private static void MarkDropboxFailure(IList<HomeDeliveryExportResult> results, string stage, string errorMessage)
        {
            foreach (HomeDeliveryExportResult result in results)
            {
                result.Success = false;
                result.Stage = stage;
                result.ErrorMessage = errorMessage;
                result.CompletedAt = DateTime.Now;
            }
        }

        private string BuildOutputFilePath(HomeDeliveryExportOptions options, string fileName)
        {
            string safeFileName = Path.GetFileName(fileName);
            if (string.IsNullOrWhiteSpace(safeFileName))
            {
                throw new InvalidOperationException("Export file name is required.");
            }

            return Path.Combine(options.WorkingFolder, safeFileName);
        }

        private string BuildDropboxPath(HomeDeliveryExportOptions options, HomeDeliveryExportResult result)
        {
            string fileName = Path.GetFileName(result.FileName);
            string folder = string.IsNullOrWhiteSpace(result.DropboxTargetFolder)
                ? options.DropboxRootFolder
                : result.DropboxTargetFolder;

            folder = NormalizeDropboxFolder(folder);
            return folder + "/" + fileName;
        }

        private static string NormalizeDropboxFolder(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder)) return string.Empty;
            folder = folder.Replace("\\", "/").Trim();
            if (!folder.StartsWith("/", StringComparison.Ordinal)) folder = "/" + folder;
            return folder.TrimEnd('/');
        }

        private static string GetSafeDropboxFolder(string dropboxPath)
        {
            int index = dropboxPath.LastIndexOf("/", StringComparison.Ordinal);
            return index <= 0 ? "/" : dropboxPath.Substring(0, index);
        }
    }
}
