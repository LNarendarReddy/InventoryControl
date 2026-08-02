using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;

namespace NSRetail.Reports
{
    public partial class rptPickList : XtraReport
    {
        public rptPickList(string supplierName, string category, DataTable table)
        {
            InitializeComponent();
            label1.Text = $"Pick list - {category} - {supplierName} ";

            Detail.Controls.Clear();
            Detail.Controls.Add(BuildTable(table));
        }

        public XRTable BuildTable(DataTable dt)
        {
            XRTable table = new XRTable();
            table.BeginInit();

            table.LocationF = new PointF(0, 0);
            table.WidthF = 1100;
            table.Borders = BorderSide.All;
            table.BorderWidth = 1;
            table.Font = new Font("Segoe UI", 9);

            int branchesPerRow = 12;

            var items = dt.AsEnumerable()
                          .GroupBy(r => new
                          {
                              ItemName = r["ITEMNAME"].ToString(),
                              ItemCode = r["ITEMCODE"].ToString()
                          });

            foreach (var item in items)
            {

                //==========================
                // Item + Code Row
                //==========================
                XRTableRow headerRow = new XRTableRow
                {
                    HeightF = 22
                };

                XRTableCell itemCell = new XRTableCell()
                {
                    Text = "ITEM : " + item.Key.ItemName,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Weight = 3,
                    TextAlignment = TextAlignment.MiddleLeft,
                    Padding = new PaddingInfo(4, 2, 0, 0)
                };

                XRTableCell codeCell = new XRTableCell()
                {
                    Text = "CODE : " + item.Key.ItemCode,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Weight = 3,
                    TextAlignment = TextAlignment.MiddleRight,
                    Padding = new PaddingInfo(2, 4, 0, 0)
                };

                headerRow.Cells.Add(itemCell);
                headerRow.Cells.Add(codeCell);

                table.Rows.Add(headerRow);

                //==========================
                // Branch Rows
                //==========================
                var branches = item.ToList();

                for (int i = 0; i < branches.Count; i += branchesPerRow)
                {
                    XRTableRow branchRow = new XRTableRow();
                    XRTableRow qtyRow = new XRTableRow();

                    var chunk = branches.Skip(i).Take(branchesPerRow);

                    foreach (var b in chunk)
                    {
                        branchRow.Cells.Add(new XRTableCell()
                        {
                            Text = b["BRANCHCODE"].ToString(),
                            TextAlignment = TextAlignment.MiddleCenter
                        });

                        qtyRow.Cells.Add(new XRTableCell()
                        {
                            Text = b["AVAILABLEQUANTITY"].ToString(),
                            TextAlignment = TextAlignment.MiddleCenter
                        });
                    }

                    table.Rows.Add(branchRow);
                    table.Rows.Add(qtyRow);
                }

                //==========================
                // Blank Separator
                //==========================
                XRTableRow blank = new XRTableRow();
                blank.Cells.Add(new XRTableCell()
                {
                    Text = "",
                    Borders = BorderSide.None,
                    HeightF = 10
                });

                table.Rows.Add(blank);
            }

            table.EndInit();

            return table;
        }
    }
}
