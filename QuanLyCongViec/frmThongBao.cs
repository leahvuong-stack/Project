using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    public partial class frmThongBao : Form
    {
        private int _userId;
        private string _currentUserName = "";

        public frmThongBao(int userId = 0)
        {
            InitializeComponent();
            _userId = userId;

            // Lấy UserId từ CurrentUser nếu không được truyền vào
            if (_userId == 0)
            {
                _userId = CurrentUser.GetUserId();
            }
            _currentUserName = CurrentUser.GetFullName();
            this.Load += (s, e) => {
                SetupGrid();
                HienThiDanhSach();
            };

            // Đăng ký event handlers cho các buttons
            btnMarkAsRead.Click += btnMarkAsRead_Click;
            btnDelete.Click += btnDelete_Click;

            // TẢI LẠI: Load lại dữ liệu từ database
            btnReload.Click += (s, e) => {
                HienThiDanhSach();
                MessageBox.Show("Đã tải lại danh sách thông báo!", "Tải lại", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            btnClose.Click += (s, e) => this.Close();
        }

        private void SetupGrid()
        {
            dgvThongBao.Columns.Clear();
            dgvThongBao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongBao.AllowUserToAddRows = false;
            dgvThongBao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThongBao.ReadOnly = true;

            dgvThongBao.Columns.Add("NguoiDung", "Người dùng");
            dgvThongBao.Columns.Add("MaCV", "Mã CV");
            dgvThongBao.Columns.Add("TieuDe", "Tiêu đề");
            dgvThongBao.Columns.Add("HanChot", "Hạn chót");
            dgvThongBao.Columns.Add("UuTien", "Ưu tiên");
            dgvThongBao.Columns.Add("ConNgay", "Còn (ngày)");
            dgvThongBao.Columns.Add("TrangThai", "Trạng thái");
        }

        private void HienThiDanhSach()
        {
            try
            {
                dgvThongBao.Rows.Clear();

                // Lấy tasks quá hạn và sắp đến hạn (trong 7 ngày tới)
                // Gọi 2 lần: một lần cho overdue, một lần cho due soon, sau đó merge và loại bỏ duplicates
                DataTable dtOverdue = DatabaseHelper.ExecuteStoredProcedure("sp_GetTasksByFilter",
                    new SqlParameter("@UserId", _userId),
                    new SqlParameter("@IsOverdue", true)
                );

                DataTable dtDueSoon = DatabaseHelper.ExecuteStoredProcedure("sp_GetTasksByFilter",
                    new SqlParameter("@UserId", _userId),
                    new SqlParameter("@IsDueSoon", true)
                );

                // Merge và loại bỏ duplicates bằng Dictionary
                Dictionary<int, DataRow> taskDict = new Dictionary<int, DataRow>();

                if (dtOverdue != null)
                {
                    foreach (DataRow row in dtOverdue.Rows)
                    {
                        int taskId = Convert.ToInt32(row["Id"]);
                        if (!taskDict.ContainsKey(taskId))
                        {
                            taskDict[taskId] = row;
                        }
                    }
                }

                if (dtDueSoon != null)
                {
                    foreach (DataRow row in dtDueSoon.Rows)
                    {
                        int taskId = Convert.ToInt32(row["Id"]);
                        if (!taskDict.ContainsKey(taskId))
                        {
                            taskDict[taskId] = row;
                        }
                    }
                }

                // Hiển thị dữ liệu vào DataGridView
                foreach (DataRow row in taskDict.Values)
                {
                    string nguoiDung = row["UserFullName"]?.ToString() ?? "";
                    string maCV = "CV" + row["Id"].ToString();
                    string tieuDe = row["Title"]?.ToString() ?? "";
                    DateTime dueDate = Convert.ToDateTime(row["DueDate"]);
                    string hanChot = dueDate.ToString("dd/MM/yyyy");
                    string uuTien = ConvertPriority(row["Priority"]?.ToString() ?? "");
                    string conNgay = TinhSoNgayConLai(dueDate);
                    string trangThai = ConvertStatus(row["Status"]?.ToString() ?? "");

                    DataGridViewRow newRow = dgvThongBao.Rows[dgvThongBao.Rows.Add(nguoiDung, maCV, tieuDe, hanChot, uuTien, conNgay, trangThai)];
                    // Lưu TaskId vào Tag để dễ lấy lại sau này
                    newRow.Tag = Convert.ToInt32(row["Id"]);
                }

                // Cập nhật tiêu đề form
                if (string.IsNullOrEmpty(_currentUserName))
                {
                    _currentUserName = CurrentUser.GetFullName();
                }
                this.Text = "🔔 Thông Báo - " + _currentUserName;

                UpdateFormatting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thông báo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ConvertPriority(string priority)
        {
            switch (priority)
            {
                case "High": return "Cao";
                case "Medium": return "Trung bình";
                case "Low": return "Thấp";
                default: return priority;
            }
        }

        private string ConvertStatus(string status)
        {
            switch (status)
            {
                case "Todo": return "Chưa bắt đầu";
                case "Doing": return "Đang làm";
                case "Done": return "Hoàn thành";
                default: return status;
            }
        }

        private string TinhSoNgayConLai(DateTime dueDate)
        {
            DateTime today = DateTime.Today;
            int soNgay = (dueDate.Date - today).Days;

            if (soNgay < 0)
            {
                return Math.Abs(soNgay) + " ngày quá hạn";
            }
            else if (soNgay == 0)
            {
                return "Hôm nay";
            }
            else if (soNgay == 1)
            {
                return "1 ngày";
            }
            else
            {
                return soNgay + " ngày";
            }
        }

        private void UpdateFormatting()
        {
            foreach (DataGridViewRow row in dgvThongBao.Rows)
            {
                string tt = row.Cells["TrangThai"].Value?.ToString();
                string cn = row.Cells["ConNgay"].Value?.ToString();

                // Đặt màu chữ theo trạng thái
                if (tt == "Chưa bắt đầu") row.DefaultCellStyle.ForeColor = Color.Red;
                else if (tt == "Đang làm") row.DefaultCellStyle.ForeColor = Color.Orange;
                else if (tt == "Hoàn thành") row.DefaultCellStyle.ForeColor = Color.Green;
                else row.DefaultCellStyle.ForeColor = Color.Black;

                // Đặt màu nền theo số ngày còn lại
                if (cn == "Hôm nay") row.DefaultCellStyle.BackColor = Color.Yellow;
                else if (cn.Contains("quá hạn")) row.DefaultCellStyle.BackColor = Color.LightCoral;
                else row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            if (dgvThongBao.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một công việc để đánh dấu hoàn thành!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvThongBao.SelectedRows[0];
            int taskId = (int)selectedRow.Tag;

            try
            {
                // Lấy thông tin task hiện tại từ database
                DataTable dtTask = DatabaseHelper.ExecuteStoredProcedure("sp_GetTaskById",
                    new SqlParameter("@TaskId", taskId));

                if (dtTask == null || dtTask.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy công việc!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow taskRow = dtTask.Rows[0];

                // Cập nhật Status = 'Done' và giữ nguyên các field khác
                SqlParameter[] parameters = 
                {
                    new SqlParameter("@TaskId", taskId),
                    new SqlParameter("@Title", taskRow["Title"].ToString()),
                    new SqlParameter("@Description", taskRow["Description"] ?? DBNull.Value),
                    new SqlParameter("@UserId", _userId),
                    new SqlParameter("@Priority", taskRow["Priority"].ToString()),
                    new SqlParameter("@Status", "Done"),
                    new SqlParameter("@Category", taskRow["Category"].ToString()),
                    new SqlParameter("@DueDate", Convert.ToDateTime(taskRow["DueDate"]))
                };

                DatabaseHelper.ExecuteStoredProcedureNonQuery("sp_UpdateTask", parameters);

                // Reload danh sách
                HienThiDanhSach();
                MessageBox.Show("Đánh dấu hoàn thành thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đánh dấu hoàn thành: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvThongBao.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một công việc để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dgvThongBao.SelectedRows[0];
            int taskId = (int)selectedRow.Tag;

            if (MessageBox.Show("Bạn có chắc muốn xóa công việc này?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlParameter[] parameters = 
                    {
                        new SqlParameter("@TaskId", taskId),
                        new SqlParameter("@UserId", _userId)
                    };

                    DatabaseHelper.ExecuteStoredProcedureNonQuery("sp_DeleteTask", parameters);

                    // Reload danh sách
                    HienThiDanhSach();
                    MessageBox.Show("Xóa công việc thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa công việc: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}