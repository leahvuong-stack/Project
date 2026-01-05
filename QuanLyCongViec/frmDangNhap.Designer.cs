namespace QuanLyCongViec
{
    partial class frmDangNhap
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.linklblDangKy = new System.Windows.Forms.LinkLabel();
            this.linklblQuenMK = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHintTaiKhoan = new System.Windows.Forms.Label();
            this.lblHintMatKhau = new System.Windows.Forms.Label();
            this.chkGhiNhoDangNhap = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaiKhoan.Location = new System.Drawing.Point(42, 37);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(79, 19);
            this.lblTaiKhoan.TabIndex = 0;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.Location = new System.Drawing.Point(42, 87);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(76, 19);
            this.lblMatKhau.TabIndex = 0;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Location = new System.Drawing.Point(133, 34);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(250, 22);
            this.txtTaiKhoan.TabIndex = 1;
            this.txtTaiKhoan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaiKhoan_KeyDown);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(133, 84);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(250, 22);
            this.txtMatKhau.TabIndex = 2;
            this.txtMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatKhau_KeyDown);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.Location = new System.Drawing.Point(133, 162);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(250, 41);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "Đăng nhập";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // linklblDangKy
            // 
            this.linklblDangKy.AutoSize = true;
            this.linklblDangKy.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklblDangKy.Location = new System.Drawing.Point(275, 217);
            this.linklblDangKy.Name = "linklblDangKy";
            this.linklblDangKy.Size = new System.Drawing.Size(64, 19);
            this.linklblDangKy.TabIndex = 4;
            this.linklblDangKy.TabStop = true;
            this.linklblDangKy.Text = "Đăng ký";
            this.linklblDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblDangKy_LinkClicked);
            // 
            // linklblQuenMK
            // 
            this.linklblQuenMK.AutoSize = true;
            this.linklblQuenMK.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklblQuenMK.Location = new System.Drawing.Point(133, 247);
            this.linklblQuenMK.Name = "linklblQuenMK";
            this.linklblQuenMK.Size = new System.Drawing.Size(116, 19);
            this.linklblQuenMK.TabIndex = 6;
            this.linklblQuenMK.TabStop = true;
            this.linklblQuenMK.Text = "Quên mật khẩu?";
            this.linklblQuenMK.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblQuenMK_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 217);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chưa có tài khoản?";
            // 
            // lblHintTaiKhoan
            // 
            this.lblHintTaiKhoan.AutoSize = true;
            this.lblHintTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintTaiKhoan.ForeColor = System.Drawing.Color.Gray;
            this.lblHintTaiKhoan.Location = new System.Drawing.Point(133, 59);
            this.lblHintTaiKhoan.Name = "lblHintTaiKhoan";
            this.lblHintTaiKhoan.Size = new System.Drawing.Size(192, 17);
            this.lblHintTaiKhoan.TabIndex = 5;
            this.lblHintTaiKhoan.Text = "Nhập tên đăng nhập của bạn";
            // 
            // lblHintMatKhau
            // 
            this.lblHintMatKhau.AutoSize = true;
            this.lblHintMatKhau.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintMatKhau.ForeColor = System.Drawing.Color.Gray;
            this.lblHintMatKhau.Location = new System.Drawing.Point(133, 109);
            this.lblHintMatKhau.Name = "lblHintMatKhau";
            this.lblHintMatKhau.Size = new System.Drawing.Size(216, 17);
            this.lblHintMatKhau.TabIndex = 5;
            this.lblHintMatKhau.Text = "Nhập mật khẩu (tối thiểu 6 ký tự)";
            // 
            // chkGhiNhoDangNhap
            // 
            this.chkGhiNhoDangNhap.AutoSize = true;
            this.chkGhiNhoDangNhap.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGhiNhoDangNhap.Location = new System.Drawing.Point(133, 132);
            this.chkGhiNhoDangNhap.Name = "chkGhiNhoDangNhap";
            this.chkGhiNhoDangNhap.Size = new System.Drawing.Size(156, 23);
            this.chkGhiNhoDangNhap.TabIndex = 3;
            this.chkGhiNhoDangNhap.Text = "Ghi nhớ đăng nhập";
            this.chkGhiNhoDangNhap.UseVisualStyleBackColor = true;
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 290);
            this.Controls.Add(this.linklblQuenMK);
            this.Controls.Add(this.chkGhiNhoDangNhap);
            this.Controls.Add(this.lblHintMatKhau);
            this.Controls.Add(this.lblHintTaiKhoan);
            this.Controls.Add(this.linklblDangKy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTaiKhoan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.lblTaiKhoan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Ứng dụng Quản lý Công việc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.LinkLabel linklblDangKy;
        private System.Windows.Forms.LinkLabel linklblQuenMK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHintTaiKhoan;
        private System.Windows.Forms.Label lblHintMatKhau;
        private System.Windows.Forms.CheckBox chkGhiNhoDangNhap;
    }
}

