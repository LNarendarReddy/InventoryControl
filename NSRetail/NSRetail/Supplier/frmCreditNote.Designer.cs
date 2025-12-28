namespace NSRetail.Supplier
{
    partial class frmCreditNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreditNote));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtCNValue = new DevExpress.XtraEditors.TextEdit();
            this.txtCNNumber = new DevExpress.XtraEditors.TextEdit();
            this.cmbPurchaseInvoiceNumber = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbCreditAdjustmentType = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider11 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCNValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCNNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPurchaseInvoiceNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreditAdjustmentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider11)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClear);
            this.layoutControl1.Controls.Add(this.txtDescription);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtCNValue);
            this.layoutControl1.Controls.Add(this.txtCNNumber);
            this.layoutControl1.Controls.Add(this.cmbPurchaseInvoiceNumber);
            this.layoutControl1.Controls.Add(this.cmbCreditAdjustmentType);
            this.layoutControl1.Controls.Add(this.cmbSupplier);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1236, 215, 650, 400);
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(593, 307);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(492, 79);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(86, 22);
            this.btnClear.StyleController = this.layoutControl1;
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.EnterMoveNextControl = true;
            this.txtDescription.Location = new System.Drawing.Point(170, 175);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Properties.MaxLength = 255;
            this.txtDescription.Size = new System.Drawing.Size(408, 85);
            this.txtDescription.StyleController = this.layoutControl1;
            this.txtDescription.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(468, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(348, 270);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCNValue
            // 
            this.txtCNValue.EnterMoveNextControl = true;
            this.txtCNValue.Location = new System.Drawing.Point(170, 143);
            this.txtCNValue.Name = "txtCNValue";
            this.txtCNValue.Size = new System.Drawing.Size(408, 22);
            this.txtCNValue.StyleController = this.layoutControl1;
            this.txtCNValue.TabIndex = 4;
            // 
            // txtCNNumber
            // 
            this.txtCNNumber.EnterMoveNextControl = true;
            this.txtCNNumber.Location = new System.Drawing.Point(170, 111);
            this.txtCNNumber.Name = "txtCNNumber";
            this.txtCNNumber.Size = new System.Drawing.Size(408, 22);
            this.txtCNNumber.StyleController = this.layoutControl1;
            this.txtCNNumber.TabIndex = 3;
            // 
            // cmbPurchaseInvoiceNumber
            // 
            this.cmbPurchaseInvoiceNumber.EnterMoveNextControl = true;
            this.cmbPurchaseInvoiceNumber.Location = new System.Drawing.Point(170, 79);
            this.cmbPurchaseInvoiceNumber.Name = "cmbPurchaseInvoiceNumber";
            this.cmbPurchaseInvoiceNumber.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPurchaseInvoiceNumber.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("StockEntryId", "StockEntryId", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SUPPLIERINVOICENO", "SUPPLIERINVOICENO")});
            this.cmbPurchaseInvoiceNumber.Properties.NullText = "";
            this.cmbPurchaseInvoiceNumber.Properties.ShowFooter = false;
            this.cmbPurchaseInvoiceNumber.Properties.ShowHeader = false;
            this.cmbPurchaseInvoiceNumber.Size = new System.Drawing.Size(312, 22);
            this.cmbPurchaseInvoiceNumber.StyleController = this.layoutControl1;
            this.cmbPurchaseInvoiceNumber.TabIndex = 2;
            this.cmbPurchaseInvoiceNumber.EditValueChanged += new System.EventHandler(this.cmbPurchaseInvoiceNumber_EditValueChanged);
            // 
            // cmbCreditAdjustmentType
            // 
            this.cmbCreditAdjustmentType.EnterMoveNextControl = true;
            this.cmbCreditAdjustmentType.Location = new System.Drawing.Point(170, 47);
            this.cmbCreditAdjustmentType.Name = "cmbCreditAdjustmentType";
            this.cmbCreditAdjustmentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCreditAdjustmentType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CreditNoteAdjustmentTypeId", "CreditNoteAdjustmentTypeId", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description")});
            this.cmbCreditAdjustmentType.Properties.NullText = "";
            this.cmbCreditAdjustmentType.Properties.ShowFooter = false;
            this.cmbCreditAdjustmentType.Properties.ShowHeader = false;
            this.cmbCreditAdjustmentType.Size = new System.Drawing.Size(408, 22);
            this.cmbCreditAdjustmentType.StyleController = this.layoutControl1;
            this.cmbCreditAdjustmentType.TabIndex = 1;
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.EnterMoveNextControl = true;
            this.cmbSupplier.Location = new System.Drawing.Point(170, 15);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSupplier.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERID", "DEALERID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("DEALERNAME", "DEALERNAME")});
            this.cmbSupplier.Properties.NullText = "";
            this.cmbSupplier.Properties.ShowHeader = false;
            this.cmbSupplier.Size = new System.Drawing.Size(408, 22);
            this.cmbSupplier.StyleController = this.layoutControl1;
            this.cmbSupplier.TabIndex = 0;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Mandatory";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
            this.dxValidationProvider11.SetValidationRule(this.cmbSupplier, conditionValidationRule1);
            this.cmbSupplier.EditValueChanged += new System.EventHandler(this.cmbSupplier_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem7,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(593, 307);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.cmbSupplier;
            this.layoutControlItem7.ControlAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.layoutControlItem7.CustomizationFormText = "Supplier";
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(573, 32);
            this.layoutControlItem7.Text = "Supplier";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(143, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cmbPurchaseInvoiceNumber;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(477, 32);
            this.layoutControlItem2.Text = "Purchase Invoice Number";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(143, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtCNNumber;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(573, 32);
            this.layoutControlItem3.Text = "CN Number";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(143, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtCNValue;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(573, 32);
            this.layoutControlItem4.Text = "CN Value";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(143, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            this.layoutControlItem5.Location = new System.Drawing.Point(333, 255);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(120, 32);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(120, 32);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(120, 32);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbCreditAdjustmentType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(573, 32);
            this.layoutControlItem1.Text = "Credit  Adjustment Type";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(143, 15);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.Location = new System.Drawing.Point(453, 255);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(120, 32);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(120, 32);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(120, 32);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 255);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(333, 32);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtDescription;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 160);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem8.Size = new System.Drawing.Size(573, 95);
            this.layoutControlItem8.Text = "Description";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(143, 15);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnClear;
            this.layoutControlItem9.Location = new System.Drawing.Point(477, 64);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem9.Size = new System.Drawing.Size(96, 32);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // frmCreditNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(593, 307);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmCreditNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Credit Note";
            this.Load += new System.EventHandler(this.frmCreditNote_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCNValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCNNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPurchaseInvoiceNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCreditAdjustmentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LookUpEdit cmbSupplier;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.LookUpEdit cmbCreditAdjustmentType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtCNValue;
        private DevExpress.XtraEditors.TextEdit txtCNNumber;
        private DevExpress.XtraEditors.LookUpEdit cmbPurchaseInvoiceNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
    }
}