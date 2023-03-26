namespace NSRetail.Master
{
    partial class frmClearHDDSNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClearHDDSNo));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnClearHDDSNo = new DevExpress.XtraEditors.SimpleButton();
            this.txtReason = new DevExpress.XtraEditors.MemoEdit();
            this.txtCounterName = new DevExpress.XtraEditors.TextEdit();
            this.txtBranchName = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dxValidationProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounterName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranchName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnClearHDDSNo);
            this.layoutControl1.Controls.Add(this.txtReason);
            this.layoutControl1.Controls.Add(this.txtCounterName);
            this.layoutControl1.Controls.Add(this.txtBranchName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(330, 271);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.ImageOptions.Image")));
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.Location = new System.Drawing.Point(249, 233);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            // 
            // btnClearHDDSNo
            // 
            this.btnClearHDDSNo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClearHDDSNo.ImageOptions.Image")));
            this.btnClearHDDSNo.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnClearHDDSNo.Location = new System.Drawing.Point(141, 233);
            this.btnClearHDDSNo.Name = "btnClearHDDSNo";
            this.btnClearHDDSNo.Size = new System.Drawing.Size(96, 22);
            this.btnClearHDDSNo.StyleController = this.layoutControl1;
            this.btnClearHDDSNo.TabIndex = 7;
            this.btnClearHDDSNo.Text = "Clear HDD #";
            this.btnClearHDDSNo.Click += new System.EventHandler(this.btnClearHDDSNo_Click);
            // 
            // txtReason
            // 
            this.txtReason.EnterMoveNextControl = true;
            this.txtReason.Location = new System.Drawing.Point(104, 84);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(210, 137);
            this.txtReason.StyleController = this.layoutControl1;
            this.txtReason.TabIndex = 6;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "Value cannot be empty";
            this.dxValidationProvider1.SetValidationRule(this.txtReason, conditionValidationRule1);
            // 
            // txtCounterName
            // 
            this.txtCounterName.EnterMoveNextControl = true;
            this.txtCounterName.Location = new System.Drawing.Point(104, 50);
            this.txtCounterName.Name = "txtCounterName";
            this.txtCounterName.Properties.ReadOnly = true;
            this.txtCounterName.Size = new System.Drawing.Size(210, 22);
            this.txtCounterName.StyleController = this.layoutControl1;
            this.txtCounterName.TabIndex = 5;
            // 
            // txtBranchName
            // 
            this.txtBranchName.EnterMoveNextControl = true;
            this.txtBranchName.Location = new System.Drawing.Point(104, 16);
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Properties.ReadOnly = true;
            this.txtBranchName.Size = new System.Drawing.Size(210, 22);
            this.txtBranchName.StyleController = this.layoutControl1;
            this.txtBranchName.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(330, 271);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtBranchName;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem1.Size = new System.Drawing.Size(310, 34);
            this.layoutControlItem1.Text = "Branch Name";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 15);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 217);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(125, 34);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtCounterName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem2.Size = new System.Drawing.Size(310, 34);
            this.layoutControlItem2.Text = "Counter";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.txtReason;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem3.Size = new System.Drawing.Size(310, 149);
            this.layoutControlItem3.Text = "Reason";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(76, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClearHDDSNo;
            this.layoutControlItem4.Location = new System.Drawing.Point(125, 217);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem4.Size = new System.Drawing.Size(108, 34);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.Location = new System.Drawing.Point(233, 217);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutControlItem5.Size = new System.Drawing.Size(77, 34);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmClearHDDSNo
            // 
            this.AcceptButton = this.btnClearHDDSNo;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(330, 271);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmClearHDDSNo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clear HDD SNo";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCounterName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranchName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxValidationProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnClearHDDSNo;
        private DevExpress.XtraEditors.MemoEdit txtReason;
        private DevExpress.XtraEditors.TextEdit txtCounterName;
        private DevExpress.XtraEditors.TextEdit txtBranchName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider dxValidationProvider1;
    }
}