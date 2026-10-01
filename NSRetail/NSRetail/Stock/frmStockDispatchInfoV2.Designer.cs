namespace NSRetail.Stock
{
    partial class frmStockDispatchInfoV2
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LabelControl lblHeading;
        private DevExpress.XtraEditors.LookUpEdit cmbFromBranch;
        private DevExpress.XtraEditors.LookUpEdit cmbToBranch;
        private DevExpress.XtraEditors.LookUpEdit cmbCategory;
        private DevExpress.XtraEditors.MemoEdit txtNotes;
        private DevExpress.XtraEditors.SimpleButton btnContinue;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutHeading;
        private DevExpress.XtraLayout.LayoutControlItem layoutFromBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutToBranch;
        private DevExpress.XtraLayout.LayoutControlItem layoutCategory;
        private DevExpress.XtraLayout.LayoutControlItem layoutNotes;
        private DevExpress.XtraLayout.EmptySpaceItem emptyButtons;
        private DevExpress.XtraLayout.LayoutControlItem layoutCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutContinue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockDispatchInfoV2));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblHeading = new DevExpress.XtraEditors.LabelControl();
            this.cmbFromBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbToBranch = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.btnContinue = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutHeading = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutFromBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutToBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutCategory = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutNotes = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptyButtons = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutContinue = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutHeading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFromBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutToBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyButtons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutContinue)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblHeading);
            this.layoutControl1.Controls.Add(this.cmbFromBranch);
            this.layoutControl1.Controls.Add(this.cmbToBranch);
            this.layoutControl1.Controls.Add(this.cmbCategory);
            this.layoutControl1.Controls.Add(this.txtNotes);
            this.layoutControl1.Controls.Add(this.btnContinue);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsFocus.EnableAutoTabOrder = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(560, 300);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // lblHeading
            // 
            this.lblHeading.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeading.Appearance.Options.UseFont = true;
            this.lblHeading.Location = new System.Drawing.Point(12, 12);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(536, 21);
            this.lblHeading.StyleController = this.layoutControl1;
            this.lblHeading.TabIndex = 0;
            // 
            // cmbFromBranch
            // 
            this.cmbFromBranch.EnterMoveNextControl = true;
            this.cmbFromBranch.Location = new System.Drawing.Point(99, 41);
            this.cmbFromBranch.Name = "cmbFromBranch";
            this.cmbFromBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFromBranch.Properties.NullText = "";
            this.cmbFromBranch.Size = new System.Drawing.Size(445, 22);
            this.cmbFromBranch.StyleController = this.layoutControl1;
            this.cmbFromBranch.TabIndex = 1;
            // 
            // cmbToBranch
            // 
            this.cmbToBranch.EnterMoveNextControl = true;
            this.cmbToBranch.Location = new System.Drawing.Point(99, 75);
            this.cmbToBranch.Name = "cmbToBranch";
            this.cmbToBranch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbToBranch.Properties.NullText = "";
            this.cmbToBranch.Size = new System.Drawing.Size(445, 22);
            this.cmbToBranch.StyleController = this.layoutControl1;
            this.cmbToBranch.TabIndex = 2;
            // 
            // cmbCategory
            // 
            this.cmbCategory.EnterMoveNextControl = true;
            this.cmbCategory.Location = new System.Drawing.Point(99, 109);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCategory.Properties.NullText = "";
            this.cmbCategory.Size = new System.Drawing.Size(445, 22);
            this.cmbCategory.StyleController = this.layoutControl1;
            this.cmbCategory.TabIndex = 3;
            // 
            // txtNotes
            // 
            this.txtNotes.EnterMoveNextControl = true;
            this.txtNotes.Location = new System.Drawing.Point(99, 143);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(445, 101);
            this.txtNotes.StyleController = this.layoutControl1;
            this.txtNotes.TabIndex = 4;
            // 
            // btnContinue
            // 
            this.btnContinue.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnContinue.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnContinue.ImageOptions.SvgImage")));
            this.btnContinue.Location = new System.Drawing.Point(428, 252);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(120, 36);
            this.btnContinue.StyleController = this.layoutControl1;
            this.btnContinue.TabIndex = 5;
            this.btnContinue.Text = "Continue";
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(304, 252);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutHeading,
            this.layoutFromBranch,
            this.layoutToBranch,
            this.layoutCategory,
            this.layoutNotes,
            this.emptyButtons,
            this.layoutCancel,
            this.layoutContinue});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(560, 300);
            this.Root.TextVisible = false;
            // 
            // layoutHeading
            // 
            this.layoutHeading.Control = this.lblHeading;
            this.layoutHeading.Location = new System.Drawing.Point(0, 0);
            this.layoutHeading.Name = "layoutHeading";
            this.layoutHeading.Size = new System.Drawing.Size(540, 25);
            this.layoutHeading.TextSize = new System.Drawing.Size(0, 0);
            this.layoutHeading.TextVisible = false;
            // 
            // layoutFromBranch
            // 
            this.layoutFromBranch.Control = this.cmbFromBranch;
            this.layoutFromBranch.Location = new System.Drawing.Point(0, 25);
            this.layoutFromBranch.Name = "layoutFromBranch";
            this.layoutFromBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutFromBranch.Size = new System.Drawing.Size(540, 34);
            this.layoutFromBranch.Text = "From Branch";
            this.layoutFromBranch.TextSize = new System.Drawing.Size(71, 15);
            // 
            // layoutToBranch
            // 
            this.layoutToBranch.Control = this.cmbToBranch;
            this.layoutToBranch.Location = new System.Drawing.Point(0, 59);
            this.layoutToBranch.Name = "layoutToBranch";
            this.layoutToBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutToBranch.Size = new System.Drawing.Size(540, 34);
            this.layoutToBranch.Text = "To Branch";
            this.layoutToBranch.TextSize = new System.Drawing.Size(71, 15);
            // 
            // layoutCategory
            // 
            this.layoutCategory.Control = this.cmbCategory;
            this.layoutCategory.Location = new System.Drawing.Point(0, 93);
            this.layoutCategory.Name = "layoutCategory";
            this.layoutCategory.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutCategory.Size = new System.Drawing.Size(540, 34);
            this.layoutCategory.Text = "Category";
            this.layoutCategory.TextSize = new System.Drawing.Size(71, 15);
            // 
            // layoutNotes
            // 
            this.layoutNotes.Control = this.txtNotes;
            this.layoutNotes.Location = new System.Drawing.Point(0, 127);
            this.layoutNotes.Name = "layoutNotes";
            this.layoutNotes.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            this.layoutNotes.Size = new System.Drawing.Size(540, 113);
            this.layoutNotes.Text = "Notes";
            this.layoutNotes.TextSize = new System.Drawing.Size(71, 15);
            // 
            // emptyButtons
            // 
            this.emptyButtons.AllowHotTrack = false;
            this.emptyButtons.Location = new System.Drawing.Point(0, 240);
            this.emptyButtons.Name = "emptyButtons";
            this.emptyButtons.Size = new System.Drawing.Size(292, 40);
            this.emptyButtons.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutCancel
            // 
            this.layoutCancel.Control = this.btnCancel;
            this.layoutCancel.Location = new System.Drawing.Point(292, 240);
            this.layoutCancel.Name = "layoutCancel";
            this.layoutCancel.Size = new System.Drawing.Size(124, 40);
            this.layoutCancel.TextSize = new System.Drawing.Size(0, 0);
            this.layoutCancel.TextVisible = false;
            // 
            // layoutContinue
            // 
            this.layoutContinue.Control = this.btnContinue;
            this.layoutContinue.Location = new System.Drawing.Point(416, 240);
            this.layoutContinue.Name = "layoutContinue";
            this.layoutContinue.Size = new System.Drawing.Size(124, 40);
            this.layoutContinue.TextSize = new System.Drawing.Size(0, 0);
            this.layoutContinue.TextVisible = false;
            // 
            // frmStockDispatchInfoV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 300);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStockDispatchInfoV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispatch Information";
            this.Load += new System.EventHandler(this.frmStockDispatchInfoV2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbFromBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbToBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutHeading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutFromBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutToBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptyButtons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutContinue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
