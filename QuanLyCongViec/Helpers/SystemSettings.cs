using System;
using System.Data;
using System.Collections.Generic;
using QuanLyCongViec.DataAccess;
using System.Data.SqlClient;

namespace QuanLyCongViec.Helpers
{
    public static class SystemSettings
    {
        // Cache để tránh query database nhiều lần
        private static Dictionary<string, string> _cache = new Dictionary<string, string>();
        private static bool _isLoaded = false;

        public static int MaxLoginAttempts
        {
            get { return GetIntSetting("MAX_LOGIN_ATTEMPTS", 5); }
        }

        public static int LockoutMinutes
        {
            get { return GetIntSetting("LOCKOUT_MINUTES", 15); }
        }

        public static int ErrorUsernameExists
        {
            get { return GetIntSetting("ERROR_USERNAME_EXISTS", -1); }
        }

        public static int ErrorEmailExists
        {
            get { return GetIntSetting("ERROR_EMAIL_EXISTS", -2); }
        }

        private static int GetIntSetting(string settingKey, int defaultValue)
        {
            string value = GetSetting(settingKey);
            if (!string.IsNullOrEmpty(value) && int.TryParse(value, out int result))
            {
                return result;
            }
            return defaultValue; // Fallback nếu không lấy được
        }

        private static string GetSetting(string settingKey)
        {
            // Load từ database lần đầu tiên
            if (!_isLoaded)
            {
                LoadSettingsFromDatabase();
            }

            // Lấy từ cache
            if (_cache.ContainsKey(settingKey))
            {
                return _cache[settingKey];
            }

            // Nếu không có trong cache, trả về null
            return null;
        }

        private static void LoadSettingsFromDatabase()
        {
            try
            {
                // Gọi stored procedure để lấy tất cả settings
                DataTable bangDuLieu = DatabaseHelper.ExecuteStoredProcedure("sp_GetAllSystemSettings");

                // Lưu vào cache
                foreach (DataRow dong in bangDuLieu.Rows)
                {
                    string key = dong["SettingKey"].ToString();
                    string value = dong["SettingValue"].ToString();
                    _cache[key] = value;
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

        public static string GetSettingValue(string settingKey)
        {
            try
            {
                SqlParameter[] thamSo = new SqlParameter[]
                {
                    new SqlParameter("@SettingKey", settingKey)
                };

                DataTable bangDuLieu = DatabaseHelper.ExecuteStoredProcedure("sp_GetSystemSetting", thamSo);

                if (bangDuLieu.Rows.Count > 0)
                {
                    return bangDuLieu.Rows[0]["SettingValue"].ToString();
                }
            }
            catch
            {
                // Bỏ qua lỗi
            }

            return null;
        }

        public static void Reset()
        {
            _cache.Clear();
            _isLoaded = false;
        }
    }
}
