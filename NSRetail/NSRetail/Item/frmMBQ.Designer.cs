namespace NSRetail
{
    partial class frmMBQ
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblItemName = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcIndentLimits = new DevExpress.XtraGrid.GridControl();
            this.gvIndentLimits = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnApplyToAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnApplyToSelected = new DevExpress.XtraEditors.SimpleButton();
            this.txtDesiredQty = new DevExpress.XtraEditors.TextEdit();
            this.txtThreshold = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcIndentLimits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentLimits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesiredQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreshold.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblItemName);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.gcIndentLimits);
            this.layoutControl1.Controls.Add(this.btnApplyToAll);
            this.layoutControl1.Controls.Add(this.btnApplyToSelected);
            this.layoutControl1.Controls.Add(this.txtDesiredQty);
            this.layoutControl1.Controls.Add(this.txtThreshold);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(806, 694);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblItemName
            // 
            this.lblItemName.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblItemName.Appearance.Options.UseFont = true;
            this.lblItemName.Appearance.Options.UseTextOptions = true;
            this.lblItemName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblItemName.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblItemName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblItemName.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblItemName.Location = new System.Drawing.Point(153, 16);
            this.lblItemName.MaximumSize = new System.Drawing.Size(500, 0);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(500, 21);
            this.lblItemName.StyleController = this.layoutControl1;
            this.lblItemName.TabIndex = 12;
            this.lblItemName.Text = "Item Name";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::NSRetail.Properties.Resources.save_16x162;
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(621, 116);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(157, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gcIndentLimits
            // 
            this.gcIndentLimits.Location = new System.Drawing.Point(12, 158);
            this.gcIndentLimits.MainView = this.gvIndentLimits;
            this.gcIndentLimits.Name = "gcIndentLimits";
            this.gcIndentLimits.Size = new System.Drawing.Size(782, 524);
            this.gcIndentLimits.TabIndex = 10;
            this.gcIndentLimits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvIndentLimits});
            // 
            // gvIndentLimits
            // 
            this.gvIndentLimits.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn9,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvIndentLimits.GridControl = this.gcIndentLimits;
            this.gvIndentLimits.Name = "gvIndentLimits";
            this.gvIndentLimits.OptionsMenu.ShowSummaryItemMode = DevExpress.Utils.DefaultBoolean.True;
            this.gvIndentLimits.OptionsSelection.MultiSelect = true;
            this.gvIndentLimits.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvIndentLimits.OptionsView.ShowFooter = true;
            this.gvIndentLimits.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "INDENTLIMITID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Branch";
            this.gridColumn2.FieldName = "BRANCHNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Branch Stock";
            this.gridColumn9.FieldName = "BRANCHSTOCK";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "BRANCHSTOCK", "SUM={0:0.##}")});
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Threshold";
            this.gridColumn3.FieldName = "THRESHOLD";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Desired qty/wght in KGs";
            this.gridColumn4.FieldName = "DESIREDQUANTITY";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "DESIREDQUANTITY", "SUM={0:0.##}")});
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Created By";
            this.gridColumn5.FieldName = "CREATEDBY";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Created Date";
            this.gridColumn6.FieldName = "CREATEDDATE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Updated By";
            this.gridColumn7.FieldName = "UPDATEDBY";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Update Date";
            this.gridColumn8.FieldName = "UPDATEDDATE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            // 
            // btnApplyToAll
            // 
            this.btnApplyToAll.ImageOptions.Image = global::NSRetail.Properties.Resources.apply_16x162;
            this.btnApplyToAll.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnApplyToAll.Location = new System.Drawing.Point(300, 116);
            this.btnApplyToAll.Name = "btnApplyToAll";
            this.btnApplyToAll.Size = new System.Drawing.Size(235, 22);
            this.btnApplyToAll.StyleController = this.layoutControl1;
            this.btnApplyToAll.TabIndex = 9;
            this.btnApplyToAll.Text = "Apply to all branches";
            this.btnApplyToAll.Click += new System.EventHandler(this.btnApplyToAll_Click);
            // 
            // btnApplyToSelected
            // 
            this.btnApplyToSelected.ImageOptions.Image = global::NSRetail.Properties.Resources.apply_16x161;
            this.btnApplyToSelected.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnApplyToSelected.Location = new System.Drawing.Point(28, 116);
            this.btnApplyToSelected.Name = "btnApplyToSelected";
            this.btnApplyToSelected.Size = new System.Drawing.Size(260, 22);
            this.btnApplyToSelected.StyleController = this.layoutControl1;
            this.btnApplyToSelected.TabIndex = 8;
            this.btnApplyToSelected.Text = "Apply to selected branches";
            this.btnApplyToSelected.Click += new System.EventHandler(this.btnApplyToSelected_Click);
            // 
            // txtDesiredQty
            // 
            this.txtDesiredQty.EditValue = "0";
            this.txtDesiredQty.Location = new System.Drawing.Point(489, 82);
            this.txtDesiredQty.Name = "txtDesiredQty";
            this.txtDesiredQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtDesiredQty.Properties.MaskSettings.Set("mask", "d");
            this.txtDesiredQty.Properties.UseMaskAsDisplayFormat = true;
            this.txtDesiredQty.Size = new System.Drawing.Size(289, 22);
            this.txtDesiredQty.StyleController = this.layoutControl1;
            this.txtDesiredQty.TabIndex = 7;
            // 
            // txtThreshold
            // 
            this.txtThreshold.EditValue = "0";
            this.txtThreshold.Location = new System.Drawing.Point(107, 82);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtThreshold.Properties.MaskSettings.Set("mask", "d");
            this.txtThreshold.Properties.UseMaskAsDisplayFormat = true;
            this.txtThreshold.Size = new System.Drawing.Size(291, 22);
            this.txtThreshold.StyleController = this.layoutControl1;
            this.txtThreshold.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.layoutControlItem7,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(806, 694);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 33);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(786, 113);
            this.layoutControlGroup2.Text = "Apply values";
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtThreshold;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(382, 34);
            this.layoutControlItem3.Text = "Threshold";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(67, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtDesiredQty;
            this.layoutControlItem4.Location = new System.Drawing.Point(382, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(380, 34);
            this.layoutControlItem4.Text = "Desired Qty.";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(67, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnApplyToSelected;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(272, 34);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnApplyToAll;
            this.layoutControlItem6.Location = new System.Drawing.Point(272, 34);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(247, 34);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(519, 34);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.emptySpaceItem1.Size = new System.Drawing.Size(74, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSave;
            this.layoutControlItem8.Location = new System.Drawing.Point(593, 34);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem8.Size = new System.Drawing.Size(169, 34);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.gcIndentLimits;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 146);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(786, 528);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem9.Control = this.lblItemName;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.OptionsPrint.AppearanceItem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.layoutControlItem9.OptionsPrint.AppearanceItem.Options.UseBorderColor = true;
            this.layoutControlItem9.OptionsPrint.AppearanceItemText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.layoutControlItem9.OptionsPrint.AppearanceItemText.Options.UseBorderColor = true;
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem9.Size = new System.Drawing.Size(786, 33);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // frmMBQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 694);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmMBQ";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Min. base quanty for Indent";
            this.Load += new System.EventHandler(this.frmMBQ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcIndentLimits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvIndentLimits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesiredQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThreshold.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtThreshold;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gcIndentLimits;
        private DevExpress.XtraGrid.Views.Grid.GridView gvIndentLimits;
        private DevExpress.XtraEditors.SimpleButton btnApplyToAll;
        private DevExpress.XtraEditors.SimpleButton btnApplyToSelected;
        private DevExpress.XtraEditors.TextEdit txtDesiredQty;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.LabelControl lblItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
    }
}