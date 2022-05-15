using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ReportRepository
    {
        public DataTable GetReportData(string procedureName, Dictionary<string, object> parameters)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    foreach (var param in parameters)
                    {
                        string paramName = param.Key;
                        paramName = paramName.StartsWith("@") ? paramName : $"@{paramName}";
                        cmd.Parameters.AddWithValue(paramName, param.Value);
                    }
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retrieving {procedureName}", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtReportData;
        }

        public DataSet GetReportDataset(string procedureName, Dictionary<string, object> parameters)
        {
            DataSet dsReportData = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    foreach (var param in parameters)
                    {
                        string paramName = param.Key;
                        paramName = paramName.StartsWith("@") ? paramName : $"@{paramName}";
                        cmd.Parameters.AddWithValue(paramName, param.Value);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retrieving {procedureName}", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsReportData;
        }
    }
}
