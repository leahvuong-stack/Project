namespace QuanLyCongViec
{
    partial class frrmMain
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Ten = new System.Windows.Forms.Label();
            this.btn_DangXuat = new System.Windows.Forms.Button();
            this.btn_Help = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_NgayThang = new System.Windows.Forms.Label();
            this.lbl_quahan = new System.Windows.Forms.Label();
            this.lbl_Done = new System.Windows.Forms.Label();
            this.lbl_Doing = new System.Windows.Forms.Label();
            this.lbl_Todo = new System.Windows.Forms.Label();
            this.btn_LichSu = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_ThongBao = new System.Windows.Forms.Button();
            this.btn_QuanLyCongViec = new System.Windows.Forms.Button();
            this.btn_BaoCao = new System.Windows.Forms.Button();
            this.btn_ThongBao = new System.Windows.Forms.Button();
            this.lbl_TongCongViec = new System.Windows.Forms.Label();
            this.panel_Tong = new System.Windows.Forms.Panel();
            this.panel_Doing = new System.Windows.Forms.Panel();
            this.panel_Done = new System.Windows.Forms.Panel();
            this.panel_QuaHan = new System.Windows.Forms.Panel();
            this.panel_Todo = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel_Tong.SuspendLayout();
            this.panel_Doing.SuspendLayout();
            this.panel_Done.SuspendLayout();
            this.panel_QuaHan.SuspendLayout();
            this.panel_Todo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Ten
            // 
            this.lbl_Ten.AutoSize = true;
            this.lbl_Ten.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lbl_Ten.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lbl_Ten.Location = new System.Drawing.Point(10, 20);
            this.lbl_Ten.Name = "lbl_Ten";
            this.lbl_Ten.Size = new System.Drawing.Size(282, 25);
            this.lbl_Ten.TabIndex = 0;
            this.lbl_Ten.Text = "👋 Xin chào, [Tên người dùng]";
            // 
            // btn_DangXuat
            // 
            this.btn_DangXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btn_DangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DangXuat.FlatAppearance.BorderSize = 0;
            this.btn_DangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DangXuat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btn_DangXuat.ForeColor = System.Drawing.Color.White;
            this.btn_DangXuat.Location = new System.Drawing.Point(337, 26);
            this.btn_DangXuat.Name = "btn_DangXuat";
            this.btn_DangXuat.Size = new System.Drawing.Size(130, 30);
            this.btn_DangXuat.TabIndex = 1;
            this.btn_DangXuat.Text = " Tài khoản ▼";
            this.btn_DangXuat.UseVisualStyleBackColor = false;
            this.btn_DangXuat.Click += new System.EventHandler(this.btn_DangXuat_Click);
            // 
            // btn_Help
            // 
            this.btn_Help.Location = new System.Drawing.Point(324, 46);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(80, 24);
            this.btn_Help.TabIndex = 5;
            this.btn_Help.Text = "Trợ giúp";
            this.btn_Help.UseVisualStyleBackColor = true;
            this.btn_Help.Click += new System.EventHandler(this.btn_Help_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lbl_NgayThang);
            this.groupBox1.Controls.Add(this.lbl_Ten);
            this.groupBox1.Controls.Add(this.btn_Help);
            this.groupBox1.Controls.Add(this.btn_DangXuat);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 89);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin người dùng";
            // 
            // lbl_NgayThang
            // 
            this.lbl_NgayThang.AutoSize = true;
            this.lbl_NgayThang.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lbl_NgayThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lbl_NgayThang.Location = new System.Drawing.Point(13, 54);
            this.lbl_NgayThang.Name = "lbl_NgayThang";
            this.lbl_NgayThang.Size = new System.Drawing.Size(97, 20);
            this.lbl_NgayThang.TabIndex = 3;
            this.lbl_NgayThang.Text = "📅 Hôm nay:";
            // 
            // lbl_quahan
            // 
            this.lbl_quahan.AutoSize = true;
            this.lbl_quahan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_quahan.ForeColor = System.Drawing.Color.White;
            this.lbl_quahan.Location = new System.Drawing.Point(8, 16);
            this.lbl_quahan.Name = "lbl_quahan";
            this.lbl_quahan.Size = new System.Drawing.Size(125, 23);
            this.lbl_quahan.TabIndex = 0;
            this.lbl_quahan.Text = "⚠️ Quá hạn: 0";
            // 
            // lbl_Done
            // 
            this.lbl_Done.AutoSize = true;
            this.lbl_Done.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_Done.ForeColor = System.Drawing.Color.White;
            this.lbl_Done.Location = new System.Drawing.Point(8, 15);
            this.lbl_Done.Name = "lbl_Done";
            this.lbl_Done.Size = new System.Drawing.Size(211, 23);
            this.lbl_Done.TabIndex = 0;
            this.lbl_Done.Text = "✅ Hoàn thành (Done): 0";
            // 
            // lbl_Doing
            // 
            this.lbl_Doing.AutoSize = true;
            this.lbl_Doing.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_Doing.ForeColor = System.Drawing.Color.White;
            this.lbl_Doing.Location = new System.Drawing.Point(8, 14);
            this.lbl_Doing.Name = "lbl_Doing";
            this.lbl_Doing.Size = new System.Drawing.Size(203, 23);
            this.lbl_Doing.TabIndex = 0;
            this.lbl_Doing.Text = "⚡ Đang làm (Doing): 0";
            // 
            // lbl_Todo
            // 
            this.lbl_Todo.AutoSize = true;
            this.lbl_Todo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_Todo.ForeColor = System.Drawing.Color.White;
            this.lbl_Todo.Location = new System.Drawing.Point(8, 14);
            this.lbl_Todo.Name = "lbl_Todo";
            this.lbl_Todo.Size = new System.Drawing.Size(187, 23);
            this.lbl_Todo.TabIndex = 0;
            this.lbl_Todo.Text = "📝 Cần làm (To-do): 0";
            // 
            // btn_LichSu
            // 
            this.btn_LichSu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btn_LichSu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LichSu.FlatAppearance.BorderSize = 0;
            this.btn_LichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LichSu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_LichSu.ForeColor = System.Drawing.Color.White;
            this.btn_LichSu.Location = new System.Drawing.Point(13, 90);
            this.btn_LichSu.Name = "btn_LichSu";
            this.btn_LichSu.Size = new System.Drawing.Size(220, 60);
            this.btn_LichSu.TabIndex = 4;
            this.btn_LichSu.Text = "📜 Lịch Sử";
            this.btn_LichSu.UseVisualStyleBackColor = false;
            this.btn_LichSu.Click += new System.EventHandler(this.btn_LichSu_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.btn_ThongBao);
            this.groupBox3.Controls.Add(this.btn_QuanLyCongViec);
            this.groupBox3.Controls.Add(this.btn_BaoCao);
            this.groupBox3.Controls.Add(this.btn_LichSu);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(12, 338);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(480, 160);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chức năng";
            // 
            // btn_ThongBao
            // 
            this.btn_ThongBao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.btn_ThongBao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ThongBao.FlatAppearance.BorderSize = 0;
            this.btn_ThongBao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ThongBao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_ThongBao.ForeColor = System.Drawing.Color.White;
            this.btn_ThongBao.Location = new System.Drawing.Point(247, 90);
            this.btn_ThongBao.Name = "btn_ThongBao";
            this.btn_ThongBao.Size = new System.Drawing.Size(220, 60);
            this.btn_ThongBao.TabIndex = 5;
            this.btn_ThongBao.Text = "📅 Thời khóa biểu";
            this.btn_ThongBao.UseVisualStyleBackColor = false;
            this.btn_ThongBao.Click += new System.EventHandler(this.btn_ThongBao_Click);
            // 
            // btn_QuanLyCongViec
            // 
            this.btn_QuanLyCongViec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btn_QuanLyCongViec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_QuanLyCongViec.FlatAppearance.BorderSize = 0;
            this.btn_QuanLyCongViec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_QuanLyCongViec.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_QuanLyCongViec.ForeColor = System.Drawing.Color.White;
            this.btn_QuanLyCongViec.Location = new System.Drawing.Point(13, 21);
            this.btn_QuanLyCongViec.Name = "btn_QuanLyCongViec";
            this.btn_QuanLyCongViec.Size = new System.Drawing.Size(220, 60);
            this.btn_QuanLyCongViec.TabIndex = 4;
            this.btn_QuanLyCongViec.Text = "📋 Quản lý công việc";
            this.btn_QuanLyCongViec.UseVisualStyleBackColor = false;
            this.btn_QuanLyCongViec.Click += new System.EventHandler(this.btn_QuanLyCongViec_Click);
            // 
            // btn_BaoCao
            // 
            this.btn_BaoCao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btn_BaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_BaoCao.FlatAppearance.BorderSize = 0;
            this.btn_BaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BaoCao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_BaoCao.ForeColor = System.Drawing.Color.White;
            this.btn_BaoCao.Location = new System.Drawing.Point(247, 21);
            this.btn_BaoCao.Name = "btn_BaoCao";
            this.btn_BaoCao.Size = new System.Drawing.Size(220, 60);
            this.btn_BaoCao.TabIndex = 4;
            this.btn_BaoCao.Text = "📊 Báo Cáo";
            this.btn_BaoCao.UseVisualStyleBackColor = false;
            this.btn_BaoCao.Click += new System.EventHandler(this.btn_BaoCao_Click);
            // 
            // btn_ThongBao
            // 
            this.btn_ThongBao.Location = new System.Drawing.Point(13, 105);
            this.btn_ThongBao.Name = "btn_ThongBao";
            this.btn_ThongBao.Size = new System.Drawing.Size(126, 74);
            this.btn_ThongBao.TabIndex = 5;
            this.btn_ThongBao.Text = "Thông Báo";
            this.btn_ThongBao.UseVisualStyleBackColor = true;
            this.btn_ThongBao.Click += new System.EventHandler(this.btn_ThongBao_Click);
            // 
            // lbl_TongCongViec
            // 
            this.lbl_TongCongViec.AutoSize = true;
            this.lbl_TongCongViec.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lbl_TongCongViec.ForeColor = System.Drawing.Color.White;
            this.lbl_TongCongViec.Location = new System.Drawing.Point(8, 14);
            this.lbl_TongCongViec.Name = "lbl_TongCongViec";
            this.lbl_TongCongViec.Size = new System.Drawing.Size(180, 23);
            this.lbl_TongCongViec.TabIndex = 0;
            this.lbl_TongCongViec.Text = "📊 Tổng công việc: 0";
            // 
            // panel_Tong
            // 
            this.panel_Tong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Tong.Controls.Add(this.lbl_TongCongViec);
            this.panel_Tong.Location = new System.Drawing.Point(12, 107);
            this.panel_Tong.Name = "panel_Tong";
            this.panel_Tong.Size = new System.Drawing.Size(236, 51);
            this.panel_Tong.TabIndex = 7;
            // 
            // panel_Doing
            // 
            this.panel_Doing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Doing.Controls.Add(this.lbl_Doing);
            this.panel_Doing.Location = new System.Drawing.Point(12, 164);
            this.panel_Doing.Name = "panel_Doing";
            this.panel_Doing.Size = new System.Drawing.Size(480, 50);
            this.panel_Doing.TabIndex = 8;
            this.panel_Doing.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel_Done
            // 
            this.panel_Done.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Done.Controls.Add(this.lbl_Done);
            this.panel_Done.Location = new System.Drawing.Point(12, 220);
            this.panel_Done.Name = "panel_Done";
            this.panel_Done.Size = new System.Drawing.Size(480, 52);
            this.panel_Done.TabIndex = 9;
            // 
            // panel_QuaHan
            // 
            this.panel_QuaHan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_QuaHan.Controls.Add(this.lbl_quahan);
            this.panel_QuaHan.Location = new System.Drawing.Point(12, 278);
            this.panel_QuaHan.Name = "panel_QuaHan";
            this.panel_QuaHan.Size = new System.Drawing.Size(480, 54);
            this.panel_QuaHan.TabIndex = 10;
            // 
            // panel_Todo
            // 
            this.panel_Todo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Todo.Controls.Add(this.lbl_Todo);
            this.panel_Todo.Location = new System.Drawing.Point(256, 107);
            this.panel_Todo.Name = "panel_Todo";
            this.panel_Todo.Size = new System.Drawing.Size(236, 51);
            this.panel_Todo.TabIndex = 11;
            // 
            // frrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(504, 510);
            this.Controls.Add(this.panel_Todo);
            this.Controls.Add(this.panel_QuaHan);
            this.Controls.Add(this.panel_Done);
            this.Controls.Add(this.panel_Doing);
            this.Controls.Add(this.panel_Tong);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frrmMain";
            this.Text = "Hệ Thống Ứng Dụng Quản Lý Công Việc";
            this.Load += new System.EventHandler(this.frrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.panel_Tong.ResumeLayout(false);
            this.panel_Tong.PerformLayout();
            this.panel_Doing.ResumeLayout(false);
            this.panel_Doing.PerformLayout();
            this.panel_Done.ResumeLayout(false);
            this.panel_Done.PerformLayout();
            this.panel_QuaHan.ResumeLayout(false);
            this.panel_QuaHan.PerformLayout();
            this.panel_Todo.ResumeLayout(false);
            this.panel_Todo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Ten;
        private System.Windows.Forms.Button btn_DangXuat;
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_NgayThang;
        private System.Windows.Forms.Button btn_LichSu;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_ThongBao;
        private System.Windows.Forms.Button btn_QuanLyCongViec;
        private System.Windows.Forms.Button btn_BaoCao;
        private System.Windows.Forms.Label lbl_quahan;
        private System.Windows.Forms.Label lbl_Done;
        private System.Windows.Forms.Label lbl_Doing;
        private System.Windows.Forms.Label lbl_Todo;
        private System.Windows.Forms.Label lbl_TongCongViec;
        private System.Windows.Forms.Panel panel_Tong;
        private System.Windows.Forms.Panel panel_Doing;
        private System.Windows.Forms.Panel panel_Done;
        private System.Windows.Forms.Panel panel_QuaHan;
        private System.Windows.Forms.Panel panel_Todo;
    }
}
