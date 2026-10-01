namespace NSRetailPOS.Operations.Reports
{
    partial class ucDispatchDifferences
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
            this.dtToDate = new DevExpress.XtraEditors.DateEdit();
            this.dtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbBranch = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cmbCategory = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItemBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemFromDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItemToDate = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToDate)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dtToDate);
            this.layoutControl1.Controls.Add(this.dtFromDate);
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(923, 45);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dtToDate
            // 
            this.dtToDate.EditValue = null;
            this.dtToDate.EnterMoveNextControl = true;
            this.dtToDate.Location = new System.Drawing.Point(794, 11);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtToDate.Size = new System.Drawing.Size(118, 22);
            this.dtToDate.StyleController = this.layoutControl1;
            this.dtToDate.TabIndex = 3;
            // 
            // dtFromDate
            // 
            this.dtFromDate.EditValue = null;
            this.dtFromDate.EnterMoveNextControl = true;
            this.dtFromDate.Location = new System.Drawing.Point(594, 11);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFromDate.Size = new System.Drawing.Size(118, 22);
            this.dtFromDate.StyleController = this.layoutControl1;
            this.dtFromDate.TabIndex = 2;
            // 
            // cmbBranch
            // 
            this.cmbBranch.Location = new System.Drawing.Point(81, 11);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Size = new System.Drawing.Size(171, 22);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 0;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(334, 11);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Size = new System.Drawing.Size(178, 22);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItemBranch,
            this.layoutControlItemCategory,
            this.layoutControlItemFromDate,
            this.layoutControlItemToDate});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(923, 45);
            this.Root.TextVisible = false;
            // 
            // layoutControlItemBranch
            // 
            this.layoutControlItemBranch.Control = this.cmbBranch;
            this.layoutControlItemBranch.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItemBranch.Name = "layoutControlItemBranch";
            this.layoutControlItemBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItemBranch.Size = new System.Drawing.Size(253, 35);
            this.layoutControlItemBranch.Text = "Branch";
            this.layoutControlItemBranch.TextSize = new System.Drawing.Size(58, 15);
            // 
            // layoutControlItemCategory
            // 
            this.layoutControlItemCategory.Control = this.cmbCategory;
            this.layoutControlItemCategory.Location = new System.Drawing.Point(253, 0);
            this.layoutControlItemCategory.Name = "layoutControlItemCategory";
            this.layoutControlItemCategory.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItemCategory.Size = new System.Drawing.Size(260, 35);
            this.layoutControlItemCategory.Text = "Category";
            this.layoutControlItemCategory.TextSize = new System.Drawing.Size(58, 15);
            // 
            // layoutControlItemFromDate
            // 
            this.layoutControlItemFromDate.Control = this.dtFromDate;
            this.layoutControlItemFromDate.Location = new System.Drawing.Point(513, 0);
            this.layoutControlItemFromDate.MaxSize = new System.Drawing.Size(200, 40);
            this.layoutControlItemFromDate.MinSize = new System.Drawing.Size(200, 35);
            this.layoutControlItemFromDate.Name = "layoutControlItemFromDate";
            this.layoutControlItemFromDate.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItemFromDate.Size = new System.Drawing.Size(200, 35);
            this.layoutControlItemFromDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemFromDate.Text = "From Date";
            this.layoutControlItemFromDate.TextSize = new System.Drawing.Size(58, 15);
            // 
            // layoutControlItemToDate
            // 
            this.layoutControlItemToDate.Control = this.dtToDate;
            this.layoutControlItemToDate.Location = new System.Drawing.Point(713, 0);
            this.layoutControlItemToDate.MaxSize = new System.Drawing.Size(200, 40);
            this.layoutControlItemToDate.MinSize = new System.Drawing.Size(200, 35);
            this.layoutControlItemToDate.Name = "layoutControlItemToDate";
            this.layoutControlItemToDate.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItemToDate.Size = new System.Drawing.Size(200, 35);
            this.layoutControlItemToDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItemToDate.Text = "To Date";
            this.layoutControlItemToDate.TextSize = new System.Drawing.Size(58, 15);
            // 
            // ucDispatchDifferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucDispatchDifferences";
            this.Size = new System.Drawing.Size(923, 45);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemToDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit dtToDate;
        private DevExpress.XtraEditors.DateEdit dtFromDate;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemFromDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemToDate;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbBranch;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbCategory;
    }
}
