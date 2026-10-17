namespace NSRetail.Master
{
    partial class frmCounter
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCounter));
            this.mainLayout = new DevExpress.XtraLayout.LayoutControl();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCounterName = new DevExpress.XtraEditors.TextEdit();
            this.chkIsMobileCounter = new DevExpress.XtraEditors.CheckEdit();
            this.gcMappings = new DevExpress.XtraGrid.GridControl();
            this.gvMappings = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAvailable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.availableEditor = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colMop = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConfigurable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGateway = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gatewayEditor = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.txtSetting1 = new DevExpress.XtraEditors.TextEdit();
            this.txtSetting2 = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.counterSection = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCounterName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciMobileCounter = new DevExpress.XtraLayout.LayoutControlItem();
            this.mopSection = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciMappings = new DevExpress.XtraLayout.LayoutControlItem();
            this.gatewaySection = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSetting1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSetting2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.buttonSpacer = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lciSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCancel = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).BeginInit();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounterName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMobileCounter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.availableEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gatewayEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCounterName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMobileCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mopSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMappings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gatewaySection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSpacer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.Controls.Add(this.cmbBranch);
            this.mainLayout.Controls.Add(this.txtCounterName);
            this.mainLayout.Controls.Add(this.chkIsMobileCounter);
            this.mainLayout.Controls.Add(this.gcMappings);
            this.mainLayout.Controls.Add(this.txtSetting1);
            this.mainLayout.Controls.Add(this.txtSetting2);
            this.mainLayout.Controls.Add(this.btnSave);
            this.mainLayout.Controls.Add(this.btnCancel);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Root = this.Root;
            this.mainLayout.Size = new System.Drawing.Size(820, 620);
            this.mainLayout.TabIndex = 0;
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(129, 40);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "Branch"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHCODE", "Code")});
            this.cmbBranch.Properties.NullText = "";
            this.cmbBranch.Size = new System.Drawing.Size(672, 22);
            this.cmbBranch.StyleController = this.mainLayout;
            this.cmbBranch.TabIndex = 0;
            // 
            // txtCounterName
            // 
            this.txtCounterName.EnterMoveNextControl = true;
            this.txtCounterName.Location = new System.Drawing.Point(129, 66);
            this.txtCounterName.Name = "txtCounterName";
            this.txtCounterName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCounterName.Properties.MaxLength = 50;
            this.txtCounterName.Size = new System.Drawing.Size(491, 22);
            this.txtCounterName.StyleController = this.mainLayout;
            this.txtCounterName.TabIndex = 1;
            // 
            // chkIsMobileCounter
            // 
            this.chkIsMobileCounter.Location = new System.Drawing.Point(624, 66);
            this.chkIsMobileCounter.Name = "chkIsMobileCounter";
            this.chkIsMobileCounter.Properties.Caption = "Is Mobile Counter";
            this.chkIsMobileCounter.Size = new System.Drawing.Size(177, 19);
            this.chkIsMobileCounter.StyleController = this.mainLayout;
            this.chkIsMobileCounter.TabIndex = 2;
            // 
            // gcMappings
            // 
            this.gcMappings.Location = new System.Drawing.Point(13, 125);
            this.gcMappings.MainView = this.gvMappings;
            this.gcMappings.Name = "gcMappings";
            this.gcMappings.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.availableEditor,
            this.gatewayEditor});
            this.gcMappings.Size = new System.Drawing.Size(794, 355);
            this.gcMappings.TabIndex = 3;
            this.gcMappings.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMappings});
            // 
            // gvMappings
            // 
            this.gvMappings.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAvailable,
            this.colMop,
            this.colConfigurable,
            this.colGateway});
            this.gvMappings.GridControl = this.gcMappings;
            this.gvMappings.Name = "gvMappings";
            this.gvMappings.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvMappings.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvMappings.OptionsView.ShowGroupPanel = false;
            this.gvMappings.OptionsView.ShowIndicator = false;
            this.gvMappings.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvMappings_RowCellStyle);
            this.gvMappings.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gvMappings_CustomRowCellEditForEditing);
            this.gvMappings.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvMappings_ShowingEditor);
            this.gvMappings.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvMappings_FocusedRowChanged);
            this.gvMappings.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMappings_CellValueChanged);
            // 
            // colAvailable
            // 
            this.colAvailable.Caption = "Available";
            this.colAvailable.ColumnEdit = this.availableEditor;
            this.colAvailable.FieldName = "ISAVAILABLE";
            this.colAvailable.Name = "colAvailable";
            this.colAvailable.OptionsColumn.FixedWidth = true;
            this.colAvailable.Visible = true;
            this.colAvailable.VisibleIndex = 0;
            // 
            // availableEditor
            // 
            this.availableEditor.Name = "availableEditor";
            this.availableEditor.EditValueChanged += new System.EventHandler(this.availableEditor_EditValueChanged);
            // 
            // colMop
            // 
            this.colMop.Caption = "MOP";
            this.colMop.FieldName = "MOPNAME";
            this.colMop.Name = "colMop";
            this.colMop.OptionsColumn.AllowEdit = false;
            this.colMop.Visible = true;
            this.colMop.VisibleIndex = 1;
            this.colMop.Width = 150;
            // 
            // colConfigurable
            // 
            this.colConfigurable.Caption = "Gateway Configurable";
            this.colConfigurable.FieldName = "ISGATEWAYCONFIGURABLE";
            this.colConfigurable.MinWidth = 145;
            this.colConfigurable.Name = "colConfigurable";
            this.colConfigurable.OptionsColumn.AllowEdit = false;
            this.colConfigurable.Visible = true;
            this.colConfigurable.VisibleIndex = 2;
            this.colConfigurable.Width = 145;
            // 
            // colGateway
            // 
            this.colGateway.Caption = "Payment Gateway";
            this.colGateway.ColumnEdit = this.gatewayEditor;
            this.colGateway.FieldName = "PAYMENTGATEWAYINFOID";
            this.colGateway.Name = "colGateway";
            this.colGateway.Visible = true;
            this.colGateway.VisibleIndex = 3;
            this.colGateway.Width = 350;
            // 
            // gatewayEditor
            // 
            this.gatewayEditor.Name = "gatewayEditor";
            // 
            // txtSetting1
            // 
            this.txtSetting1.Location = new System.Drawing.Point(219, 517);
            this.txtSetting1.Name = "txtSetting1";
            this.txtSetting1.Size = new System.Drawing.Size(582, 22);
            this.txtSetting1.StyleController = this.mainLayout;
            this.txtSetting1.TabIndex = 4;
            this.txtSetting1.EditValueChanged += new System.EventHandler(this.gatewaySetting_EditValueChanged);
            // 
            // txtSetting2
            // 
            this.txtSetting2.Location = new System.Drawing.Point(219, 543);
            this.txtSetting2.Name = "txtSetting2";
            this.txtSetting2.Size = new System.Drawing.Size(582, 22);
            this.txtSetting2.StyleController = this.mainLayout;
            this.txtSetting2.TabIndex = 5;
            this.txtSetting2.EditValueChanged += new System.EventHandler(this.gatewaySetting_EditValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Location = new System.Drawing.Point(598, 578);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 32);
            this.btnSave.StyleController = this.mainLayout;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(706, 578);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104, 32);
            this.btnCancel.StyleController = this.mainLayout;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.counterSection,
            this.mopSection,
            this.gatewaySection,
            this.buttonSpacer,
            this.lciSave,
            this.lciCancel});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 8, 8, 8);
            this.Root.Size = new System.Drawing.Size(820, 620);
            this.Root.TextVisible = false;
            // 
            // counterSection
            // 
            this.counterSection.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciBranch,
            this.lciCounterName,
            this.lciMobileCounter});
            this.counterSection.Location = new System.Drawing.Point(0, 0);
            this.counterSection.Name = "counterSection";
            this.counterSection.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.counterSection.Size = new System.Drawing.Size(804, 91);
            this.counterSection.Text = "Counter Information";
            // 
            // lciBranch
            // 
            this.lciBranch.Control = this.cmbBranch;
            this.lciBranch.Location = new System.Drawing.Point(0, 0);
            this.lciBranch.Name = "lciBranch";
            this.lciBranch.Size = new System.Drawing.Size(786, 26);
            this.lciBranch.Text = "Branch";
            this.lciBranch.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciBranch.TextSize = new System.Drawing.Size(105, 16);
            this.lciBranch.TextToControlDistance = 5;
            // 
            // lciCounterName
            // 
            this.lciCounterName.Control = this.txtCounterName;
            this.lciCounterName.Location = new System.Drawing.Point(0, 26);
            this.lciCounterName.Name = "lciCounterName";
            this.lciCounterName.Size = new System.Drawing.Size(605, 26);
            this.lciCounterName.Text = "Counter Name";
            this.lciCounterName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciCounterName.TextSize = new System.Drawing.Size(105, 16);
            this.lciCounterName.TextToControlDistance = 5;
            // 
            // lciMobileCounter
            // 
            this.lciMobileCounter.Control = this.chkIsMobileCounter;
            this.lciMobileCounter.Location = new System.Drawing.Point(605, 26);
            this.lciMobileCounter.Name = "lciMobileCounter";
            this.lciMobileCounter.Size = new System.Drawing.Size(181, 26);
            this.lciMobileCounter.TextSize = new System.Drawing.Size(0, 0);
            this.lciMobileCounter.TextVisible = false;
            // 
            // mopSection
            // 
            this.mopSection.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciMappings});
            this.mopSection.Location = new System.Drawing.Point(0, 91);
            this.mopSection.Name = "mopSection";
            this.mopSection.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.mopSection.Size = new System.Drawing.Size(804, 386);
            this.mopSection.Text = "MOP Payment Gateway Mappings";
            // 
            // lciMappings
            // 
            this.lciMappings.Control = this.gcMappings;
            this.lciMappings.Location = new System.Drawing.Point(0, 0);
            this.lciMappings.Name = "lciMappings";
            this.lciMappings.Size = new System.Drawing.Size(798, 359);
            this.lciMappings.TextSize = new System.Drawing.Size(0, 0);
            this.lciMappings.TextVisible = false;
            // 
            // gatewaySection
            // 
            this.gatewaySection.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSetting1,
            this.lciSetting2});
            this.gatewaySection.Location = new System.Drawing.Point(0, 477);
            this.gatewaySection.Name = "gatewaySection";
            this.gatewaySection.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.gatewaySection.Size = new System.Drawing.Size(804, 91);
            this.gatewaySection.Text = "Payment Gateway Configuration";
            // 
            // lciSetting1
            // 
            this.lciSetting1.Control = this.txtSetting1;
            this.lciSetting1.Location = new System.Drawing.Point(0, 0);
            this.lciSetting1.Name = "lciSetting1";
            this.lciSetting1.Size = new System.Drawing.Size(786, 26);
            this.lciSetting1.Text = "Setting 1";
            this.lciSetting1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciSetting1.TextSize = new System.Drawing.Size(195, 16);
            this.lciSetting1.TextToControlDistance = 5;
            // 
            // lciSetting2
            // 
            this.lciSetting2.Control = this.txtSetting2;
            this.lciSetting2.Location = new System.Drawing.Point(0, 26);
            this.lciSetting2.Name = "lciSetting2";
            this.lciSetting2.Size = new System.Drawing.Size(786, 26);
            this.lciSetting2.Text = "Setting 2";
            this.lciSetting2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lciSetting2.TextSize = new System.Drawing.Size(195, 16);
            this.lciSetting2.TextToControlDistance = 5;
            // 
            // buttonSpacer
            // 
            this.buttonSpacer.AllowHotTrack = false;
            this.buttonSpacer.Location = new System.Drawing.Point(0, 568);
            this.buttonSpacer.Name = "buttonSpacer";
            this.buttonSpacer.Size = new System.Drawing.Size(588, 36);
            this.buttonSpacer.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lciSave
            // 
            this.lciSave.Control = this.btnSave;
            this.lciSave.Location = new System.Drawing.Point(588, 568);
            this.lciSave.MaxSize = new System.Drawing.Size(108, 36);
            this.lciSave.MinSize = new System.Drawing.Size(108, 36);
            this.lciSave.Name = "lciSave";
            this.lciSave.Size = new System.Drawing.Size(108, 36);
            this.lciSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciSave.TextSize = new System.Drawing.Size(0, 0);
            this.lciSave.TextVisible = false;
            // 
            // lciCancel
            // 
            this.lciCancel.Control = this.btnCancel;
            this.lciCancel.Location = new System.Drawing.Point(696, 568);
            this.lciCancel.MaxSize = new System.Drawing.Size(108, 36);
            this.lciCancel.MinSize = new System.Drawing.Size(108, 36);
            this.lciCancel.Name = "lciCancel";
            this.lciCancel.Size = new System.Drawing.Size(108, 36);
            this.lciCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lciCancel.TextVisible = false;
            // 
            // frmCounter
            // 
            this.AcceptButton = this.btnSave;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(820, 620);
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.MinimumSize = new System.Drawing.Size(780, 580);
            this.Name = "frmCounter";
            this.Text = "Counter";
            this.Load += new System.EventHandler(this.frmCounter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).EndInit();
            this.mainLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounterName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMobileCounter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.availableEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gatewayEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.counterSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCounterName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMobileCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mopSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMappings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gatewaySection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonSpacer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCancel)).EndInit();
            this.ResumeLayout(false);

        }

        private DevExpress.XtraLayout.LayoutControl mainLayout;
        private DevExpress.XtraLayout.LayoutControlGroup Root, counterSection, mopSection, gatewaySection;
        private DevExpress.XtraLayout.LayoutControlItem lciBranch, lciCounterName, lciMobileCounter, lciMappings;
        private DevExpress.XtraLayout.LayoutControlItem lciSetting1, lciSetting2, lciSave, lciCancel;
        private DevExpress.XtraLayout.EmptySpaceItem buttonSpacer;
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraEditors.TextEdit txtCounterName, txtSetting1, txtSetting2;
        private DevExpress.XtraEditors.CheckEdit chkIsMobileCounter;
        private DevExpress.XtraEditors.SimpleButton btnSave, btnCancel;
        private DevExpress.XtraGrid.GridControl gcMappings;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMappings;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailable, colMop, colConfigurable, colGateway;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit availableEditor;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit gatewayEditor;
    }
}
