using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongViec.Helpers
{
    public static class FontHelper
    {
        public static Font DefaultUnicodeFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

        public static Font DataGridViewFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

        public static void SetUnicodeFont(Form form)
        {
            if (form == null) return;

            // Set font cho form
            form.Font = DefaultUnicodeFont;

            // Set font cho tất cả controls trong form
            SetUnicodeFontRecursive(form);
        }

        private static void SetUnicodeFontRecursive(Control control)
        {
            if (control == null) return;

            // Set font cho control hiện tại (trừ DataGridView - sẽ set riêng)
            if (!(control is DataGridView))
            {
                control.Font = DefaultUnicodeFont;
            }
            else
            {
                // DataGridView cần set font riêng
                DataGridView dgv = control as DataGridView;
                dgv.Font = DataGridViewFont;
                dgv.DefaultCellStyle.Font = DataGridViewFont;
            }

            // Set font cho tất cả controls con
            foreach (Control child in control.Controls)
            {
                SetUnicodeFontRecursive(child);
            }
        }

        public static void SetUnicodeFontForDataGridView(DataGridView dataGridView)
        {
            if (dataGridView == null) return;

            dataGridView.Font = DataGridViewFont;
            dataGridView.DefaultCellStyle.Font = DataGridViewFont;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = DataGridViewFont;
            dataGridView.RowHeadersDefaultCellStyle.Font = DataGridViewFont;
            
            // Đảm bảo DataGridView hỗ trợ Unicode
            dataGridView.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.CurrentCulture;
            
            // Set encoding cho tất cả các cột
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.DefaultCellStyle.Font = DataGridViewFont;
            }
        }
    }
}
