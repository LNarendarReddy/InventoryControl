namespace NSRetail.ReportForms.Branch.BranchReports
{
    partial class ucBranchIndent
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBranchIndent));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnPrintToDM = new DevExpress.XtraEditors.SimpleButton();
            this.cmbCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtIndentDays = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtSafetyDays = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndentDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafetyDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtSafetyDays);
            this.layoutControl1.Controls.Add(this.txtIndentDays);
            this.layoutControl1.Controls.Add(this.btnPrintToDM);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1017, 45);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnPrintToDM
            // 
            this.btnPrintToDM.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintToDM.ImageOptions.Image")));
            this.btnPrintToDM.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnPrintToDM.Location = new System.Drawing.Point(863, 11);
            this.btnPrintToDM.Name = "btnPrintToDM";
            this.btnPrintToDM.Size = new System.Drawing.Size(143, 22);
            this.btnPrintToDM.StyleController = this.layoutControl1;
            this.btnPrintToDM.TabIndex = 4;
            this.btnPrintToDM.Text = "Print to DM";
            this.btnPrintToDM.Click += new System.EventHandler(this.btnPrintToDM_Click);
            // 
            // cmbCategory
            // 
            this.cmbCategory.EnterMoveNextControl = true;
            this.cmbCategory.Location = new System.Drawing.Point(331, 11);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYID", "CATEGORYID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CATEGORYNAME", "Category Name")});
            this.cmbCategory.Properties.NullText = "";
            this.cmbCategory.Size = new System.Drawing.Size(191, 22);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 1;
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(86, 11);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHID", "BRANCHID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Branch Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHCODE", "Branch Code", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbBranch.Properties.NullText = "";
            this.cmbBranch.Size = new System.Drawing.Size(158, 22);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 0;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1017, 45);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbBranch;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(245, 35);
            this.layoutControlItem1.Text = "Branch";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbCategory;
            this.layoutControlItem2.Location = new System.Drawing.Point(245, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(278, 35);
            this.layoutControlItem2.Text = "Category";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnPrintToDM;
            this.layoutControlItem5.Location = new System.Drawing.Point(852, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(155, 34);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(155, 34);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(155, 35);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // txtIndentDays
            // 
            this.txtIndentDays.EnterMoveNextControl = true;
            this.txtIndentDays.Location = new System.Drawing.Point(609, 11);
            this.txtIndentDays.Name = "txtIndentDays";
            this.txtIndentDays.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtIndentDays.Properties.MaskSettings.Set("mask", "d");
            this.txtIndentDays.Properties.UseMaskAsDisplayFormat = true;
            this.txtIndentDays.Size = new System.Drawing.Size(80, 22);
            this.txtIndentDays.StyleController = this.layoutControl1;
            this.txtIndentDays.TabIndex = 5;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtIndentDays;
            this.layoutControlItem6.Location = new System.Drawing.Point(523, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(167, 35);
            this.layoutControlItem6.Text = "Indent days";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(63, 15);
            // 
            // txtSafetyDays
            // 
            this.txtSafetyDays.EnterMoveNextControl = true;
            this.txtSafetyDays.Location = new System.Drawing.Point(776, 11);
            this.txtSafetyDays.Name = "txtSafetyDays";
            this.txtSafetyDays.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSafetyDays.Properties.MaskSettings.Set("mask", "d");
            this.txtSafetyDays.Properties.UseMaskAsDisplayFormat = true;
            this.txtSafetyDays.Size = new System.Drawing.Size(75, 22);
            this.txtSafetyDays.StyleController = this.layoutControl1;
            this.txtSafetyDays.TabIndex = 6;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtSafetyDays;
            this.layoutControlItem7.Location = new System.Drawing.Point(690, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(162, 35);
            this.layoutControlItem7.Text = "Safety days";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(63, 15);
            // 
            // ucBranchIndent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucBranchIndent";
            this.Size = new System.Drawing.Size(1017, 45);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIndentDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSafetyDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LookUpEdit cmbCategory;
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnPrintToDM;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.TextEdit txtSafetyDays;
        private DevExpress.XtraEditors.TextEdit txtIndentDays;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}
