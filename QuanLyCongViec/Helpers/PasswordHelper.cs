using System;

namespace QuanLyCongViec.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống", nameof(password));
            }

            // Trả về password gốc, không hash
            return password;
        }

        public static bool VerifyPassword(string password, string storedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedPassword))
            {
                return false;
            }

            // So sánh trực tiếp, không hash
            return password.Equals(storedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}

