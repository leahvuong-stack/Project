using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frmBaoCao : Form
    {
        private List<CongViec> danhSachCongViec;

        public frmBaoCao()
        {
            InitializeComponent();
            KhoiTaoDuLieuMau();
            CapNhatThongKe();
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

        private void CapNhatThongKe()
        {
            int slDauSach = danhSachCongViec.Count;
            int slMuon = danhSachCongViec.Count(cv => cv.TrangThai != "Hoàn thành");
            int slQuaHan = danhSachCongViec.Count(cv => cv.NgayKetThuc < DateTime.Now && cv.TrangThai != "Hoàn thành");
            int slCon = slDauSach - slQuaHan;
            decimal tongGiaTri = slDauSach * 100000;

            txtSLDauSach.Text = slDauSach.ToString();
            txtSLMuon.Text = slMuon.ToString();
            txtSLQuaHan.Text = slQuaHan.ToString();
            txtSLCon.Text = slCon.ToString();
            txtTongGiaTri.Text = tongGiaTri.ToString("N0");
            var nguoiPhuTrachList = danhSachCongViec.Select(cv => cv.NguoiPhuTrach).Distinct().ToList();
            int slDocGia = nguoiPhuTrachList.Count;
            int slDGDaMuon = danhSachCongViec.Count();
            int slDGQuaHan = danhSachCongViec
                .Where(cv => cv.NgayKetThuc < DateTime.Now && cv.TrangThai != "Hoàn thành")
                .Select(cv => cv.NguoiPhuTrach)
                .Distinct()
                .Count();

            txtSLDocGia.Text = slDocGia.ToString();
            txtSLDGDaMuon.Text = slDGDaMuon.ToString();
            txtSLDGQuaHan.Text = slDGQuaHan.ToString();
        }

        private void btnDSSachQuaHan_Click(object sender, EventArgs e)
        {
            var dsQuaHan = danhSachCongViec
                .Where(cv => cv.NgayKetThuc < DateTime.Now && cv.TrangThai != "Hoàn thành")
                .Select(cv => new
                {
                    MaCV = cv.MaCongViec,
                    TenCV = cv.TenCongViec,
                    NguoiPhuTrach = cv.NguoiPhuTrach,
                    NgayBatDau = cv.NgayBatDau.ToString("dd/MM/yyyy"),
                    NgayKetThuc = cv.NgayKetThuc.ToString("dd/MM/yyyy"),
                    TrangThai = cv.TrangThai,
                    DoUuTien = cv.DoUuTien,
                    ThoiGianDuKien = cv.ThoiGianDuKien + " giờ"
                }).ToList();

            dgvBaoCao.DataSource = dsQuaHan;
            dgvBaoCao.Columns["MaCV"].HeaderText = "Mã CV";
            dgvBaoCao.Columns["TenCV"].HeaderText = "Tên CV";
            dgvBaoCao.Columns["NguoiPhuTrach"].HeaderText = "Người Phụ Trách";
            dgvBaoCao.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
            dgvBaoCao.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
            dgvBaoCao.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgvBaoCao.Columns["DoUuTien"].HeaderText = "Độ Ưu Tiên";
            dgvBaoCao.Columns["ThoiGianDuKien"].HeaderText = "Thời Gian Dự Kiến";
        }

        private void btnDSDGQuaHan_Click(object sender, EventArgs e)
        {
            var dsNguoiQuaHan = danhSachCongViec
                .Where(cv => cv.NgayKetThuc < DateTime.Now && cv.TrangThai != "Hoàn thành")
                .GroupBy(cv => cv.NguoiPhuTrach)
                .Select(g => new
                {
                    NguoiPhuTrach = g.Key,
                    SoLuongQuaHan = g.Count(),
                    DanhSachCV = string.Join(", ", g.Select(cv => cv.MaCongViec))
                })
                .ToList();

            dgvBaoCao.DataSource = dsNguoiQuaHan;
            dgvBaoCao.Columns["NguoiPhuTrach"].HeaderText = "Người Phụ Trách";
            dgvBaoCao.Columns["SoLuongQuaHan"].HeaderText = "Số CV Quá Hạn";
            dgvBaoCao.Columns["DanhSachCV"].HeaderText = "Danh Sách Mã CV";
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