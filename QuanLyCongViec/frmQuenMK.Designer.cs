namespace QuanLyCongViec
{
    partial class frmQuenMK
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUsernameOrEmail = new System.Windows.Forms.Label();
            this.txtUsernameOrEmail = new System.Windows.Forms.TextBox();
            this.lblMatKhauMoi = new System.Windows.Forms.Label();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.lblXacNhanMatKhau = new System.Windows.Forms.Label();
            this.txtXacNhanMatKhau = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.linklblDangNhap = new System.Windows.Forms.LinkLabel();
            this.lblHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(120, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(263, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đặt lại mật khẩu";
            // 
            // lblUsernameOrEmail
            // 
            this.lblUsernameOrEmail.AutoSize = true;
            this.lblUsernameOrEmail.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsernameOrEmail.Location = new System.Drawing.Point(11, 79);
            this.lblUsernameOrEmail.Name = "lblUsernameOrEmail";
            this.lblUsernameOrEmail.Size = new System.Drawing.Size(190, 19);
            this.lblUsernameOrEmail.TabIndex = 1;
            this.lblUsernameOrEmail.Text = "Tên đăng nhập hoặc Email:";
            // 
            // txtUsernameOrEmail
            // 
            this.txtUsernameOrEmail.Location = new System.Drawing.Point(197, 76);
            this.txtUsernameOrEmail.Name = "txtUsernameOrEmail";
            this.txtUsernameOrEmail.Size = new System.Drawing.Size(250, 22);
            this.txtUsernameOrEmail.TabIndex = 1;
            this.txtUsernameOrEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsernameOrEmail_KeyDown);
            // 
            // lblMatKhauMoi
            // 
            this.lblMatKhauMoi.AutoSize = true;
            this.lblMatKhauMoi.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhauMoi.Location = new System.Drawing.Point(11, 119);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new System.Drawing.Size(107, 19);
            this.lblMatKhauMoi.TabIndex = 1;
            this.lblMatKhauMoi.Text = "Mật khẩu mới:";
            // 
            // txtMatKhauMoi
            // 
            this.txtMatKhauMoi.Location = new System.Drawing.Point(197, 116);
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.PasswordChar = '*';
            this.txtMatKhauMoi.Size = new System.Drawing.Size(250, 22);
            this.txtMatKhauMoi.TabIndex = 2;
            this.txtMatKhauMoi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatKhauMoi_KeyDown);
            // 
            // lblXacNhanMatKhau
            // 
            this.lblXacNhanMatKhau.AutoSize = true;
            this.lblXacNhanMatKhau.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXacNhanMatKhau.Location = new System.Drawing.Point(11, 159);
            this.lblXacNhanMatKhau.Name = "lblXacNhanMatKhau";
            this.lblXacNhanMatKhau.Size = new System.Drawing.Size(140, 19);
            this.lblXacNhanMatKhau.TabIndex = 1;
            this.lblXacNhanMatKhau.Text = "Xác nhận mật khẩu:";
            // 
            // txtXacNhanMatKhau
            // 
            this.txtXacNhanMatKhau.Location = new System.Drawing.Point(197, 156);
            this.txtXacNhanMatKhau.Name = "txtXacNhanMatKhau";
            this.txtXacNhanMatKhau.PasswordChar = '*';
            this.txtXacNhanMatKhau.Size = new System.Drawing.Size(250, 22);
            this.txtXacNhanMatKhau.TabIndex = 3;
            this.txtXacNhanMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtXacNhanMatKhau_KeyDown);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXacNhan.Location = new System.Drawing.Point(197, 209);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(120, 35);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(326, 209);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 35);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // linklblDangNhap
            // 
            this.linklblDangNhap.AutoSize = true;
            this.linklblDangNhap.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklblDangNhap.Location = new System.Drawing.Point(346, 259);
            this.linklblDangNhap.Name = "linklblDangNhap";
            this.linklblDangNhap.Size = new System.Drawing.Size(72, 17);
            this.linklblDangNhap.TabIndex = 6;
            this.linklblDangNhap.TabStop = true;
            this.linklblDangNhap.Text = "Đăng nhập";
            this.linklblDangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblDangNhap_LinkClicked);
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.Color.Gray;
            this.lblHint.Location = new System.Drawing.Point(188, 259);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(152, 17);
            this.lblHint.TabIndex = 7;
            this.lblHint.Text = "Nhớ mật khẩu? Quay lại";
            // 
            // frmQuenMK
            // 
            this.AcceptButton = this.btnXacNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(480, 290);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.linklblDangNhap);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtXacNhanMatKhau);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.txtUsernameOrEmail);
            this.Controls.Add(this.lblXacNhanMatKhau);
            this.Controls.Add(this.lblMatKhauMoi);
            this.Controls.Add(this.lblUsernameOrEmail);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuenMK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quên mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUsernameOrEmail;
        private System.Windows.Forms.TextBox txtUsernameOrEmail;
        private System.Windows.Forms.Label lblMatKhauMoi;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.Label lblXacNhanMatKhau;
        private System.Windows.Forms.TextBox txtXacNhanMatKhau;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.LinkLabel linklblDangNhap;
        private System.Windows.Forms.Label lblHint;
    }
}



