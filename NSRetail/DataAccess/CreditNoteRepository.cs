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
            catch (Exception ex)
            {
                throw new Exception("Error while saving credit note", ex);
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
                throw new Exception("Error while retrieving credit note", ex);
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
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving credit note adjustment types", ex);
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
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving purchase invoices", ex);
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
                throw ex;
            }

            return dt;
        }

        public object MapStockEntryCreditNote(object stockEntryId, object creditNoteId, object creditValue, object userId)
        {
            object id = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_CU_STOCKENTRY_CREDITNOTEMAP";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", stockEntryId);
                    cmd.Parameters.AddWithValue("@CREDITNOTEID", creditNoteId);
                    cmd.Parameters.AddWithValue("@CREDITVALUE", creditValue);
                    cmd.Parameters.AddWithValue("@USERID", userId);

                    object obj = cmd.ExecuteScalar();
                    if (int.TryParse(Convert.ToString(obj), out int iValue))
                        id = obj;
                    else
                        throw new Exception(Convert.ToString(obj));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while mapping credit note to invoice", ex);
            }

            return id;
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
