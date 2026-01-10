using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    //Form đăng ký tài khoản mới cho hệ thống Quản lý Công việc
    public partial class frmDangKy : Form
    {
        #region Properties - Thuộc tính
        //Username đã đăng ký thành công (để trả về form đăng nhập)
        public string TenDangNhapDaDangKy { get; private set; }
        
        //Lưu giá trị hợp lệ cuối cùng để restore khi phát hiện ký tự không hợp lệ
        private string _matKhauHopLeCuoi = "";
        private string _xacNhanMatKhauHopLeCuoi = "";
        private bool _dangKiemTra = false; //Flag để tránh recursive calls
        #endregion

        #region Constructor - Hàm khởi tạo
        //Khởi tạo form đăng ký
        public frmDangKy()
        {
            InitializeComponent();
            DangKySuKienKiemTraThoiGianThuc();
        }
        
        //Đăng ký các event handlers cho validation real-time
        private void DangKySuKienKiemTraThoiGianThuc()
        {
            txtTaiKhoan.KeyPress += txtTaiKhoan_KeyPress;
            txtMatKhau.TextChanged += txtMatKhau_TextChanged;
            txtXacNhanMatKhau.TextChanged += txtXacNhanMatKhau_TextChanged;
        }
        #endregion

        #region Event Handlers - Xử lý sự kiện
        //Xử lý sự kiện click nút Đăng ký
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            ThucHienDangKy();
        }
        //Xử lý sự kiện click link Đăng nhập
        //Đóng form và quay về form đăng nhập
        private void linklblDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DongVaQuayVeDangNhap();
        }
        #endregion

        #region Private Methods - Các phương thức riêng tư
        //Đóng form và trả về DialogResult.Cancel để báo hiệu người dùng hủy đăng ký
        private void DongVaQuayVeDangNhap()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        //Thực hiện quá trình đăng ký tài khoản mới
        //Bao gồm: validate input, gọi stored procedure, xử lý kết quả
        private void ThucHienDangKy()
        {
            try
            {
                //Validate dữ liệu đầu vào
                if (!KiemTraDuLieu())
                {
                    return;
                }
                //Lấy thông tin từ form
                DuLieuDangKy duLieuDangKy = LayDuLieuDangKy();
                //Đăng ký tài khoản vào database
                int maNguoiDung = DangKyNguoiDungVaoDatabase(duLieuDangKy, duLieuDangKy.MatKhau);
                //Xử lý kết quả đăng ký
                XuLyKetQuaDangKy(maNguoiDung, duLieuDangKy);
            }
            catch (Exception loi)
            {
                HienThiLoiDangKy(loi);
            }
        }

        //Lấy dữ liệu đăng ký từ các control trên form
        private DuLieuDangKy LayDuLieuDangKy()
        {
            return new DuLieuDangKy
            {
                TenDangNhap = txtTaiKhoan.Text.Trim(),
                MatKhau = txtMatKhau.Text,
                HoTen = txtHoTen.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };
        }

        //Đăng ký user vào database thông qua stored procedure
        private int DangKyNguoiDungVaoDatabase(DuLieuDangKy duLieuDangKy, string matKhau)
        {
            SqlParameter thamSoMaNguoiDung = new SqlParameter("@UserId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            //Hash mật khẩu trước khi lưu vào database
            string matKhauDaHash = PasswordHelper.HashPassword(matKhau);

            SqlParameter[] thamSo = new SqlParameter[]
            {
                new SqlParameter("@Username", duLieuDangKy.TenDangNhap),
                new SqlParameter("@Password", matKhauDaHash),
                new SqlParameter("@FullName", duLieuDangKy.HoTen),
                new SqlParameter("@Email", duLieuDangKy.Email),
                thamSoMaNguoiDung
            };

            try
            {
                int soDongAnhHuong = DatabaseHelper.ExecuteStoredProcedureNonQuery("sp_UserRegister", thamSo);
                if (thamSoMaNguoiDung.Value != null && thamSoMaNguoiDung.Value != DBNull.Value)
                {
                    int maNguoiDung = Convert.ToInt32(thamSoMaNguoiDung.Value);
                    return maNguoiDung;
                }
                return 0;
            }
            catch (Exception loi)
            {
                //Ném lại exception với thông điệp rõ ràng hơn
                throw new Exception($"Lỗi khi đăng ký vào database: {loi.Message}", loi);
            }
        }

        //Xử lý kết quả đăng ký từ database
        private void XuLyKetQuaDangKy(int maNguoiDung, DuLieuDangKy duLieuDangKy)
        {
            //Lấy các mã lỗi từ database
            int maLoiTenDangNhapTonTai = SystemSettings.ErrorUsernameExists;
            int maLoiEmailTonTai = SystemSettings.ErrorEmailExists;

            if (maNguoiDung > 0)
            {
                XuLyDangKyThanhCong(maNguoiDung, duLieuDangKy);
            }
            else if (maNguoiDung == maLoiTenDangNhapTonTai)
            {
                XuLyLoiTenDangNhapTonTai();
            }
            else if (maNguoiDung == maLoiEmailTonTai)
            {
                XuLyLoiEmailTonTai();
            }
            else
            {
                XuLyLoiDangKyKhongXacDinh();
            }
        }

        //Xử lý khi đăng ký thành công
        private void XuLyDangKyThanhCong(int maNguoiDung, DuLieuDangKy duLieuDangKy)
        {
            TenDangNhapDaDangKy = duLieuDangKy.TenDangNhap;

            string thongBaoThanhCong = $"Đăng ký thành công!\n\n" +
                                   $"Tài khoản: {duLieuDangKy.TenDangNhap}\n" +
                                   $"Họ tên: {duLieuDangKy.HoTen}\n\n" +
                                   $"Bạn có thể đăng nhập ngay bây giờ.";

            MessageBox.Show(
                thongBaoThanhCong,
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //Xử lý lỗi username đã tồn tại
        private void XuLyLoiTenDangNhapTonTai()
        {
            string thongBaoLoi = "Tên đăng nhập đã được sử dụng!\n\nVui lòng chọn tên đăng nhập khác.";

            MessageBox.Show(
                thongBaoLoi,
                "Lỗi đăng ký",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            ChuyenFocusVaChonTatCa(txtTaiKhoan);
        }
        
        //Xử lý lỗi email đã tồn tại        
        private void XuLyLoiEmailTonTai()
        {
            string thongBaoLoi = "Email đã được sử dụng!\n\nVui lòng sử dụng email khác.";

            MessageBox.Show(
                thongBaoLoi,
                "Lỗi đăng ký",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );

            ChuyenFocusVaChonTatCa(txtEmail);
        }

        //Xử lý lỗi không xác định khi đăng ký        
        private void XuLyLoiDangKyKhongXacDinh()
        {
            MessageBox.Show(
                "Đăng ký thất bại!\n\nVui lòng thử lại sau.",
                "Lỗi đăng ký",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
        
        //Focus vào control và select all text
        private void ChuyenFocusVaChonTatCa(Control dong)
        {
            dong.Focus();
            if (dong is TextBox oNhapVanBan)
            {
                oNhapVanBan.SelectAll();
            }
        }

        //Hiển thị thông báo lỗi khi có exception xảy ra
        private void HienThiLoiDangKy(Exception loi)
        {
            MessageBox.Show(
                $"Lỗi khi đăng ký!\n\nChi tiết: {loi.Message}",
                "Lỗi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        //Validate toàn bộ dữ liệu đầu vào của form đăng ký
        private bool KiemTraDuLieu()
        {
            return KiemTraTenDangNhap() &&
                   KiemTraMatKhau() &&
                   KiemTraXacNhanMatKhau() &&
                   KiemTraHoTen() &&
                   KiemTraEmail() &&
                   KiemTraDieuKhoan();
        }

        //Validate tên đăng nhập
        private bool KiemTraTenDangNhap()
        {
            if (string.IsNullOrWhiteSpace(txtTaiKhoan.Text))
            {
                HienThiLoiKiemTra("Vui lòng nhập tên đăng nhập!", txtTaiKhoan);
                return false;
            }

            string tenDangNhap = txtTaiKhoan.Text.Trim();

            int doDaiToiThieu = Helpers.ValidationLimits.MinUsernameLength;
            int doDaiToiDa = Helpers.ValidationLimits.MaxUsernameLength;

            if (tenDangNhap.Length < doDaiToiThieu)
            {
                HienThiLoiKiemTra($"Tên đăng nhập phải có ít nhất {doDaiToiThieu} ký tự!", txtTaiKhoan);
                return false;
            }

            if (tenDangNhap.Length > doDaiToiDa)
            {
                HienThiLoiKiemTra($"Tên đăng nhập không được vượt quá {doDaiToiDa} ký tự!", txtTaiKhoan);
                return false;
            }

            //Kiểm tra định dạng tên đăng nhập - chỉ cho phép chữ, số, dấu gạch dưới
            if (!PasswordHelper.IsValidUsername(tenDangNhap))
            {
                HienThiLoiKiemTra("Tên đăng nhập không hợp lệ!\n\nTên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới (_).\nKhông được chứa ký tự đặc biệt, khoảng trắng hoặc các ký tự nguy hiểm.", txtTaiKhoan);
                return false;
            }

            return true;
        }

        //Validate mật khẩu
        private bool KiemTraMatKhau()
        {
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                HienThiLoiKiemTra("Vui lòng nhập mật khẩu!", txtMatKhau);
                return false;
            }

            string matKhau = txtMatKhau.Text;

            int doDaiToiThieu = Helpers.ValidationLimits.MinPasswordLength;
            int doDaiToiDa = Helpers.ValidationLimits.MaxPasswordLength;

            if (matKhau.Length < doDaiToiThieu)
            {
                HienThiLoiKiemTra($"Mật khẩu phải có ít nhất {doDaiToiThieu} ký tự!", txtMatKhau);
                return false;
            }

            if (matKhau.Length > doDaiToiDa)
            {
                HienThiLoiKiemTra($"Mật khẩu không được vượt quá {doDaiToiDa} ký tự!", txtMatKhau);
                return false;
            }

            //Kiểm tra mật khẩu không chứa SQL injection patterns
            if (!PasswordHelper.IsValidPassword(matKhau))
            {
                HienThiLoiKiemTra("Mật khẩu chứa ký tự không được phép!\n\nVui lòng sử dụng mật khẩu hợp lệ, không chứa các ký tự đặc biệt nguy hiểm.", txtMatKhau);
                return false;
            }

            return true;
        }

        //Validate xác nhận mật khẩu
        private bool KiemTraXacNhanMatKhau()
        {
            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                HienThiLoiKiemTra("Vui lòng xác nhận mật khẩu!", txtXacNhanMatKhau);
                return false;
            }

            if (txtMatKhau.Text != txtXacNhanMatKhau.Text)
            {
                HienThiLoiKiemTra("Mật khẩu xác nhận không khớp!\n\nVui lòng nhập lại.", txtXacNhanMatKhau);
                return false;
            }

            return true;
        }

        //Validate họ tên
        private bool KiemTraHoTen()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                HienThiLoiKiemTra("Vui lòng nhập họ tên!", txtHoTen);
                return false;
            }

            string hoTen = txtHoTen.Text.Trim();
            int doDaiToiDa = Helpers.ValidationLimits.MaxFullNameLength;
            if (hoTen.Length > doDaiToiDa)
            {
                HienThiLoiKiemTra($"Họ tên không được vượt quá {doDaiToiDa} ký tự!", txtHoTen);
                return false;
            }

            //Kiểm tra họ tên không chứa SQL injection patterns
            if (PasswordHelper.ContainsDangerousCharacters(hoTen))
            {
                HienThiLoiKiemTra("Họ tên chứa ký tự không được phép!", txtHoTen);
                return false;
            }

            return true;
        }

        //Validate email
        private bool KiemTraEmail()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                HienThiLoiKiemTra("Vui lòng nhập email!", txtEmail);
                return false;
            }

            string thuDienTu = txtEmail.Text.Trim();

            int doDaiToiDa = Helpers.ValidationLimits.MaxEmailLength;
            if (thuDienTu.Length > doDaiToiDa)
            {
                HienThiLoiKiemTra($"Email không được vượt quá {doDaiToiDa} ký tự!", txtEmail);
                return false;
            }

            //Kiểm tra email không chứa SQL injection patterns
            if (PasswordHelper.ContainsDangerousCharacters(thuDienTu))
            {
                HienThiLoiKiemTra("Email chứa ký tự không được phép!", txtEmail);
                return false;
            }

            if (!EmailHopLe(thuDienTu))
            {
                HienThiLoiKiemTra("Email không hợp lệ!\n\nVui lòng nhập đúng định dạng email.\nVí dụ: example@email.com", txtEmail);
                return false;
            }

            return true;
        }

        //Validate checkbox đồng ý điều khoản
        private bool KiemTraDieuKhoan()
        {
            if (!chkDongYDieuKhoan.Checked)
            {
                HienThiLoiKiemTra("Bạn phải đồng ý với các điều khoản sử dụng để tiếp tục đăng ký!", chkDongYDieuKhoan);
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

            //Select all text nếu là TextBox
            if (dong is TextBox oNhapVanBan)
            {
                oNhapVanBan.SelectAll();
            }
        }

        //Kiểm tra email có hợp lệ không bằng regex pattern
        private bool EmailHopLe(string thuDienTu)
        {
            if (string.IsNullOrWhiteSpace(thuDienTu))
            {
                return false;
            }

            try
            {
                //Regex pattern để kiểm tra định dạng email
                string mauRegex = @"^[a-zA-Z0-9]([a-zA-Z0-9._-]*[a-zA-Z0-9])?@[a-zA-Z0-9]([a-zA-Z0-9.-]*[a-zA-Z0-9])?\.[a-zA-Z]{2,}$";

                //Kiểm tra cơ bản trước
                if (!thuDienTu.Contains("@") || !thuDienTu.Contains("."))
                {
                    return false;
                }

                //Kiểm tra không có khoảng trắng
                if (thuDienTu.Contains(" "))
                {
                    return false;
                }

                //Kiểm tra @ không ở đầu hoặc cuối
                if (thuDienTu.StartsWith("@") || thuDienTu.EndsWith("@"))
                {
                    return false;
                }

                //Kiểm tra . không ở đầu hoặc cuối
                if (thuDienTu.StartsWith(".") || thuDienTu.EndsWith("."))
                {
                    return false;
                }

                //Kiểm tra regex pattern
                return Regex.IsMatch(thuDienTu, mauRegex, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        //Xử lý sự kiện KeyPress cho username - chặn ký tự không hợp lệ ngay khi nhấn phím
        private void txtTaiKhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Cho phép các phím điều khiển (Backspace, Delete, Tab, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            //Kiểm tra ký tự có hợp lệ không (chỉ cho phép chữ, số, dấu gạch dưới)
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '_')
            {
                e.Handled = true;
                MessageBox.Show(
                    "Tên đăng nhập chỉ được chứa chữ cái, số và dấu gạch dưới (_)!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            //Kiểm tra nếu chuỗi sau khi thêm ký tự mới có chứa pattern nguy hiểm không
            int viTriConTro = txtTaiKhoan.SelectionStart;
            string vanBanHienTai = txtTaiKhoan.Text;
            string chuoiKiemTra = vanBanHienTai.Substring(0, viTriConTro) + e.KeyChar + vanBanHienTai.Substring(viTriConTro);
            
            if (PasswordHelper.ContainsDangerousCharactersStrict(chuoiKiemTra))
            {
                e.Handled = true;
                MessageBox.Show(
                    "Tên đăng nhập không được chứa các ký tự hoặc pattern nguy hiểm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        //Xử lý sự kiện TextChanged cho password - kiểm tra và chặn pattern nguy hiểm
        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {
            //Tránh recursive calls
            if (_dangKiemTra) return;

            string vanBanHienTai = txtMatKhau.Text;

            //Nếu text rỗng, lưu và return
            if (string.IsNullOrEmpty(vanBanHienTai))
            {
                _matKhauHopLeCuoi = "";
                return;
            }

            //Kiểm tra nếu có ký tự/pattern nguy hiểm
            if (PasswordHelper.ContainsDangerousCharacters(vanBanHienTai))
            {
                _dangKiemTra = true;

                int viTriBatDau = txtMatKhau.SelectionStart;

                //Hiển thị thông báo
                MessageBox.Show(
                    "Mật khẩu chứa ký tự hoặc pattern không được phép!\n\nVui lòng không sử dụng các pattern SQL injection như: OR, AND, UNION, SELECT, DROP, --, ;, v.v.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                //Khôi phục về giá trị hợp lệ trước đó
                txtMatKhau.Text = _matKhauHopLeCuoi;
                txtMatKhau.SelectionStart = Math.Min(viTriBatDau - 1, txtMatKhau.Text.Length);
                txtMatKhau.Focus();

                _dangKiemTra = false;
            }
            else
            {
                //Lưu giá trị hợp lệ
                _matKhauHopLeCuoi = vanBanHienTai;
            }
        }

        //Xử lý sự kiện TextChanged cho password confirmation
        private void txtXacNhanMatKhau_TextChanged(object sender, EventArgs e)
        {
            //Tránh recursive calls
            if (_dangKiemTra) return;

            string vanBanHienTai = txtXacNhanMatKhau.Text;

            //Nếu text rỗng, lưu và return
            if (string.IsNullOrEmpty(vanBanHienTai))
            {
                _xacNhanMatKhauHopLeCuoi = "";
                return;
            }

            //Kiểm tra nếu có ký tự/pattern nguy hiểm
            if (PasswordHelper.ContainsDangerousCharacters(vanBanHienTai))
            {
                _dangKiemTra = true;

                int viTriBatDau = txtXacNhanMatKhau.SelectionStart;

                //Hiển thị thông báo
                MessageBox.Show(
                    "Mật khẩu xác nhận chứa ký tự hoặc pattern không được phép!\n\nVui lòng không sử dụng các pattern SQL injection như: OR, AND, UNION, SELECT, DROP, --, ;, v.v.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                //Khôi phục về giá trị hợp lệ trước đó
                txtXacNhanMatKhau.Text = _xacNhanMatKhauHopLeCuoi;
                txtXacNhanMatKhau.SelectionStart = Math.Min(viTriBatDau - 1, txtXacNhanMatKhau.Text.Length);
                txtXacNhanMatKhau.Focus();

                _dangKiemTra = false;
            }
            else
            {
                //Lưu giá trị hợp lệ
                _xacNhanMatKhauHopLeCuoi = vanBanHienTai;
            }
        }

        #endregion

        #region Nested Classes - Lớp lồng nhau

        //Lớp chứa thông tin đăng ký từ form
        private class DuLieuDangKy
        {
            //Tên đăng nhập
            public string TenDangNhap { get; set; }
            //Mật khẩu
            public string MatKhau { get; set; }
            //Họ tên đầy đủ
            public string HoTen { get; set; }
            //Email
            public string Email { get; set; }
        }

        #endregion
    }
}
