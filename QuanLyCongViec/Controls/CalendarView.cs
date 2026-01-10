using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;

namespace QuanLyCongViec.Controls
{
    /// <summary>
    /// UserControl hiển thị lịch với tasks theo ngày
    /// </summary>
    public partial class CalendarView : UserControl
    {
        private DateTime _currentMonth;
        private int _userId;
        private Dictionary<DateTime, int> _taskCountByDate; // Số lượng task theo ngày
        private Panel[,] _dayPanels; // Ma trận các panel ngày
        private const int ROWS = 6; // 6 tuần
        private const int COLS = 7; // 7 ngày

        public event EventHandler<DateTime> DateClicked; // Event khi click vào ngày

        public CalendarView()
        {
            InitializeComponent();
            _currentMonth = DateTime.Today;
            _taskCountByDate = new Dictionary<DateTime, int>();
            _dayPanels = new Panel[ROWS, COLS];
            InitializeCalendarLayout();
        }

        /// <summary>
        /// Set UserId để load tasks
        /// </summary>
        public void SetUserId(int userId)
        {
            _userId = userId;
            LoadTasksForMonth();
        }

        /// <summary>
        /// Khởi tạo layout cho lịch
        /// </summary>
        private void InitializeCalendarLayout()
        {
            this.SuspendLayout();
            this.Controls.Clear();
            this.BackColor = Color.White;
            this.Size = new Size(350, 320);

            // Header: Tháng/Năm và nút điều hướng
            Panel headerPanel = CreateHeaderPanel();
            this.Controls.Add(headerPanel);

            // Tên các ngày trong tuần
            Panel weekDaysPanel = CreateWeekDaysPanel();
            this.Controls.Add(weekDaysPanel);

            // Grid các ngày trong tháng
            Panel daysGrid = CreateDaysGrid();
            this.Controls.Add(daysGrid);

            this.ResumeLayout();
        }

        /// <summary>
        /// Tạo panel header (tháng/năm và nút điều hướng)
        /// </summary>
        private Panel CreateHeaderPanel()
        {
            Panel panel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(350, 40),
                BackColor = Color.FromArgb(52, 152, 219)
            };

            // Nút Previous Month
            Button btnPrev = new Button
            {
                Text = "◀",
                Location = new Point(10, 8),
                Size = new Size(35, 25),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPrev.FlatAppearance.BorderSize = 0;
            btnPrev.Click += (s, e) =>
            {
                _currentMonth = _currentMonth.AddMonths(-1);
                LoadTasksForMonth();
            };

            // Label tháng/năm
            Label lblMonthYear = new Label
            {
                Name = "lblMonthYear",
                Text = _currentMonth.ToString("MMMM yyyy"),
                Location = new Point(50, 8),
                Size = new Size(250, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            // Nút Next Month
            Button btnNext = new Button
            {
                Text = "▶",
                Location = new Point(305, 8),
                Size = new Size(35, 25),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Click += (s, e) =>
            {
                _currentMonth = _currentMonth.AddMonths(1);
                LoadTasksForMonth();
            };

            panel.Controls.AddRange(new Control[] { btnPrev, lblMonthYear, btnNext });
            return panel;
        }

        /// <summary>
        /// Tạo panel tên các ngày trong tuần
        /// </summary>
        private Panel CreateWeekDaysPanel()
        {
            Panel panel = new Panel
            {
                Location = new Point(0, 40),
                Size = new Size(350, 30),
                BackColor = Color.FromArgb(236, 240, 241)
            };

            string[] dayNames = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            int dayWidth = 50;

            for (int i = 0; i < 7; i++)
            {
                Label lbl = new Label
                {
                    Text = dayNames[i],
                    Location = new Point(i * dayWidth, 5),
                    Size = new Size(dayWidth, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 73, 94)
                };
                panel.Controls.Add(lbl);
            }

            return panel;
        }

        /// <summary>
        /// Tạo grid các ngày trong tháng
        /// </summary>
        private Panel CreateDaysGrid()
        {
            Panel panel = new Panel
            {
                Name = "pnlDaysGrid",
                Location = new Point(0, 70),
                Size = new Size(350, 250),
                BackColor = Color.White
            };

            int dayWidth = 50;
            int dayHeight = 40;

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    Panel dayPanel = new Panel
                    {
                        Location = new Point(col * dayWidth, row * dayHeight),
                        Size = new Size(dayWidth, dayHeight),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Cursor = Cursors.Hand,
                        Tag = null // Sẽ set DateTime sau
                    };

                    // Label hiển thị số ngày
                    Label lblDay = new Label
                    {
                        Name = "lblDay",
                        Location = new Point(2, 2),
                        Size = new Size(46, 18),
                        TextAlign = ContentAlignment.TopRight,
                        Font = new Font("Segoe UI", 9F),
                        BackColor = Color.Transparent,
                        Cursor = Cursors.Hand
                    };

                    // Label hiển thị số task
                    Label lblTaskCount = new Label
                    {
                        Name = "lblTaskCount",
                        Location = new Point(2, 20),
                        Size = new Size(46, 16),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 7F, FontStyle.Bold),
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Visible = false,
                        Cursor = Cursors.Hand
                    };

                    dayPanel.Controls.Add(lblDay);
                    dayPanel.Controls.Add(lblTaskCount);

                    // Event handlers cho panel
                    dayPanel.MouseEnter += DayPanel_MouseEnter;
                    dayPanel.MouseLeave += DayPanel_MouseLeave;
                    dayPanel.Click += DayPanel_Click;
                    
                    // Event handlers cho labels (để click vào text cũng work)
                    lblDay.MouseEnter += DayPanel_MouseEnter;
                    lblDay.MouseLeave += DayPanel_MouseLeave;
                    lblDay.Click += DayPanel_Click;
                    
                    lblTaskCount.MouseEnter += DayPanel_MouseEnter;
                    lblTaskCount.MouseLeave += DayPanel_MouseLeave;
                    lblTaskCount.Click += DayPanel_Click;

                    _dayPanels[row, col] = dayPanel;
                    panel.Controls.Add(dayPanel);
                }
            }

            return panel;
        }

