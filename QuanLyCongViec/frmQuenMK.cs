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
            PerformResetPassword();
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
                PerformResetPassword();
            }
        }

        #endregion

        #region Private Methods - Các phương thức riêng tư
        //Thực hiện reset mật khẩu
        private void PerformResetPassword()
        {
            try
            {
                // Validate dữ liệu đầu vào
                if (!ValidateInput())
                {
                    return;
                }

                // Lấy thông tin từ form
                string usernameOrEmail = txtUsernameOrEmail.Text.Trim();
                string matKhauMoi = txtMatKhauMoi.Text;

                // Reset password trong database
                int result = ResetPasswordInDatabase(usernameOrEmail, matKhauMoi);

                // Xử lý kết quả
                ProcessResetResult(result);
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

        /// <summary>
        /// Validate dữ liệu đầu vào
        /// </summary>
        /// <returns>True nếu hợp lệ, False nếu không hợp lệ</returns>
        private bool ValidateInput()
        {
            // Kiểm tra username/email
            if (string.IsNullOrWhiteSpace(txtUsernameOrEmail.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên đăng nhập hoặc email!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtUsernameOrEmail.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mới
            if (string.IsNullOrWhiteSpace(txtMatKhauMoi.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu mới!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtMatKhauMoi.Focus();
                return false;
            }

            // Kiểm tra độ dài mật khẩu
            int minPasswordLength = ValidationLimits.MinPasswordLength;
            int maxPasswordLength = ValidationLimits.MaxPasswordLength;

            if (txtMatKhauMoi.Text.Length < minPasswordLength)
            {
                MessageBox.Show(
                    $"Mật khẩu phải có ít nhất {minPasswordLength} ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtMatKhauMoi.Focus();
                return false;
            }

            if (txtMatKhauMoi.Text.Length > maxPasswordLength)
            {
                MessageBox.Show(
                    $"Mật khẩu không được vượt quá {maxPasswordLength} ký tự!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtMatKhauMoi.Focus();
                return false;
            }

            // Kiểm tra xác nhận mật khẩu
            if (string.IsNullOrWhiteSpace(txtXacNhanMatKhau.Text))
            {
                MessageBox.Show(
                    "Vui lòng xác nhận mật khẩu mới!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtXacNhanMatKhau.Focus();
                return false;
            }

            // Kiểm tra mật khẩu khớp
            if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show(
                    "Mật khẩu xác nhận không khớp!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtXacNhanMatKhau.Focus();
                txtXacNhanMatKhau.SelectAll();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reset password trong database
        /// </summary>
        /// <returns>1: Thành công, -1: Không tìm thấy user, -2: Tài khoản bị vô hiệu hóa</returns>
        private int ResetPasswordInDatabase(string usernameOrEmail, string newPassword)
        {
            SqlParameter[] thamSo = new SqlParameter[]
            {
                new SqlParameter("@UsernameOrEmail", usernameOrEmail),
                new SqlParameter("@NewPassword", newPassword),
                new SqlParameter("@UserId", SqlDbType.Int) { Direction = ParameterDirection.Output },
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            DatabaseHelper.ExecuteStoredProcedureNonQuery("sp_ResetPassword", thamSo);

            int result = Convert.ToInt32(thamSo[3].Value);
            return result;
        }

        //Xử lý kết quả reset password
        private void ProcessResetResult(int result)
        {
            switch (result)
            {
                case 1: // Thành công
                    MessageBox.Show(
                        "Đặt lại mật khẩu thành công!\nVui lòng đăng nhập lại với mật khẩu mới.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case -1: // Không tìm thấy user
                    MessageBox.Show(
                        "Không tìm thấy tài khoản với thông tin đã nhập!\nVui lòng kiểm tra lại tên đăng nhập hoặc email.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtUsernameOrEmail.Focus();
                    txtUsernameOrEmail.SelectAll();
                    break;

                case -2: // Tài khoản bị vô hiệu hóa
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

