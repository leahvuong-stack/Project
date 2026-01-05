using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Uncomment dòng dưới để mở form tính hash (dùng để tạo script SQL)
            // Application.Run(new frmHashPassword());
            
            // Form đăng nhập chính
            frmDangNhap loginForm = new frmDangNhap();
            FontHelper.SetUnicodeFont(loginForm);
            Application.Run(loginForm);
        }
    }
}

