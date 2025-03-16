namespace NSRetailPOS.Operations.Reports
{
    partial class ucOfferStatus
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cmbBranch = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.txtNoOfDays = new DevExpress.XtraEditors.TextEdit();
            this.cmbOfferSearchType = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbCategory = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cmbClass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoOfDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOfferSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Controls.Add(this.txtNoOfDays);
            this.layoutControl1.Controls.Add(this.cmbOfferSearchType);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Controls.Add(this.cmbClass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1087, 50);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cmbBranch
            // 
            this.cmbBranch.EditValue = "";
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(89, 13);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Size = new System.Drawing.Size(171, 22);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 1;
            // 
            // txtNoOfDays
            // 
            this.txtNoOfDays.EnterMoveNextControl = true;
            this.txtNoOfDays.Location = new System.Drawing.Point(1023, 12);
            this.txtNoOfDays.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNoOfDays.Name = "txtNoOfDays";
            this.txtNoOfDays.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtNoOfDays.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtNoOfDays.Properties.MaskSettings.Set("mask", "n0");
            this.txtNoOfDays.Properties.UseMaskAsDisplayFormat = true;
            this.txtNoOfDays.Size = new System.Drawing.Size(52, 22);
            this.txtNoOfDays.StyleController = this.layoutControl1;
            this.txtNoOfDays.TabIndex = 4;
            // 
            // cmbOfferSearchType
            // 
            this.cmbOfferSearchType.EnterMoveNextControl = true;
            this.cmbOfferSearchType.Location = new System.Drawing.Point(788, 12);
            this.cmbOfferSearchType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbOfferSearchType.Name = "cmbOfferSearchType";
            this.cmbOfferSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOfferSearchType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OFFERSEARCHTYPEID", "OFFERSEARCHTYPEID", 23, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OFFERSEARCHTYPE", "Search type", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbOfferSearchType.Properties.NullText = "";
            this.cmbOfferSearchType.Size = new System.Drawing.Size(147, 22);
            this.cmbOfferSearchType.StyleController = this.layoutControl1;
            this.cmbOfferSearchType.TabIndex = 1;
            this.cmbOfferSearchType.EditValueChanged += new System.EventHandler(this.cmbOfferSearchType_EditValueChanged);
            // 
            // cmbCategory
            // 
            this.cmbCategory.EnterMoveNextControl = true;
            this.cmbCategory.Location = new System.Drawing.Point(350, 13);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Size = new System.Drawing.Size(123, 22);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 2;
            // 
            // cmbClass
            // 
            this.cmbClass.EnterMoveNextControl = true;
            this.cmbClass.Location = new System.Drawing.Point(563, 13);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbClass.Size = new System.Drawing.Size(136, 22);
            this.cmbClass.StyleController = this.layoutControl1;
            this.cmbClass.TabIndex = 2;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.layoutControlItem15,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1087, 50);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbBranch;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "Branch";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem2.Size = new System.Drawing.Size(261, 38);
            this.layoutControlItem2.Text = "Branch";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(64, 15);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtNoOfDays;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "Item Code";
            this.layoutControlItem6.Location = new System.Drawing.Point(935, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(140, 38);
            this.layoutControlItem6.Text = "No of Days";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(64, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbOfferSearchType;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "Category";
            this.layoutControlItem4.Location = new System.Drawing.Point(700, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(235, 38);
            this.layoutControlItem4.Text = "Search type";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(64, 15);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.cmbCategory;
            this.layoutControlItem15.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem15.CustomizationFormText = "Category";
            this.layoutControlItem15.Location = new System.Drawing.Point(261, 0);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem15.Size = new System.Drawing.Size(213, 38);
            this.layoutControlItem15.Text = "Category";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(64, 15);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbClass;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "Category";
            this.layoutControlItem1.Location = new System.Drawing.Point(474, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem1.Size = new System.Drawing.Size(226, 38);
            this.layoutControlItem1.Text = "Class";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(64, 15);
            // 
            // ucOfferStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ucOfferStatus";
            this.Size = new System.Drawing.Size(1087, 50);
            this.Load += new System.EventHandler(this.ucOfferStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoOfDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOfferSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbBranch;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.TextEdit txtNoOfDays;
        private DevExpress.XtraEditors.LookUpEdit cmbOfferSearchType;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbCategory;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
