namespace NSRetail
{
    partial class frmChangeGST
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeGST));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.dtLiveDate = new DevExpress.XtraEditors.DateEdit();
            this.chkImmediate = new DevExpress.XtraEditors.CheckEdit();
            this.txtItemName = new DevExpress.XtraEditors.TextEdit();
            this.txtSKUCode = new DevExpress.XtraEditors.TextEdit();
            this.cmbGSTCode = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtLiveDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLiveDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImmediate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSKUCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGSTCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.dtLiveDate);
            this.layoutControl1.Controls.Add(this.chkImmediate);
            this.layoutControl1.Controls.Add(this.txtItemName);
            this.layoutControl1.Controls.Add(this.txtSKUCode);
            this.layoutControl1.Controls.Add(this.cmbGSTCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(573, 199);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(450, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(320, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(118, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtLiveDate
            // 
            this.dtLiveDate.EditValue = null;
            this.dtLiveDate.EnterMoveNextControl = true;
            this.dtLiveDate.Location = new System.Drawing.Point(236, 118);
            this.dtLiveDate.Name = "dtLiveDate";
            this.dtLiveDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtLiveDate.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtLiveDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtLiveDate.Properties.DisplayFormat.FormatString = "g";
            this.dtLiveDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtLiveDate.Properties.EditFormat.FormatString = "g";
            this.dtLiveDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtLiveDate.Properties.MaskSettings.Set("mask", "g");
            this.dtLiveDate.Properties.UseMaskAsDisplayFormat = true;
            this.dtLiveDate.Size = new System.Drawing.Size(321, 22);
            this.dtLiveDate.StyleController = this.layoutControl1;
            this.dtLiveDate.TabIndex = 8;
            // 
            // chkImmediate
            // 
            this.chkImmediate.EnterMoveNextControl = true;
            this.chkImmediate.Location = new System.Drawing.Point(16, 118);
            this.chkImmediate.Name = "chkImmediate";
            this.chkImmediate.Properties.Caption = "Immediate";
            this.chkImmediate.Size = new System.Drawing.Size(135, 19);
            this.chkImmediate.StyleController = this.layoutControl1;
            this.chkImmediate.TabIndex = 7;
            this.chkImmediate.CheckedChanged += new System.EventHandler(this.chkImmediate_CheckedChanged);
            // 
            // txtItemName
            // 
            this.txtItemName.EnterMoveNextControl = true;
            this.txtItemName.Location = new System.Drawing.Point(89, 50);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Properties.UseMaskAsDisplayFormat = true;
            this.txtItemName.Size = new System.Drawing.Size(468, 22);
            this.txtItemName.StyleController = this.layoutControl1;
            this.txtItemName.TabIndex = 5;
            // 
            // txtSKUCode
            // 
            this.txtSKUCode.EnterMoveNextControl = true;
            this.txtSKUCode.Location = new System.Drawing.Point(89, 16);
            this.txtSKUCode.Name = "txtSKUCode";
            this.txtSKUCode.Properties.UseMaskAsDisplayFormat = true;
            this.txtSKUCode.Size = new System.Drawing.Size(468, 22);
            this.txtSKUCode.StyleController = this.layoutControl1;
            this.txtSKUCode.TabIndex = 4;
            // 
            // cmbGSTCode
            // 
            this.cmbGSTCode.Location = new System.Drawing.Point(89, 84);
            this.cmbGSTCode.Name = "cmbGSTCode";
            this.cmbGSTCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGSTCode.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GSTCODE", "GST Code"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GSTID", "GSTID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbGSTCode.Properties.NullText = "";
            this.cmbGSTCode.Properties.PopupSizeable = false;
            this.cmbGSTCode.Size = new System.Drawing.Size(468, 22);
            this.cmbGSTCode.StyleController = this.layoutControl1;
            this.cmbGSTCode.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem4,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(573, 199);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtSKUCode;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(553, 34);
            this.layoutControlItem1.Text = "SKU Code";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(61, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtItemName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(553, 34);
            this.layoutControlItem2.Text = "Item Name";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(61, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbGSTCode;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(553, 34);
            this.layoutControlItem3.Text = "GST Code";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(61, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtLiveDate;
            this.layoutControlItem5.Location = new System.Drawing.Point(147, 102);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(406, 34);
            this.layoutControlItem5.Text = "Go Live at";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(61, 15);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            this.layoutControlItem6.Location = new System.Drawing.Point(304, 136);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(130, 43);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnCancel;
            this.layoutControlItem7.Location = new System.Drawing.Point(434, 136);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(119, 43);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkImmediate;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(147, 34);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 136);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(304, 43);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmChangeGST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(573, 199);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmChangeGST";
            this.Text = "Change GST ";
            this.Load += new System.EventHandler(this.frmChangeGST_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtLiveDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtLiveDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImmediate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSKUCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGSTCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.DateEdit dtLiveDate;
        private DevExpress.XtraEditors.CheckEdit chkImmediate;
        private DevExpress.XtraEditors.TextEdit txtItemName;
        private DevExpress.XtraEditors.TextEdit txtSKUCode;
        private DevExpress.XtraEditors.LookUpEdit cmbGSTCode;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}