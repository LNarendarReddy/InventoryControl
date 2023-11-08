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

        public DataTable GetBranch(bool UsedInReport = false)
        {
            DataTable dtBranch = new ReportRepository().GetReportData("USP_R_BRANCH");

            if (UsedInReport)
            {
                DataRow dr = dtBranch.NewRow();
                dr["BRANCHID"] = 0;
                dr["BRANCHNAME"] = "ALL";
                dtBranch.Rows.InsertAt(dr, 0);
            }

            return dtBranch;
        }

        public DataTable GetUOM()
        {
            return new ReportRepository().GetReportData("USP_R_UOM");
        }

        public DataTable GetSubCategory()
        {
            return new ReportRepository().GetReportData("USP_R_SUBCATEGORY");
        }

    }
}
 