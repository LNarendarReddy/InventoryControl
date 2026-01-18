using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CreditNoteRepository
    {
        public CreditNote SaveCreditNote(CreditNote creditNote)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_cu_CreditNote";

                    cmd.Parameters.AddWithValue("@CreditNoteId", creditNote.CreditNoteId);
                    cmd.Parameters.AddWithValue("@CNNumber", creditNote.CNNumber);
                    cmd.Parameters.AddWithValue("@Description", creditNote.Description);
                    cmd.Parameters.AddWithValue("@SupplierId", creditNote.SupplierId);
                    cmd.Parameters.AddWithValue("@CreditValue", creditNote.CreditValue);
                    cmd.Parameters.AddWithValue("@CreditNoteAdjustmentTypeId", creditNote.CreditNoteAdjustmentTypeId);
                    cmd.Parameters.AddWithValue("@StockEntryId", creditNote.StockEntryId);
                    cmd.Parameters.AddWithValue("@UserId", creditNote.UserId);

                    creditNote.CreditNoteId = cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error while saving credit note");
            }

            return creditNote;
        }

        public void DeleteCreditNote(object CreditNoteId, object UserId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_D_CreditNote";
                    cmd.Parameters.AddWithValue("@CreditNoteId", CreditNoteId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting credit note", ex);
            }
        }

        public DataTable GetCreditNoteById(object CreditNoteId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_R_CreditNote_GetById";
                    cmd.Parameters.AddWithValue("@CreditNoteId", CreditNoteId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving credit note");
            }

            return dt;
        }

        public DataTable GetCreditNoteAdjustmentTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_R_CreditNoteAdjustmentType";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error while retrieving credit note adjustment types");
            }

            return dt;
        }

        public DataTable GetPurchaseInvoices(object supplierId)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_R_PurchaseInvoices";

                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Error while retrieving purchase invoices");
            }

            return dt;
        }

        public DataTable GetCreditNotesForMapping(object SupplierRefId, string refType)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_R_CreditNotes_ForMapping";
                    cmd.Parameters.AddWithValue("@SupplierRefId", SupplierRefId);
                    cmd.Parameters.AddWithValue("@ReferenceType", refType);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                
                throw new Exception(ex.Message);
            }

            return dt;
        }

        public DataTable GetMappedCreditNotes(object SupplierRefId, string referenceType)
        {
            var dt = new DataTable();

            try
            {
                using (var con = SQLCon.Sqlconn())
                using (var cmd = new SqlCommand("USP_R_SUPPLIERRETURNS_CREDITNOTES", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@SupplierRefId", SqlDbType.Int).Value = SupplierRefId;
                    cmd.Parameters.Add("@ReferenceType", SqlDbType.VarChar, 50).Value = referenceType;

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

            return dt;
        }

        public void DeleteCreditNoteMapping(object mapID, string mapType, object userId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_D_CREDITNOTEMAP";
                    cmd.Parameters.AddWithValue("@MAPID", mapID);
                    cmd.Parameters.AddWithValue("@MAPTYPE", mapType);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting credit note", ex);
            }
        }

    }
}
