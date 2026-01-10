using System;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyCongViec.Helpers
{
    public static class PasswordHelper
    {
        // Salt mặc định để hash password (trong thực tế nên random cho mỗi user)
        private const string DEFAULT_SALT = "QuanLyCongViec_Salt_2024";

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống", nameof(password));
            }

            // Tạo salt từ password (có thể cải thiện bằng random salt cho mỗi user)
            string salt = DEFAULT_SALT + password.Length.ToString();

            // Kết hợp password + salt
            string combined = password + salt;

            // Hash bằng SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combined));

                // Chuyển đổi byte array thành string hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string password, string storedPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedPasswordHash))
            {
                return false;
            }

            // Hash password và so sánh với stored hash
            string hashedPassword = HashPassword(password);
            return hashedPassword.Equals(storedPasswordHash, StringComparison.Ordinal);
        }

        /// <summary>
        /// Kiểm tra input có chứa ký tự nguy hiểm (SQL injection patterns) không
        /// Dùng cho password - cho phép ký tự đặc biệt nhưng chặn SQL injection patterns
        /// </summary>
        public static bool ContainsDangerousCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            string upperInput = input.ToUpper();
            
            // Các pattern SQL injection nguy hiểm - chặn các từ khóa SQL và pattern injection
            // KHÔNG chặn single quote/double quote đơn lẻ (cho phép trong password)
            string[] dangerousPatterns = {
                "--", "/*", "*/", ";",
                " OR ", " AND ", " UNION ", " SELECT ", " DROP ",
                " INSERT ", " UPDATE ", " DELETE ", " EXEC ", " EXECUTE ",
                " CREATE ", " ALTER ", " TRUNCATE ", " GRANT ", " REVOKE ",
                " OR'", " OR\"", "'OR", "\"OR", "1=1", "1='1'", "'='",
                " OR 1", " AND 1", " OR '", " AND '", " OR \"", " AND \"",
                "' OR", "\" OR", " OR ", " AND ", " UNION", " SELECT", " DROP"
            };

            foreach (string pattern in dangerousPatterns)
            {
                if (upperInput.Contains(pattern.ToUpper()))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra input có chứa ký tự nguy hiểm (chặt chẽ hơn - dùng cho username)
        /// Chặn cả single quote và double quote
        /// </summary>
        public static bool ContainsDangerousCharactersStrict(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            // Chặn single quote và double quote (nguy hiểm cho SQL injection)
            if (input.Contains("'") || input.Contains("\""))
                return true;

            // Chặn các pattern SQL injection khác
            return ContainsDangerousCharacters(input);
        }

        /// <summary>
        /// Validate username: chỉ cho phép chữ cái, số, và dấu gạch dưới
        /// </summary>
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            // Username chỉ được chứa chữ cái, số, và dấu gạch dưới
            foreach (char c in username)
            {
                if (!char.IsLetterOrDigit(c) && c != '_')
                    return false;
            }

            // Không được chứa SQL injection patterns (strict - chặn cả quote)
            if (ContainsDangerousCharactersStrict(username))
                return false;

            return true;
        }

        /// <summary>
        /// Validate password: không được chứa SQL injection patterns
        /// Cho phép một số ký tự đặc biệt trong password (để password mạnh hơn)
        /// nhưng vẫn chặn các pattern SQL injection nguy hiểm
        /// </summary>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // Password không được chứa SQL injection patterns (không strict - cho phép ký tự đặc biệt)
            // Chỉ chặn các pattern SQL keywords, không chặn single/double quote đơn lẻ
            if (ContainsDangerousCharacters(password))
                return false;

            return true;
        }
    }
}

