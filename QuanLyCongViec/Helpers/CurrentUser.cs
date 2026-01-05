using System;

namespace QuanLyCongViec.Helpers
{
    public static class CurrentUser
    {
        private static int _userId = 0;
        private static string _username = string.Empty;
        private static string _fullName = string.Empty;

        public static void SetCurrentUser(int userId, string username, string fullName)
        {
            _userId = userId;
            _username = username;
            _fullName = fullName;
        }

        public static int GetUserId()
        {
            return _userId;
        }

        public static string GetUsername()
        {
            return _username;
        }

        public static string GetFullName()
        {
            return _fullName;
        }

        public static bool IsLoggedIn()
        {
            return _userId > 0;
        }

        public static void Clear()
        {
            _userId = 0;
            _username = string.Empty;
            _fullName = string.Empty;
        }
    }
}

