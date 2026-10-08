using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace WarehouseCloudSync.HomeDelivery
{
    public class CsvExportService
    {
        public IList<CsvExportResult> WriteBranches(IDataReader reader, string workingFolder, string fileName, CancellationToken cancellationToken)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            string baseFileName = Path.GetFileName(fileName);
            if (string.IsNullOrWhiteSpace(baseFileName)) throw new InvalidOperationException("Export file name is required.");

            int branchOrdinal = -1;
            for (int index = 0; index < reader.FieldCount; index++)
            {
                if (string.Equals(reader.GetName(index), "BRANCHCODE", StringComparison.OrdinalIgnoreCase)) branchOrdinal = index;
            }
            if (branchOrdinal < 0) throw new InvalidOperationException("Inventory export must return a BRANCHCODE column.");

            Directory.CreateDirectory(workingFolder);
            IDictionary<string, StreamWriter> writers = new Dictionary<string, StreamWriter>(StringComparer.OrdinalIgnoreCase);
            IDictionary<string, CsvExportResult> results = new Dictionary<string, CsvExportResult>(StringComparer.OrdinalIgnoreCase);
            try
            {
                while (true)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (!reader.Read()) break;
                    string branchCode = Convert.ToString(reader.GetValue(branchOrdinal)).Trim();
                    if (string.IsNullOrWhiteSpace(branchCode) || branchCode.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                    {
                        throw new InvalidOperationException($"Inventory export contains a blank or invalid BRANCHCODE: '{branchCode}'.");
                    }

                    StreamWriter writer;
                    if (!writers.TryGetValue(branchCode, out writer))
                    {
                        string outputPath = Path.Combine(workingFolder, branchCode + "_" + baseFileName);
                        CsvExportResult result = new CsvExportResult
                        {
                            OutputFilePath = outputPath,
                            TemporaryFilePath = outputPath + ".tmp",
                            ColumnCount = reader.FieldCount - 1,
                            StartedAt = DateTime.Now
                        };
                        writer = new StreamWriter(result.TemporaryFilePath, false, new UTF8Encoding(false));
                        writers.Add(branchCode, writer);
                        results.Add(branchCode, result);
                        WriteRecord(writer, reader, branchOrdinal, true);
                    }

                    WriteRecord(writer, reader, branchOrdinal, false);
                    results[branchCode].RowCount++;
                }

                if (results.Count == 0) throw new InvalidOperationException("Inventory export returned no branch rows. No inventory files were generated.");
                foreach (StreamWriter writer in writers.Values) writer.Dispose();
                writers.Clear();

                // Publish only after the entire SQL result has been read successfully.
                foreach (CsvExportResult result in results.Values)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (File.Exists(result.OutputFilePath)) File.Replace(result.TemporaryFilePath, result.OutputFilePath, null);
                    else File.Move(result.TemporaryFilePath, result.OutputFilePath);
                    result.Success = true;
                    result.FileSizeBytes = new FileInfo(result.OutputFilePath).Length;
                    result.CompletedAt = DateTime.Now;
                }

                return new List<CsvExportResult>(results.Values);
            }
            finally
            {
                foreach (StreamWriter writer in writers.Values) writer.Dispose();
            }
        }

        private static void WriteRecord(StreamWriter writer, IDataRecord record, int excludedOrdinal, bool header)
        {
            bool first = true;
            for (int index = 0; index < record.FieldCount; index++)
            {
                if (index == excludedOrdinal) continue;
                if (!first) writer.Write(",");
                writer.Write(Escape(header ? record.GetName(index) : ConvertValue(record.GetValue(index))));
                first = false;
            }
            writer.WriteLine();
        }

        public CsvExportResult Write(DataTable data, string outputFilePath, CancellationToken cancellationToken)
        {
            DateTime startedAt = DateTime.Now;
            string temporaryFilePath = outputFilePath + ".tmp";
            CsvExportResult result = new CsvExportResult
            {
                OutputFilePath = outputFilePath,
                TemporaryFilePath = temporaryFilePath,
                StartedAt = startedAt
            };

            try
            {
                if (data == null) throw new ArgumentNullException(nameof(data));
                if (string.IsNullOrWhiteSpace(outputFilePath)) throw new ArgumentException("Output file path is required.", nameof(outputFilePath));

                string directory = Path.GetDirectoryName(outputFilePath);
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (File.Exists(temporaryFilePath))
                {
                    File.Delete(temporaryFilePath);
                }

                using (StreamWriter writer = new StreamWriter(temporaryFilePath, false, new UTF8Encoding(false)))
                {
                    WriteHeader(writer, data, cancellationToken);
                    WriteRows(writer, data, cancellationToken);
                }

                if (File.Exists(outputFilePath))
                {
                    File.Delete(outputFilePath);
                }

                File.Move(temporaryFilePath, outputFilePath);

                FileInfo fileInfo = new FileInfo(outputFilePath);
                result.Success = true;
                result.RowCount = data.Rows.Count;
                result.ColumnCount = data.Columns.Count;
                result.FileSizeBytes = fileInfo.Length;
                result.CompletedAt = DateTime.Now;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
                result.CompletedAt = DateTime.Now;
                return result;
            }
        }

        private static void WriteHeader(StreamWriter writer, DataTable data, CancellationToken cancellationToken)
        {
            for (int columnIndex = 0; columnIndex < data.Columns.Count; columnIndex++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (columnIndex > 0) writer.Write(",");
                writer.Write(Escape(data.Columns[columnIndex].ColumnName));
            }

            writer.WriteLine();
        }

        private static void WriteRows(StreamWriter writer, DataTable data, CancellationToken cancellationToken)
        {
            foreach (DataRow row in data.Rows)
            {
                cancellationToken.ThrowIfCancellationRequested();

                for (int columnIndex = 0; columnIndex < data.Columns.Count; columnIndex++)
                {
                    if (columnIndex > 0) writer.Write(",");
                    writer.Write(Escape(ConvertValue(row[columnIndex])));
                }

                writer.WriteLine();
            }
        }

        private static string ConvertValue(object value)
        {
            if (value == null || value == DBNull.Value) return string.Empty;
            if (value is string) return (string)value;
            if (value is IFormattable) return ((IFormattable)value).ToString(null, CultureInfo.InvariantCulture);
            return value.ToString();
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            bool mustQuote = value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0;
            if (value.Contains("\""))
            {
                value = value.Replace("\"", "\"\"");
            }

            return mustQuote ? "\"" + value + "\"" : value;
        }
    }
}