        /// <summary>
        /// Load tasks từ database cho tháng hiện tại
        /// </summary>
        private void LoadTasksForMonth()
        {
            try
            {
                _taskCountByDate.Clear();

                if (_userId > 0)
                {
                    // Query SQL trực tiếp để lấy task count theo ngày (thay thế stored procedure)
                    string query = @"
                        SELECT CAST(DueDate AS DATE) AS TaskDate, COUNT(*) AS TaskCount
                        FROM Tasks
                        WHERE UserId = @UserId 
                            AND IsDeleted = 0
                            AND YEAR(DueDate) = @Year
                            AND MONTH(DueDate) = @Month
                        GROUP BY CAST(DueDate AS DATE)
                        ORDER BY TaskDate";

                    DataTable dt = DatabaseHelper.ExecuteQuery(
                        query,
                        new System.Data.SqlClient.SqlParameter("@UserId", _userId),
                        new System.Data.SqlClient.SqlParameter("@Year", _currentMonth.Year),
                        new System.Data.SqlClient.SqlParameter("@Month", _currentMonth.Month)
                    );

                    foreach (DataRow row in dt.Rows)
                    {
                        DateTime date = Convert.ToDateTime(row["TaskDate"]);
                        int count = Convert.ToInt32(row["TaskCount"]);
                        _taskCountByDate[date.Date] = count;
                    }
                }

                UpdateCalendarDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật hiển thị lịch
        /// </summary>
        private void UpdateCalendarDisplay()
        {
            // Cập nhật label tháng/năm
            Control lblMonthYear = this.Controls.Find("lblMonthYear", true).FirstOrDefault();
            if (lblMonthYear != null)
            {
                lblMonthYear.Text = _currentMonth.ToString("MMMM yyyy");
            }

            // Tính toán ngày đầu tiên của tháng
            DateTime firstDayOfMonth = new DateTime(_currentMonth.Year, _currentMonth.Month, 1);
            int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek; // 0 = Chủ nhật
            int daysInMonth = DateTime.DaysInMonth(_currentMonth.Year, _currentMonth.Month);

            // Ngày hôm nay
            DateTime today = DateTime.Today;

            int dayNumber = 1;
            bool monthStarted = false;

            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLS; col++)
                {
                    Panel dayPanel = _dayPanels[row, col];
                    Label lblDay = dayPanel.Controls["lblDay"] as Label;
                    Label lblTaskCount = dayPanel.Controls["lblTaskCount"] as Label;

                    if (!monthStarted && row == 0 && col == startDayOfWeek)
                    {
                        monthStarted = true;
                    }

                    if (monthStarted && dayNumber <= daysInMonth)
                    {
                        DateTime currentDate = new DateTime(_currentMonth.Year, _currentMonth.Month, dayNumber);

                        lblDay.Text = dayNumber.ToString();
                        lblDay.ForeColor = Color.FromArgb(52, 73, 94);
                        dayPanel.Tag = currentDate;
                        dayPanel.BackColor = Color.White;

                        // Highlight ngày hôm nay
                        if (currentDate == today)
                        {
                            dayPanel.BackColor = Color.FromArgb(52, 152, 219);
                            lblDay.ForeColor = Color.White;
                        }

                        // Hiển thị số lượng task
                        if (_taskCountByDate.ContainsKey(currentDate) && _taskCountByDate[currentDate] > 0)
                        {
                            int taskCount = _taskCountByDate[currentDate];
                            lblTaskCount.Text = $"{taskCount} task";
                            lblTaskCount.BackColor = Color.FromArgb(231, 76, 60);
                            lblTaskCount.Visible = true;
                        }
                        else
                        {
                            lblTaskCount.Visible = false;
                        }

                        dayNumber++;
                    }
                    else
                    {
                        // Ngày ngoài tháng hiện tại
                        lblDay.Text = "";
                        lblTaskCount.Visible = false;
                        dayPanel.Tag = null;
                        dayPanel.BackColor = Color.FromArgb(245, 245, 245);
                    }
                }
            }
        }

