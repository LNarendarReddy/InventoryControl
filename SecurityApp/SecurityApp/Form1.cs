using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CategisSecurity;

namespace SecurityApp
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerateEText_Click(object sender, EventArgs e)
        {
            txtEText.Text =  Security.Encrypt(txtPlaintext.Text);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            txtPlaintext.Text = Security.Decrypt(txtEText.Text);
        }
    }
}
