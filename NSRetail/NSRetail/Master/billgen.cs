using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Dropbox.Api.TeamLog.EventCategory;

namespace NSRetail.Master
{
    public partial class billgen : DevExpress.XtraEditors.XtraForm
    {
        decimal item1Total = 0, item2Total, item3Total = 0;

        readonly int totalRangeMinutes = 0, startHour = 8, endHour = 22; // 10 PM in 24-hour format
        int bid = 0;

        DataTable dtBranches, dtCounter;
        string exportPath, DataSetPath, ReportPath;


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            MergePDFs();
        }

        DataSet dsBaseline;

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            GeneratePDFs();
        }

        List<DataSet> bills = new List<DataSet>();

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            AddRandomItem();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            CalculateTotals();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DataRepository dataRepository = new DataRepository();

            DataSet ds = GetEmptyDataSet();

            int curCount = 0;
            foreach (var dsBill in bills)
            {
                curCount++;

                ds.Tables[0].ImportRow(dsBill.Tables[0].Rows[0]);

                foreach(DataRow row in dsBill.Tables[1].Rows)
                    ds.Tables[1].ImportRow(row);

                foreach (DataRow row in dsBill.Tables[2].Rows)
                    ds.Tables[2].ImportRow(row);

                if (curCount % 1000 == 0 || curCount == bills.Count)
                {
                    dataRepository.ExecuteNonQuery("[usp_TMP_SaveFullBill]", true, new Dictionary<string, object>()
                    {
                        { "@Bill", ds.Tables[0] },
                        { "@Details", ds.Tables[1] },
                        { "@Mops", ds.Tables[2] }
                    });
                    AddText($"Saved {curCount} of {bills.Count} bills");
                    ds = GetEmptyDataSet();
                }
            }

            AddText("Save complete");
        }

        public billgen()
        {
            InitializeComponent();
            // Calculate total minutes in the range (14 hours * 60 min)
            totalRangeMinutes = (endHour - startHour) * 60;

            dtBranches = new MasterRepository().GetBranch();
            dtCounter = new MasterRepository().GetCounter();
            dsBaseline = new ReportRepository().GetReportDataset("USP_TMP_IP");
            dsBaseline.Tables[0].TableName = "ITEMPRICE_ALL";
            dsBaseline.Tables[1].TableName = "ITEMPRICE_IMP";
            dsBaseline.Tables[2].TableName = "ITEMPRICE_RND";
            dsBaseline.Tables[3].TableName = "ITEMPRICE_MILL";
            dsBaseline.Tables[4].TableName = "USER";

            exportPath = @"D:\To Delete\Bills";
            DataSetPath = Path.Combine(exportPath, "DataSet");
            ReportPath = Path.Combine(exportPath, "Report");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            GenerateBills();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            int curCount = 0, totalCount = bills.Count;
            while (bills.Count > 0)
            {
                var dsBillDetails = bills.Last();

                if (++curCount % 1000 == 0)
                    AddText($"Saved {curCount} of {totalCount} bills to files");

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(dsBillDetails, Newtonsoft.Json.Formatting.Indented);

                // Write string to file
                File.WriteAllText(Path.Combine(DataSetPath, dsBillDetails.Tables[0].Rows[0]["BILLNUMBER"].ToString().Replace("/", "-") + ".json"), json);

                bills.RemoveAt(bills.Count - 1);
            }

            AddText("Operation completed");
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(DataSetPath, "*.json");
            int curCount = 0;
            foreach (string file in files)
            {
                DataSet dsBillDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(file));

                if (++curCount % 100 == 0)
                    AddText($"Processed {curCount} of {files.Length} bills");

                rptBill rpt = new rptBill(dsBillDetails.Tables[1], dsBillDetails.Tables[2]);
                rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = dsBillDetails.Tables[0].Rows[0]["ADDRESS"];
                rpt.Parameters["BillDate"].Value = dsBillDetails.Tables[0].Rows[0]["BILLCLOSEDDATE"];
                rpt.Parameters["BillNumber"].Value = dsBillDetails.Tables[0].Rows[0]["BILLNUMBER"];
                rpt.Parameters["CustomerName"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERNAME"];
                rpt.Parameters["CustomerNumber"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERNUMBER"];
                rpt.Parameters["CustomerGST"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERGST"];
                rpt.Parameters["TenderedCash"].Value = dsBillDetails.Tables[0].Rows[0]["TENDEREDCASH"];
                rpt.Parameters["TenderedChange"].Value = dsBillDetails.Tables[0].Rows[0]["TENDEREDCHANGE"];
                rpt.Parameters["IsDoorDelivery"].Value = dsBillDetails.Tables[0].Rows[0]["ISDOORDELIVERY"];
                rpt.Parameters["BranchName"].Value = dsBillDetails.Tables[0].Rows[0]["BRANCHNAME"];
                rpt.Parameters["CounterName"].Value = dsBillDetails.Tables[0].Rows[0]["COUNTERNAME"];
                rpt.Parameters["Phone"].Value = dsBillDetails.Tables[0].Rows[0]["PHONENO"];
                rpt.Parameters["UserName"].Value = dsBillDetails.Tables[0].Rows[0]["CREATEDBY"];
                rpt.Parameters["RoundingFactor"].Value = dsBillDetails.Tables[0].Rows[0]["ROUNDING"];
                rpt.Parameters["IsIGSTBill"].Value = dsBillDetails.Tables[0].Rows[0]["IsIGSTBill"];
                rpt.Parameters["IsDuplicate"].Value = false;
                rpt.ExportToPdfAsync(Path.Combine(ReportPath, dsBillDetails.Tables[0].Rows[0]["BILLNUMBER"].ToString().Replace("/", "-") + ".pdf")
                    , new DevExpress.XtraPrinting.PdfExportOptions() { ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Low });

            }
        }

        private DataSet GetEmptyDataSet()
        {
            DataSet ds = new DataSet();

            DataTable dtBill = new DataTable("BILL");
            dtBill.Columns.Add("BILLID",  typeof(int));
            dtBill.Columns.Add("BILLNUMBER", typeof(string));
            dtBill.Columns.Add("BILLSTATUS", typeof(int));
            dtBill.Columns.Add("CREATEDDATE", typeof(DateTime));
            dtBill.Columns.Add("ROUNDING", typeof(decimal));
            dtBill.Columns.Add("ISDOORDELIVERY", typeof(int));
            dtBill.Columns.Add("CUSTOMERNAME", typeof(string));
            dtBill.Columns.Add("CUSTOMERNUMBER", typeof(string));
            dtBill.Columns.Add("CUSTOMERGST", typeof(string));
            dtBill.Columns.Add("TENDEREDCASH", typeof(decimal));
            dtBill.Columns.Add("TENDEREDCHANGE", typeof(decimal));
            dtBill.Columns.Add("ADDRESS", typeof(string));
            dtBill.Columns.Add("BRANCHNAME", typeof(string));
            dtBill.Columns.Add("COUNTERNAME", typeof(string));
            dtBill.Columns.Add("PHONENO", typeof(string));
            dtBill.Columns.Add("CREATEDBY", typeof(string));
            dtBill.Columns.Add("ISIGSTBILL", typeof(bool));
            dtBill.Columns.Add("BILLCLOSEDDATE", typeof(DateTime));
            dtBill.Columns.Add("BID", typeof(int));

            DataTable dtBillDetail = new DataTable("BILLDETAIL");
            dtBillDetail.Columns.Add("BILLDETAILID", typeof(int));
            dtBillDetail.Columns.Add("BILLID", typeof(int));
            dtBillDetail.Columns.Add("ITEMPRICEID", typeof(int));
            dtBillDetail.Columns.Add("SNO", typeof(int));
            dtBillDetail.Columns.Add("ITEMNAME", typeof(string));
            dtBillDetail.Columns.Add("ITEMCODE", typeof(string));
            dtBillDetail.Columns.Add("HSNCODE", typeof(string));
            dtBillDetail.Columns.Add("MRP", typeof(decimal));
            dtBillDetail.Columns.Add("SALEPRICE", typeof(decimal));
            dtBillDetail.Columns.Add("GSTCODE", typeof(string));
            dtBillDetail.Columns.Add("QUANTITY", typeof(int));
            dtBillDetail.Columns.Add("WEIGHTINKGS", typeof(decimal));
            dtBillDetail.Columns.Add("BILLEDAMOUNT", typeof(decimal));
            dtBillDetail.Columns.Add("CGST", typeof(decimal));
            dtBillDetail.Columns.Add("SGST", typeof(decimal));
            dtBillDetail.Columns.Add("IGST", typeof(decimal));
            dtBillDetail.Columns.Add("CESS", typeof(decimal));
            dtBillDetail.Columns.Add("GSTVALUE", typeof(decimal));
            dtBillDetail.Columns.Add("GSTID", typeof(int));
            dtBillDetail.Columns.Add("CGSTDESC", typeof(decimal));
            dtBillDetail.Columns.Add("SGSTDESC", typeof(decimal));
            dtBillDetail.Columns.Add("CESSDESC", typeof(decimal));
            dtBillDetail.Columns.Add("ISOPENITEM", typeof(int));
            dtBillDetail.Columns.Add("DISCOUNT", typeof(decimal));
            dtBillDetail.Columns.Add("OFFERID", typeof(int));
            dtBillDetail.Columns.Add("OFFERTYPECODE", typeof(string));
            dtBillDetail.Columns.Add("BID", typeof(int));


            DataTable dtBillMopDetail = new DataTable("BILLMOPDETAIL");
            dtBillMopDetail.Columns.Add("MOPID", typeof(int));
            dtBillMopDetail.Columns.Add("MOPNAME", typeof(string));
            dtBillMopDetail.Columns.Add("MOPVALUE", typeof(decimal));
            dtBillMopDetail.Columns.Add("BID", typeof(int));

            ds.Tables.Add(dtBill);
            ds.Tables.Add(dtBillDetail);
            ds.Tables.Add(dtBillMopDetail);

            return ds;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            AddMillets();
        }

        private async Task AddMillets()
        {
            item3Total = 0;
            
            dtCounter.DefaultView.RowFilter = "ISMOBILECOUNTER = False AND BRANCHID NOT IN (45,92, 97, 103, 100, 105)";
            dtCounter = dtCounter.DefaultView.ToTable();

            Dictionary<int, DataRow> counterPairs = dtCounter.Rows.Cast<DataRow>().ToDictionary(x => int.Parse(x["COUNTERID"].ToString()), x => x);
            Dictionary<int, DataRow> branchPairs = dtBranches.Rows.Cast<DataRow>().ToDictionary(x => int.Parse(x["BRANCHID"].ToString()), x => x);

            List<int> itemPriceIds = new List<int>();

            Random rndItemPricesAll = new Random();

            for (int i = 0; i < 365; i++)
            {
                DateTime curDate = new DateTime(2025, 4, 1).AddDays(i);
                AddText($"Processing day {curDate:yyyy-MM-dd}");

                dtCounter.DefaultView.RowFilter = $"CREATEDDATE < '{curDate}'";

                Dictionary<int, int> counterIds = new Dictionary<int, int>();

                foreach (DataRowView drv in dtCounter.DefaultView)
                    counterIds.Add(int.Parse(drv["COUNTERID"].ToString()), 0);

                Random random = new Random();
                counterIds = counterIds.OrderBy(x => random.Next()).Take((int)(counterIds.Count * 0.25)).ToDictionary(x => x.Key, y => y.Value);
                DataView dvFilterMillets = dsBaseline.Tables["ITEMPRICE_MILL"].DefaultView;

                foreach (int counterId in counterIds.Keys.ToList())
                {
                    int billsCount = random.Next(1, 7);

                    DataView dvUsers = dsBaseline.Tables["USER"].DefaultView;
                    dvUsers.RowFilter = $"BRANCHID = {counterPairs[counterId]["BRANCHID"]} AND CREATEDDATE < '{curDate}' AND (DELETEDDATE IS NULL OR DELETEDDATE < '{curDate}')";
                    string createdBy = string.Empty;
                    if (dvUsers.Count > 0)
                        createdBy = dvUsers[random.Next(0, dvUsers.Count)]["FULLNAME"].ToString();
                    else
                    {
                        dvUsers.RowFilter = $"BRANCHID = {counterPairs[counterId]["BRANCHID"]}";
                        if (dvUsers.Count > 0)
                            createdBy = dvUsers[random.Next(0, dvUsers.Count)]["FULLNAME"].ToString();
                    }

                    for (int bill = 0; bill < billsCount; bill++)
                    {
                        DataSet dsBillDetails = GetEmptyDataSet();
                        int billId = counterIds[counterId] + random.Next(1, 8);
                        counterIds[counterId] = billId;

                        DataRow drBill = dsBillDetails.Tables[0].NewRow();
                        drBill["BID"] = ++bid;
                        drBill["BILLID"] = billId;
                        drBill["BILLNUMBER"] = $"{counterPairs[counterId]["COUNTERNAME"]}/{curDate:yyMMdd}/{counterIds[counterId].ToString().PadLeft(5, '0')}";
                        drBill["BILLSTATUS"] = 1;

                        int randomMinutes = random.Next(0, totalRangeMinutes + 1);

                        // Add the start time (8 AM) and the random offset to the base date
                        drBill["CREATEDDATE"] = curDate.Date.AddHours(startHour).AddMinutes(randomMinutes);
                        drBill["BILLCLOSEDDATE"] = curDate.Date.AddHours(startHour).AddMinutes(randomMinutes);
                        drBill["ISDOORDELIVERY"] = 0;
                        drBill["ISIGSTBILL"] = false;

                        int branchId = int.Parse(counterPairs[counterId]["BRANCHID"].ToString());
                        drBill["ADDRESS"] = branchPairs[branchId]["ADDRESS"];
                        drBill["BRANCHNAME"] = branchPairs[branchId]["BRANCHNAME"];
                        drBill["PHONENO"] = branchPairs[branchId]["PHONENO"];
                        drBill["COUNTERNAME"] = counterPairs[counterId]["COUNTERNAME"];
                        drBill["CREATEDBY"] = createdBy;

                        dsBillDetails.Tables[0].Rows.Add(drBill);

                        int itemsCount = random.Next(0, 3);
                        int sno = 1;

                        List<DataRow> impRows = dvFilterMillets.Cast<DataRowView>().OrderBy(x => random.Next()).Take(2).Select(x => x.Row).ToList();
                        List<DataRow> otherRows = new List<DataRow>();

                        for (int k = 0; k < itemsCount; k++)
                        {
                            otherRows.Add(dsBaseline.Tables["ITEMPRICE_ALL"].Rows[rndItemPricesAll.Next(dsBaseline.Tables["ITEMPRICE_ALL"].Rows.Count)]);
                        }

                        List<DataRow> allRows = new List<DataRow>();
                        allRows.AddRange(impRows);
                        allRows.AddRange(otherRows);

                        foreach (DataRow randomRow in allRows)
                        {
                            DataRow drBillDetail = dsBillDetails.Tables[1].NewRow();

                            drBillDetail["BILLDETAILID"] = 0;
                            drBillDetail["BID"] = bid;
                            drBillDetail["BILLID"] = counterIds[counterId];
                            drBillDetail["ITEMPRICEID"] = randomRow["ITEMPRICEID"];
                            drBillDetail["SNO"] = sno++;
                            drBillDetail["ITEMNAME"] = randomRow["ITEMNAME"];
                            drBillDetail["ITEMCODE"] = randomRow["ITEMCODE"];
                            drBillDetail["HSNCODE"] = randomRow["HSNCODE"];
                            drBillDetail["MRP"] = randomRow["MRP"];
                            drBillDetail["SALEPRICE"] = randomRow["SALEPRICE"];
                            drBillDetail["GSTCODE"] = randomRow["GSTCODE"];
                            drBillDetail["GSTID"] = randomRow["GSTID"];
                            drBillDetail["ISOPENITEM"] = randomRow["ISOPENITEM"];

                            bool isOPenItem = bool.Parse(randomRow["ISOPENITEM"].ToString());
                            int quantity = !isOPenItem ? random.Next(1, 3) : 0;

                            //override quantity in special cases

                            //if (randomRow["ITEMNAME"].ToString().Equals("RAGI LADDU 1000GM"))
                            //    quantity = random.Next(1, 2);
                            //else if (randomRow["ITEMNAME"].ToString().Equals("RAGI LADDU 500GM"))
                            //    quantity = random.Next(1, 3);
                            //else if (randomRow["ITEMNAME"].ToString().Equals("VICTORY MILLETS CHIKKI 25GM"))
                            //    quantity = random.Next(3, 8);
                            //else if (randomRow["ITEMNAME"].ToString().Contains("MILLET"))
                            //    quantity = random.Next(3, 10);

                            decimal weight = (decimal)(isOPenItem ? random.NextDouble() + random.Next(1, 3) : 0);

                            drBillDetail["QUANTITY"] = quantity;
                            drBillDetail["WEIGHTINKGS"] = weight;
                            decimal billedAmount = Math.Round((isOPenItem ? weight : quantity) * decimal.Parse(randomRow["SALEPRICE"].ToString()), 2);
                            drBillDetail["BILLEDAMOUNT"] = billedAmount;

                            drBillDetail["CGSTDESC"] = randomRow["CGST"];
                            drBillDetail["SGSTDESC"] = randomRow["SGST"];
                            drBillDetail["CESSDESC"] = randomRow["CESS"];

                            decimal cgst, sgst, cess, gst, gstvalue, taxableValue;
                            cgst = decimal.Parse(randomRow["CGST"].ToString());
                            sgst = decimal.Parse(randomRow["SGST"].ToString());
                            cess = decimal.Parse(randomRow["CESS"].ToString());

                            gst = cgst + sgst + cess;
                            taxableValue = Math.Round(billedAmount * 100 / (100 + gst), 2);
                            gstvalue = billedAmount - taxableValue;

                            drBillDetail["CGST"] = Math.Round(taxableValue * cgst / 100, 2);
                            drBillDetail["SGST"] = Math.Round(taxableValue * sgst / 100, 2);
                            drBillDetail["CESS"] = Math.Round(taxableValue * cess / 100, 2);
                            drBillDetail["GSTVALUE"] = gstvalue;
                            drBillDetail["DISCOUNT"] = ((isOPenItem ? weight : quantity) * decimal.Parse(randomRow["MRP"].ToString())) - billedAmount;

                            dsBillDetails.Tables[1].Rows.Add(drBillDetail);
                        }

                        DataTable dtTemp = dsBillDetails.Tables[1].DefaultView.ToTable();
                        foreach (DataRow impRow in impRows)
                        {
                            dtTemp.DefaultView.RowFilter = $"ITEMPRICEID = {impRow["ITEMPRICEID"]}";
                            item3Total += decimal.Parse(dtTemp.DefaultView[0]["BILLEDAMOUNT"].ToString());
                        }

                        bills.Add(dsBillDetails);
                    }
                }
            }
            AddText("Operation completed");
            XtraMessageBox.Show(item3Total.ToString());
            XtraMessageBox.Show(bills.Count.ToString());
        }

        private async Task GenerateBills()
        {
            bills = new List<DataSet>();
            item1Total = 0;
            bid = 0;

            dtCounter.DefaultView.RowFilter = "ISMOBILECOUNTER = False AND BRANCHID NOT IN (45,92, 97, 103, 100, 105)";
            dtCounter = dtCounter.DefaultView.ToTable();

            Dictionary<int, DataRow> counterPairs = dtCounter.Rows.Cast<DataRow>().ToDictionary(x => int.Parse(x["COUNTERID"].ToString()), x => x);
            Dictionary<int, DataRow> branchPairs = dtBranches.Rows.Cast<DataRow>().ToDictionary(x => int.Parse(x["BRANCHID"].ToString()), x => x);

            List<int> itemPriceIds = new List<int>();

            Random rndItemPricesAll = new Random();

            for (int i = 0; i < 365; i++)
            {
                DateTime curDate = new DateTime(2025, 4, 1).AddDays(i);
                AddText($"Processing day {curDate:yyyy-MM-dd}");

                dtCounter.DefaultView.RowFilter = $"CREATEDDATE < '{curDate}'";

                Dictionary<int, int> counterIds = new Dictionary<int, int>();

                foreach (DataRowView drv in dtCounter.DefaultView)
                    counterIds.Add(int.Parse(drv["COUNTERID"].ToString()), 0);

                Random random = new Random();
                counterIds = counterIds.OrderBy(x => random.Next()).Take((int)(counterIds.Count * 0.4)).ToDictionary(x => x.Key, y => y.Value);

                DataView dvFilterImp = dsBaseline.Tables["ITEMPRICE_IMP"].DefaultView;                                
                dvFilterImp.RowFilter = $"ITEMPRICEID <> 191851 AND ITEMPRICEID <> 174710";

                foreach (int counterId in counterIds.Keys.ToList())
                {
                    int billsCount = random.Next(5, 20);

                    DataView dvUsers = dsBaseline.Tables["USER"].DefaultView;
                    dvUsers.RowFilter = $"BRANCHID = {counterPairs[counterId]["BRANCHID"]} AND CREATEDDATE < '{curDate}' AND (DELETEDDATE IS NULL OR DELETEDDATE < '{curDate}')";
                    string createdBy = string.Empty;
                    if (dvUsers.Count > 0)
                        createdBy = dvUsers[random.Next(0, dvUsers.Count)]["FULLNAME"].ToString();
                    else
                    {
                        dvUsers.RowFilter = $"BRANCHID = {counterPairs[counterId]["BRANCHID"]}";
                        if (dvUsers.Count > 0)
                            createdBy = dvUsers[random.Next(0, dvUsers.Count)]["FULLNAME"].ToString();
                    }

                    for (int bill = 0; bill < billsCount; bill++)
                    {
                        DataSet dsBillDetails = GetEmptyDataSet();
                        int billId = counterIds[counterId] + random.Next(1, 8);
                        counterIds[counterId] = billId;

                        DataRow drBill = dsBillDetails.Tables[0].NewRow();
                        drBill["BID"] = ++bid;
                        drBill["BILLID"] = billId;
                        drBill["BILLNUMBER"] = $"{counterPairs[counterId]["COUNTERNAME"]}/{curDate:yyMMdd}/{counterIds[counterId].ToString().PadLeft(5, '0')}";
                        drBill["BILLSTATUS"] = 1;

                        int randomMinutes = random.Next(0, totalRangeMinutes + 1);

                        // Add the start time (8 AM) and the random offset to the base date
                        drBill["CREATEDDATE"] = curDate.Date.AddHours(startHour).AddMinutes(randomMinutes);
                        drBill["BILLCLOSEDDATE"] = curDate.Date.AddHours(startHour).AddMinutes(randomMinutes);
                        drBill["ISDOORDELIVERY"] = 0;
                        drBill["ISIGSTBILL"] = false;

                        int branchId = int.Parse(counterPairs[counterId]["BRANCHID"].ToString());
                        drBill["ADDRESS"] = branchPairs[branchId]["ADDRESS"];
                        drBill["BRANCHNAME"] = branchPairs[branchId]["BRANCHNAME"];
                        drBill["PHONENO"] = branchPairs[branchId]["PHONENO"];
                        drBill["COUNTERNAME"] = counterPairs[counterId]["COUNTERNAME"];
                        drBill["CREATEDBY"] = createdBy;

                        dsBillDetails.Tables[0].Rows.Add(drBill);

                        int itemsCount = random.Next(0, 3);
                        int sno = 1;

                        List<DataRow> impRows = dvFilterImp.Cast<DataRowView>().OrderBy(x => random.Next()).Take(random.Next(1, dvFilterImp.Count)).Select(x => x.Row).ToList();
                        List<DataRow> otherRows = new List<DataRow>();

                        for (int k = 0; k < itemsCount; k++)
                        {
                            otherRows.Add(dsBaseline.Tables["ITEMPRICE_ALL"].Rows[rndItemPricesAll.Next(dsBaseline.Tables["ITEMPRICE_ALL"].Rows.Count)]);
                        }

                        List<DataRow> allRows = new List<DataRow>();
                        allRows.AddRange(impRows);
                        allRows.AddRange(otherRows);

                        foreach (DataRow randomRow in allRows)
                        {
                            DataRow drBillDetail = dsBillDetails.Tables[1].NewRow();

                            drBillDetail["BILLDETAILID"] = 0;
                            drBillDetail["BID"] = bid;
                            drBillDetail["BILLID"] = counterIds[counterId];
                            drBillDetail["ITEMPRICEID"] = randomRow["ITEMPRICEID"];
                            drBillDetail["SNO"] = sno++;
                            drBillDetail["ITEMNAME"] = randomRow["ITEMNAME"];
                            drBillDetail["ITEMCODE"] = randomRow["ITEMCODE"];
                            drBillDetail["HSNCODE"] = randomRow["HSNCODE"];
                            drBillDetail["MRP"] = randomRow["MRP"];
                            drBillDetail["SALEPRICE"] = randomRow["SALEPRICE"];
                            drBillDetail["GSTCODE"] = randomRow["GSTCODE"];
                            drBillDetail["GSTID"] = randomRow["GSTID"];
                            drBillDetail["ISOPENITEM"] = randomRow["ISOPENITEM"];

                            bool isOPenItem = bool.Parse(randomRow["ISOPENITEM"].ToString());
                            int quantity = !isOPenItem ? random.Next(1, 3) : 0;

                            //override quantity in special cases

                            if (randomRow["ITEMNAME"].ToString().Equals("RAGI LADDU 1000GM"))
                                quantity = random.Next(1, 2);
                            else if (randomRow["ITEMNAME"].ToString().Equals("RAGI LADDU 500GM"))
                                quantity = random.Next(1, 3);
                            else if (randomRow["ITEMNAME"].ToString().Equals("VICTORY MILLETS CHIKKI 25GM"))
                                quantity = random.Next(3, 8);
                            else if (randomRow["ITEMNAME"].ToString().Contains("MILLET"))
                                quantity = random.Next(3, 10);

                            decimal weight = (decimal)(isOPenItem ? random.NextDouble() + random.Next(1, 3) : 0);

                            drBillDetail["QUANTITY"] = quantity;
                            drBillDetail["WEIGHTINKGS"] = weight;
                            decimal billedAmount = Math.Round((isOPenItem ? weight : quantity) * decimal.Parse(randomRow["SALEPRICE"].ToString()), 2);
                            drBillDetail["BILLEDAMOUNT"] = billedAmount;

                            drBillDetail["CGSTDESC"] = randomRow["CGST"];
                            drBillDetail["SGSTDESC"] = randomRow["SGST"];
                            drBillDetail["CESSDESC"] = randomRow["CESS"];

                            decimal cgst, sgst, cess, gst, gstvalue, taxableValue;
                            cgst = decimal.Parse(randomRow["CGST"].ToString());
                            sgst = decimal.Parse(randomRow["SGST"].ToString());
                            cess = decimal.Parse(randomRow["CESS"].ToString());

                            gst = cgst + sgst + cess;
                            taxableValue = Math.Round(billedAmount * 100 / (100 + gst), 2);
                            gstvalue = billedAmount - taxableValue;

                            drBillDetail["CGST"] = Math.Round(taxableValue * cgst / 100, 2);
                            drBillDetail["SGST"] = Math.Round(taxableValue * sgst / 100, 2);
                            drBillDetail["CESS"] = Math.Round(taxableValue * cess / 100, 2);
                            drBillDetail["GSTVALUE"] = gstvalue;
                            drBillDetail["DISCOUNT"] = ((isOPenItem ? weight : quantity) * decimal.Parse(randomRow["MRP"].ToString())) - billedAmount;

                            dsBillDetails.Tables[1].Rows.Add(drBillDetail);
                        }

                        DataTable dtTemp = dsBillDetails.Tables[1].DefaultView.ToTable();
                        foreach (DataRow impRow in impRows)
                        {
                            dtTemp.DefaultView.RowFilter = $"ITEMPRICEID = {impRow["ITEMPRICEID"]}";
                            item1Total += decimal.Parse(dtTemp.DefaultView[0]["BILLEDAMOUNT"].ToString());
                        }

                        bills.Add(dsBillDetails);
                    }
                }
            }
            AddText("Operation completed");
            XtraMessageBox.Show(item1Total.ToString());
            XtraMessageBox.Show(bills.Count.ToString());
        }

        private async Task GeneratePDFs()
        {
            int curCount = 0, totalCount = bills.Count;
            while(bills.Count > 0)
            {
                var dsBillDetails = bills.Last();

                if (++curCount % 100 == 0) 
                    AddText($"Processed {curCount} of {totalCount} bills");
                rptBill rpt = new rptBill(dsBillDetails.Tables[1], dsBillDetails.Tables[2]);
                rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = dsBillDetails.Tables[0].Rows[0]["ADDRESS"];
                rpt.Parameters["BillDate"].Value = dsBillDetails.Tables[0].Rows[0]["BILLCLOSEDDATE"];
                rpt.Parameters["BillNumber"].Value = dsBillDetails.Tables[0].Rows[0]["BILLNUMBER"];
                rpt.Parameters["CustomerName"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERNAME"];
                rpt.Parameters["CustomerNumber"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERNUMBER"];
                rpt.Parameters["CustomerGST"].Value = dsBillDetails.Tables[0].Rows[0]["CUSTOMERGST"];
                rpt.Parameters["TenderedCash"].Value = dsBillDetails.Tables[0].Rows[0]["TENDEREDCASH"];
                rpt.Parameters["TenderedChange"].Value = dsBillDetails.Tables[0].Rows[0]["TENDEREDCHANGE"];
                rpt.Parameters["IsDoorDelivery"].Value = dsBillDetails.Tables[0].Rows[0]["ISDOORDELIVERY"];
                rpt.Parameters["BranchName"].Value = dsBillDetails.Tables[0].Rows[0]["BRANCHNAME"];
                rpt.Parameters["CounterName"].Value = dsBillDetails.Tables[0].Rows[0]["COUNTERNAME"];
                rpt.Parameters["Phone"].Value = dsBillDetails.Tables[0].Rows[0]["PHONENO"];
                rpt.Parameters["UserName"].Value = dsBillDetails.Tables[0].Rows[0]["CREATEDBY"];
                rpt.Parameters["RoundingFactor"].Value = dsBillDetails.Tables[0].Rows[0]["ROUNDING"];
                rpt.Parameters["IsIGSTBill"].Value = dsBillDetails.Tables[0].Rows[0]["IsIGSTBill"];
                rpt.Parameters["IsDuplicate"].Value = false;
                rpt.ExportToPdfAsync(Path.Combine(ReportPath, dsBillDetails.Tables[0].Rows[0]["BILLNUMBER"].ToString().Replace("/", "-") + ".pdf")
                    , new DevExpress.XtraPrinting.PdfExportOptions() { ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.Low });

                bills.RemoveAt(bills.Count - 1);
            }

            AddText("Operation completed");
        }

        private async Task MergePDFs()
        {
            List<string> files = Directory.GetFiles(ReportPath, "*.pdf").ToList();
            //MergeSubPDFs(files.ToArray(), "singleFile.pdf");

            //string[] files1 = files.Take(100000).ToArray();
            //files.RemoveRange(0, 100000);
            //string[] files2 = files.Take(100000).ToArray();
            //files.RemoveRange(0, 100000);
            //string[] files3 = files.Take(100000).ToArray();

            //MergeSubPDFs(files1, "SingleFile1.pdf");
            //MergeSubPDFs(files2, "SingleFile2.pdf");
            //MergeSubPDFs(files3, "SingleFile3.pdf");

            int mergeCount = 120, curCount = 0, fileSuffix = 0;
            List<string> subList = new List<string>();

            foreach (string file in files)
            {
                if (curCount < mergeCount)
                {
                    curCount++;
                    subList.Add(file);
                    continue;
                }

                await MergeSubPDFs(subList.ToArray(), $"file{++fileSuffix}.pdf");
                subList = new List<string>();
                curCount = 0;
            }

            AddText("Operation completed");
        }

        private async Task MergeSubPDFs(string[] files, string fileName)
        {
            int curCount = 0;
            using (PdfDocument outputDocument = new PdfDocument())
            {
                outputDocument.Options.NoCompression = false;
                outputDocument.Options.CompressContentStreams = true;
                outputDocument.Options.FlateEncodeMode = PdfFlateEncodeMode.BestCompression;
                outputDocument.Options.EnableCcittCompressionForBilevelImages = false;
                outputDocument.Options.UseFlateDecoderForJpegImages = PdfUseFlateDecoderForJpegImages.Automatic;

                foreach (string file in files)
                {
                    if (++curCount % 1000 == 0) AddText($"Processed {curCount} of {files.Length} bills");
                    try
                    {
                        // Open the document in Import mode
                        using (PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import))
                        {
                            foreach (PdfPage page in inputDocument.Pages)
                            {
                                outputDocument.AddPage(page);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        AddText($"Failed processing file: {ex.Message}");
                    }
                }
                AddText("Saving file: " + fileName);
                outputDocument.Save(Path.Combine("D:\\To Delete\\Bills\\PrintMerged", fileName));
            }
        }

        private async Task AddRandomItem()
        {
            item2Total = 0;
            int count = 0;
            Random random = new Random();
            bills = bills.OrderBy(x => random.Next()).ToList(); // randomize
            for (int i = 0; i<bills.Count;i++)
            {
                if (++count > 30000) break;

                DataSet dsBillDetails = bills[i];
                if (count % 1000 == 0) AddText($"Processed {count} of {bills.Count} bills");
                DateTime billDate = DateTime.Parse(dsBillDetails.Tables["BILL"].Rows[0]["CREATEDDATE"].ToString());
                string itemPriceID = billDate < new DateTime(2025, 10, 11) ? "191851" : "174710";

                DataView dvFilterImp = dsBaseline.Tables["ITEMPRICE_RND"].DefaultView;
                dvFilterImp.RowFilter = $"ITEMPRICEID = {itemPriceID}";
                DataRowView randomRow = dvFilterImp[0];

                DataRow drBillDetail = dsBillDetails.Tables[1].NewRow();

                drBillDetail["BID"] = dsBillDetails.Tables["BILL"].Rows[0]["BID"]; ;
                drBillDetail["BILLDETAILID"] = 0;
                drBillDetail["BILLID"] = dsBillDetails.Tables["BILL"].Rows[0]["BILLID"];
                drBillDetail["ITEMPRICEID"] = randomRow["ITEMPRICEID"];
                drBillDetail["SNO"] = dsBillDetails.Tables[1].Rows.Count + 1;
                drBillDetail["ITEMNAME"] = randomRow["ITEMNAME"];
                drBillDetail["ITEMCODE"] = randomRow["ITEMCODE"];
                drBillDetail["HSNCODE"] = randomRow["HSNCODE"];
                drBillDetail["MRP"] = randomRow["MRP"];
                drBillDetail["SALEPRICE"] = randomRow["SALEPRICE"];
                drBillDetail["GSTCODE"] = randomRow["GSTCODE"];
                drBillDetail["GSTID"] = randomRow["GSTID"];
                drBillDetail["ISOPENITEM"] = randomRow["ISOPENITEM"];

                bool isOPenItem = bool.Parse(randomRow["ISOPENITEM"].ToString());
                int quantity = !isOPenItem ? random.Next(1, 2) : 0;
                decimal weight = (decimal)(isOPenItem ? random.NextDouble() + random.Next(1, 2) : 0);

                drBillDetail["QUANTITY"] = quantity;
                drBillDetail["WEIGHTINKGS"] = weight;
                decimal billedAmount = Math.Round((isOPenItem ? weight : quantity) * decimal.Parse(randomRow["SALEPRICE"].ToString()), 2);
                drBillDetail["BILLEDAMOUNT"] = billedAmount;
                item2Total += billedAmount;

                drBillDetail["CGSTDESC"] = randomRow["CGST"];
                drBillDetail["SGSTDESC"] = randomRow["SGST"];
                drBillDetail["CESSDESC"] = randomRow["CESS"];

                decimal cgst, sgst, cess, gst, gstvalue, taxableValue;
                cgst = decimal.Parse(randomRow["CGST"].ToString());
                sgst = decimal.Parse(randomRow["SGST"].ToString());
                cess = decimal.Parse(randomRow["CESS"].ToString());

                gst = cgst + sgst + cess;
                taxableValue = Math.Round(billedAmount * 100 / (100 + gst), 2);
                gstvalue = billedAmount - taxableValue;

                drBillDetail["CGST"] = Math.Round(taxableValue * cgst / 100, 2);
                drBillDetail["SGST"] = Math.Round(taxableValue * sgst / 100, 2);
                drBillDetail["CESS"] = Math.Round(taxableValue * cess / 100, 2);
                drBillDetail["GSTVALUE"] = gstvalue;
                drBillDetail["DISCOUNT"] = ((isOPenItem ? weight : quantity) * decimal.Parse(randomRow["MRP"].ToString())) - billedAmount;

                dsBillDetails.Tables[1].Rows.Add(drBillDetail);
            }

            XtraMessageBox.Show(item2Total.ToString());
            AddText("Operation completed");
        }

        private async Task CalculateTotals()
        {
            int curCount = 0, seventy = (int)Math.Round(bills.Count * 0.7, 0), eighty = (int)Math.Round(bills.Count * 0.8, 0), thirty = (int)Math.Round(bills.Count * 0.32, 0);
            bills = bills.OrderBy(x => new Random().Next()).ToList(); // randomize
            foreach (DataSet dsBill in bills)
            {
                decimal billTotal = 0, rounding = 0;
                if(++curCount % 1000 == 0) AddText($"Processed {curCount} of {bills.Count} bills");
                foreach (DataRow drBillDetail in dsBill.Tables[1].Rows)
                {
                    billTotal += decimal.Parse(drBillDetail["BILLEDAMOUNT"].ToString());
                }

                rounding = Math.Round(billTotal, 0) - billTotal;

                int multiplier = curCount < seventy ? 100 : curCount < eighty ? 50 : 500;
                int add = multiplier / Math.Abs(multiplier);
                decimal tenderedCash = (((int)((billTotal + multiplier - add) / multiplier)) * multiplier);
                dsBill.Tables[0].Rows[0]["TENDEREDCASH"] = tenderedCash;
                dsBill.Tables[0].Rows[0]["TENDEREDCHANGE"] = tenderedCash - (billTotal + rounding);
                dsBill.Tables[0].Rows[0]["ROUNDING"] = rounding;

                DataRow drMop = dsBill.Tables[2].NewRow();
                drMop["BID"] = dsBill.Tables[0].Rows[0]["BID"];
                drMop["MOPID"] = 1;
                drMop["MOPNAME"] = "Cash";
                drMop["MOPVALUE"] = billTotal + rounding;
                dsBill.Tables[2].Rows.Add(drMop);
            }

            AddText("Totals completed, processing phone pe");

            bills = bills.OrderBy(x => new Random().Next()).ToList(); // randomize again for phone pe
            curCount = 0;
            foreach (DataSet dsBill in bills)
            {
                decimal billTotal = 0;
                if (++curCount % 1000 == 0) AddText($"Processed {curCount} of {bills.Count} bills");
                foreach (DataRow drBillDetail in dsBill.Tables[1].Rows)
                {
                    billTotal += decimal.Parse(drBillDetail["BILLEDAMOUNT"].ToString());
                }

                dsBill.Tables[0].Rows[0]["TENDEREDCASH"] = DBNull.Value;
                dsBill.Tables[0].Rows[0]["TENDEREDCHANGE"] = DBNull.Value;
                dsBill.Tables[0].Rows[0]["ROUNDING"] = DBNull.Value;

                DataRow drMop = dsBill.Tables[2].Rows[0];
                drMop["MOPID"] = 2;
                drMop["MOPNAME"] = "PhonePe";
                drMop["MOPVALUE"] = billTotal;

                if (curCount > thirty) break;
            }

            AddText("Operation completed");
        }

        private void AddText(string text)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => AddText(text)));
                return;
            }

            memoEdit1.Text += $"{DateTime.Now:yyyy-MM-dd hh:mm:ss tt}: {text}{Environment.NewLine}";
            memoEdit1.SelectionStart = memoEdit1.Text.Length;
            memoEdit1.ScrollToCaret();
        }
    } 
}



//select CAST(B.BILLCLOSEDDATE AS DATE) BILLDATE, B.BRANCHNAME, BD.ITEMCODE, BD.ITEMNAME, BD.MRP, BD.SALEPRICE
//	, SUM(BD.QUANTITY) QUANTITY, SUM(BD.WEIGHTINKGS) WEIGHTINKGS, SUM(BILLEDAMOUNT - GSTVALUE) TAXABLEAMOUNT 
//	, SUM(BD.CGST) CGST, SUM(BD.SGST) SGST, SUM(BD.CESS) CESS, SUM(BD.GSTVALUE) GSTVALUE, SUM(BILLEDAMOUNT) BILLEDAMOUNT
//from 
//	TMP_BillDetail BD
//	inner join TMP_Bill b on b.bid = bd.bid
//GROUP BY B.BRANCHNAME, CAST(B.BILLCLOSEDDATE AS DATE), BD.ITEMCODE, BD.ITEMNAME, BD.MRP, BD.SALEPRICE
//ORDER BY  CAST(B.BILLCLOSEDDATE AS DATE), B.BRANCHNAME, BD.ITEMNAME
