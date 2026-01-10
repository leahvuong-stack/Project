using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frmHuongDan : Form
    {
        public frmHuongDan()
        {
            InitializeComponent();
            LoadMenuItems();
            // Mở mặc định trang quan trọng nhất để người dùng nắm bắt quy trình ngay
            ShowContent("Cập nhật Tiến độ Task");
        }

        private void LoadMenuItems()
        {
            // Danh sách các mục hướng dẫn
            string[] menuItems = {
                "Đăng nhập Hệ thống",
                "Quên mật khẩu",
                "Thông tin cá nhân",
                "Báo cáo",
                "Lịch sử Hoạt động",
                "Quản lý Công việc",
                "Quản lý Thông báo",
                "Chi tiết Task Chuyên sâu",
                "Mẹo & Thủ thuật",
                "Cập nhật Tiến độ Task"
            };

            Array.Reverse(menuItems);

            foreach (string item in menuItems)
            {
                Button btn = new Button();
                btn.Text = item;
                btn.Dock = DockStyle.Top;
                btn.Height = 50;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(25, 0, 0, 0);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(245, 245, 245);
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                btn.Cursor = Cursors.Hand;

                btn.Click += (s, e) => {
                    ShowContent(item);
                    HighlightButton(btn);
                };
                pnlSidebar.Controls.Add(btn);
            }
        }

        private void HighlightButton(Button clickedBtn)
        {
            foreach (Control ctrl in pnlSidebar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(245, 245, 245);
                    btn.ForeColor = Color.Black;
                }
            }
            clickedBtn.BackColor = Color.FromArgb(0, 120, 215);
            clickedBtn.ForeColor = Color.White;
        }

        private void ShowContent(string menuItem)
        {
            pnlContent.Controls.Clear();
            pnlContent.Padding = new Padding(40);

            switch (menuItem)
            {
                case "Đăng nhập Hệ thống":
                    ShowGenericContent("HƯỚNG DẪN TRUY CẬP & BẢO MẬT TÀI KHOẢN",
                        "Xác thực danh tính là bước đầu tiên để hệ thống phân quyền chính xác các đầu việc cho từng cá nhân.",
                        "• QUY TRÌNH ĐĂNG NHẬP: Nhập 'Tên đăng nhập' và 'Mật khẩu' được cấp (ví dụ: 'user1').\n\n" +
                        "• CHẾ ĐỘ GHI NHỚ: Sử dụng tính năng 'Ghi nhớ đăng nhập' trên máy tính cá nhân để hệ thống tự điền thông tin cho những lần sau.\n\n" +
                        "• AN TOÀN BẢO MẬT: Sau khi làm việc xong trên máy tính chung, hãy đăng xuất để bảo vệ dữ liệu cá nhân.");
                    break;

                case "Quên mật khẩu":
                    ShowGenericContent("KHÔI PHỤC TRUY CẬP (frmQuenMatKhau)",
                        "Hệ thống hỗ trợ lấy lại quyền truy cập trong trường hợp bạn không nhớ thông tin đăng nhập.",
                        "• XÁC THỰC EMAIL: Nhập địa chỉ Email đã đăng ký. Mã xác nhận (OTP) sẽ được gửi về hộp thư này.\n\n" +
                        "• THIẾT LẬP MỚI: Sau khi nhập đúng OTP, hệ thống cho phép bạn tạo mật khẩu mới.\n\n" +
                        "• TRỢ GIÚP: Nếu không nhận được Email, vui lòng kiểm tra hộp thư rác hoặc liên hệ quản trị viên.");
                    break;

                case "Thông tin cá nhân":
                    ShowGenericContent("QUẢN LÝ HỒ SƠ CÁ NHÂN (frmProfile)",
                        "Nơi cập nhật thông tin nhận diện và các tùy chỉnh cá nhân hóa tài khoản.",
                        "• CẬP NHẬT ẢNH: Giúp đồng nghiệp dễ dàng nhận diện bạn trên danh sách phụ trách.\n\n" +
                        "• THÔNG TIN LIÊN HỆ: Cập nhật Số điện thoại và Email để nhận thông báo khẩn cấp.\n\n" +
                        "• ĐỔI MẬT KHẨU: Bạn nên chủ động thay đổi mật khẩu định kỳ để tăng tính bảo mật.");
                    break;

                case "Lịch sử Hoạt động":
                    ShowGenericContent("TRUY XUẤT NHẬT KÝ & MINH BẠCH DỮ LIỆU",
                        "Tính năng giúp người quản lý và nhân viên theo dõi toàn bộ các thay đổi phát sinh.",
                        "• TÌM KIẾM: Lọc theo 'Người thao tác' hoặc 'Mã lịch sử' để truy vết sự kiện.\n\n" +
                        "• HÀNH ĐỘNG: Bảng hiển thị rõ các hành vi Thêm - Sửa - Xóa, giúp kiểm soát sai sót.\n\n" +
                        "• ĐỒNG BỘ: Nhấn 'Load Danh Sách' để cập nhật tương tác mới nhất từ các thành viên khác.");
                    break;

                case "Quản lý Công việc":
                    ShowGenericContent("QUY TRÌNH QUẢN TRỊ TASK TẬP TRUNG",
                        "Giao diện điều hành chính - nơi khởi tạo, phân bổ nhân sự và thiết lập thời hạn.",
                        "• KHỞI TẠO: Nhập Mã công việc và mô tả. Chọn 'Người phụ trách' để Task xuất hiện trên màn hình của họ.\n\n" +
                        "• DEADLINE: Chọn ngày bắt đầu/kết thúc. Hệ thống sẽ cảnh báo nếu công việc bị đình trệ.\n\n" +
                        "• THAO TÁC: Sử dụng các nút Thêm/Sửa/Xóa sau khi chọn dòng tương ứng trên bảng.");
                    break;

                case "Chi tiết Task Chuyên sâu":
                    ShowGenericContent("HIỆU CHỈNH CHI TIẾT & BÁO CÁO NGHIỆP VỤ",
                        "Dành cho nhân viên báo cáo cụ thể về kết quả thực hiện và các vướng mắc.",
                        "• ƯU TIÊN: Thiết lập mức Thấp - Trung bình - Cao để ưu tiên xử lý việc khẩn cấp.\n\n" +
                        "• GHI CHÚ: Lưu lại các kết quả bàn giao hoặc giải trình lý do chậm tiến độ.\n\n" +
                        "• LƯU TRỮ: Nhấn 'Lưu lại' để hệ thống ghi nhận thay đổi vào lịch sử.");
                    break;

                case "Báo cáo":
                    ShowGenericContent("PHÂN TÍCH HIỆU SUẤT & THỐNG KÊ QUẢN TRỊ",
                        "Chuyển đổi dữ liệu thô thành thông tin quản trị quý giá để đánh giá năng lực.",
                        "• DASHBOARD: Theo dõi nhanh: Tổng việc, việc đang làm và việc đã quá hạn.\n\n" +
                        "• XỬ LÝ RỦI RO: Lọc danh sách 'Quá hạn' để ưu tiên nguồn lực xử lý gấp.\n\n" +
                        "• ĐÁNH GIÁ: Cơ sở để người quản lý khen thưởng hoặc đôn đốc nhân sự.");
                    break;

                case "Quản lý Thông báo":
                    ShowGenericContent("TRUNG TÂM TƯƠNG TÁC & NHẮC VIỆC",
                        "Đảm bảo dòng chảy thông tin thông suốt mà không cần kiểm tra thủ công.",
                        "• ĐIỀU HƯỚNG: Nhấp đúp vào thông báo để mở ngay màn hình Chi tiết Task tương ứng.\n\n" +
                        "• CẢNH BÁO: Hệ thống tự nhắc khi sắp đến deadline hoặc có thay đổi từ quản lý.\n\n" +
                        "• ĐÁNH DẤU HOÀN THÀNH: Khi hoàn thành công việc, nhấn nút hoàn thành, hệ thống sẽ cập nhật trạng thái hiển thị hoàn tất.");
                    break;

                case "Cập nhật Tiến độ Task":
                    ShowProgressContent();
                    break;

                case "Mẹo & Thủ thuật":
                    ShowGenericContent("TỐI ƯU HÓA TRẢI NGHIỆM NGƯỜI DÙNG",

                        "Sử dụng các quy tắc ngầm định để làm việc chuyên nghiệp hơn.",

                        "• Màu sắc: Màu Đỏ tượng trưng cho 'Quá hạn', Xanh dương là 'Cần làm', Vàng là 'Đang làm' và Xanh lá là 'Hoàn thành'.\n" +

                        "• Đồng bộ: Luôn nhấn các nút 'Load' hoặc 'Hiển thị' để đảm bảo bạn đang xem dữ liệu mới nhất từ máy chủ.");

                    break;

            }

        }



        private void ShowGenericContent(string title, string desc, string bullets)
        {
            AddLabel(title, new Font("Segoe UI", 18, FontStyle.Bold), Color.FromArgb(30, 60, 100), 10);
            Panel line = new Panel { Height = 2, BackColor = Color.FromArgb(30, 60, 100), Dock = DockStyle.Top };
            pnlContent.Controls.Add(line);
            AddLabel(desc, new Font("Segoe UI", 11, FontStyle.Italic), Color.FromArgb(50, 50, 50), 25);
            AddLabel(bullets, new Font("Segoe UI", 11), Color.FromArgb(30, 30, 30), 0);
        }

        private void ShowProgressContent()
        {
            AddLabel("HƯỚNG DẪN CẬP NHẬT TIẾN ĐỘ CÔNG VIỆC", new Font("Segoe UI", 18, FontStyle.Bold), Color.FromArgb(30, 60, 100), 5);
            Panel line = new Panel { Height = 2, BackColor = Color.FromArgb(30, 60, 100), Dock = DockStyle.Top };
            pnlContent.Controls.Add(line);
            AddLabel("Việc cập nhật tiến độ thường xuyên giúp hệ thống tính toán chính xác tình trạng dự án.", new Font("Segoe UI", 11), Color.Black, 25);

            Panel progPanel = new Panel { Dock = DockStyle.Top, Height = 140, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, Margin = new Padding(0, 0, 0, 30) };
            pnlContent.Controls.Add(progPanel);

            Label lblPct = new Label { Text = "75%", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(35, 20), AutoSize = true };
            progPanel.Controls.Add(lblPct);

            Label lblStatus = new Label { Text = "Đang thực hiện", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(130, 32), Size = new Size(130, 30), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
            progPanel.Controls.Add(lblStatus);

            TrackBar track = new TrackBar { Location = new Point(30, 80), Size = new Size(550, 45), Minimum = 0, Maximum = 100, Value = 75, TickFrequency = 10 };
            progPanel.Controls.Add(track);

            track.ValueChanged += (s, e) => {
                int v = track.Value;
                lblPct.Text = v + "%";
                if (v < 40) { lblPct.ForeColor = Color.Red; lblStatus.Text = "Chậm tiến độ"; lblStatus.ForeColor = Color.Red; }
                else if (v < 100) { lblPct.ForeColor = Color.DarkOrange; lblStatus.Text = "Đang thực hiện"; lblStatus.ForeColor = Color.DarkOrange; }
                else { lblPct.ForeColor = Color.Green; lblStatus.Text = "Đã hoàn thành"; lblStatus.ForeColor = Color.Green; }
            };

            AddLabel("💡 QUY ĐỊNH VỀ TRẠNG THÁI MÀU SẮC:", new Font("Segoe UI", 10, FontStyle.Bold), Color.Black, 5);
            AddLabel("🔴 Red: Cảnh báo chậm tiến độ (< 40%)", new Font("Segoe UI", 10), Color.Red, 0);



            AddLabel("🟠 Orange: Công việc đang triển khai (40% - 99%)", new Font("Segoe UI", 10), Color.DarkOrange, 0);



            AddLabel("🟢 Green: Hoàn thành mục tiêu (100%)", new Font("Segoe UI", 10), Color.Green, 30);

            Panel warn = new Panel { Dock = DockStyle.Top, BackColor = Color.FromArgb(255, 252, 225), Padding = new Padding(20), BorderStyle = BorderStyle.FixedSingle, Height = 100 };
            pnlContent.Controls.Add(warn);
            Label warnTitle = new Label { Text = "⭐ LỜI KHUYÊN QUAN TRỌNG:", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkRed, Dock = DockStyle.Top, AutoSize = true };
            warn.Controls.Add(warnTitle);
            Label warnText = new Label { Text = "Đừng quên nhấn nút 'Lưu lại' sau khi kéo thanh tiến độ. Nếu bạn thoát ra mà không lưu, báo cáo sẽ không được ghi nhận.", Font = new Font("Segoe UI", 10), Dock = DockStyle.Fill, ForeColor = Color.FromArgb(64, 64, 64) };
            warn.Controls.Add(warnText);
        }

        private void AddLabel(string text, Font font, Color color, int bottomMargin)
        {
            Label lbl = new Label { Text = text, Font = font, ForeColor = color, Dock = DockStyle.Top, AutoSize = true, MaximumSize = new Size(700, 0), Padding = new Padding(0, 5, 0, bottomMargin) };
            pnlContent.Controls.Add(lbl);
        }
    }
}