namespace NSRetail
{
    partial class frmItemAdditionalFields
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmItemAdditionalFields));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.lblItemName = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.txtSKUCode = new DevExpress.XtraEditors.TextEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtProductRank = new DevExpress.XtraEditors.TextEdit();
            this.txtOuterCaseQty = new DevExpress.XtraEditors.TextEdit();
            this.txtInnerCaseQty = new DevExpress.XtraEditors.TextEdit();
            this.cmbSupplierIndentType = new DevExpress.XtraEditors.LookUpEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSKUCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductRank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOuterCaseQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInnerCaseQty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplierIndentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Location = new System.Drawing.Point(136, 65);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(7, 7);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(18, 18);
            this.Root.TextVisible = false;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.AutoSize = true;
            this.dataLayoutControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.dataLayoutControl1.Controls.Add(this.txtSKUCode);
            this.dataLayoutControl1.Controls.Add(this.lblItemName);
            this.dataLayoutControl1.Controls.Add(this.btnCancel);
            this.dataLayoutControl1.Controls.Add(this.btnSave);
            this.dataLayoutControl1.Controls.Add(this.txtProductRank);
            this.dataLayoutControl1.Controls.Add(this.txtOuterCaseQty);
            this.dataLayoutControl1.Controls.Add(this.txtInnerCaseQty);
            this.dataLayoutControl1.Controls.Add(this.cmbSupplierIndentType);
            this.dataLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.Root = this.layoutControlGroup1;
            this.dataLayoutControl1.Size = new System.Drawing.Size(456, 243);
            this.dataLayoutControl1.TabIndex = 1;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // lblItemName
            // 
            this.lblItemName.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblItemName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblItemName.Appearance.Options.UseFont = true;
            this.lblItemName.Appearance.Options.UseForeColor = true;
            this.lblItemName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblItemName.Location = new System.Drawing.Point(177, 15);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(102, 19);
            this.lblItemName.StyleController = this.dataLayoutControl1;
            this.lblItemName.TabIndex = 10;
            this.lblItemName.Text = "labelControl1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(456, 243);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.ContentHorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlItem7.Control = this.lblItemName;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem7.Size = new System.Drawing.Size(438, 31);
            this.layoutControlItem7.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 191);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(192, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // txtSKUCode
            // 
            this.txtSKUCode.EnterMoveNextControl = true;
            this.txtSKUCode.Location = new System.Drawing.Point(136, 45);
            this.txtSKUCode.Name = "txtSKUCode";
            this.txtSKUCode.Properties.ReadOnly = true;
            this.txtSKUCode.Size = new System.Drawing.Size(306, 22);
            this.txtSKUCode.StyleController = this.dataLayoutControl1;
            this.txtSKUCode.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = global::NSRetail.Properties.Resources.cancel_16x163;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(335, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 22);
            this.btnCancel.StyleController = this.dataLayoutControl1;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.Location = new System.Drawing.Point(206, 205);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 22);
            this.btnSave.StyleController = this.dataLayoutControl1;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtProductRank
            // 
            this.txtProductRank.Location = new System.Drawing.Point(136, 173);
            this.txtProductRank.Name = "txtProductRank";
            this.txtProductRank.Size = new System.Drawing.Size(306, 22);
            this.txtProductRank.StyleController = this.dataLayoutControl1;
            this.txtProductRank.TabIndex = 7;
            // 
            // txtOuterCaseQty
            // 
            this.txtOuterCaseQty.Location = new System.Drawing.Point(136, 141);
            this.txtOuterCaseQty.Name = "txtOuterCaseQty";
            this.txtOuterCaseQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtOuterCaseQty.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtOuterCaseQty.Properties.MaskSettings.Set("mask", "###");
            this.txtOuterCaseQty.Size = new System.Drawing.Size(306, 22);
            this.txtOuterCaseQty.StyleController = this.dataLayoutControl1;
            this.txtOuterCaseQty.TabIndex = 6;
            // 
            // txtInnerCaseQty
            // 
            this.txtInnerCaseQty.Location = new System.Drawing.Point(136, 109);
            this.txtInnerCaseQty.Name = "txtInnerCaseQty";
            this.txtInnerCaseQty.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.txtInnerCaseQty.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.txtInnerCaseQty.Properties.MaskSettings.Set("mask", "###");
            this.txtInnerCaseQty.Size = new System.Drawing.Size(306, 22);
            this.txtInnerCaseQty.StyleController = this.dataLayoutControl1;
            this.txtInnerCaseQty.TabIndex = 5;
            // 
            // cmbSupplierIndentType
            // 
            this.cmbSupplierIndentType.Location = new System.Drawing.Point(136, 77);
            this.cmbSupplierIndentType.Name = "cmbSupplierIndentType";
            this.cmbSupplierIndentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSupplierIndentType.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ENUMID", "ENUMID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ENUMVALUE", "Supplier Indent Item Type")});
            this.cmbSupplierIndentType.Properties.NullText = "";
            this.cmbSupplierIndentType.Size = new System.Drawing.Size(306, 22);
            this.cmbSupplierIndentType.StyleController = this.dataLayoutControl1;
            this.cmbSupplierIndentType.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbSupplierIndentType;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 63);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(438, 32);
            this.layoutControlItem1.Text = "Supplier Indent Type";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(112, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtInnerCaseQty;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 95);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(438, 32);
            this.layoutControlItem2.Text = "Inner Case Qty";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(112, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtOuterCaseQty;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 127);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(438, 32);
            this.layoutControlItem3.Text = "Outer Case Qty";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(112, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.txtProductRank;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 159);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(438, 32);
            this.layoutControlItem4.Text = "Product Rank";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(112, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            this.layoutControlItem5.Location = new System.Drawing.Point(192, 191);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(129, 32);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(129, 32);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(129, 34);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.Location = new System.Drawing.Point(321, 191);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(117, 32);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(117, 32);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(117, 34);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.txtSKUCode;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem8.Size = new System.Drawing.Size(438, 32);
            this.layoutControlItem8.Text = "SKU Code";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(112, 15);
            // 
            // frmItemAdditionalFields
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(456, 243);
            this.Controls.Add(this.dataLayoutControl1);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmItemAdditionalFields";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Additional Fields";
            this.Load += new System.EventHandler(this.frmItemAdditionalFields_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            this.dataLayoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSKUCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProductRank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOuterCaseQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInnerCaseQty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSupplierIndentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtProductRank;
        private DevExpress.XtraEditors.TextEdit txtOuterCaseQty;
        private DevExpress.XtraEditors.TextEdit txtInnerCaseQty;
        private DevExpress.XtraEditors.LookUpEdit cmbSupplierIndentType;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtSKUCode;
        private DevExpress.XtraEditors.LabelControl lblItemName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}