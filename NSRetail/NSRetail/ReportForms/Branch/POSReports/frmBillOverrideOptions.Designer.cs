namespace NSRetail.ReportForms.Branch.POSReports
{
    partial class frmBillOverrideOptions
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.chkSameAsBillTo = new DevExpress.XtraEditors.CheckEdit();
            this.txtShipToAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtShipToNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtShipToName = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomerAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtCustomerNumber = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgBillTo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciCustomerName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCustomerNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCustomerAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSameAsBillTo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgShipTo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciShipToName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciShipToNumber = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciShipToAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciApply = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSameAsBillTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipToAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipToNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipToName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBillTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomerNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomerAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSameAsBillTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgShipTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciShipToName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciShipToNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciShipToAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.chkSameAsBillTo);
            this.layoutControl1.Controls.Add(this.txtShipToAddress);
            this.layoutControl1.Controls.Add(this.txtShipToNumber);
            this.layoutControl1.Controls.Add(this.txtShipToName);
            this.layoutControl1.Controls.Add(this.txtCustomerAddress);
            this.layoutControl1.Controls.Add(this.txtCustomerNumber);
            this.layoutControl1.Controls.Add(this.txtCustomerName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(560, 434);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(435, 404);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(316, 404);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(107, 22);
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chkSameAsBillTo
            // 
            this.chkSameAsBillTo.EditValue = true;
            this.chkSameAsBillTo.Location = new System.Drawing.Point(8, 178);
            this.chkSameAsBillTo.Name = "chkSameAsBillTo";
            this.chkSameAsBillTo.Properties.Caption = "Ship To same as Bill To";
            this.chkSameAsBillTo.Size = new System.Drawing.Size(544, 19);
            this.chkSameAsBillTo.StyleController = this.layoutControl1;
            this.chkSameAsBillTo.TabIndex = 3;
            this.chkSameAsBillTo.CheckedChanged += new System.EventHandler(this.chkSameAsBillTo_CheckedChanged);
            // 
            // txtShipToAddress
            // 
            this.txtShipToAddress.Location = new System.Drawing.Point(111, 277);
            this.txtShipToAddress.Name = "txtShipToAddress";
            this.txtShipToAddress.Size = new System.Drawing.Size(441, 95);
            this.txtShipToAddress.StyleController = this.layoutControl1;
            this.txtShipToAddress.TabIndex = 6;
            // 
            // txtShipToNumber
            // 
            this.txtShipToNumber.Location = new System.Drawing.Point(111, 243);
            this.txtShipToNumber.Name = "txtShipToNumber";
            this.txtShipToNumber.Size = new System.Drawing.Size(441, 22);
            this.txtShipToNumber.StyleController = this.layoutControl1;
            this.txtShipToNumber.TabIndex = 5;
            // 
            // txtShipToName
            // 
            this.txtShipToName.Location = new System.Drawing.Point(111, 209);
            this.txtShipToName.Name = "txtShipToName";
            this.txtShipToName.Size = new System.Drawing.Size(441, 22);
            this.txtShipToName.StyleController = this.layoutControl1;
            this.txtShipToName.TabIndex = 4;
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.Location = new System.Drawing.Point(111, 76);
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.Size = new System.Drawing.Size(441, 90);
            this.txtCustomerAddress.StyleController = this.layoutControl1;
            this.txtCustomerAddress.TabIndex = 2;
            this.txtCustomerAddress.EditValueChanged += new System.EventHandler(this.BillTo_EditValueChanged);
            // 
            // txtCustomerNumber
            // 
            this.txtCustomerNumber.Location = new System.Drawing.Point(111, 42);
            this.txtCustomerNumber.Name = "txtCustomerNumber";
            this.txtCustomerNumber.Size = new System.Drawing.Size(441, 22);
            this.txtCustomerNumber.StyleController = this.layoutControl1;
            this.txtCustomerNumber.TabIndex = 1;
            this.txtCustomerNumber.EditValueChanged += new System.EventHandler(this.BillTo_EditValueChanged);
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(111, 8);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(441, 22);
            this.txtCustomerName.StyleController = this.layoutControl1;
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.EditValueChanged += new System.EventHandler(this.BillTo_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgBillTo,
            this.lciSameAsBillTo,
            this.lcgShipTo,
            this.emptySpaceItem1,
            this.lciApply,
            this.lciCancel});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(560, 434);
            this.Root.TextVisible = false;
            // 
            // lcgBillTo
            // 
            this.lcgBillTo.GroupBordersVisible = false;
            this.lcgBillTo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciCustomerName,
            this.lciCustomerNumber,
            this.lciCustomerAddress});
            this.lcgBillTo.Location = new System.Drawing.Point(0, 0);
            this.lcgBillTo.Name = "lcgBillTo";
            this.lcgBillTo.Size = new System.Drawing.Size(556, 170);
            this.lcgBillTo.TextVisible = false;
            // 
            // lciCustomerName
            // 
            this.lciCustomerName.Control = this.txtCustomerName;
            this.lciCustomerName.Location = new System.Drawing.Point(0, 0);
            this.lciCustomerName.Name = "lciCustomerName";
            this.lciCustomerName.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciCustomerName.Size = new System.Drawing.Size(556, 34);
            this.lciCustomerName.Text = "Bill To Name";
            this.lciCustomerName.TextSize = new System.Drawing.Size(91, 15);
            // 
            // lciCustomerNumber
            // 
            this.lciCustomerNumber.Control = this.txtCustomerNumber;
            this.lciCustomerNumber.Location = new System.Drawing.Point(0, 34);
            this.lciCustomerNumber.Name = "lciCustomerNumber";
            this.lciCustomerNumber.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciCustomerNumber.Size = new System.Drawing.Size(556, 34);
            this.lciCustomerNumber.Text = "Bill To #";
            this.lciCustomerNumber.TextSize = new System.Drawing.Size(91, 15);
            // 
            // lciCustomerAddress
            // 
            this.lciCustomerAddress.Control = this.txtCustomerAddress;
            this.lciCustomerAddress.Location = new System.Drawing.Point(0, 68);
            this.lciCustomerAddress.Name = "lciCustomerAddress";
            this.lciCustomerAddress.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciCustomerAddress.Size = new System.Drawing.Size(556, 102);
            this.lciCustomerAddress.Text = "Bill To Address";
            this.lciCustomerAddress.TextSize = new System.Drawing.Size(91, 15);
            // 
            // lciSameAsBillTo
            // 
            this.lciSameAsBillTo.Control = this.chkSameAsBillTo;
            this.lciSameAsBillTo.Location = new System.Drawing.Point(0, 170);
            this.lciSameAsBillTo.Name = "lciSameAsBillTo";
            this.lciSameAsBillTo.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciSameAsBillTo.Size = new System.Drawing.Size(556, 31);
            this.lciSameAsBillTo.TextSize = new System.Drawing.Size(0, 0);
            this.lciSameAsBillTo.TextVisible = false;
            // 
            // lcgShipTo
            // 
            this.lcgShipTo.GroupBordersVisible = false;
            this.lcgShipTo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciShipToName,
            this.lciShipToNumber,
            this.lciShipToAddress});
            this.lcgShipTo.Location = new System.Drawing.Point(0, 201);
            this.lcgShipTo.Name = "lcgShipTo";
            this.lcgShipTo.Size = new System.Drawing.Size(556, 175);
            this.lcgShipTo.TextVisible = false;
            // 
            // lciShipToName
            // 
            this.lciShipToName.Control = this.txtShipToName;
            this.lciShipToName.Location = new System.Drawing.Point(0, 0);
            this.lciShipToName.Name = "lciShipToName";
            this.lciShipToName.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciShipToName.Size = new System.Drawing.Size(556, 34);
            this.lciShipToName.Text = "Ship To Name";
            this.lciShipToName.TextSize = new System.Drawing.Size(91, 15);
            // 
            // lciShipToNumber
            // 
            this.lciShipToNumber.Control = this.txtShipToNumber;
            this.lciShipToNumber.Location = new System.Drawing.Point(0, 34);
            this.lciShipToNumber.Name = "lciShipToNumber";
            this.lciShipToNumber.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciShipToNumber.Size = new System.Drawing.Size(556, 34);
            this.lciShipToNumber.Text = "Ship To #";
            this.lciShipToNumber.TextSize = new System.Drawing.Size(91, 15);
            // 
            // lciShipToAddress
            // 
            this.lciShipToAddress.Control = this.txtShipToAddress;
            this.lciShipToAddress.Location = new System.Drawing.Point(0, 68);
            this.lciShipToAddress.Name = "lciShipToAddress";
            this.lciShipToAddress.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciShipToAddress.Size = new System.Drawing.Size(556, 107);
            this.lciShipToAddress.Text = "Ship To Address";
            this.lciShipToAddress.TextSize = new System.Drawing.Size(91, 15);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 376);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(308, 54);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciApply
            // 
            this.lciApply.Control = this.btnApply;
            this.lciApply.Location = new System.Drawing.Point(308, 376);
            this.lciApply.Name = "lciApply";
            this.lciApply.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 26, 6);
            this.lciApply.Size = new System.Drawing.Size(119, 54);
            this.lciApply.TextSize = new System.Drawing.Size(0, 0);
            this.lciApply.TextVisible = false;
            // 
            // lciCancel
            // 
            this.lciCancel.Control = this.btnCancel;
            this.lciCancel.Location = new System.Drawing.Point(427, 376);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 14, 26, 6);
            this.lciCancel.Size = new System.Drawing.Size(129, 54);
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextVisible = false;
            // 
            // frmBillOverrideOptions
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 434);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmBillOverrideOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bill override options";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkSameAsBillTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipToAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipToNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipToName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBillTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomerNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCustomerAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSameAsBillTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgShipTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciShipToName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciShipToNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciShipToAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraEditors.CheckEdit chkSameAsBillTo;
        private DevExpress.XtraEditors.MemoEdit txtShipToAddress;
        private DevExpress.XtraEditors.TextEdit txtShipToNumber;
        private DevExpress.XtraEditors.TextEdit txtShipToName;
        private DevExpress.XtraEditors.MemoEdit txtCustomerAddress;
        private DevExpress.XtraEditors.TextEdit txtCustomerNumber;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBillTo;
        private DevExpress.XtraLayout.LayoutControlItem lciCustomerName;
        private DevExpress.XtraLayout.LayoutControlItem lciCustomerNumber;
        private DevExpress.XtraLayout.LayoutControlItem lciCustomerAddress;
        private DevExpress.XtraLayout.LayoutControlItem lciSameAsBillTo;
        private DevExpress.XtraLayout.LayoutControlGroup lcgShipTo;
        private DevExpress.XtraLayout.LayoutControlItem lciShipToName;
        private DevExpress.XtraLayout.LayoutControlItem lciShipToNumber;
        private DevExpress.XtraLayout.LayoutControlItem lciShipToAddress;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lciApply;
        private DevExpress.XtraLayout.LayoutControlItem lciCancel;
    }
}
