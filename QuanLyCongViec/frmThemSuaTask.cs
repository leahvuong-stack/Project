using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frmThemSuaTask : Form
    {
        private List<CongViec> danhSachCongViec;

        public frmThemSuaTask()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();
            HienThiDanhSach();
        }

        private void KhoiTaoDuLieuMau()
        {
            danhSachCongViec = new List<CongViec>
            {
                new CongViec { MaCongViec = "CV01", TenCongViec = "Phân tích yêu cầu hệ thống", TrangThai = "Chưa bắt đầu", NguoiPhuTrach = "Nguyễn Văn A", NgayBatDau = new DateTime(2025, 11, 01), NgayKetThuc = new DateTime(2025, 11, 10), DoUuTien = "Cao", ThoiGianDuKien = 40 },
                new CongViec { MaCongViec = "CV02", TenCongViec = "Thiết kế giao diện người dùng", TrangThai = "Đang thực hiện", NguoiPhuTrach = "Trần Thị B", NgayBatDau = new DateTime(2025, 11, 05), NgayKetThuc = new DateTime(2025, 11, 15), DoUuTien = "Trung bình", ThoiGianDuKien = 30 },
                new CongViec { MaCongViec = "CV03", TenCongViec = "Lập trình module đăng nhập", TrangThai = "Hoàn thành", NguoiPhuTrach = "Lê Văn C", NgayBatDau = new DateTime(2025, 11, 10), NgayKetThuc = new DateTime(2025, 11, 20), DoUuTien = "Cao", ThoiGianDuKien = 25 },
                new CongViec { MaCongViec = "CV04", TenCongViec = "Kiểm thử tích hợp", TrangThai = "Chưa bắt đầu", NguoiPhuTrach = "Phạm Thị D", NgayBatDau = new DateTime(2025, 11, 25), NgayKetThuc = new DateTime(2025, 12, 05), DoUuTien = "Cao", ThoiGianDuKien = 35 },
                new CongViec { MaCongViec = "CV05", TenCongViec = "Viết tài liệu hướng dẫn sử dụng", TrangThai = "Đang thực hiện", NguoiPhuTrach = "Hoàng Văn E", NgayBatDau = new DateTime(2025, 11, 15), NgayKetThuc = new DateTime(2025, 12, 01), DoUuTien = "Thấp", ThoiGianDuKien = 20 },
                new CongViec { MaCongViec = "CV06", TenCongViec = "Triển khai lên server", TrangThai = "Chưa bắt đầu", NguoiPhuTrach = "Ngô Thị F", NgayBatDau = new DateTime(2025, 12, 10), NgayKetThuc = new DateTime(2025, 12, 15), DoUuTien = "Cao", ThoiGianDuKien = 15 }
            };
        }

        private void HienThiDanhSach()
        {
            dgvCongViec.DataSource = null;
            dgvCongViec.DataSource = danhSachCongViec.Select(cv => new
            {
                cv.MaCongViec,
                cv.TenCongViec,
                cv.TrangThai,
                cv.NguoiPhuTrach,
                NgayBatDau = cv.NgayBatDau.ToString("dd/MM/yyyy"),
                NgayKetThuc = cv.NgayKetThuc.ToString("dd/MM/yyyy"),
                cv.DoUuTien,
                cv.ThoiGianDuKien
            }).ToList();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaCongViec.Text) || string.IsNullOrWhiteSpace(txtTenCongViec.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Công Việc và Tên Công Việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var congViecMoi = new CongViec
            {
                MaCongViec = txtMaCongViec.Text.Trim(),
                TenCongViec = txtTenCongViec.Text.Trim(),
                TrangThai = txtTrangThai.Text.Trim(),
                NguoiPhuTrach = txtNguoiPhuTrach.Text.Trim(),
                NgayBatDau = dtpNgayBatDau.Value,
                NgayKetThuc = dtpNgayKetThuc.Value,
                DoUuTien = txtDoUuTien.Text.Trim(),
                ThoiGianDuKien = int.TryParse(txtThoiGianDuKien.Text, out int tg) ? tg : 0
            };

            if (danhSachCongViec.Any(cv => cv.MaCongViec == congViecMoi.MaCongViec))
            {
                MessageBox.Show("Mã Công Việc đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            danhSachCongViec.Add(congViecMoi);
            HienThiDanhSach();
            LamRongForm();
            MessageBox.Show("Thêm công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvCongViec.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một công việc để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCongViec = dgvCongViec.SelectedRows[0].Cells["MaCongViec"].Value?.ToString();
            var congViecCanSua = danhSachCongViec.FirstOrDefault(cv => cv.MaCongViec == maCongViec);

            if (congViecCanSua == null)
            {
                MessageBox.Show("Không tìm thấy công việc để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            congViecCanSua.TenCongViec = txtTenCongViec.Text.Trim();
            congViecCanSua.TrangThai = txtTrangThai.Text.Trim();
            congViecCanSua.NguoiPhuTrach = txtNguoiPhuTrach.Text.Trim();
            congViecCanSua.NgayBatDau = dtpNgayBatDau.Value;
            congViecCanSua.NgayKetThuc = dtpNgayKetThuc.Value;
            congViecCanSua.DoUuTien = txtDoUuTien.Text.Trim();
            congViecCanSua.ThoiGianDuKien = int.TryParse(txtThoiGianDuKien.Text, out int tg) ? tg : congViecCanSua.ThoiGianDuKien;

            HienThiDanhSach();
            MessageBox.Show("Cập nhật công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvCongViec.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một công việc để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCongViec = dgvCongViec.SelectedRows[0].Cells["MaCongViec"].Value?.ToString();
            var congViecCanXoa = danhSachCongViec.FirstOrDefault(cv => cv.MaCongViec == maCongViec);

            if (congViecCanXoa == null)
            {
                MessageBox.Show("Không tìm thấy công việc để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa công việc '{congViecCanXoa.TenCongViec}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                danhSachCongViec.Remove(congViecCanXoa);
                HienThiDanhSach();
                LamRongForm();
                MessageBox.Show("Xóa công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            var ketQuaTim = danhSachCongViec.Where(cv => cv.MaCongViec.ToLower().Contains(tuKhoa)).ToList();

            dgvCongViec.DataSource = ketQuaTim.Select(cv => new
            {
                cv.MaCongViec,
                cv.TenCongViec,
                cv.TrangThai,
                cv.NguoiPhuTrach,
                NgayBatDau = cv.NgayBatDau.ToString("dd/MM/yyyy"),
                NgayKetThuc = cv.NgayKetThuc.ToString("dd/MM/yyyy"),
                cv.DoUuTien,
                cv.ThoiGianDuKien
            }).ToList();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            HienThiDanhSach();
            LamRongForm();
        }

        private void dgvCongViec_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCongViec.SelectedRows.Count > 0)
            {
                var row = dgvCongViec.SelectedRows[0];
                txtMaCongViec.Text = row.Cells["MaCongViec"].Value?.ToString();
                txtTenCongViec.Text = row.Cells["TenCongViec"].Value?.ToString();
                txtTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
                txtNguoiPhuTrach.Text = row.Cells["NguoiPhuTrach"].Value?.ToString();
                dtpNgayBatDau.Value = DateTime.TryParse(row.Cells["NgayBatDau"].Value?.ToString(), out DateTime dt1) ? dt1 : DateTime.Now;
                dtpNgayKetThuc.Value = DateTime.TryParse(row.Cells["NgayKetThuc"].Value?.ToString(), out DateTime dt2) ? dt2 : DateTime.Now;
                txtDoUuTien.Text = row.Cells["DoUuTien"].Value?.ToString();
                txtThoiGianDuKien.Text = row.Cells["ThoiGianDuKien"].Value?.ToString();
            }
        }

        private void LamRongForm()
        {
            txtMaCongViec.Clear();
            txtTenCongViec.Clear();
            txtTrangThai.Clear();
            txtNguoiPhuTrach.Clear();
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;
            txtDoUuTien.Clear();
            txtThoiGianDuKien.Clear();
            txtMaCongViec.Focus();
        }

        public class CongViec
        {
            public string MaCongViec { get; set; }
            public string TenCongViec { get; set; }
            public string TrangThai { get; set; }
            public string NguoiPhuTrach { get; set; }
            public DateTime NgayBatDau { get; set; }
            public DateTime NgayKetThuc { get; set; }
            public string DoUuTien { get; set; }
            public int ThoiGianDuKien { get; set; }
        }
    }
}