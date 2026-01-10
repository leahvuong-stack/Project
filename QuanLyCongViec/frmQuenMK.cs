using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    public partial class frmQuenMK : Form
    {
        #region Constants - Hằng số
        private const int DAT_LAI_MAT_KHAU_THANH_CONG = 1;
        private const int KHONG_TIM_THAY_USER = -1;
        private const int TAI_KHOAN_BI_VO_HIEU_HOA = -2;
        #endregion

        #region Constructor - Hàm khởi tạo
        //Khởi tạo form quên mật khẩu
        public frmQuenMK()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers - Xử lý sự kiện
        //Xử lý sự kiện click nút Xác nhận
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            ThucHienDatLaiMatKhau();
        }

        //Xử lý sự kiện click nút Hủy
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Xử lý sự kiện click link Đăng nhập
        private void linklblDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Xử lý sự kiện nhấn phím Enter trong textbox Username/Email
        private void txtUsernameOrEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtMatKhauMoi.Focus();
            }
        }

        //Xử lý sự kiện nhấn phím Enter trong textbox Mật khẩu mới
        private void txtMatKhauMoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtXacNhanMatKhau.Focus();
            }
        }

        //Xử lý sự kiện nhấn phím Enter trong textbox Xác nhận mật khẩu
        private void txtXacNhanMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ThucHienDatLaiMatKhau();
            }
        }

        #endregion

        #region Private Methods - Các phương thức riêng tư
        //Thực hiện reset mật khẩu
        private void ThucHienDatLaiMatKhau()
        {
            try
            {
                //Validate dữ liệu đầu vào
                if (!KiemTraDuLieu())
                {
                    return;
                }

                //Lấy thông tin từ form
                string tenDangNhapHoacEmail = txtUsernameOrEmail.Text.Trim();
                string matKhauMoi = txtMatKhauMoi.Text;

                //Reset password trong database
                int ketQua = DatLaiMatKhauTrongDatabase(tenDangNhapHoacEmail, matKhauMoi);

                //Xử lý kết quả
                XuLyKetQuaDatLaiMatKhau(ketQua);
            }
            catch (Exception loi)
            {
                MessageBox.Show(
                    $"Lỗi khi reset mật khẩu: {loi.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //Kiểm tra dữ liệu đầu vào
        private bool KiemTraDuLieu()
        {
            return KiemTraTenDangNhapHoacEmail() &&
                   KiemTraMatKhauMoi() &&
                   KiemTraXacNhanMatKhau();
        }

        //Kiểm tra tên đăng nhập hoặc email
        private bool KiemTraTenDangNhapHoacEmail()
        {
            if (string.IsNullOrWhiteSpace(txtUsernameOrEmail.Text))
            {
                HienThiThongBaoLoi("Vui lòng nhập tên đăng nhập hoặc email!", txtUsernameOrEmail);
                return false;
            }

            string tenDangNhapHoacEmail = txtUsernameOrEmail.Text.Trim();

            //Kiểm tra không chứa SQL injection patterns
            if (PasswordHelper.ContainsDangerousCharacters(tenDangNhapHoacEmail))
            {
                HienThiThongBaoLoi("Tên đăng nhập hoặc email chứa ký tự không được phép!", txtUsernameOrEmail);
                txtUsernameOrEmail.SelectAll();
                return false;
            }

            return true;
        }

        //Kiểm tra mật khẩu mới
        private bool KiemTraMatKhauMoi()
        {
            if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
            {
                HienThiThongBaoLoi("Vui lòng nhập mật khẩu mới!", txtMatKhauMoi);
                return false;
            }

            //Kiểm tra độ dài mật khẩu
            int doDaiMatKhauToiThieu = ValidationLimits.MinPasswordLength;
            int doDaiMatKhauToiDa = ValidationLimits.MaxPasswordLength;

            if (txtMatKhauMoi.Text.Length < doDaiMatKhauToiThieu)
            {
                HienThiThongBaoLoi($"Mật khẩu phải có ít nhất {doDaiMatKhauToiThieu} ký tự!", txtMatKhauMoi);
                return false;
            }

            if (txtMatKhauMoi.Text.Length > doDaiMatKhauToiDa)
            {
                HienThiThongBaoLoi($"Mật khẩu không được vượt quá {doDaiMatKhauToiDa} ký tự!", txtMatKhauMoi);
                return false;
            }

            //Kiểm tra không chứa SQL injection patterns
            if (!PasswordHelper.IsValidPassword(txtMatKhauMoi.Text))
            {
                HienThiThongBaoLoi("Mật khẩu chứa ký tự không được phép!\n\nVui lòng sử dụng mật khẩu hợp lệ, không chứa các ký tự đặc biệt nguy hiểm.", txtMatKhauMoi);
                return false;
            }

            return true;
        }

        //Kiểm tra xác nhận mật khẩu
        private bool KiemTraXacNhanMatKhau()
        {
            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                HienThiThongBaoLoi("Vui lòng xác nhận mật khẩu mới!", txtXacNhanMatKhau);
                return false;
            }

            //Kiểm tra mật khẩu khớp
            if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                HienThiThongBaoLoi("Mật khẩu xác nhận không khớp!", txtXacNhanMatKhau);
                txtXacNhanMatKhau.SelectAll();
                return false;
            }

            return true;
        }

        //Hiển thị thông báo lỗi và focus vào control
        private void HienThiThongBaoLoi(string thongBao, Control dong)
        {
            MessageBox.Show(
                thongBao,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            dong.Focus();
        }

        //Đặt lại mật khẩu trong database
        //Trả về: 1 (thành công), -1 (không tìm thấy user), -2 (tài khoản bị vô hiệu hóa)
        private int DatLaiMatKhauTrongDatabase(string tenDangNhapHoacEmail, string matKhauMoi)
        {
            //Hash mật khẩu trước khi lưu vào database
            string matKhauDaHash = PasswordHelper.HashPassword(matKhauMoi);

            SqlParameter[] thamSo = new SqlParameter[]
            {
                new SqlParameter("@UsernameOrEmail", tenDangNhapHoacEmail),
                new SqlParameter("@NewPassword", matKhauDaHash),
                new SqlParameter("@UserId", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            DatabaseHelper.ExecuteStoredProcedureNonQuery("sp_ResetPassword", thamSo);

            int ketQua = Convert.ToInt32(thamSo[3].Value);
            return ketQua;
        }

        //Xử lý kết quả đặt lại mật khẩu
        private void XuLyKetQuaDatLaiMatKhau(int ketQua)
        {
            switch (ketQua)
            {
                case DAT_LAI_MAT_KHAU_THANH_CONG:
                    MessageBox.Show(
                        "Đặt lại mật khẩu thành công!\nVui lòng đăng nhập lại với mật khẩu mới.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case KHONG_TIM_THAY_USER:
                    MessageBox.Show(
                        "Không tìm thấy tài khoản với thông tin đã nhập!\nVui lòng kiểm tra lại tên đăng nhập hoặc email.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtUsernameOrEmail.Focus();
                    txtUsernameOrEmail.SelectAll();
                    break;

                case TAI_KHOAN_BI_VO_HIEU_HOA:
                    MessageBox.Show(
                        "Tài khoản của bạn đã bị vô hiệu hóa!\nVui lòng liên hệ quản trị viên để được hỗ trợ.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtUsernameOrEmail.Focus();
                    txtUsernameOrEmail.SelectAll();
                    break;

                default:
                    MessageBox.Show(
                        "Có lỗi xảy ra khi reset mật khẩu!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    break;
            }
        }

        #endregion
    }
}

