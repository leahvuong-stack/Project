using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    public partial class frmBaoCao : Form
    {
        private int _userId;

        public frmBaoCao(int userId = 0)
        {
            InitializeComponent();
            // Tắt AutoScale để tránh font bị nhỏ do DPI cao
            this.AutoScaleMode = AutoScaleMode.None;

            _userId = userId;
            CapNhatThongKe();
        }

        private void CapNhatThongKe()
        {
            try
            {
                // Lấy thống kê từ database
                SqlParameter[] parameters = { new SqlParameter("@UserId", _userId) };
                DataSet ds = new DataSet();
                
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetReportData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }

                // Hiển thị thống kê tổng quan
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    int totalTasks = Convert.ToInt32(row["TotalTasks"]);
                    int completedTasks = Convert.ToInt32(row["CompletedTasks"]);
                    int overdueTasks = Convert.ToInt32(row["OverdueTasks"]);
                    int inProgress = totalTasks - completedTasks;

                    txtSLDauSach.Text = totalTasks.ToString();
                    txtSLMuon.Text = inProgress.ToString();
                    txtSLQuaHan.Text = overdueTasks.ToString();
                    txtSLCon.Text = (totalTasks - overdueTasks).ToString();
                    txtTongGiaTri.Text = totalTasks.ToString("N0");
                }

                // Thống kê người phụ trách
                DataTable dtUsers = DatabaseHelper.ExecuteStoredProcedure("sp_GetUsersWithOverdueTasks");
                if (dtUsers != null && dtUsers.Rows.Count > 0)
                {
                    int totalUsers = dtUsers.Rows.Count;
                    int totalOverdue = 0;
                    foreach (DataRow dr in dtUsers.Rows)
                    {
                        totalOverdue += Convert.ToInt32(dr["SoLuongQuaHan"]);
                    }

                    txtSLDocGia.Text = totalUsers.ToString();
                    txtSLDGDaMuon.Text = totalOverdue.ToString();
                    txtSLDGQuaHan.Text = totalUsers.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDSSachQuaHan_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetOverdueTasks");
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Định dạng cột ngày tháng
                    dgvBaoCao.DataSource = dt;
                    if (dgvBaoCao.Columns["NgayBatDau"] != null)
                        dgvBaoCao.Columns["NgayBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    if (dgvBaoCao.Columns["NgayKetThuc"] != null)
                        dgvBaoCao.Columns["NgayKetThuc"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    
                    // Đặt header text
                    if (dgvBaoCao.Columns["MaCV"] != null)
                        dgvBaoCao.Columns["MaCV"].HeaderText = "Mã CV";
                    if (dgvBaoCao.Columns["TenCV"] != null)
                        dgvBaoCao.Columns["TenCV"].HeaderText = "Tên CV";
                    if (dgvBaoCao.Columns["NguoiPhuTrach"] != null)
                        dgvBaoCao.Columns["NguoiPhuTrach"].HeaderText = "Người Phụ Trách";
                    if (dgvBaoCao.Columns["NgayBatDau"] != null)
                        dgvBaoCao.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                    if (dgvBaoCao.Columns["NgayKetThuc"] != null)
                        dgvBaoCao.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
                    if (dgvBaoCao.Columns["TrangThai"] != null)
                        dgvBaoCao.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    if (dgvBaoCao.Columns["DoUuTien"] != null)
                        dgvBaoCao.Columns["DoUuTien"].HeaderText = "Độ Ưu Tiên";
                }
                else
                {
                    dgvBaoCao.DataSource = null;
                    MessageBox.Show("Không có công việc nào quá hạn.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách công việc quá hạn: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDSDGQuaHan_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetUsersWithOverdueTasks");
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Tạo DataTable mới với cột DanhSachCV
                    DataTable dtResult = new DataTable();
                    dtResult.Columns.Add("NguoiPhuTrach", typeof(string));
                    dtResult.Columns.Add("SoLuongQuaHan", typeof(int));
                    dtResult.Columns.Add("DanhSachCV", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        string nguoiPhuTrach = row["NguoiPhuTrach"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuongQuaHan"]);
                        
                        // Lấy danh sách mã CV quá hạn của người này
                        DataTable dtTasks = DatabaseHelper.ExecuteStoredProcedure("sp_GetOverdueTasks");
                        string danhSachCV = "";
                        if (dtTasks != null)
                        {
                            var tasks = dtTasks.Select($"NguoiPhuTrach = '{nguoiPhuTrach}'");
                            var maCVList = new System.Collections.Generic.List<string>();
                            foreach (DataRow taskRow in tasks)
                            {
                                maCVList.Add(taskRow["MaCV"].ToString());
                            }
                            danhSachCV = string.Join(", ", maCVList);
                        }

                        DataRow newRow = dtResult.NewRow();
                        newRow["NguoiPhuTrach"] = nguoiPhuTrach;
                        newRow["SoLuongQuaHan"] = soLuong;
                        newRow["DanhSachCV"] = danhSachCV;
                        dtResult.Rows.Add(newRow);
                    }

                    dgvBaoCao.DataSource = dtResult;
                    if (dgvBaoCao.Columns["NguoiPhuTrach"] != null)
                        dgvBaoCao.Columns["NguoiPhuTrach"].HeaderText = "Người Phụ Trách";
                    if (dgvBaoCao.Columns["SoLuongQuaHan"] != null)
                        dgvBaoCao.Columns["SoLuongQuaHan"].HeaderText = "Số CV Quá Hạn";
                    if (dgvBaoCao.Columns["DanhSachCV"] != null)
                        dgvBaoCao.Columns["DanhSachCV"].HeaderText = "Danh Sách Mã CV";
                }
                else
                {
                    dgvBaoCao.DataSource = null;
                    MessageBox.Show("Không có người nào có công việc quá hạn.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách người quá hạn: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
