namespace NSRetailPOS.UI
{
    partial class frmPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayment));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkIsDoorDelivery = new DevExpress.XtraEditors.CheckEdit();
            this.rgPaymentOptions = new DevExpress.XtraEditors.RadioGroup();
            this.txtRemainingAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtPaidAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtBilledAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtItemQuantity = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.gcMOP = new DevExpress.XtraGrid.GridControl();
            this.gvMOP = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtCustomerName = new DevExpress.XtraEditors.TextEdit();
            this.txtMobileNo = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDoorDelivery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgPaymentOptions.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemainingAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBilledAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMOP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.Control.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.Control.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.ControlDisabled.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.ControlDropDown.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.ControlDropDownHeader.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.ControlFocused.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseFont = true;
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.layoutControl1.Appearance.DisabledLayoutGroupCaption.Options.UseFont = true;
            this.layoutControl1.Controls.Add(this.chkIsDoorDelivery);
            this.layoutControl1.Controls.Add(this.rgPaymentOptions);
            this.layoutControl1.Controls.Add(this.txtRemainingAmount);
            this.layoutControl1.Controls.Add(this.txtPaidAmount);
            this.layoutControl1.Controls.Add(this.txtBilledAmount);
            this.layoutControl1.Controls.Add(this.txtItemQuantity);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOk);
            this.layoutControl1.Controls.Add(this.gcMOP);
            this.layoutControl1.Controls.Add(this.txtCustomerName);
            this.layoutControl1.Controls.Add(this.txtMobileNo);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1071, 613);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkIsDoorDelivery
            // 
            this.chkIsDoorDelivery.Enabled = false;
            this.chkIsDoorDelivery.EnterMoveNextControl = true;
            this.chkIsDoorDelivery.Location = new System.Drawing.Point(137, 33);
            this.chkIsDoorDelivery.Name = "chkIsDoorDelivery";
            this.chkIsDoorDelivery.Properties.Caption = "";
            this.chkIsDoorDelivery.Size = new System.Drawing.Size(35, 18);
            this.chkIsDoorDelivery.StyleController = this.layoutControl1;
            this.chkIsDoorDelivery.TabIndex = 0;
            // 
            // rgPaymentOptions
            // 
            this.rgPaymentOptions.Enabled = false;
            this.rgPaymentOptions.EnterMoveNextControl = true;
            this.rgPaymentOptions.Location = new System.Drawing.Point(132, 185);
            this.rgPaymentOptions.MaximumSize = new System.Drawing.Size(0, 35);
            this.rgPaymentOptions.Name = "rgPaymentOptions";
            this.rgPaymentOptions.Size = new System.Drawing.Size(766, 35);
            this.rgPaymentOptions.StyleController = this.layoutControl1;
            this.rgPaymentOptions.TabIndex = 7;
            // 
            // txtRemainingAmount
            // 
            this.txtRemainingAmount.Enabled = false;
            this.txtRemainingAmount.Location = new System.Drawing.Point(665, 140);
            this.txtRemainingAmount.Name = "txtRemainingAmount";
            this.txtRemainingAmount.Properties.Appearance.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemainingAmount.Properties.Appearance.Options.UseFont = true;
            this.txtRemainingAmount.Size = new System.Drawing.Size(394, 30);
            this.txtRemainingAmount.StyleController = this.layoutControl1;
            this.txtRemainingAmount.TabIndex = 6;
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Enabled = false;
            this.txtPaidAmount.Location = new System.Drawing.Point(137, 140);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Properties.Appearance.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaidAmount.Properties.Appearance.Options.UseFont = true;
            this.txtPaidAmount.Size = new System.Drawing.Size(393, 30);
            this.txtPaidAmount.StyleController = this.layoutControl1;
            this.txtPaidAmount.TabIndex = 5;
            // 
            // txtBilledAmount
            // 
            this.txtBilledAmount.Enabled = false;
            this.txtBilledAmount.Location = new System.Drawing.Point(665, 100);
            this.txtBilledAmount.Name = "txtBilledAmount";
            this.txtBilledAmount.Properties.Appearance.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBilledAmount.Properties.Appearance.Options.UseFont = true;
            this.txtBilledAmount.Size = new System.Drawing.Size(394, 30);
            this.txtBilledAmount.StyleController = this.layoutControl1;
            this.txtBilledAmount.TabIndex = 4;
            // 
            // txtItemQuantity
            // 
            this.txtItemQuantity.Enabled = false;
            this.txtItemQuantity.Location = new System.Drawing.Point(137, 100);
            this.txtItemQuantity.Name = "txtItemQuantity";
            this.txtItemQuantity.Properties.Appearance.Font = new System.Drawing.Font("Arial Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemQuantity.Properties.Appearance.Options.UseFont = true;
            this.txtItemQuantity.Size = new System.Drawing.Size(393, 30);
            this.txtItemQuantity.StyleController = this.layoutControl1;
            this.txtItemQuantity.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(974, 584);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.Appearance.Font = new System.Drawing.Font("Arial", 10F);
            this.btnOk.Appearance.Options.UseFont = true;
            this.btnOk.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.ImageOptions.Image")));
            this.btnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnOk.Location = new System.Drawing.Point(870, 584);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 22);
            this.btnOk.StyleController = this.layoutControl1;
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // gcMOP
            // 
            this.gcMOP.Location = new System.Drawing.Point(4, 227);
            this.gcMOP.MainView = this.gvMOP;
            this.gcMOP.Name = "gcMOP";
            this.gcMOP.Size = new System.Drawing.Size(1063, 350);
            this.gcMOP.TabIndex = 8;
            this.gcMOP.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMOP});
            // 
            // gvMOP
            // 
            this.gvMOP.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMOP.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F);
            this.gvMOP.Appearance.Row.Options.UseFont = true;
            this.gvMOP.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gvMOP.GridControl = this.gcMOP;
            this.gvMOP.Name = "gvMOP";
            this.gvMOP.OptionsCustomization.AllowSort = false;
            this.gvMOP.OptionsView.ShowGroupPanel = false;
            this.gvMOP.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvMOP_ShowingEditor);
            this.gvMOP.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvMOP_CellValueChanged);
            this.gvMOP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gvMOP_KeyDown);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "MOPID";
            this.gridColumn1.FieldName = "MOPID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Mode of payment";
            this.gridColumn2.FieldName = "MOPNAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Paid Amount";
            this.gridColumn3.FieldName = "MOPVALUE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "MOPVALUE", "{0:0.##}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Enabled = false;
            this.txtCustomerName.EnterMoveNextControl = true;
            this.txtCustomerName.Location = new System.Drawing.Point(664, 33);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(395, 26);
            this.txtCustomerName.StyleController = this.layoutControl1;
            this.txtCustomerName.TabIndex = 2;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Enabled = false;
            this.txtMobileNo.EnterMoveNextControl = true;
            this.txtMobileNo.Location = new System.Drawing.Point(307, 33);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(222, 26);
            this.txtMobileNo.StyleController = this.layoutControl1;
            this.txtMobileNo.TabIndex = 1;
            // 
            // Root
            // 
            this.Root.AppearanceGroup.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.Root.AppearanceGroup.Options.UseFont = true;
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 9F);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.layoutControlItem7,
            this.layoutControlGroup1,
            this.layoutControlGroup2,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(1071, 613);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcMOP;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 223);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1067, 354);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnCancel;
            this.layoutControlItem8.Location = new System.Drawing.Point(967, 577);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(100, 32);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(100, 32);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem8.Size = new System.Drawing.Size(100, 32);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 577);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(863, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnOk;
            this.layoutControlItem7.Location = new System.Drawing.Point(863, 577);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(104, 32);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(104, 32);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(104, 32);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 67);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1067, 111);
            this.layoutControlGroup1.Text = "Bill Details";
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.txtItemQuantity;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem12.Size = new System.Drawing.Size(528, 40);
            this.layoutControlItem12.Text = "Item Quantity";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.txtBilledAmount;
            this.layoutControlItem13.Location = new System.Drawing.Point(528, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem13.Size = new System.Drawing.Size(529, 40);
            this.layoutControlItem13.Text = "Bill Amount";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtPaidAmount;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(528, 40);
            this.layoutControlItem3.Text = "Paid Amount";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtRemainingAmount;
            this.layoutControlItem4.Location = new System.Drawing.Point(528, 40);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(529, 40);
            this.layoutControlItem4.Text = "Remaining Amount";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem11});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1067, 67);
            this.layoutControlGroup2.Text = "Customer Details";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtMobileNo;
            this.layoutControlItem1.Location = new System.Drawing.Point(170, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(357, 36);
            this.layoutControlItem1.Text = "Customer Mobile No";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtCustomerName;
            this.layoutControlItem2.Location = new System.Drawing.Point(527, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(530, 36);
            this.layoutControlItem2.Text = "Customer Name";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.chkIsDoorDelivery;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem11.Size = new System.Drawing.Size(170, 36);
            this.layoutControlItem11.Text = "Door Delivery";
            this.layoutControlItem11.TextSize = new System.Drawing.Size(113, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.rgPaymentOptions;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 178);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(901, 45);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(901, 45);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(1067, 45);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Payment selection";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(113, 15);
            // 
            // frmPayment
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1071, 613);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Breakup ";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDoorDelivery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgPaymentOptions.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemainingAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaidAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBilledAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMOP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.GridControl gcMOP;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMOP;
        private DevExpress.XtraEditors.TextEdit txtCustomerName;
        private DevExpress.XtraEditors.TextEdit txtMobileNo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.TextEdit txtBilledAmount;
        private DevExpress.XtraEditors.TextEdit txtItemQuantity;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.TextEdit txtRemainingAmount;
        private DevExpress.XtraEditors.TextEdit txtPaidAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.RadioGroup rgPaymentOptions;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.CheckEdit chkIsDoorDelivery;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
    }
}