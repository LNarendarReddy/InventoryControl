namespace NSRetail.Countning
{
    partial class frmSliceCountingAddItems
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnRandom = new DevExpress.XtraEditors.SimpleButton();
            this.lblSelectedRowCount = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gcAvailableItems = new DevExpress.XtraGrid.GridControl();
            this.gvAvailableItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtTopRows = new DevExpress.XtraEditors.SpinEdit();
            this.txtSelectRandom = new DevExpress.XtraEditors.SpinEdit();
            this.cmbCriteria = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtTopSellingSaleDays = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSaleDays = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciOpenItems = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            this.lblTotalRowCount = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcAvailableItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAvailableItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopRows.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectRandom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCriteria.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopSellingSaleDays.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSaleDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOpenItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblTotalRowCount);
            this.layoutControl1.Controls.Add(this.btnRandom);
            this.layoutControl1.Controls.Add(this.lblSelectedRowCount);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnAdd);
            this.layoutControl1.Controls.Add(this.gcAvailableItems);
            this.layoutControl1.Controls.Add(this.txtTopRows);
            this.layoutControl1.Controls.Add(this.txtSelectRandom);
            this.layoutControl1.Controls.Add(this.cmbCriteria);
            this.layoutControl1.Controls.Add(this.btnSearch);
            this.layoutControl1.Controls.Add(this.txtTopSellingSaleDays);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(997, 552);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnRandom
            // 
            this.btnRandom.ImageOptions.Image = global::NSRetail.Properties.Resources.number_16x16;
            this.btnRandom.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnRandom.Location = new System.Drawing.Point(375, 502);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(111, 22);
            this.btnRandom.StyleController = this.layoutControl1;
            this.btnRandom.TabIndex = 13;
            this.btnRandom.Text = "Random select";
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // lblSelectedRowCount
            // 
            this.lblSelectedRowCount.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblSelectedRowCount.Appearance.Options.UseFont = true;
            this.lblSelectedRowCount.Location = new System.Drawing.Point(778, 484);
            this.lblSelectedRowCount.Name = "lblSelectedRowCount";
            this.lblSelectedRowCount.Size = new System.Drawing.Size(138, 16);
            this.lblSelectedRowCount.StyleController = this.layoutControl1;
            this.lblSelectedRowCount.TabIndex = 10;
            this.lblSelectedRowCount.Text = "lblSelectedRowCount";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ImageOptions.Image = global::NSRetail.Properties.Resources.delete_16x16;
            this.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClose.Location = new System.Drawing.Point(887, 508);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            // 
            // btnAdd
            // 
            this.btnAdd.ImageOptions.Image = global::NSRetail.Properties.Resources.add_16x161;
            this.btnAdd.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnAdd.Location = new System.Drawing.Point(782, 508);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(93, 22);
            this.btnAdd.StyleController = this.layoutControl1;
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gcAvailableItems
            // 
            this.gcAvailableItems.Location = new System.Drawing.Point(12, 91);
            this.gcAvailableItems.MainView = this.gvAvailableItems;
            this.gcAvailableItems.Name = "gcAvailableItems";
            this.gcAvailableItems.Size = new System.Drawing.Size(973, 370);
            this.gcAvailableItems.TabIndex = 4;
            this.gcAvailableItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAvailableItems});
            // 
            // gvAvailableItems
            // 
            this.gvAvailableItems.GridControl = this.gcAvailableItems;
            this.gvAvailableItems.Name = "gvAvailableItems";
            this.gvAvailableItems.OptionsSelection.MultiSelect = true;
            this.gvAvailableItems.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvAvailableItems.OptionsView.ShowGroupPanel = false;
            this.gvAvailableItems.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gvAvailableItems_MasterRowExpanded);
            this.gvAvailableItems.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvAvailableItems_SelectionChanged);
            this.gvAvailableItems.ColumnFilterChanged += new System.EventHandler(this.gvAvailableItems_ColumnFilterChanged);
            // 
            // txtTopRows
            // 
            this.txtTopRows.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTopRows.Location = new System.Drawing.Point(288, 502);
            this.txtTopRows.Name = "txtTopRows";
            this.txtTopRows.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtTopRows.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtTopRows.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtTopRows.Properties.MaskSettings.Set("mask", "d");
            this.txtTopRows.Properties.UseMaskAsDisplayFormat = true;
            this.txtTopRows.Size = new System.Drawing.Size(75, 22);
            this.txtTopRows.StyleController = this.layoutControl1;
            this.txtTopRows.TabIndex = 12;
            // 
            // txtSelectRandom
            // 
            this.txtSelectRandom.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtSelectRandom.Location = new System.Drawing.Point(125, 502);
            this.txtSelectRandom.Name = "txtSelectRandom";
            this.txtSelectRandom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSelectRandom.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.txtSelectRandom.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtSelectRandom.Properties.MaskSettings.Set("mask", "d");
            this.txtSelectRandom.Properties.UseMaskAsDisplayFormat = true;
            this.txtSelectRandom.Size = new System.Drawing.Size(54, 22);
            this.txtSelectRandom.StyleController = this.layoutControl1;
            this.txtSelectRandom.TabIndex = 11;
            // 
            // cmbCriteria
            // 
            this.cmbCriteria.Location = new System.Drawing.Point(125, 49);
            this.cmbCriteria.Name = "cmbCriteria";
            this.cmbCriteria.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCriteria.Properties.NullText = "";
            this.cmbCriteria.Properties.PopupSizeable = false;
            this.cmbCriteria.Size = new System.Drawing.Size(547, 22);
            this.cmbCriteria.StyleController = this.layoutControl1;
            this.cmbCriteria.TabIndex = 5;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Mandatory";
            this.dxValidationProvider1.SetValidationRule(this.cmbCriteria, conditionValidationRule2);
            this.cmbCriteria.EditValueChanged += new System.EventHandler(this.cmbCriteria_EditValueChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.ImageOptions.Image = global::NSRetail.Properties.Resources.zoom_16x16;
            this.btnSearch.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSearch.Location = new System.Drawing.Point(855, 49);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(114, 22);
            this.btnSearch.StyleController = this.layoutControl1;
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTopSellingSaleDays
            // 
            this.txtTopSellingSaleDays.Location = new System.Drawing.Point(781, 49);
            this.txtTopSellingSaleDays.Name = "txtTopSellingSaleDays";
            this.txtTopSellingSaleDays.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtTopSellingSaleDays.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtTopSellingSaleDays.Properties.MaskSettings.Set("mask", "d");
            this.txtTopSellingSaleDays.Properties.UseMaskAsDisplayFormat = true;
            this.txtTopSellingSaleDays.Size = new System.Drawing.Size(62, 22);
            this.txtTopSellingSaleDays.StyleController = this.layoutControl1;
            this.txtTopSellingSaleDays.TabIndex = 6;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlItem4,
            this.emptySpaceItem3,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(997, 552);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcAvailableItems;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 79);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(977, 374);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnAdd;
            this.layoutControlItem2.Location = new System.Drawing.Point(766, 492);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(105, 34);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(105, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(105, 40);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnClose;
            this.layoutControlItem3.Location = new System.Drawing.Point(871, 492);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(106, 34);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(106, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(106, 40);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.lciSaleDays,
            this.lciOpenItems});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(977, 79);
            this.layoutControlGroup1.Text = "Search criteria";
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.cmbCriteria;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem6.CustomizationFormText = "Criteria";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem6.Size = new System.Drawing.Size(656, 34);
            this.layoutControlItem6.Text = "Criteria";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(85, 15);
            // 
            // lciSaleDays
            // 
            this.lciSaleDays.Control = this.txtTopSellingSaleDays;
            this.lciSaleDays.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lciSaleDays.CustomizationFormText = "Sale days";
            this.lciSaleDays.Location = new System.Drawing.Point(656, 0);
            this.lciSaleDays.MaxSize = new System.Drawing.Size(171, 0);
            this.lciSaleDays.MinSize = new System.Drawing.Size(171, 34);
            this.lciSaleDays.Name = "lciSaleDays";
            this.lciSaleDays.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciSaleDays.Size = new System.Drawing.Size(171, 34);
            this.lciSaleDays.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciSaleDays.Text = "Sale days";
            this.lciSaleDays.TextSize = new System.Drawing.Size(85, 15);
            // 
            // lciOpenItems
            // 
            this.lciOpenItems.Control = this.btnSearch;
            this.lciOpenItems.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.lciOpenItems.CustomizationFormText = "lciOpenItems";
            this.lciOpenItems.Location = new System.Drawing.Point(827, 0);
            this.lciOpenItems.MaxSize = new System.Drawing.Size(126, 34);
            this.lciOpenItems.MinSize = new System.Drawing.Size(126, 34);
            this.lciOpenItems.Name = "lciOpenItems";
            this.lciOpenItems.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciOpenItems.Size = new System.Drawing.Size(126, 34);
            this.lciOpenItems.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciOpenItems.TextSize = new System.Drawing.Size(0, 0);
            this.lciOpenItems.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 453);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(494, 79);
            this.layoutControlGroup2.Text = "Random selection";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.txtSelectRandom;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(163, 0);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(163, 25);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(163, 34);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Select Random";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(85, 15);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.txtTopRows;
            this.layoutControlItem7.Location = new System.Drawing.Point(163, 0);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(184, 0);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(184, 25);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(184, 34);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "of top rows";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(85, 15);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnRandom;
            this.layoutControlItem8.Location = new System.Drawing.Point(347, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(123, 34);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(123, 34);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem8.Size = new System.Drawing.Size(123, 34);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.ContentVertAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControlItem4.Control = this.lblSelectedRowCount;
            this.layoutControlItem4.Location = new System.Drawing.Point(766, 472);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(211, 20);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.Location = new System.Drawing.Point(494, 453);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(272, 79);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblTotalRowCount
            // 
            this.lblTotalRowCount.Location = new System.Drawing.Point(778, 465);
            this.lblTotalRowCount.Name = "lblTotalRowCount";
            this.lblTotalRowCount.Size = new System.Drawing.Size(99, 15);
            this.lblTotalRowCount.StyleController = this.layoutControl1;
            this.lblTotalRowCount.TabIndex = 14;
            this.lblTotalRowCount.Text = "lblTotalRowCount";
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lblTotalRowCount;
            this.layoutControlItem9.Location = new System.Drawing.Point(766, 453);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(211, 19);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // frmSliceCountingAddItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(997, 552);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmSliceCountingAddItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmSliceCountingAddItems_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcAvailableItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAvailableItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopRows.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectRandom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCriteria.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTopSellingSaleDays.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSaleDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciOpenItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraGrid.GridControl gcAvailableItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAvailableItems;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.LabelControl lblSelectedRowCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnRandom;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SpinEdit txtTopRows;
        private DevExpress.XtraEditors.SpinEdit txtSelectRandom;
        private DevExpress.XtraEditors.LookUpEdit cmbCriteria;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem lciOpenItems;
        private DevExpress.XtraEditors.TextEdit txtTopSellingSaleDays;
        private DevExpress.XtraLayout.LayoutControlItem lciSaleDays;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.LabelControl lblTotalRowCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}
