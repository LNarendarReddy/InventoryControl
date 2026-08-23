using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using WarehouseCloudSync.Data;

namespace WarehouseCloudSync.HomeDelivery
{
    public class HomeDeliveryExportRepository
    {
        private const string HomeDeliveryConfigProcedure = "USP_R_HOMEDELIVERY_CONFIG";
        private const string DropboxConfigProcedure = "USP_R_DROPBOX_CONFIG";
        private static readonly Regex ProcedureNamePattern = new Regex(@"^[A-Za-z0-9_\.\[\]]+$", RegexOptions.Compiled);
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();
        private readonly int defaultCommandTimeoutSeconds;

        public HomeDeliveryExportRepository()
            : this(3600)
        {
        }

        public HomeDeliveryExportRepository(int defaultCommandTimeoutSeconds)
        {
            this.defaultCommandTimeoutSeconds = defaultCommandTimeoutSeconds > 0 ? defaultCommandTimeoutSeconds : 3600;
        }

        public HomeDeliveryExportOptions GetHomeDeliveryConfig()
        {
            string json = ExecuteJsonConfigProcedure(HomeDeliveryConfigProcedure);
            HomeDeliveryExportOptions options = string.IsNullOrWhiteSpace(json)
                ? new HomeDeliveryExportOptions()
                : serializer.Deserialize<HomeDeliveryExportOptions>(json);

            options.ApplyDefaults();
            return options;
        }

        public DropboxUploadConfig GetDropboxConfig(int commandTimeoutSeconds)
        {
            string json = ExecuteJsonConfigProcedure(DropboxConfigProcedure, commandTimeoutSeconds);
            DropboxUploadConfig options = string.IsNullOrWhiteSpace(json)
                ? new DropboxUploadConfig()
                : serializer.Deserialize<DropboxUploadConfig>(json);

            options.ApplyDefaults();
            return options;
        }

        public IList<HomeDeliveryExportDefinition> GetExportDefinitions(HomeDeliveryExportOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            DataTable table = ExecuteStoredProcedure(options.MainProcedureName, options.CommandTimeoutSeconds);
            return table.Rows.Cast<DataRow>()
                .Select(MapDefinition)
                .Where(definition => definition.Enabled)
                .OrderBy(definition => definition.ExecutionOrder)
                .ToList();
        }

        public DataTable ExecuteExportProcedure(string procedureName, int commandTimeoutSeconds)
        {
            return ExecuteStoredProcedure(procedureName, commandTimeoutSeconds);
        }

        private string ExecuteJsonConfigProcedure(string procedureName, int? commandTimeoutSeconds = null)
        {
            ValidateProcedureName(procedureName);

            using (SqlConnection connection = CreateWarehouseConnection())
            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = GetCommandTimeout(commandTimeoutSeconds);
                connection.Open();

                object value = command.ExecuteScalar();
                return value == null || value == DBNull.Value ? string.Empty : Convert.ToString(value);
            }
        }

        private DataTable ExecuteStoredProcedure(string procedureName, int commandTimeoutSeconds)
        {
            ValidateProcedureName(procedureName);

            DataTable table = new DataTable();
            using (SqlConnection connection = CreateWarehouseConnection())
            using (SqlCommand command = new SqlCommand(procedureName, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = GetCommandTimeout(commandTimeoutSeconds);
                connection.Open();
                adapter.Fill(table);
            }

            return table;
        }

        private SqlConnection CreateWarehouseConnection()
        {
            string buildType = Convert.ToString(ConfigurationManager.AppSettings["BuildType"]);
            string serverName = SqlCon.Decrypt(ConfigurationManager.AppSettings[$"{buildType}WHServerName"].ToString());
            string databaseName = SqlCon.Decrypt(ConfigurationManager.AppSettings[$"{buildType}WHDBName"].ToString());
            string userName = SqlCon.Decrypt(ConfigurationManager.AppSettings[$"{buildType}WHusername"].ToString());
            string password = SqlCon.Decrypt(ConfigurationManager.AppSettings[$"{buildType}WHpwd"].ToString());
            string connectionString = "Data Source = " + serverName + "; Initial Catalog = " + databaseName + "; User Id = " + userName + "; Password = " + password + "; Pooling = True; Connect Timeout = 1024; Max Pool Size = 200";

            return new SqlConnection(connectionString);
        }

        private int GetCommandTimeout(int? commandTimeoutSeconds)
        {
            int timeout = commandTimeoutSeconds.GetValueOrDefault(defaultCommandTimeoutSeconds);
            return timeout > 0 ? timeout : defaultCommandTimeoutSeconds;
        }

        private static HomeDeliveryExportDefinition MapDefinition(DataRow row)
        {
            return new HomeDeliveryExportDefinition
            {
                ExportId = GetInt(row, "ExportId"),
                ExportCode = GetString(row, "ExportCode"),
                FileName = GetString(row, "FileName"),
                ExportProcedureName = GetString(row, "ExportProcedureName"),
                ExecutionOrder = GetInt(row, "ExecutionOrder"),
                Enabled = GetBool(row, "Enabled"),
                DropboxTargetFolder = GetString(row, "DropboxTargetFolder")
            };
        }

        private static void ValidateProcedureName(string procedureName)
        {
            if (string.IsNullOrWhiteSpace(procedureName))
            {
                throw new ArgumentException("Stored procedure name is required.", nameof(procedureName));
            }

            if (!ProcedureNamePattern.IsMatch(procedureName))
            {
                throw new ArgumentException("Stored procedure name contains invalid characters.", nameof(procedureName));
            }
        }

        private static string GetString(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value) return string.Empty;
            return Convert.ToString(row[columnName]);
        }

        private static int GetInt(DataRow row, string columnName)
        {
            int value;
            return int.TryParse(GetString(row, columnName), out value) ? value : 0;
        }

        private static bool GetBool(DataRow row, string columnName)
        {
            object value = row.Table.Columns.Contains(columnName) ? row[columnName] : null;
            if (value == null || value == DBNull.Value) return false;
            if (value is bool) return (bool)value;
            if (value is int) return (int)value != 0;

            bool boolValue;
            if (bool.TryParse(Convert.ToString(value), out boolValue)) return boolValue;

            int intValue;
            return int.TryParse(Convert.ToString(value), out intValue) && intValue != 0;
        }
    }
}
