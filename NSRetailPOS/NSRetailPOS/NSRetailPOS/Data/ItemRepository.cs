using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class ItemRepository
    {
        public DataTable GetItemCodes(object categoryID = null)
        {
            DataTable dtItemCodes = new DataTable();
            SqlConnection conn = SQLCon.Sqlconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_ITEMCODES]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", categoryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item codes List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtItemCodes;
        }

        public DataTable GetNonEAN()
        {
            DataTable dtItemCodes = new DataTable();
            SqlConnection conn = SQLCon.Sqlconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_NONEAN]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item codes List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtItemCodes;
        }

        public DataTable GetCategory()
        {
            DataTable dtCategory = new DataTable();
            SqlConnection conn = SQLCon.Sqlconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_CATEGORY]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Category List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtCategory;
        }

        public DataTable GetMRPList(object ITEMCODEID, bool showAllMRP = false)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_ITEMMRPLIST]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
                    cmd.Parameters.AddWithValue("@FilterMRPByStock", showAllMRP ? true : Utility.branchInfo.FilterMRPByStock);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving MRP List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItemCodes;
        }

        public DataTable GetOfferList(object ItemPriceID)
        {
            DataTable dtOffers = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_GETOFFERS]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOffers);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offer List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtOffers;
        }
    }
}
