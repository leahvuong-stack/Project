using System;
using System.Data;
using System.Collections.Generic;
using QuanLyCongViec.DataAccess;

namespace QuanLyCongViec.Helpers
{
    public static class ValidationLimits
    {
        // Cache để tránh query database nhiều lần
        private static Dictionary<string, int> _cache = new Dictionary<string, int>();
        private static bool _isLoaded = false;

        public static int MinUsernameLength
        {
            get { return GetLimit("Username", "Min"); }
        }

        public static int MaxUsernameLength
        {
            get { return GetLimit("Username", "Max"); }
        }

        public static int MinPasswordLength
        {
            get { return GetLimit("PasswordHash", "Min"); }
        }

        public static int MaxPasswordLength
        {
            get { return GetLimit("PasswordHash", "Max"); }
        }

        public static int MinEmailLength
        {
            get { return GetLimit("Email", "Min"); }
        }

        public static int MaxEmailLength
        {
            get { return GetLimit("Email", "Max"); }
        }

        public static int MinFullNameLength
        {
            get { return GetLimit("FullName", "Min"); }
        }

        public static int MaxFullNameLength
        {
            get { return GetLimit("FullName", "Max"); }
        }

        private static int GetLimit(string fieldName, string limitType)
        {
            // Load từ database lần đầu tiên
            if (!_isLoaded)
            {
                LoadLimitsFromDatabase();
            }

            // Tạo key để lấy từ cache
            string key = $"{fieldName}_{limitType}";

            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }

            // Nếu không có trong cache, trả về giá trị mặc định
            // (fallback nếu không kết nối được database)
            return GetDefaultValue(fieldName, limitType);
        }

        private static void LoadLimitsFromDatabase()
        {
            try
            {
                // Gọi stored procedure để lấy các giới hạn
                DataTable bangDuLieu = DatabaseHelper.ExecuteStoredProcedure("sp_GetValidationLimits");

                // Lưu vào cache
                foreach (DataRow dong in bangDuLieu.Rows)
                {
                    string tenTruong = dong["TenTruong"].ToString();
                    int doDaiToiDa = Convert.ToInt32(dong["DoDaiToiDa"]);
                    int doDaiToiThieu = Convert.ToInt32(dong["DoDaiToiThieu"]);

                    _cache[$"{tenTruong}_Max"] = doDaiToiDa;
                    _cache[$"{tenTruong}_Min"] = doDaiToiThieu;
                }

                _isLoaded = true;
            }
            catch
            {
                // Nếu lỗi, sử dụng giá trị mặc định
                // Không throw exception để không làm gián đoạn ứng dụng
                _isLoaded = true; // Đánh dấu đã load (dù thất bại) để không retry liên tục
            }
        }

        private static int GetDefaultValue(string fieldName, string limitType)
        {
            // Các giá trị mặc định dựa trên định nghĩa trong database
            // (fallback nếu không query được)
            switch (fieldName)
            {
                case "Username":
                    return limitType == "Min" ? 3 : 50;
                case "PasswordHash":
                    return limitType == "Min" ? 6 : 255;
                case "Email":
                    return limitType == "Min" ? 5 : 100;
                case "FullName":
                    return limitType == "Min" ? 1 : 100;
                default:
                    return limitType == "Min" ? 1 : 100;
            }
        }

        public static void Reset()
        {
            _cache.Clear();
            _isLoaded = false;
        }
    }
}
