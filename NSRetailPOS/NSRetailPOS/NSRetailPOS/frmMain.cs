using System;
using System.Data;
using System.Data.SqlClient;
//using System.Data.SQLite;

namespace NSRetailPOS
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
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

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NSRetailPOS.mdf;Integrated security=true";

            var con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(
                "SELECT * FROM ITEMCODE IC " +
                "INNER JOIN ITEMPRICE IP ON IP.ITEMCODEID = IC.ITEMCODEID" +
                " ", con);
            DataSet dsEmployees = new DataSet();
            adapter.Fill(dsEmployees, "prices");
            gridControl1.DataSource = dsEmployees.Tables[0];
        }

        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

        }
    }
}
