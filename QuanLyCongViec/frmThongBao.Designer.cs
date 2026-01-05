namespace QuanLyCongViec
{
    partial class frmThongBao
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvThongBao;
        private System.Windows.Forms.Button btnMarkAsRead;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvThongBao = new System.Windows.Forms.DataGridView();
            this.btnMarkAsRead = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongBao)).BeginInit();
            this.SuspendLayout();

            // Cấu hình DataGridView
            this.dgvThongBao.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.dgvThongBao.Location = new System.Drawing.Point(12, 12);
            this.dgvThongBao.Size = new System.Drawing.Size(960, 430);
            this.dgvThongBao.Name = "dgvThongBao";

            // btnMarkAsRead
            this.btnMarkAsRead.Location = new System.Drawing.Point(12, 455);
            this.btnMarkAsRead.Size = new System.Drawing.Size(150, 35);
            this.btnMarkAsRead.Text = "Đánh dấu hoàn thành";

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(168, 455);
            this.btnDelete.Size = new System.Drawing.Size(80, 35);
            this.btnDelete.Text = "Xóa";

            // btnReload
            this.btnReload.Location = new System.Drawing.Point(254, 455);
            this.btnReload.Size = new System.Drawing.Size(80, 35);
            this.btnReload.Text = "Tải lại";

            // btnClose
            this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.btnClose.Location = new System.Drawing.Point(892, 455);
            this.btnClose.Size = new System.Drawing.Size(80, 35);
            this.btnClose.Text = "Đóng";

            // frmThongBao
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 502);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvThongBao, this.btnMarkAsRead, this.btnDelete,
                this.btnReload, this.btnClose
            });
            this.Name = "frmThongBao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông Báo";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongBao)).EndInit();
            this.ResumeLayout(false);
        }
    }
}