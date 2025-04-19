using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataRepository
    {
        public DataTable GetDataTable(string procedureName, bool useWHConn, Dictionary<string, object> parameters = null)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlConnection connection = SQLCon.Sqlconn())
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while executing {procedureName} - {ex.Message}", ex);
            }
            return dtReportData;
        }

        public DataSet GetDataset(string procedureName, bool useWHConn, Dictionary<string, object> parameters = null)
        {
            DataSet dsReportData = new DataSet();
            try
            {
                using (SqlConnection connection = SQLCon.Sqlconn())
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while executing {procedureName} - {ex.Message}", ex);
            }
            return dsReportData;
        }

        public DataSet GetDatasetWithTransaction(string procedureName, bool useWHConn, Dictionary<string, object> parameters = null)
        {
            DataSet dsReportData = new DataSet();
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = SQLCon.Sqlconn())
                using (SqlCommand cmd = new SqlCommand())
                {
                    transaction = connection.BeginTransaction();
                    cmd.Connection = connection;
                    cmd.Transaction = transaction;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsReportData);
                    }
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception($"Error while executing {procedureName} - {ex.Message}", ex);
            }
            return dsReportData;
        }

        public object ExecuteScalar(string procedureName, bool useWHConn, Dictionary<string, object> parameters = null)
        {
            object obj = null;
            try
            {
                using (SqlConnection connection = SQLCon.Sqlconn())
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);
                    obj = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while executing {procedureName} - {ex.Message}", ex);
            }
            return obj;
        }

        public object ExecuteScalarWithTransaction(string procedureName, bool useWHConn, Dictionary<string, object> parameters = null)
        {
            object obj = null;
            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection connection = SQLCon.Sqlconn())
                using (SqlCommand cmd = new SqlCommand())
                {
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);
                    obj = cmd.ExecuteScalar();
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception($"Error while executing {procedureName} - {ex.Message}", ex);
            }
            return obj;
        }

        public int ExecuteNonQuery(string procedureName, bool useWHConn, Dictionary<string, object> parameters = null, bool UseTransaction = false)
        {
            int rowcount = 0;
            SqlTransaction sqlTransaction = null;

            try
            {
                using (SqlConnection sqlConnection = SQLCon.Sqlconn())
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (UseTransaction)
                        sqlTransaction = sqlConnection.BeginTransaction();
                    cmd.Connection = sqlConnection;
                    if (UseTransaction)
                        cmd.Transaction = sqlTransaction;
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);
                    rowcount = cmd.ExecuteNonQuery();
                    sqlTransaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction?.Rollback();
                throw new Exception($"Error while executing {procedureName} - {ex.Message}", ex);
            }
            finally
            {
                sqlTransaction?.Dispose();
            }

            return rowcount;
        }

        private void ProcessParameters(SqlCommand sqlCommand, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
            {
                string paramName = param.Key;
                paramName = paramName.StartsWith("@") ? paramName : $"@{paramName}";
                sqlCommand.Parameters.AddWithValue(paramName, param.Value);
            }
        }
    }
}
