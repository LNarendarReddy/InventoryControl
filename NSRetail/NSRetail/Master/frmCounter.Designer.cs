
namespace NSRetail.Master
{
    partial class frmCounter
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCounter));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.luPaymentGateway = new DevExpress.XtraEditors.LookUpEdit();
            this.txtSetting2 = new DevExpress.XtraEditors.TextEdit();
            this.txtSetting1 = new DevExpress.XtraEditors.TextEdit();
            this.chkIsMobileCounter = new DevExpress.XtraEditors.CheckEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtCounterName = new DevExpress.XtraEditors.TextEdit();
            this.cmbBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgPaymentGatewayConfig = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciSetting1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSetting2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.luPaymentGateway.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMobileCounter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounterName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPaymentGatewayConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Appearance.Control.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.Control.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDisabled.Font = new System.Drawing.Font("Arial", 8F);
            this.layoutControl1.Appearance.ControlDisabled.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDropDown.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.ControlDropDown.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlDropDownHeader.Font = new System.Drawing.Font("Arial", 8F);
            this.layoutControl1.Appearance.ControlDropDownHeader.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlFocused.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControl1.Appearance.ControlFocused.Options.UseFont = true;
            this.layoutControl1.Appearance.ControlReadOnly.Font = new System.Drawing.Font("Arial", 8F);
            this.layoutControl1.Appearance.ControlReadOnly.Options.UseFont = true;
            this.layoutControl1.Controls.Add(this.luPaymentGateway);
            this.layoutControl1.Controls.Add(this.txtSetting2);
            this.layoutControl1.Controls.Add(this.txtSetting1);
            this.layoutControl1.Controls.Add(this.chkIsMobileCounter);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtCounterName);
            this.layoutControl1.Controls.Add(this.cmbBranch);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(680, 34, 650, 400);
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(453, 253);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // luPaymentGateway
            // 
            this.luPaymentGateway.EnterMoveNextControl = true;
            this.luPaymentGateway.Location = new System.Drawing.Point(127, 76);
            this.luPaymentGateway.Name = "luPaymentGateway";
            this.luPaymentGateway.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.luPaymentGateway.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PAYMENTGATEWAYINFOID", "PAYMENTGATEWAYINFOID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PAYMENTGATEWAYINFONAME", "Name"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("PAYMENTGATEWAYINFOTYPE", "Type")});
            this.luPaymentGateway.Properties.NullText = "";
            this.luPaymentGateway.Size = new System.Drawing.Size(318, 22);
            this.luPaymentGateway.StyleController = this.layoutControl1;
            this.luPaymentGateway.TabIndex = 2;
            this.luPaymentGateway.EditValueChanged += new System.EventHandler(this.luPaymentGateway_Leave);
            this.luPaymentGateway.Leave += new System.EventHandler(this.luPaymentGateway_Leave);
            // 
            // txtSetting2
            // 
            this.txtSetting2.EnterMoveNextControl = true;
            this.txtSetting2.Location = new System.Drawing.Point(139, 177);
            this.txtSetting2.Name = "txtSetting2";
            this.txtSetting2.Size = new System.Drawing.Size(294, 22);
            this.txtSetting2.StyleController = this.layoutControl1;
            this.txtSetting2.TabIndex = 4;
            // 
            // txtSetting1
            // 
            this.txtSetting1.EnterMoveNextControl = true;
            this.txtSetting1.Location = new System.Drawing.Point(139, 143);
            this.txtSetting1.Name = "txtSetting1";
            this.txtSetting1.Size = new System.Drawing.Size(294, 22);
            this.txtSetting1.StyleController = this.layoutControl1;
            this.txtSetting1.TabIndex = 3;
            // 
            // chkIsMobileCounter
            // 
            this.chkIsMobileCounter.EnterMoveNextControl = true;
            this.chkIsMobileCounter.Location = new System.Drawing.Point(8, 223);
            this.chkIsMobileCounter.Name = "chkIsMobileCounter";
            this.chkIsMobileCounter.Properties.Caption = "Is Mobile Counter";
            this.chkIsMobileCounter.Size = new System.Drawing.Size(128, 20);
            this.chkIsMobileCounter.StyleController = this.layoutControl1;
            this.chkIsMobileCounter.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(361, 223);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(259, 223);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCounterName
            // 
            this.txtCounterName.EnterMoveNextControl = true;
            this.txtCounterName.Location = new System.Drawing.Point(127, 42);
            this.txtCounterName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCounterName.Name = "txtCounterName";
            this.txtCounterName.Size = new System.Drawing.Size(318, 22);
            this.txtCounterName.StyleController = this.layoutControl1;
            this.txtCounterName.TabIndex = 1;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Mandatory";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.txtCounterName, conditionValidationRule1);
            // 
            // cmbBranch
            // 
            this.cmbBranch.EnterMoveNextControl = true;
            this.cmbBranch.Location = new System.Drawing.Point(127, 8);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHID", "BRANCHID", 23, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHNAME", "BRANCHNAME", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("BRANCHCODE", "BRANCHCODE", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.cmbBranch.Properties.NullText = "";
            this.cmbBranch.Size = new System.Drawing.Size(318, 22);
            this.cmbBranch.StyleController = this.layoutControl1;
            this.cmbBranch.TabIndex = 0;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "Mandatory";
            conditionValidationRule2.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider1.SetValidationRule(this.cmbBranch, conditionValidationRule2);
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 10F);
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.lcgPaymentGatewayConfig,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.Root.Size = new System.Drawing.Size(453, 253);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 10F);
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.txtCounterName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(449, 34);
            this.layoutControlItem1.Text = "Counter Name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(107, 16);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            this.layoutControlItem7.Location = new System.Drawing.Point(251, 215);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(102, 34);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnCancel;
            this.layoutControlItem8.Location = new System.Drawing.Point(353, 215);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem8.Size = new System.Drawing.Size(96, 34);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(140, 215);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(111, 34);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkIsMobileCounter;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 215);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(140, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbBranch;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(449, 34);
            this.layoutControlItem2.Text = "Branch";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(107, 16);
            // 
            // lcgPaymentGatewayConfig
            // 
            this.lcgPaymentGatewayConfig.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciSetting1,
            this.lciSetting2});
            this.lcgPaymentGatewayConfig.Location = new System.Drawing.Point(0, 102);
            this.lcgPaymentGatewayConfig.Name = "lcgPaymentGatewayConfig";
            this.lcgPaymentGatewayConfig.Size = new System.Drawing.Size(449, 113);
            this.lcgPaymentGatewayConfig.Text = "PineLabs Payment Gateway configuration";
            // 
            // lciSetting1
            // 
            this.lciSetting1.Control = this.txtSetting1;
            this.lciSetting1.Location = new System.Drawing.Point(0, 0);
            this.lciSetting1.Name = "lciSetting1";
            this.lciSetting1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciSetting1.Size = new System.Drawing.Size(425, 34);
            this.lciSetting1.Text = "Store ID";
            this.lciSetting1.TextSize = new System.Drawing.Size(107, 16);
            // 
            // lciSetting2
            // 
            this.lciSetting2.Control = this.txtSetting2;
            this.lciSetting2.Location = new System.Drawing.Point(0, 34);
            this.lciSetting2.Name = "lciSetting2";
            this.lciSetting2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.lciSetting2.Size = new System.Drawing.Size(425, 34);
            this.lciSetting2.Text = "Clent ID";
            this.lciSetting2.TextSize = new System.Drawing.Size(107, 16);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.luPaymentGateway;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(449, 34);
            this.layoutControlItem4.Text = "Payment Gateway";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(107, 16);
            // 
            // frmCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(453, 253);
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmCounter";
            this.Text = "Counter";
            this.Load += new System.EventHandler(this.frmCounter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.luPaymentGateway.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSetting1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMobileCounter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounterName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPaymentGatewayConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSetting2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtCounterName;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LookUpEdit cmbBranch;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
        private DevExpress.XtraEditors.CheckEdit chkIsMobileCounter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.TextEdit txtSetting1;
        private DevExpress.XtraLayout.LayoutControlItem lciSetting1;
        private DevExpress.XtraEditors.TextEdit txtSetting2;
        private DevExpress.XtraLayout.LayoutControlItem lciSetting2;
        private DevExpress.XtraLayout.LayoutControlGroup lcgPaymentGatewayConfig;
        private DevExpress.XtraEditors.LookUpEdit luPaymentGateway;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}