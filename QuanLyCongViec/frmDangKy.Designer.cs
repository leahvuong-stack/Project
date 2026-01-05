namespace QuanLyCongViec
{
    partial class frmDangKy
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
            this.linklblDangNhap = new System.Windows.Forms.LinkLabel();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblHintTaiKhoan = new System.Windows.Forms.Label();
            this.lblHintMatKhau = new System.Windows.Forms.Label();
            this.lblHintXacNhanMatKhau = new System.Windows.Forms.Label();
            this.lblHintHoTen = new System.Windows.Forms.Label();
            this.lblHintEmail = new System.Windows.Forms.Label();
            this.chkDongYDieuKhoan = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // linklblDangNhap
            // 
            this.linklblDangNhap.AutoSize = true;
            this.linklblDangNhap.Location = new System.Drawing.Point(245, 317);
            this.linklblDangNhap.Name = "linklblDangNhap";
            this.linklblDangNhap.Size = new System.Drawing.Size(72, 16);
            this.linklblDangNhap.TabIndex = 7;
            this.linklblDangNhap.TabStop = true;
            this.linklblDangNhap.Text = "Đăng nhập";
            this.linklblDangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblDangNhap_LinkClicked);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(105, 267);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(250, 41);
            this.btnXacNhan.TabIndex = 7;
            this.btnXacNhan.Text = "Đăng ký";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(139, 63);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(250, 22);
            this.txtMatKhau.TabIndex = 2;
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Location = new System.Drawing.Point(139, 23);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(250, 22);
            this.txtTaiKhoan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(105, 317);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đã có tài khoản?";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.Location = new System.Drawing.Point(50, 66);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(76, 19);
            this.lblMatKhau.TabIndex = 0;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaiKhoan.Location = new System.Drawing.Point(50, 26);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(79, 19);
            this.lblTaiKhoan.TabIndex = 0;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Xác nhận:";
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(139, 103);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.PasswordChar = '*';
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(250, 22);
            this.txtXacNhanMatKhau.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Họ tên:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(139, 143);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(250, 22);
            this.txtHoTen.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(139, 183);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 22);
            this.txtEmail.TabIndex = 5;
            // 
            // lblHintTaiKhoan
            // 
            this.lblHintTaiKhoan.AutoSize = true;
            this.lblHintTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintTaiKhoan.ForeColor = System.Drawing.Color.Gray;
            this.lblHintTaiKhoan.Location = new System.Drawing.Point(139, 48);
            this.lblHintTaiKhoan.Name = "lblHintTaiKhoan";
            this.lblHintTaiKhoan.Size = new System.Drawing.Size(229, 17);
            this.lblHintTaiKhoan.TabIndex = 8;
            this.lblHintTaiKhoan.Text = "3-50 ký tự, không có khoảng trống";
            // 
            // lblHintMatKhau
            // 
            this.lblHintMatKhau.AutoSize = true;
            this.lblHintMatKhau.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintMatKhau.ForeColor = System.Drawing.Color.Gray;
            this.lblHintMatKhau.Location = new System.Drawing.Point(139, 87);
            this.lblHintMatKhau.Name = "lblHintMatKhau";
            this.lblHintMatKhau.Size = new System.Drawing.Size(217, 17);
            this.lblHintMatKhau.TabIndex = 8;
            this.lblHintMatKhau.Text = "Tối thiểu 6 ký tự, tối đa 100 ký tự";
            // 
            // lblHintXacNhanMatKhau
            // 
            this.lblHintXacNhanMatKhau.AutoSize = true;
            this.lblHintXacNhanMatKhau.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintXacNhanMatKhau.ForeColor = System.Drawing.Color.Gray;
            this.lblHintXacNhanMatKhau.Location = new System.Drawing.Point(139, 128);
            this.lblHintXacNhanMatKhau.Name = "lblHintXacNhanMatKhau";
            this.lblHintXacNhanMatKhau.Size = new System.Drawing.Size(205, 17);
            this.lblHintXacNhanMatKhau.TabIndex = 8;
            this.lblHintXacNhanMatKhau.Text = "Nhập lại mật khẩu để xác nhận";
            // 
            // lblHintHoTen
            // 
            this.lblHintHoTen.AutoSize = true;
            this.lblHintHoTen.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintHoTen.ForeColor = System.Drawing.Color.Gray;
            this.lblHintHoTen.Location = new System.Drawing.Point(139, 169);
            this.lblHintHoTen.Name = "lblHintHoTen";
            this.lblHintHoTen.Size = new System.Drawing.Size(206, 17);
            this.lblHintHoTen.TabIndex = 8;
            this.lblHintHoTen.Text = "Nhập họ và tên đầy đủ của bạn";
            // 
            // lblHintEmail
            // 
            this.lblHintEmail.AutoSize = true;
            this.lblHintEmail.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintEmail.ForeColor = System.Drawing.Color.Gray;
            this.lblHintEmail.Location = new System.Drawing.Point(139, 208);
            this.lblHintEmail.Name = "lblHintEmail";
            this.lblHintEmail.Size = new System.Drawing.Size(177, 17);
            this.lblHintEmail.TabIndex = 8;
            this.lblHintEmail.Text = "Ví dụ: example@email.com";
            // 
            // chkDongYDieuKhoan
            // 
            this.chkDongYDieuKhoan.AutoSize = true;
            this.chkDongYDieuKhoan.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDongYDieuKhoan.Location = new System.Drawing.Point(105, 237);
            this.chkDongYDieuKhoan.Name = "chkDongYDieuKhoan";
            this.chkDongYDieuKhoan.Size = new System.Drawing.Size(290, 23);
            this.chkDongYDieuKhoan.TabIndex = 6;
            this.chkDongYDieuKhoan.Text = "Tôi đồng ý với các điều khoản sử dụng";
            this.chkDongYDieuKhoan.UseVisualStyleBackColor = true;
            // 
            // frmDangKy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 361);
            this.Controls.Add(this.chkDongYDieuKhoan);
            this.Controls.Add(this.lblHintEmail);
            this.Controls.Add(this.lblHintHoTen);
            this.Controls.Add(this.lblHintXacNhanMatKhau);
            this.Controls.Add(this.lblHintMatKhau);
            this.Controls.Add(this.lblHintTaiKhoan);
            this.Controls.Add(this.linklblDangNhap);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.txtXacNhanMatKhau);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTaiKhoan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.lblTaiKhoan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký - Ứng dụng Quản lý Công việc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linklblDangNhap;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblHintTaiKhoan;
        private System.Windows.Forms.Label lblHintMatKhau;
        private System.Windows.Forms.Label lblHintXacNhanMatKhau;
        private System.Windows.Forms.Label lblHintHoTen;
        private System.Windows.Forms.Label lblHintEmail;
        private System.Windows.Forms.CheckBox chkDongYDieuKhoan;
    }
}
