using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    //Form đăng nhập vào hệ thống Quản lý Công việc
    public partial class frmDangNhap : Form
    {
        #region Constants - Hằng số
        private const int KHONG_CO_LOI = 0;
        private const int TAI_KHOAN_BI_KHOA = 2;
        #endregion

        #region Private Fields - Các trường dữ liệu riêng tư
        //Số lần đăng nhập thất bại liên tiếp
        private int _soLanDangNhapSai = 0;
        //Thời điểm tài khoản bị khóa đến khi nào (null nếu không bị khóa)
        private DateTime? _thoiGianKhoaDen = null;
        #endregion

        #region Constructor - Hàm khởi tạo

        //Khởi tạo form đăng nhập
        public frmDangNhap()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers - Xử lý sự kiện
        //Xử lý sự kiện click nút Đăng nhập
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            ThucHienDangNhap();
        }
        //Xử lý sự kiện click link Đăng ký
        //Mở form đăng ký và xử lý kết quả sau khi đóng
        private void linklblDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoFormDangKy();
        }
        //Xử lý sự kiện click link Quên mật khẩu
        //Mở form quên mật khẩu
        private void linklblQuenMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MoFormQuenMatKhau();
        }
        //Xử lý sự kiện nhấn phím Enter trong textbox Tài khoản
        //Chuyển focus sang textbox Mật khẩu
        private void txtTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMatKhau.Focus();
            }
        }
        //Xử lý sự kiện nhấn phím Enter trong textbox Mật khẩu
        //Thực hiện đăng nhập
        private void txtMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThucHienDangNhap();
            }
        }
        #endregion

        #region Private Methods - Các phương thức riêng tư
        //Mở form đăng ký và xử lý kết quả
        //Nếu đăng ký thành công, tự động điền username vào form đăng nhập
        private void MoFormDangKy()
        {
            frmDangKy frmDangKy = new frmDangKy();
            this.Hide();

            DialogResult ketQua = frmDangKy.ShowDialog();
            this.Show();

            if (ketQua == DialogResult.OK && !string.IsNullOrWhiteSpace(frmDangKy.TenDangNhapDaDangKy))
            {
                DienTenDangNhapSauDangKy(frmDangKy.TenDangNhapDaDangKy);
            }
        }
        //Mở form quên mật khẩu
        private void MoFormQuenMatKhau()
        {
            frmQuenMK frmQuenMK = new frmQuenMK();
            this.Hide();

            DialogResult ketQua = frmQuenMK.ShowDialog();
            this.Show();

            //Nếu reset password thành công, có thể focus vào textbox tài khoản
            if (ketQua == DialogResult.OK)
            {
                txtTaiKhoan.Focus();
            }
        }
        //Điền tên đăng nhập vào textbox sau khi đăng ký thành công
        private void DienTenDangNhapSauDangKy(string tenDangNhap)
        {
            txtTaiKhoan.Text = tenDangNhap;
            txtMatKhau.Clear();
            txtMatKhau.Focus();
        }
        //Thực hiện quá trình đăng nhập
        //Bao gồm: kiểm tra lockout, validate input, xác thực thông tin, xử lý kết quả
        private void ThucHienDangNhap()
        {
            try
            {
                //Kiểm tra tài khoản có bị khóa không
                if (TaiKhoanBiKhoa())
                {
                    return;
                }
                //Reset lockout nếu đã hết hạn
                DatLaiKhoaNeuHetHan();
                //Validate dữ liệu đầu vào
                if (!KiemTraDuLieuDangNhap())
                {
                    return;
                }
                //Thực hiện xác thực đăng nhập
                DataTable loginResult = XacThucNguoiDung();
                //Xử lý kết quả đăng nhập
                XuLyKetQuaDangNhap(loginResult);
            }
            catch (Exception loi)
            {
                HienThiLoiDangNhap(loi);
            }
        }
        //Kiểm tra tài khoản có đang bị khóa không
        private bool TaiKhoanBiKhoa()
        {
            if (_thoiGianKhoaDen.HasValue && DateTime.Now < _thoiGianKhoaDen.Value)
            {
                int soPhutConLai = (int)(_thoiGianKhoaDen.Value - DateTime.Now).TotalMinutes;
                HienThiThongBaoTaiKhoanBiKhoa(soPhutConLai);
                return true;
            }
            return false;
        }
        //Hiển thị thông báo tài khoản bị khóa
        private void HienThiThongBaoTaiKhoanBiKhoa(int soPhutConLai)
        {
            string thongBao = $"Tài khoản đã bị khóa tạm thời do đăng nhập sai quá nhiều lần!\n\n" +
                           $"Vui lòng thử lại sau {soPhutConLai} phút.";

            MessageBox.Show(
                thongBao,
                "Tài khoản bị khóa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
        //Reset lockout nếu thời gian khóa đã hết hạn
        private void DatLaiKhoaNeuHetHan()
        {
            if (_thoiGianKhoaDen.HasValue && DateTime.Now >= _thoiGianKhoaDen.Value)
            {
                _thoiGianKhoaDen = null;
                _soLanDangNhapSai = 0;
            }
        }
        //Kiểm tra dữ liệu đầu vào của form đăng nhập
        private bool KiemTraDuLieuDangNhap()
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                HienThiLoiKiemTra("Vui lòng nhập tên đăng nhập!", txtTaiKhoan);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                HienThiLoiKiemTra("Vui lòng nhập mật khẩu!", txtMatKhau);
                return false;
            }

            //Kiểm tra định dạng tên đăng nhập
            if (!PasswordHelper.IsValidUsername(txtTaiKhoan.Text.Trim()))
            {
                HienThiLoiKiemTra("Tên đăng nhập không hợp lệ!\n\nTên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới (_).\nKhông được chứa ký tự đặc biệt hoặc khoảng trắng.", txtTaiKhoan);
                return false;
            }

            //Kiểm tra mật khẩu không chứa ký tự nguy hiểm
            if (!PasswordHelper.IsValidPassword(txtMatKhau.Text))
            {
                HienThiLoiKiemTra("Mật khẩu chứa ký tự không được phép!\n\nVui lòng sử dụng mật khẩu hợp lệ.", txtMatKhau);
                return false;
            }

            return true;
        }
        //Hiển thị thông báo lỗi validation và focus vào control tương ứng
        private void HienThiLoiKiemTra(string thongBao, Control dong)
        {
            MessageBox.Show(
                thongBao,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            dong.Focus();
        }
        //Xác thực thông tin đăng nhập với database
        //Hỗ trợ cả mật khẩu dạng plain text (database cũ) và mật khẩu đã hash (database mới)
        private DataTable XacThucNguoiDung()
        {
            string tenDangNhap = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text;

            //Thử đăng nhập với mật khẩu plain text trước (tương thích với database cũ)
            SqlParameter[] thamSoMatKhauThuong = new SqlParameter[]
            {
                new SqlParameter("@Username", tenDangNhap),
                new SqlParameter("@Password", matKhau)
            };

            DataTable ketQuaMatKhauThuong = DatabaseHelper.ExecuteStoredProcedure("sp_UserLogin", thamSoMatKhauThuong);

            //Nếu đăng nhập thành công với plain text, trả về kết quả
            if (ketQuaMatKhauThuong != null && ketQuaMatKhauThuong.Rows.Count > 0)
            {
                return ketQuaMatKhauThuong;
            }

            //Nếu không thành công, thử với mật khẩu đã hash (tương thích với database mới)
            string matKhauDaHash = PasswordHelper.HashPassword(matKhau);

            SqlParameter[] thamSoMatKhauDaHash = new SqlParameter[]
            {
                new SqlParameter("@Username", tenDangNhap),
                new SqlParameter("@Password", matKhauDaHash)
            };

            DataTable ketQuaMatKhauDaHash = DatabaseHelper.ExecuteStoredProcedure("sp_UserLogin", thamSoMatKhauDaHash);

            //Trả về kết quả (có thể là empty nếu cả hai đều thất bại)
            return ketQuaMatKhauDaHash;
        }
        //Xử lý kết quả đăng nhập từ database
        private void XuLyKetQuaDangNhap(DataTable ketQuaDangNhap)
        {
            if (ketQuaDangNhap == null || ketQuaDangNhap.Rows.Count == 0)
            {
                XuLyDangNhapThatBai();
                return;
            }

            DataRow dongNguoiDung = ketQuaDangNhap.Rows[0];

            //Kiểm tra ErrorCode nếu stored procedure trả về
            if (CoMaLoi(dongNguoiDung))
            {
                XuLyPhanHoiMaLoi(dongNguoiDung);
                return;
            }

            //Kiểm tra Id có null không (trường hợp stored procedure cũ)
            if (MaNguoiDungLaNull(dongNguoiDung))
            {
                XuLyDangNhapThatBai();
                return;
            }

            //Đăng nhập thành công
            XuLyDangNhapThanhCong(dongNguoiDung);
        }
        //Kiểm tra xem kết quả có chứa ErrorCode không
        private bool CoMaLoi(DataRow dongNguoiDung)
        {
            return dongNguoiDung.Table.Columns.Contains("ErrorCode");
        }
        //Xử lý response có ErrorCode từ stored procedure
        private void XuLyPhanHoiMaLoi(DataRow dongNguoiDung)
        {
            object doiTuongMaLoi = dongNguoiDung["ErrorCode"];
            if (doiTuongMaLoi == null || doiTuongMaLoi == DBNull.Value)
            {
                return;
            }

            int maLoi = Convert.ToInt32(doiTuongMaLoi);
            if (maLoi == KHONG_CO_LOI)
            {
                return;
            }

            string thongBaoLoi = dongNguoiDung["ErrorMessage"]?.ToString() ?? "Đăng nhập thất bại";

            if (maLoi == TAI_KHOAN_BI_KHOA)
            {
                HienThiThongBaoTaiKhoanBiVoHieu(thongBaoLoi);
            }
            else
            {
                _soLanDangNhapSai++;
                XuLyDangNhapThatBai();
            }
        }
        //Hiển thị thông báo tài khoản bị vô hiệu hóa
        private void HienThiThongBaoTaiKhoanBiVoHieu(string thongBaoLoi)
        {
            string thongBao = $"Tài khoản đã bị khóa hoặc vô hiệu hóa!\n\n{thongBaoLoi}\n\nVui lòng liên hệ quản trị viên.";

            MessageBox.Show(
                thongBao,
                "Tài khoản bị khóa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
        //Kiểm tra mã người dùng có null không
        private bool MaNguoiDungLaNull(DataRow dongNguoiDung)
        {
            return dongNguoiDung["Id"] == null || dongNguoiDung["Id"] == DBNull.Value;
        }
        //Xử lý khi đăng nhập thành công
        private void XuLyDangNhapThanhCong(DataRow dongNguoiDung)
        {
            int maNguoiDung = Convert.ToInt32(dongNguoiDung["Id"]);
            string tenDangNhap = txtTaiKhoan.Text.Trim();
            string hoTen = dongNguoiDung["FullName"].ToString();

            //Reset số lần thất bại
            DatLaiSoLanDangNhapSai();

            //Lưu thông tin đăng nhập nếu chọn "Ghi nhớ"
            LuuHoacXoaThongTinDangNhap(tenDangNhap);

            //Lưu thông tin người dùng vào CurrentUser để các form khác có thể sử dụng
            CurrentUser.SetCurrentUser(maNguoiDung, tenDangNhap, hoTen);

            //Hiển thị thông báo thành công
            HienThiThongBaoThanhCong(hoTen);

            //Mở form chính và đóng form đăng nhập
            this.Hide();
            frrmMain formChinh = new frrmMain(maNguoiDung, tenDangNhap, hoTen);
            formChinh.ShowDialog();
            this.Close();
        }
        //Reset số lần đăng nhập thất bại về 0
        private void DatLaiSoLanDangNhapSai()
        {
            _soLanDangNhapSai = 0;
            _thoiGianKhoaDen = null;
        }
        //Lưu hoặc xóa thông tin đăng nhập tùy theo checkbox "Ghi nhớ"
        private void LuuHoacXoaThongTinDangNhap(string tenDangNhap)
        {
            if (chkGhiNhoDangNhap.Checked)
            {
                LuuThongTinDangNhap(tenDangNhap);
            }
            else
            {
                XoaThongTinDangNhap();
            }
        }
        //Hiển thị thông báo đăng nhập thành công
        private void HienThiThongBaoThanhCong(string hoTen)
        {
            MessageBox.Show(
                $"Đăng nhập thành công!\n\nXin chào, {hoTen}!",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        //Xử lý khi đăng nhập thất bại
        //Tăng số lần thất bại và hiển thị thông báo phù hợp
        private void XuLyDangNhapThatBai()
        {
            _soLanDangNhapSai++;

            string thongBaoLoi = TaoThongBaoDangNhapThatBai();

            MessageBox.Show(
                thongBaoLoi,
                "Đăng nhập thất bại",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            XoaTruongMatKhau();
        }
        //Tạo thông báo lỗi khi đăng nhập thất bại
        private string TaoThongBaoDangNhapThatBai()
        {
            string thongBaoCoBan = "Tên đăng nhập hoặc mật khẩu không đúng!\n\nVui lòng kiểm tra lại.";

            //Lấy giá trị từ database
            int soLanDangNhapSaiToiDa = Helpers.SystemSettings.MaxLoginAttempts;
            int thoiGianKhoaPhut = Helpers.SystemSettings.LockoutMinutes;

            if (_soLanDangNhapSai >= soLanDangNhapSaiToiDa)
            {
                _thoiGianKhoaDen = DateTime.Now.AddMinutes(thoiGianKhoaPhut);
                return thongBaoCoBan + $"\n\nCảnh báo: Bạn đã đăng nhập sai {_soLanDangNhapSai} lần.\n" +
                                   $"Tài khoản sẽ bị khóa tạm thời trong {thoiGianKhoaPhut} phút.";
            }
            else
            {
                int soLanConLai = soLanDangNhapSaiToiDa - _soLanDangNhapSai;
                return thongBaoCoBan + $"\n\nCòn lại {soLanConLai} lần thử.";
            }
        }
        //Xóa trường mật khẩu và focus vào đó
        private void XoaTruongMatKhau()
        {
            txtMatKhau.Clear();
            txtMatKhau.Focus();
        }
        //Hiển thị thông báo lỗi khi có exception xảy ra
        private void HienThiLoiDangNhap(Exception loi)
        {
            MessageBox.Show(
                $"Lỗi khi đăng nhập!\n\nChi tiết: {loi.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        //Lưu thông tin đăng nhập vào Settings (chỉ lưu tên đăng nhập, không lưu mật khẩu)
        private void LuuThongTinDangNhap(string tenDangNhap)
        {
            try
            {
                Properties.Settings.Default.RememberedUsername = tenDangNhap;
                Properties.Settings.Default.Save();
            }
            catch
            {
                //Bỏ qua lỗi khi lưu settings để không làm gián đoạn quá trình đăng nhập
            }
        }
        //Xóa thông tin đăng nhập đã lưu trong Settings
        private void XoaThongTinDangNhap()
        {
            try
            {
                Properties.Settings.Default.RememberedUsername = string.Empty;
                Properties.Settings.Default.Save();
            }
            catch
            {
                //Bỏ qua lỗi khi lưu settings
            }
        }
        //Load thông tin đăng nhập đã lưu từ Settings
        //Tự động điền tên đăng nhập và check checkbox "Ghi nhớ" nếu có
        private void TaiThongTinDangNhap()
        {
            try
            {
                string tenDangNhapDaNho = Properties.Settings.Default.RememberedUsername;
                if (!string.IsNullOrWhiteSpace(tenDangNhapDaNho))
                {
                    txtTaiKhoan.Text = tenDangNhapDaNho;
                    chkGhiNhoDangNhap.Checked = true;
                    txtMatKhau.Focus();
                }
            }
            catch
            {
                //Bỏ qua lỗi khi load settings
            }
        }
        #endregion
    }
}
