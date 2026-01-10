using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongViec.Controls;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    /// <summary>
    /// Form hiển thị lịch công việc theo tháng
    /// </summary>
    public partial class frmThoiKhoaBieu : Form
    {
        private int _userId; // UserId của user hiện tại

        public frmThoiKhoaBieu()
        {
            try
            {
                InitializeComponent();
                ConfigureDataGridView();
                // Lấy UserId từ CurrentUser
                _userId = CurrentUser.GetUserId() > 0 ? CurrentUser.GetUserId() : 1;
                LoadCalendar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Constructor nhận UserId (để truyền từ form khác)
        public frmThoiKhoaBieu(int userId)
        {
            try
            {
                InitializeComponent();
                ConfigureDataGridView();
                _userId = userId;
                LoadCalendar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}\n\n{ex.StackTrace}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cấu hình styling cho DataGridView
        /// </summary>
        private void ConfigureDataGridView()
        {
            // Styling cho DataGridView
            dgvTasks.Font = new Font("Segoe UI", 9F);
            dgvTasks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvTasks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTasks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvTasks.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
            dgvTasks.ColumnHeadersHeight = 35;
            dgvTasks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvTasks.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvTasks.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvTasks.RowTemplate.Height = 30;
        }

        /// <summary>
        /// Load calendar với UserId hiện tại
        /// </summary>
        private void LoadCalendar()
        {
            try
            {
                // Sử dụng _userId đã được set trong constructor
                calendarView.SetUserId(_userId);
                
                // Cập nhật thông tin
                UpdateTaskInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Event khi click vào ngày trong calendar
        /// </summary>
        private void CalendarView_DateClicked(object sender, DateTime selectedDate)
        {
            LoadTasksForDate(selectedDate);
        }

        /// <summary>
        /// Load danh sách tasks cho ngày được chọn
        /// </summary>
        private void LoadTasksForDate(DateTime selectedDate)
        {
            try
            {
                lblSelectedDate.Text = $"📅 {selectedDate:dddd, dd/MM/yyyy}";

                // Sử dụng _userId của user hiện tại
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure(
                    "sp_GetTasksByDate",
                    new System.Data.SqlClient.SqlParameter("@UserId", _userId),
                    new System.Data.SqlClient.SqlParameter("@SelectedDate", selectedDate.Date)
                );

                if (dt.Rows.Count > 0)
                {
                    // Cấu hình columns
                    dgvTasks.DataSource = dt;
                    dgvTasks.Columns["Id"].Visible = false;
                    dgvTasks.Columns["Description"].Visible = false;
                    dgvTasks.Columns["CreatedDate"].Visible = false;
                    dgvTasks.Columns["CompletedDate"].Visible = false;
                    dgvTasks.Columns["PriorityColor"].Visible = false;
                    dgvTasks.Columns["StatusLabel"].Visible = false;
                    dgvTasks.Columns["DateRangeLabel"].Visible = false; // ✅ Ẩn cột Phạm vi
                    dgvTasks.Columns["StartDate"].Visible = false; // ✅ Ẩn cột Ngày bắt đầu

                    dgvTasks.Columns["Title"].HeaderText = "Tiêu đề";
                    dgvTasks.Columns["Priority"].HeaderText = "Ưu tiên";
                    dgvTasks.Columns["Status"].HeaderText = "Trạng thái";
                    dgvTasks.Columns["Category"].HeaderText = "Danh mục";
                    dgvTasks.Columns["DueDate"].HeaderText = "Ngày kết thúc";

                    // Sắp xếp lại thứ tự columns
                    dgvTasks.Columns["Title"].DisplayIndex = 0;
                    dgvTasks.Columns["DueDate"].DisplayIndex = 1;
                    dgvTasks.Columns["Priority"].DisplayIndex = 2;
                    dgvTasks.Columns["Status"].DisplayIndex = 3;
                    dgvTasks.Columns["Category"].DisplayIndex = 4;

                    // Format ngày
                    dgvTasks.Columns["DueDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                    
                    // Set AutoSizeMode Fill để chiếm toàn bộ chiều rộng
                    dgvTasks.Columns["Title"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvTasks.Columns["DueDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTasks.Columns["Priority"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTasks.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTasks.Columns["Category"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    // Tắt sort cho tất cả các cột
                    dgvTasks.Columns["Title"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTasks.Columns["DueDate"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTasks.Columns["Priority"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTasks.Columns["Status"].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTasks.Columns["Category"].SortMode = DataGridViewColumnSortMode.NotSortable;

                    // Tô màu theo priority
                    dgvTasks.CellFormatting += (s, e) =>
                    {
                        if (e.RowIndex >= 0 && dgvTasks.Columns[e.ColumnIndex].Name == "Priority")
                        {
                            string priority = dgvTasks.Rows[e.RowIndex].Cells["Priority"].Value?.ToString();
                            switch (priority)
                            {
                                case "High":
                                    e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                                    e.CellStyle.ForeColor = Color.White;
                                    e.Value = "⚠️ Cao";
                                    break;
                                case "Medium":
                                    e.CellStyle.BackColor = Color.FromArgb(243, 156, 18);
                                    e.CellStyle.ForeColor = Color.White;
                                    e.Value = "⚡ Trung bình";
                                    break;
                                case "Low":
                                    e.CellStyle.BackColor = Color.FromArgb(149, 165, 166);
                                    e.CellStyle.ForeColor = Color.White;
                                    e.Value = "✓ Thấp";
                                    break;
                            }
                        }
                    };

                    lblTaskInfo.Text = $"✅ Tìm thấy {dt.Rows.Count} công việc trong ngày {selectedDate:dd/MM/yyyy}";
                }
                else
                {
                    dgvTasks.DataSource = null;
                    lblTaskInfo.Text = $"📭 Không có công việc nào trong ngày {selectedDate:dd/MM/yyyy}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách công việc: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thông tin task
        /// </summary>
        private void UpdateTaskInfo()
        {
            try
            {
                // Đếm tổng số tasks của user hiện tại
                string query = "SELECT COUNT(*) FROM Tasks WHERE UserId = @UserId AND IsDeleted = 0";
                int totalTasks = Convert.ToInt32(DatabaseHelper.ExecuteScalar(query,
                    new System.Data.SqlClient.SqlParameter("@UserId", _userId)));

                lblTaskInfo.Text = $"📊 Tổng số công việc: {totalTasks}\n💡 Click vào ngày để xem chi tiết";
            }
            catch
            {
                // Ignore errors
            }
        }

        /// <summary>
        /// Nút làm mới
        /// </summary>
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            calendarView.RefreshData();
            lblSelectedDate.Text = "Chọn một ngày để xem công việc";
            dgvTasks.DataSource = null;
            UpdateTaskInfo();
            MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
