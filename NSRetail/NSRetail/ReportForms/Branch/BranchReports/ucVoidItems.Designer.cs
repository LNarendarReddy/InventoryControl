﻿namespace NSRetail.ReportForms.Branch
{
    partial class ucVoidItems : SearchCriteriaBase
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
            this.dtpFromDate = new DevExpress.XtraEditors.DateEdit();
            this.dtpToDate = new DevExpress.XtraEditors.DateEdit();
            this.cmbPeriodicity = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbBranch = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.cmbItemCode = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbCategory = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPeriodicity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dtpFromDate);
            this.layoutControl1.Controls.Add(this.dtpToDate);
            this.layoutControl1.Controls.Add(this.cmbPeriodicity);
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Controls.Add(this.cmbItemCode);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1389, 50);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.EditValue = null;
            this.dtpFromDate.EnterMoveNextControl = true;
            this.dtpFromDate.Location = new System.Drawing.Point(589, 13);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFromDate.Size = new System.Drawing.Size(94, 24);
            this.dtpFromDate.StyleController = this.layoutControl1;
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.EditValue = null;
            this.dtpToDate.EnterMoveNextControl = true;
            this.dtpToDate.Location = new System.Drawing.Point(779, 13);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpToDate.Size = new System.Drawing.Size(102, 24);
            this.dtpToDate.StyleController = this.layoutControl1;
            this.dtpToDate.TabIndex = 3;
            // 
            // cmbPeriodicity
            // 
            this.cmbPeriodicity.EnterMoveNextControl = true;
            this.cmbPeriodicity.Location = new System.Drawing.Point(95, 13);
            this.cmbPeriodicity.Name = "cmbPeriodicity";
            this.cmbPeriodicity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPeriodicity.Properties.NullText = "";
            this.cmbPeriodicity.Size = new System.Drawing.Size(119, 24);
            this.cmbPeriodicity.StyleController = this.layoutControl1;
            this.cmbPeriodicity.TabIndex = 0;
            // 
            // cmbBranch
            // 
            this.cmbBranch.EditValue = "";
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(310, 13);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Size = new System.Drawing.Size(183, 24);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 1;
            // 
            // cmbItemCode
            // 
            this.cmbItemCode.EnterMoveNextControl = true;
            this.cmbItemCode.Location = new System.Drawing.Point(1201, 12);
            this.cmbItemCode.Name = "cmbItemCode";
            this.cmbItemCode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbItemCode.Properties.NullText = "";
            this.cmbItemCode.Properties.PopupView = this.searchLookUpEdit1View;
            this.cmbItemCode.Size = new System.Drawing.Size(176, 24);
            this.cmbItemCode.StyleController = this.layoutControl1;
            this.cmbItemCode.TabIndex = 4;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn11,
            this.gridColumn2,
            this.gridColumn3});
            this.searchLookUpEdit1View.DetailHeight = 397;
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "SKU Code";
            this.gridColumn11.FieldName = "SKUCODE";
            this.gridColumn11.MinWidth = 23;
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 0;
            this.gridColumn11.Width = 86;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Item Code";
            this.gridColumn2.FieldName = "ITEMCODE";
            this.gridColumn2.MinWidth = 23;
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 86;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Item Name";
            this.gridColumn3.FieldName = "ITEMNAME";
            this.gridColumn3.MinWidth = 23;
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 86;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(976, 12);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Size = new System.Drawing.Size(131, 24);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 1;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.Root.Size = new System.Drawing.Size(1389, 50);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbBranch;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem2.CustomizationFormText = "Branch";
            this.layoutControlItem2.Location = new System.Drawing.Point(215, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem2.Size = new System.Drawing.Size(279, 38);
            this.layoutControlItem2.Text = "Branch";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(70, 17);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.dtpFromDate;
            this.layoutControlItem1.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem1.CustomizationFormText = "From Date";
            this.layoutControlItem1.Location = new System.Drawing.Point(494, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem1.Size = new System.Drawing.Size(190, 38);
            this.layoutControlItem1.Text = "From Date";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(70, 17);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.dtpToDate;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem5.CustomizationFormText = "To Date";
            this.layoutControlItem5.Location = new System.Drawing.Point(684, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem5.Size = new System.Drawing.Size(198, 38);
            this.layoutControlItem5.Text = "To Date";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(70, 17);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cmbPeriodicity;
            this.layoutControlItem3.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem3.CustomizationFormText = "Periodicity";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 7, 7, 7);
            this.layoutControlItem3.Size = new System.Drawing.Size(215, 38);
            this.layoutControlItem3.Text = "Periodicity";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(70, 17);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cmbItemCode;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "Item Code";
            this.layoutControlItem6.Location = new System.Drawing.Point(1107, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(270, 38);
            this.layoutControlItem6.Text = "Item Code";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(70, 17);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.cmbCategory;
            this.layoutControlItem4.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem4.CustomizationFormText = "Category";
            this.layoutControlItem4.Location = new System.Drawing.Point(882, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(225, 38);
            this.layoutControlItem4.Text = "Category";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(70, 17);
            // 
            // ucVoidItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "ucVoidItems";
            this.Size = new System.Drawing.Size(1389, 50);
            this.Load += new System.EventHandler(this.ucSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPeriodicity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit dtpFromDate;
        private DevExpress.XtraEditors.DateEdit dtpToDate;
        private DevExpress.XtraEditors.LookUpEdit cmbPeriodicity;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbBranch;
        private DevExpress.XtraEditors.SearchLookUpEdit cmbItemCode;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbCategory;
    }
}
