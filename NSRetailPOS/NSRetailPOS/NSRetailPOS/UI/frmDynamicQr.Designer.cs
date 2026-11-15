namespace NSRetailPOS.UI
{
    partial class frmDynamicQr
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator2 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.layout = new DevExpress.XtraLayout.LayoutControl();
            this.barcode = new DevExpress.XtraEditors.BarCodeControl();
            this.order = new DevExpress.XtraEditors.TextEdit();
            this.button = new DevExpress.XtraEditors.SimpleButton();
            this.root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.qr = new DevExpress.XtraLayout.LayoutControlItem();
            this.orderItem = new DevExpress.XtraLayout.LayoutControlItem();
            this.cancelItem = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layout)).BeginInit();
            this.layout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.order.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cancelItem)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.Controls.Add(this.barcode);
            this.layout.Controls.Add(this.order);
            this.layout.Controls.Add(this.button);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Root = this.root;
            this.layout.Size = new System.Drawing.Size(377, 433);
            this.layout.TabIndex = 0;
            // 
            // barcode
            // 
            this.barcode.Appearance.BackColor = System.Drawing.Color.White;
            this.barcode.Appearance.ForeColor = System.Drawing.Color.Black;
            this.barcode.Appearance.Options.UseBackColor = true;
            this.barcode.Appearance.Options.UseForeColor = true;
            this.barcode.AutoModule = true;
            this.barcode.Location = new System.Drawing.Point(12, 12);
            this.barcode.Name = "barcode";
            this.barcode.Padding = new System.Windows.Forms.Padding(40);
            this.barcode.ShowText = false;
            this.barcode.Size = new System.Drawing.Size(353, 346);
            this.barcode.StyleController = this.layout;
            qrCodeGenerator2.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator2.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.Version1;
            this.barcode.Symbology = qrCodeGenerator2;
            this.barcode.TabIndex = 0;
            // 
            // order
            // 
            this.order.Location = new System.Drawing.Point(56, 362);
            this.order.Name = "order";
            this.order.Properties.ReadOnly = true;
            this.order.Size = new System.Drawing.Size(309, 22);
            this.order.StyleController = this.layout;
            this.order.TabIndex = 1;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(12, 390);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(353, 28);
            this.button.StyleController = this.layout;
            this.button.TabIndex = 2;
            this.button.Text = "Cancel Payment";
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // root
            // 
            this.root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.root.GroupBordersVisible = false;
            this.root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.qr,
            this.orderItem,
            this.cancelItem});
            this.root.Name = "root";
            this.root.Size = new System.Drawing.Size(377, 433);
            this.root.TextVisible = false;
            // 
            // qr
            // 
            this.qr.Control = this.barcode;
            this.qr.Location = new System.Drawing.Point(0, 0);
            this.qr.MaxSize = new System.Drawing.Size(0, 350);
            this.qr.MinSize = new System.Drawing.Size(350, 350);
            this.qr.Name = "qr";
            this.qr.Size = new System.Drawing.Size(357, 350);
            this.qr.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.qr.TextSize = new System.Drawing.Size(0, 0);
            this.qr.TextVisible = false;
            // 
            // orderItem
            // 
            this.orderItem.Control = this.order;
            this.orderItem.Location = new System.Drawing.Point(0, 350);
            this.orderItem.MaxSize = new System.Drawing.Size(0, 28);
            this.orderItem.MinSize = new System.Drawing.Size(1, 28);
            this.orderItem.Name = "orderItem";
            this.orderItem.Size = new System.Drawing.Size(357, 28);
            this.orderItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.orderItem.Text = "Order";
            this.orderItem.TextSize = new System.Drawing.Size(32, 15);
            // 
            // cancelItem
            // 
            this.cancelItem.Control = this.button;
            this.cancelItem.Location = new System.Drawing.Point(0, 378);
            this.cancelItem.MaxSize = new System.Drawing.Size(0, 32);
            this.cancelItem.MinSize = new System.Drawing.Size(1, 32);
            this.cancelItem.Name = "cancelItem";
            this.cancelItem.Size = new System.Drawing.Size(357, 35);
            this.cancelItem.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.cancelItem.TextSize = new System.Drawing.Size(0, 0);
            this.cancelItem.TextVisible = false;
            // 
            // frmDynamicQr
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(377, 433);
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDynamicQr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scan to Pay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDynamicQr_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.layout)).EndInit();
            this.layout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.order.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cancelItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layout;
        private DevExpress.XtraEditors.BarCodeControl barcode;
        private DevExpress.XtraEditors.TextEdit order;
        private DevExpress.XtraEditors.SimpleButton button;
        private DevExpress.XtraLayout.LayoutControlGroup root;
        private DevExpress.XtraLayout.LayoutControlItem qr;
        private DevExpress.XtraLayout.LayoutControlItem orderItem;
        private DevExpress.XtraLayout.LayoutControlItem cancelItem;
    }
}
