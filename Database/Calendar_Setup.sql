-- =====================================================================
-- CALENDAR SETUP - CÀI ĐẶT LỊCH CÔNG VIỆC
-- =====================================================================

USE QuanLyCongViec;
GO

PRINT '========================================';
PRINT '  BẮT ĐẦU CÀI ĐẶT CALENDAR';
PRINT '========================================';
GO

-- =====================================================================
-- BƯỚC 1: THÊM CỘT STARTDATE
-- =====================================================================

PRINT '';
PRINT 'BƯỚC 1: Kiểm tra và thêm cột StartDate...';
GO

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Tasks') AND name = 'StartDate')
BEGIN
    ALTER TABLE Tasks ADD StartDate DATETIME NULL;
    PRINT '✅ Đã thêm cột StartDate';
END
ELSE
BEGIN
    PRINT '⚠️  Cột StartDate đã tồn tại';
END
GO

-- =====================================================================
-- BƯỚC 2: CẬP NHẬT DỮ LIỆU
-- =====================================================================

PRINT '';
PRINT 'BƯỚC 2: Cập nhật dữ liệu cho StartDate...';
GO

UPDATE Tasks
SET StartDate = CASE 
    WHEN CreatedDate < DueDate THEN CreatedDate
    ELSE DATEADD(DAY, -1, DueDate)
END
WHERE StartDate IS NULL;

PRINT '✅ Đã cập nhật StartDate cho các task cũ';
GO

-- =====================================================================
-- BƯỚC 3: ĐẶT NOT NULL
-- =====================================================================

PRINT '';
PRINT 'BƯỚC 3: Đặt StartDate thành NOT NULL...';
GO

ALTER TABLE Tasks ALTER COLUMN StartDate DATETIME NOT NULL;
PRINT '✅ StartDate đã là NOT NULL';
GO

-- =====================================================================
-- BƯỚC 4: TẠO INDEXES
-- =====================================================================

PRINT '';
PRINT 'BƯỚC 4: Tạo indexes...';
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Tasks_StartDate' AND object_id = OBJECT_ID('Tasks'))
BEGIN
    CREATE INDEX IX_Tasks_StartDate ON Tasks(StartDate);
    PRINT '✅ Index IX_Tasks_StartDate';
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Tasks_StartDate_DueDate' AND object_id = OBJECT_ID('Tasks'))
BEGIN
    CREATE INDEX IX_Tasks_StartDate_DueDate ON Tasks(StartDate, DueDate);
    PRINT '✅ Index IX_Tasks_StartDate_DueDate';
END
GO

-- =====================================================================
-- BƯỚC 5: STORED PROCEDURES
-- =====================================================================

PRINT '';
PRINT 'BƯỚC 5: Tạo stored procedures...';
GO

-- SP: Tạo task
IF OBJECT_ID('sp_CreateTask', 'P') IS NOT NULL DROP PROCEDURE sp_CreateTask;
GO

CREATE PROCEDURE sp_CreateTask
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
    @UserId INT,
    @Priority NVARCHAR(20) = 'Medium',
    @Status NVARCHAR(20) = 'Todo',
    @Category NVARCHAR(20) = 'Work',
    @StartDate DATETIME,
    @DueDate DATETIME,
    @TaskId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    IF @StartDate > @DueDate
    BEGIN
        RAISERROR(N'Ngày bắt đầu phải <= ngày kết thúc!', 16, 1);
        RETURN;
    END
    INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, StartDate, DueDate, CreatedDate)
    VALUES (@Title, @Description, @UserId, @Priority, @Status, @Category, @StartDate, @DueDate, GETDATE());
    SET @TaskId = SCOPE_IDENTITY();
END;
GO

PRINT '✅ sp_CreateTask';
GO

-- SP: Cập nhật task
IF OBJECT_ID('sp_UpdateTask', 'P') IS NOT NULL DROP PROCEDURE sp_UpdateTask;
GO

CREATE PROCEDURE sp_UpdateTask
    @TaskId INT,
    @Title NVARCHAR(200),
    @Description NVARCHAR(MAX) = NULL,
    @UserId INT,
    @Priority NVARCHAR(20),
    @Status NVARCHAR(20),
    @Category NVARCHAR(20),
    @StartDate DATETIME,
    @DueDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 1 FROM Tasks WHERE Id = @TaskId AND UserId = @UserId AND IsDeleted = 0)
    BEGIN
        RAISERROR(N'Không có quyền!', 16, 1);
        RETURN;
    END
    IF @StartDate > @DueDate
    BEGIN
        RAISERROR(N'Ngày bắt đầu phải <= ngày kết thúc!', 16, 1);
        RETURN;
    END
    UPDATE Tasks
    SET Title = @Title, Description = @Description, Priority = @Priority,
        Status = @Status, Category = @Category, StartDate = @StartDate, DueDate = @DueDate
    WHERE Id = @TaskId;
END;
GO

PRINT '✅ sp_UpdateTask';
GO

-- SP: Lấy tasks theo filter
IF OBJECT_ID('sp_GetTasksByFilter', 'P') IS NOT NULL DROP PROCEDURE sp_GetTasksByFilter;
GO

