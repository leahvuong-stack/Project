using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frmLichSu : Form
    {
        private List<LichSu> danhSachLichSu;

        public frmLichSu()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();
            HienThiDanhSach();
        }

        private void KhoiTaoDuLieuMau()
        {
            danhSachLichSu = new List<LichSu>
            {
                new LichSu { MaLichSu = "LS001", NguoiThaoTac = "Nguyễn Văn A", MaCongViec = "CV01", NgayTao = new DateTime(2025, 12, 01), HanhDong = "Thêm", DoUuTien = "Cao", ChiTiet = "Thêm công việc: Phân tích yêu cầu hệ thống" },
                new LichSu { MaLichSu = "LS002", NguoiThaoTac = "Trần Thị B", MaCongViec = "CV02", NgayTao = new DateTime(2025, 12, 02), HanhDong = "Sửa", DoUuTien = "Trung bình", ChiTiet = "Sửa trạng thái từ 'Chưa bắt đầu' sang 'Đang thực hiện'" },
                new LichSu { MaLichSu = "LS003", NguoiThaoTac = "Lê Văn C", MaCongViec = "CV03", NgayTao = new DateTime(2025, 12, 03), HanhDong = "Xóa", DoUuTien = "Cao", ChiTiet = "Xóa công việc: Lập trình module đăng nhập" },
                new LichSu { MaLichSu = "LS004", NguoiThaoTac = "Phạm Thị D", MaCongViec = "CV04", NgayTao = new DateTime(2025, 12, 04), HanhDong = "Sửa", DoUuTien = "Cao", ChiTiet = "Cập nhật thời gian dự kiến từ 35 lên 40 giờ" },
                new LichSu { MaLichSu = "LS005", NguoiThaoTac = "Hoàng Văn E", MaCongViec = "CV05", NgayTao = new DateTime(2025, 12, 05), HanhDong = "Thêm", DoUuTien = "Thấp", ChiTiet = "Thêm công việc: Viết tài liệu hướng dẫn sử dụng" },
                new LichSu { MaLichSu = "LS006", NguoiThaoTac = "Ngô Thị F", MaCongViec = "CV06", NgayTao = new DateTime(2025, 12, 06), HanhDong = "Sửa", DoUuTien = "Cao", ChiTiet = "Cập nhật người phụ trách sang Ngô Thị F" }
            };
        }

        private void HienThiDanhSach()
        {
            dgvLichSu.DataSource = null;
            dgvLichSu.DataSource = danhSachLichSu.Select(ls => new
            {
                ls.MaLichSu,
                ls.NguoiThaoTac,
                ls.MaCongViec,
                NgayTao = ls.NgayTao.ToString("dd/MM/yyyy"),
                ls.HanhDong,
                ls.DoUuTien,
                ls.ChiTiet
            }).ToList();
        }

        private void btnLoadDanhSach_Click(object sender, EventArgs e)
        {
            HienThiDanhSach();
            MessageBox.Show("Đã tải lại danh sách lịch sử!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng lịch sử để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvLichSu.SelectedRows[0];
            string maLichSu = row.Cells["MaLichSu"].Value?.ToString();

            var lichSu = danhSachLichSu.FirstOrDefault(ls => ls.MaLichSu == maLichSu);

            if (lichSu != null)
            {
                MessageBox.Show($"Chi tiết:\n{lichSu.ChiTiet}", "Chi Tiết Lịch Sử", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtMaPhieu.Text = row.Cells["MaLichSu"].Value?.ToString();
                txtMaDG.Text = row.Cells["NguoiThaoTac"].Value?.ToString();
                txtMaSach.Text = row.Cells["MaCongViec"].Value?.ToString();
                dtpNgayMuon.Value = DateTime.TryParse(row.Cells["NgayTao"].Value?.ToString(), out DateTime dt) ? dt : DateTime.Now;
                txtSoLuong.Text = row.Cells["DoUuTien"].Value?.ToString();
                txtTinhTrang.Text = row.Cells["HanhDong"].Value?.ToString();
                txtGhiChu.Text = row.Cells["ChiTiet"].Value?.ToString();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                HienThiDanhSach();
                return;
            }

            var ketQuaTim = rbMaDG.Checked
                ? danhSachLichSu.Where(ls => ls.NguoiThaoTac.ToLower().Contains(tuKhoa)).ToList()
                : danhSachLichSu.Where(ls => ls.MaCongViec.ToLower().Contains(tuKhoa)).ToList();

            dgvLichSu.DataSource = ketQuaTim.Select(ls => new
            {
                ls.MaLichSu,
                ls.NguoiThaoTac,
                ls.MaCongViec,
                NgayTao = ls.NgayTao.ToString("dd/MM/yyyy"),
                ls.HanhDong,
                ls.DoUuTien,
                ls.ChiTiet
            }).ToList();
        }

        private void frmLichSu_Load(object sender, EventArgs e)
        {
        }

        public class LichSu
        {
            public string MaLichSu { get; set; }
            public string NguoiThaoTac { get; set; }
            public string MaCongViec { get; set; }
            public DateTime NgayTao { get; set; }
            public string HanhDong { get; set; }
            public string DoUuTien { get; set; }
            public string ChiTiet { get; set; }
        }
    }
}