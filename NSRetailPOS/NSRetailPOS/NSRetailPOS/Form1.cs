using System;
using System.Data;
//using System.Data.SQLite;

namespace NSRetailPOS
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string cs = @"URI=file:./NSRetailPOS.db";

            //var con = new SQLiteConnection(cs);
            //con.Open();
            //SQLiteDataAdapter adapter = new SQLiteDataAdapter(
            //    "SELECT * FROM ITEMCODE IC " +
            //    "INNER JOIN ITEMPRICE IP ON IP.ITEMCODEID = IC.ITEMCODEID" +
            //    "WHERE IC.ITEMCODE = '8902061190091' ", con);
            //DataSet dsEmployees = new DataSet();
            //adapter.Fill(dsEmployees, "Employees");
            //gridControl1.DataSource = dsEmployees.Tables[0];
        }
    }
}
