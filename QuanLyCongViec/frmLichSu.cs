using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;
using QuanLyCongViec.Helpers;

namespace QuanLyCongViec
{
    public partial class frmLichSu : Form
    {
        public frmLichSu()
        {
            InitializeComponent();
            Helpers.FontHelper.SetUnicodeFont(this);
            Helpers.FontHelper.SetUnicodeFontForDataGridView(dgvLichSu);

            // Nhấn phím Enter để tìm kiếm
            txtTimKiem.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnTimKiem_Click(s, e);
                    e.SuppressKeyPress = true;
                }
            };

            HienThiDanhSach();
        }

        private void HienThiDanhSach()
        {
            try
            {
                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_GetAllTaskHistory");
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvLichSu.DataSource = dt;
                    
                    // Định dạng cột ngày tháng
                    if (dgvLichSu.Columns["NgayTao"] != null)
                    {
                        dgvLichSu.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                    
                    // Đặt header text
                    if (dgvLichSu.Columns["MaLichSu"] != null)
                        dgvLichSu.Columns["MaLichSu"].HeaderText = "Mã Lịch Sử";
                    if (dgvLichSu.Columns["NguoiThaoTac"] != null)
                        dgvLichSu.Columns["NguoiThaoTac"].HeaderText = "Người Thao Tác";
                    if (dgvLichSu.Columns["MaCongViec"] != null)
                        dgvLichSu.Columns["MaCongViec"].HeaderText = "Mã CV";
                    if (dgvLichSu.Columns["NgayTao"] != null)
                        dgvLichSu.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                    if (dgvLichSu.Columns["HanhDong"] != null)
                        dgvLichSu.Columns["HanhDong"].HeaderText = "Hành Động";
                    if (dgvLichSu.Columns["DoUuTien"] != null)
                        dgvLichSu.Columns["DoUuTien"].HeaderText = "Độ Ưu Tiên";
                    if (dgvLichSu.Columns["ChiTiet"] != null)
                        dgvLichSu.Columns["ChiTiet"].HeaderText = "Chi Tiết";
                    txtTimKiem.Text = "";
                    rbMaDG.Checked = true;
                }
                else
                {
                    dgvLichSu.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lịch sử: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadDanhSach_Click(object sender, EventArgs e)
        {
            HienThiDanhSach();
            MessageBox.Show("Đã tải lại danh sách lịch sử!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng lịch sử để xem chi tiết!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvLichSu.SelectedRows[0];
            string chiTiet = row.Cells["ChiTiet"]?.Value?.ToString() ?? "";

            if (!string.IsNullOrEmpty(chiTiet))
            {
                MessageBox.Show($"Chi tiết:\n{chiTiet}", "Chi Tiết Lịch Sử", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không có thông tin chi tiết.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvLichSu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count > 0)
            {
                var row = dgvLichSu.SelectedRows[0];
                txtMaPhieu.Text = row.Cells["MaLichSu"]?.Value?.ToString() ?? "";
                txtMaDG.Text = row.Cells["NguoiThaoTac"]?.Value?.ToString() ?? "";
                txtMaSach.Text = row.Cells["MaCongViec"]?.Value?.ToString() ?? "";
                
                // Xử lý ngày tạo
                DateTime? ngayTao = null;
                if (row.Cells["NgayTao"]?.Value != null)
                {
                    if (row.Cells["NgayTao"].Value is DateTime dt)
                    {
                        ngayTao = dt;
                    }
                    else if (DateTime.TryParse(row.Cells["NgayTao"].Value.ToString(), out DateTime parsedDate))
                    {
                        ngayTao = parsedDate;
                    }
                }
                
                if (ngayTao.HasValue)
                {
                    dtpNgayMuon.Value = ngayTao.Value;
                    dtpNgayTra.Value = ngayTao.Value;
                }
                
                txtSoLuong.Text = row.Cells["DoUuTien"]?.Value?.ToString() ?? "";
                txtTinhTrang.Text = row.Cells["HanhDong"]?.Value?.ToString() ?? "";
                txtGhiChu.Text = row.Cells["ChiTiet"]?.Value?.ToString() ?? "";
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                HienThiDanhSach();
                return;
            }

            try
            {
                string searchType = rbMaDG.Checked ? "User" : "Task";
                SqlParameter[] parameters = 
                {
                    new SqlParameter("@SearchTerm", tuKhoa),
                    new SqlParameter("@SearchType", searchType)
                };

                DataTable dt = DatabaseHelper.ExecuteStoredProcedure("sp_SearchTaskHistory", parameters);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvLichSu.DataSource = dt;
                    
                    // Định dạng cột ngày tháng
                    if (dgvLichSu.Columns["NgayTao"] != null)
                    {
                        dgvLichSu.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                    
                    // Đặt header text
                    if (dgvLichSu.Columns["MaLichSu"] != null)
                        dgvLichSu.Columns["MaLichSu"].HeaderText = "Mã Lịch Sử";
                    if (dgvLichSu.Columns["NguoiThaoTac"] != null)
                        dgvLichSu.Columns["NguoiThaoTac"].HeaderText = "Người Thao Tác";
                    if (dgvLichSu.Columns["MaCongViec"] != null)
                        dgvLichSu.Columns["MaCongViec"].HeaderText = "Mã CV";
                    if (dgvLichSu.Columns["NgayTao"] != null)
                        dgvLichSu.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                    if (dgvLichSu.Columns["HanhDong"] != null)
                        dgvLichSu.Columns["HanhDong"].HeaderText = "Hành Động";
                    if (dgvLichSu.Columns["DoUuTien"] != null)
                        dgvLichSu.Columns["DoUuTien"].HeaderText = "Độ Ưu Tiên";
                    if (dgvLichSu.Columns["ChiTiet"] != null)
                        dgvLichSu.Columns["ChiTiet"].HeaderText = "Chi Tiết";
                }
                else
                {
                    dgvLichSu.DataSource = null;
                    MessageBox.Show("Không tìm thấy kết quả nào.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLichSu_Load(object sender, EventArgs e)
        {
        }
    }
}
