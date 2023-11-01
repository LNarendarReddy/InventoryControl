using System.Collections.Generic;
using System.Data;

namespace NSRetailPOS.Data
{
    public class MasterRepository
    {
        public DataTable GetDealer(bool UsedInReport = false)
        {
            DataTable dtDealer = new ReportRepository().GetReportData("USP_R_DEALER");
            if (UsedInReport)
            {
                DataRow dr = dtDealer.NewRow();
                dr["DEALERID"] = 0;
                dr["DEALERNAME"] = "ALL";
                dtDealer.Rows.InsertAt(dr, 0);
            }

            return dtDealer;
        }

        public DataTable GetGST()
        {
            return new ReportRepository().GetReportData("USP_R_GST");
        }

        public DataTable GetCategory()
        {
            return new ReportRepository().GetReportData("USP_R_CATEGORY", 
                new Dictionary<string, object>() { { "IsBranchCategory", true } });
        }
    }
}
 