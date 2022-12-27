namespace NSRetail
{
    partial class frmAddOffer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddOffer));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lbBranches = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.gcOffers = new DevExpress.XtraGrid.GridControl();
            this.gvOffers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkActiveOffer = new DevExpress.XtraEditors.CheckEdit();
            this.dtpEndDate = new DevExpress.XtraEditors.DateEdit();
            this.tpStartDate = new DevExpress.XtraEditors.DateEdit();
            this.txtOfferName = new DevExpress.XtraEditors.TextEdit();
            this.txtOfferCode = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbBranches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOffers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOffers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActiveOffer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOfferName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOfferCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.lbBranches);
            this.layoutControl1.Controls.Add(this.gcOffers);
            this.layoutControl1.Controls.Add(this.chkActiveOffer);
            this.layoutControl1.Controls.Add(this.dtpEndDate);
            this.layoutControl1.Controls.Add(this.tpStartDate);
            this.layoutControl1.Controls.Add(this.txtOfferName);
            this.layoutControl1.Controls.Add(this.txtOfferCode);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1270, 357, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1056, 618);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnCancel.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancel.ImageOptions.SvgImage")));
            this.btnCancel.Location = new System.Drawing.Point(139, 138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 36);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btnSave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSave.ImageOptions.SvgImage")));
            this.btnSave.Location = new System.Drawing.Point(12, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 36);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lbBranches
            // 
            this.lbBranches.Location = new System.Drawing.Point(378, 12);
            this.lbBranches.Name = "lbBranches";
            this.lbBranches.Size = new System.Drawing.Size(666, 162);
            this.lbBranches.StyleController = this.layoutControl1;
            this.lbBranches.TabIndex = 10;
            // 
            // gcOffers
            // 
            this.gcOffers.Location = new System.Drawing.Point(12, 178);
            this.gcOffers.MainView = this.gvOffers;
            this.gcOffers.Name = "gcOffers";
            this.gcOffers.Size = new System.Drawing.Size(1032, 428);
            this.gcOffers.TabIndex = 9;
            this.gcOffers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOffers});
            // 
            // gvOffers
            // 
            this.gvOffers.GridControl = this.gcOffers;
            this.gvOffers.Name = "gvOffers";
            this.gvOffers.OptionsView.ShowGroupPanel = false;
            // 
            // chkActiveOffer
            // 
            this.chkActiveOffer.Location = new System.Drawing.Point(87, 116);
            this.chkActiveOffer.Name = "chkActiveOffer";
            this.chkActiveOffer.Properties.Caption = "";
            this.chkActiveOffer.Size = new System.Drawing.Size(287, 18);
            this.chkActiveOffer.StyleController = this.layoutControl1;
            this.chkActiveOffer.TabIndex = 8;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = null;
            this.dtpEndDate.Location = new System.Drawing.Point(87, 90);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Size = new System.Drawing.Size(287, 22);
            this.dtpEndDate.StyleController = this.layoutControl1;
            this.dtpEndDate.TabIndex = 7;
            // 
            // tpStartDate
            // 
            this.tpStartDate.EditValue = null;
            this.tpStartDate.Location = new System.Drawing.Point(87, 64);
            this.tpStartDate.Name = "tpStartDate";
            this.tpStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tpStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tpStartDate.Size = new System.Drawing.Size(287, 22);
            this.tpStartDate.StyleController = this.layoutControl1;
            this.tpStartDate.TabIndex = 6;
            // 
            // txtOfferName
            // 
            this.txtOfferName.Location = new System.Drawing.Point(87, 38);
            this.txtOfferName.Name = "txtOfferName";
            this.txtOfferName.Size = new System.Drawing.Size(287, 22);
            this.txtOfferName.StyleController = this.layoutControl1;
            this.txtOfferName.TabIndex = 5;
            // 
            // txtOfferCode
            // 
            this.txtOfferCode.Location = new System.Drawing.Point(87, 12);
            this.txtOfferCode.Name = "txtOfferCode";
            this.txtOfferCode.Size = new System.Drawing.Size(287, 22);
            this.txtOfferCode.StyleController = this.layoutControl1;
            this.txtOfferCode.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.layoutControlItem10});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1056, 618);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.txtOfferCode;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(366, 26);
            this.layoutControlItem1.Text = "Offer Code";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.txtOfferName;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(366, 26);
            this.layoutControlItem2.Text = "Offer Name";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.tpStartDate;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(366, 26);
            this.layoutControlItem3.Text = "Start Date";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.dtpEndDate;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 78);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(366, 26);
            this.layoutControlItem4.Text = "End Date";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkActiveOffer;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 104);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(366, 22);
            this.layoutControlItem5.Text = "Active Offer";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(63, 15);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gcOffers;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 166);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(1036, 432);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lbBranches;
            this.layoutControlItem7.Location = new System.Drawing.Point(366, 0);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(670, 166);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnSave;
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 126);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(127, 40);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnCancel;
            this.layoutControlItem9.Location = new System.Drawing.Point(127, 126);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(113, 40);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextVisible = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.simpleButton1.Location = new System.Drawing.Point(252, 138);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(122, 36);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 13;
            this.simpleButton1.Text = "Import Excel";
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.simpleButton1;
            this.layoutControlItem10.Location = new System.Drawing.Point(240, 126);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(126, 40);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // frmAddOffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 618);
            this.Controls.Add(this.layoutControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "frmAddOffer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Offer";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbBranches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOffers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOffers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActiveOffer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tpStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOfferName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOfferCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.CheckEdit chkActiveOffer;
        private DevExpress.XtraEditors.DateEdit dtpEndDate;
        private DevExpress.XtraEditors.DateEdit tpStartDate;
        private DevExpress.XtraEditors.TextEdit txtOfferName;
        private DevExpress.XtraEditors.TextEdit txtOfferCode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.GridControl gcOffers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvOffers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.CheckedListBoxControl lbBranches;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
    }
}