CREATE PROCEDURE sp_GetTasksByFilter
    @UserId INT,
    @Status NVARCHAR(20) = NULL,
    @Priority NVARCHAR(20) = NULL,
    @Category NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.Id, t.Title, t.Description, t.Priority, t.Status, t.Category,
           t.StartDate, t.DueDate, t.CreatedDate, t.CompletedDate, u.FullName AS UserFullName
    FROM Tasks t
    INNER JOIN Users u ON t.UserId = u.Id
    WHERE t.UserId = @UserId AND t.IsDeleted = 0
      AND (@Status IS NULL OR t.Status = @Status)
      AND (@Priority IS NULL OR t.Priority = @Priority)
      AND (@Category IS NULL OR t.Category = @Category)
    ORDER BY CASE t.Priority WHEN 'High' THEN 1 WHEN 'Medium' THEN 2 WHEN 'Low' THEN 3 ELSE 4 END, t.StartDate;
END;
GO

PRINT '✅ sp_GetTasksByFilter';
GO

-- SP: Đếm tasks theo tháng
IF OBJECT_ID('sp_GetTaskCountByMonth', 'P') IS NOT NULL DROP PROCEDURE sp_GetTaskCountByMonth;
GO

CREATE PROCEDURE sp_GetTaskCountByMonth
    @UserId INT,
    @Year INT,
    @Month INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @FirstDay DATE = DATEFROMPARTS(@Year, @Month, 1);
    DECLARE @LastDay DATE = EOMONTH(@FirstDay);
    
    WITH DateRange AS (
        SELECT @FirstDay AS TaskDate
        UNION ALL
        SELECT DATEADD(DAY, 1, TaskDate) FROM DateRange WHERE TaskDate < @LastDay
    ),
    TaskCounts AS (
        SELECT d.TaskDate, COUNT(t.Id) AS TaskCount
        FROM DateRange d
        LEFT JOIN Tasks t ON t.UserId = @UserId AND t.IsDeleted = 0
            AND CAST(t.StartDate AS DATE) <= d.TaskDate
            -- ✅ Nếu task Done: chỉ hiển thị đến ngày hoàn thành
            AND (
                (t.Status = 'Done' AND CAST(ISNULL(t.CompletedDate, t.DueDate) AS DATE) >= d.TaskDate)
                OR (t.Status != 'Done' AND CAST(t.DueDate AS DATE) >= d.TaskDate)
            )
        GROUP BY d.TaskDate
    )
    SELECT TaskDate, TaskCount
    FROM TaskCounts
    WHERE TaskCount > 0
    ORDER BY TaskDate
    OPTION (MAXRECURSION 31);
END;
GO

PRINT '✅ sp_GetTaskCountByMonth';
GO

-- SP: Lấy tasks theo ngày
IF OBJECT_ID('sp_GetTasksByDate', 'P') IS NOT NULL DROP PROCEDURE sp_GetTasksByDate;
GO

CREATE PROCEDURE sp_GetTasksByDate
    @UserId INT,
    @SelectedDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Title, Description, Priority, Status, Category, StartDate, DueDate,
           CreatedDate, CompletedDate,
           CASE 
               WHEN Status = 'Done' THEN N'Hoàn thành'
               WHEN CAST(DueDate AS DATE) < CAST(GETDATE() AS DATE) THEN N'Quá hạn'
               WHEN CAST(DueDate AS DATE) = CAST(GETDATE() AS DATE) THEN N'Hôm nay'
               ELSE N'Sắp tới'
           END AS StatusLabel,
           CASE Priority
               WHEN N'High' THEN '#E74C3C'
               WHEN N'Medium' THEN '#F39C12'
               WHEN N'Low' THEN '#95A5A6'
               ELSE '#BDC3C7'
           END AS PriorityColor,
           CASE 
               WHEN CAST(StartDate AS DATE) = @SelectedDate AND CAST(DueDate AS DATE) = @SelectedDate THEN N'📍 Trong ngày'
               WHEN CAST(StartDate AS DATE) = @SelectedDate THEN N'🚀 Bắt đầu'
               WHEN CAST(DueDate AS DATE) = @SelectedDate THEN N'🏁 Kết thúc'
               ELSE N'⏳ Đang tiến hành'
           END AS DateRangeLabel
    FROM Tasks
    WHERE UserId = @UserId AND IsDeleted = 0
      AND CAST(StartDate AS DATE) <= @SelectedDate
      -- ✅ Nếu task Done: chỉ hiển thị đến ngày hoàn thành
      AND (
          (Status = 'Done' AND CAST(ISNULL(CompletedDate, DueDate) AS DATE) >= @SelectedDate)
          OR (Status != 'Done' AND CAST(DueDate AS DATE) >= @SelectedDate)
      )
    ORDER BY CASE Priority WHEN N'High' THEN 1 WHEN N'Medium' THEN 2 WHEN N'Low' THEN 3 ELSE 4 END, StartDate;
END;
GO

PRINT '✅ sp_GetTasksByDate';
GO

-- =====================================================================
-- HOÀN TẤT
-- =====================================================================

PRINT '';
PRINT '========================================';
PRINT '  ✅ CÀI ĐẶT HOÀN TẤT!';
PRINT '========================================';
PRINT '';
