using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace WarehouseCloudSync.HomeDelivery
{
    public class CsvExportService
    {
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