        /// <summary>
        /// Lấy panel cha từ control (có thể là panel hoặc label)
        /// </summary>
        private Panel GetDayPanel(object sender)
        {
            Control control = sender as Control;
            if (control == null) return null;
            
            // Nếu là panel thì return luôn
            if (control is Panel) return control as Panel;
            
            // Nếu là label thì lấy parent (panel)
            return control.Parent as Panel;
        }

        /// <summary>
        /// Event khi hover vào ngày
        /// </summary>
        private void DayPanel_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = GetDayPanel(sender);
            if (panel?.Tag != null)
            {
                DateTime date = (DateTime)panel.Tag;
                if (date != DateTime.Today)
                {
                    panel.BackColor = Color.FromArgb(189, 195, 199);
                }
            }
        }

        /// <summary>
        /// Event khi rời khỏi ngày
        /// </summary>
        private void DayPanel_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = GetDayPanel(sender);
            if (panel?.Tag != null)
            {
                DateTime date = (DateTime)panel.Tag;
                if (date == DateTime.Today)
                {
                    panel.BackColor = Color.FromArgb(52, 152, 219);
                }
                else
                {
                    panel.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// Event khi click vào ngày
        /// </summary>
        private void DayPanel_Click(object sender, EventArgs e)
        {
            Panel panel = GetDayPanel(sender);
            if (panel?.Tag != null)
            {
                DateTime selectedDate = (DateTime)panel.Tag;
                
                // Visual feedback: Flash màu khi click
                Color originalColor = panel.BackColor;
                panel.BackColor = Color.FromArgb(46, 204, 113); // Xanh lá
                
                // Sử dụng Timer để restore màu sau 100ms
                Timer flashTimer = new Timer();
                flashTimer.Interval = 100;
                flashTimer.Tick += (s, args) =>
                {
                    if (selectedDate == DateTime.Today)
                    {
                        panel.BackColor = Color.FromArgb(52, 152, 219);
                    }
                    else
                    {
                        panel.BackColor = Color.White;
                    }
                    flashTimer.Stop();
                    flashTimer.Dispose();
                };
                flashTimer.Start();
                
                // Trigger event
                DateClicked?.Invoke(this, selectedDate);
            }
        }

        /// <summary>
        /// Làm mới dữ liệu
        /// </summary>
        public void RefreshData()
        {
            LoadTasksForMonth();
        }
    }
}

