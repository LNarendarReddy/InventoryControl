namespace NSRetailPOS.Operations.Reports
{
    partial class ucItempriceChanges
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
            this.chkAllItems = new DevExpress.XtraEditors.CheckEdit();
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbCategory = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cmbClass = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllItems.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkAllItems);
            this.layoutControl1.Controls.Add(this.dtpFromDate);
            this.layoutControl1.Controls.Add(this.dtpToDate);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Controls.Add(this.cmbClass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(995, 50);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkAllItems
            // 
            this.chkAllItems.Location = new System.Drawing.Point(886, 11);
            this.chkAllItems.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkAllItems.Name = "chkAllItems";
            this.chkAllItems.Properties.Caption = "All Items";
            this.chkAllItems.Size = new System.Drawing.Size(98, 19);
            this.chkAllItems.StyleController = this.layoutControl1;
            this.chkAllItems.TabIndex = 4;
            this.chkAllItems.CheckedChanged += new System.EventHandler(this.chkAllItems_CheckedChanged);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.EditValue = null;
            this.dtpFromDate.EnterMoveNextControl = true;
            this.dtpFromDate.Location = new System.Drawing.Point(559, 11);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Size = new System.Drawing.Size(115, 22);
            this.dtpFromDate.StyleController = this.layoutControl1;
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(758, 11);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Size = new System.Drawing.Size(116, 22);
            this.dtpToDate.StyleController = this.layoutControl1;
            this.dtpToDate.TabIndex = 3;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem15,
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(995, 50);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dtpFromDate;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "From Date";
            this.layoutControlItem4.Location = new System.Drawing.Point(476, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(199, 40);
            this.layoutControlItem4.Text = "From Date";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(58, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtpToDate;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "To Date";
            this.layoutControlItem5.Location = new System.Drawing.Point(675, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(200, 40);
            this.layoutControlItem5.Text = "To Date";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(58, 15);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkAllItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(875, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(110, 40);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // cmbCategory
            // 
            this.cmbCategory.EnterMoveNextControl = true;
            this.cmbCategory.Location = new System.Drawing.Point(85, 13);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Size = new System.Drawing.Size(144, 22);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 2;
            // 
            // cmbClass
            // 
            this.cmbClass.EnterMoveNextControl = true;
            this.cmbClass.Location = new System.Drawing.Point(317, 13);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbClass.Size = new System.Drawing.Size(156, 22);
            this.cmbClass.StyleController = this.layoutControl1;
            this.cmbClass.TabIndex = 2;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.cmbCategory;
            this.layoutControlItem15.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem15.CustomizationFormText = "Category";
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItem15.Size = new System.Drawing.Size(232, 40);
            this.layoutControlItem15.Text = "Category";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(58, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbClass;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "Category";
            this.layoutControlItem2.Location = new System.Drawing.Point(232, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.layoutControlItem2.Size = new System.Drawing.Size(244, 40);
            this.layoutControlItem2.Text = "Class";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(58, 15);
            // 
            // ucItempriceChanges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ucItempriceChanges";
            this.Size = new System.Drawing.Size(995, 50);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkAllItems.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckEdit chkAllItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbCategory;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbClass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
