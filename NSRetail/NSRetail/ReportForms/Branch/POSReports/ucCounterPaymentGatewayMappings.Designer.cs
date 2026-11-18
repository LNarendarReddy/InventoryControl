namespace NSRetail.ReportForms.Branch.POSReports
{
    partial class ucCounterPaymentGatewayMappings
    {
        private void InitializeComponent()
        {
            this.criteriaLayout = new DevExpress.XtraLayout.LayoutControl();
            this.cmbBranch = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.rootGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.branchItem = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.criteriaLayout)).BeginInit();
            this.criteriaLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchItem)).BeginInit();
            this.SuspendLayout();
            // 
            // criteriaLayout
            // 
            this.criteriaLayout.Controls.Add(this.cmbBranch);
            this.criteriaLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.criteriaLayout.Location = new System.Drawing.Point(0, 0);
            this.criteriaLayout.Name = "criteriaLayout";
            this.criteriaLayout.Root = this.rootGroup;
            this.criteriaLayout.Size = new System.Drawing.Size(777, 45);
            this.criteriaLayout.TabIndex = 0;
            // 
            // cmbBranch
            // 
            this.cmbBranch.EditValue = "";
            this.cmbBranch.Location = new System.Drawing.Point(62, 11);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBranch.Size = new System.Drawing.Size(704, 22);
            this.cmbBranch.StyleController = this.criteriaLayout;
            this.cmbBranch.TabIndex = 0;
            // 
            // rootGroup
            // 
            this.rootGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.rootGroup.GroupBordersVisible = false;
            this.rootGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.branchItem});
            this.rootGroup.Name = "rootGroup";
            this.rootGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.rootGroup.Size = new System.Drawing.Size(777, 45);
            // 
            // branchItem
            // 
            this.branchItem.Control = this.cmbBranch;
            this.branchItem.Location = new System.Drawing.Point(0, 0);
            this.branchItem.Name = "branchItem";
            this.branchItem.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.branchItem.Size = new System.Drawing.Size(767, 35);
            this.branchItem.Text = "Branch";
            this.branchItem.TextSize = new System.Drawing.Size(39, 15);
            // 
            // ucCounterPaymentGatewayMappings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.criteriaLayout);
            this.Name = "ucCounterPaymentGatewayMappings";
            this.Size = new System.Drawing.Size(777, 45);
            ((System.ComponentModel.ISupportInitialize)(this.criteriaLayout)).EndInit();
            this.criteriaLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rootGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.branchItem)).EndInit();
            this.ResumeLayout(false);

        }

        private DevExpress.XtraLayout.LayoutControl criteriaLayout;
        private DevExpress.XtraEditors.CheckedComboBoxEdit cmbBranch;
        private DevExpress.XtraLayout.LayoutControlGroup rootGroup;
        private DevExpress.XtraLayout.LayoutControlItem branchItem;
    }
}
