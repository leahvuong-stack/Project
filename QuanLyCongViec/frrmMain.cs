using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frrmMain : Form
    {

        #region 1. KHAI BÁO BIẾN & THUỘC TÍNH (Fields)

        private int _currentUserId;
        private string _username;
        private string _fullName;
        private Timer _timer; // Timer dùng để cập nhật thời gian liên tục

        #endregion

        #region 2. CONSTRUCTOR & KHỞI TẠO

        public frrmMain(int userId, string username, string fullName)
        {
            // Constructor nhận thông tin user từ form đăng nhập truyền sang
            InitializeComponent();
            _currentUserId = userId;
            _username = username;
            _fullName = fullName;

            // Khởi tạo giao diện & dữ liệu ban đầu
            HienThiThongTinUser();   // Hiển thị tên, username
            CapNhatDashboard();      // Load số liệu Dashboard
            KhoiTaoTimer();          // Khởi tạo đồng hồ thời gian


            // Thiết lập màu sắc cho các panel
            panel_Tong.BackColor = ColorTranslator.FromHtml("#4C84FF");
            panel_Todo.BackColor = ColorTranslator.FromHtml("#6AA9FF");
            panel_Doing.BackColor = ColorTranslator.FromHtml("#FFC94D");
            panel_Done.BackColor = ColorTranslator.FromHtml("#69D16F");
            panel_QuaHan.BackColor = ColorTranslator.FromHtml("#FF6B6B");
        }

        // Khi đóng form → dừng timer để tránh rò rỉ tài nguyên
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion

        #region 3. CÁC HÀM XỬ LÝ GIAO DIỆN & THỜI GIAN

        // Hiển thị tên + username
        private void HienThiThongTinUser()
        {
            lbl_Ten.Text = $"Xin chào, {_fullName}";
            llb_Username.Text = $"Tài khoản: {_username}";
            CapNhatThoiGian();
        }

        // Cập nhật ngày tháng hiện tại
        private void CapNhatThoiGian()
        {
            DateTime ngayHienTai = DateTime.Now;
            lbl_NgayThang.Text = ngayHienTai.ToString("'hôm nay:' dd/MM/yyyy");
        }

        // Khởi tạo timer → mỗi 1 giây sẽ cập nhật thời gian trên giao diện
        private void KhoiTaoTimer()
        {
            _timer = new Timer();
            _timer.Interval = 1000; // 1 giây
            _timer.Tick += (s, e) => CapNhatThoiGian();
            _timer.Start();
        }

        #endregion

        #region 4. CÁC HÀM XỬ LÝ DỮ LIỆU (DATA LOGIC)

        // Hàm gọi Stored Procedure để lấy thống kê công việc
        private void CapNhatDashboard()
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", _currentUserId) // chỉ lấy dữ liệu của user hiện tại
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure(
                    "sp_GetDashboardStats",
                    parameters
                );

                if (dt.Rows.Count > 0)
                {
                    // Đổ dữ liệu vào label Dashboard
                    DataRow row = dt.Rows[0];
                    lbl_TongCongViec.Text = $"Tổng công việc: {row["TotalTasks"]}";
                    lbl_Todo.Text = $"Cần làm (To-do): {row["TodoCount"]}";
                    lbl_Doing.Text = $"Đang làm (Doing): {row["DoingCount"]}";
                    lbl_Done.Text = $"Hoàn thành (Done): {row["DoneCount"]}";
                    lbl_quahan.Text = $"Quá hạn: {row["OverdueCount"]}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dashboard: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 5. XỬ LÝ SỰ KIỆN CỦA CONTROLS (Events)

        // Mở form báo cáo
        private void btn_BaoCao_Click(object sender, EventArgs e)
        {
            frmBaoCao reportForm = new frmBaoCao();
            reportForm.ShowDialog();
        }

        // Mở form lịch sử
        private void btn_LichSu_Click(object sender, EventArgs e)
        {
            frmLichSu historyForm = new frmLichSu();
            historyForm.ShowDialog();
        }

        // Mở form quản lý công việc
        private void btn_QuanLyCongViec_Click(object sender, EventArgs e)
        {
            frmThemSuaTask taskForm = new frmThemSuaTask();
            taskForm.ShowDialog();
            CapNhatDashboard(); // Refresh sau khi đóng
        }

        // Xử lý đăng xuất
        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _timer?.Stop();
                this.Hide();
                frmDangNhap loginForm = new frmDangNhap();
                loginForm.ShowDialog();
                this.Close();
            }
        }

        // Mở form Profile khi click vào link username
        private void llb_Username_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmProfile profileForm = new frmProfile(_currentUserId, _username, _fullName);
            profileForm.ShowDialog();

            // Nếu người dùng đổi tên → cập nhật lại trên Main
            if (!string.IsNullOrEmpty(profileForm.NewFullName))
            {
                // Cập nhật lại thông tin người dùng nếu họ đổi tên trong Profile
                _fullName = profileForm.NewFullName;
                HienThiThongTinUser();
            }
        }

        // Sự kiện không có logic nghiệp vụ, có thể bỏ qua hoặc giữ lại
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Không làm gì
        }

        #endregion

        // Khi form load → khóa resize + tắt nút phóng to
        private void frrmMain_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
