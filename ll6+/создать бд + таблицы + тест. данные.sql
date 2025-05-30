-- Создание базы данных
CREATE DATABASE Lunopark;
GO

USE Lunopark;
GO

-- Создание таблицы Персонал
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200),
    Phone NVARCHAR(20),
    BirthYear INT CHECK (BirthYear BETWEEN 1900 AND YEAR(GETDATE())),
    Position NVARCHAR(50) NOT NULL
);
GO

-- Создание таблицы Аттракционы
CREATE TABLE Attractions (
    AttractionID INT PRIMARY KEY IDENTITY(1,1),
    AttractionName NVARCHAR(100) NOT NULL,
    InstallationYear INT NOT NULL CHECK (InstallationYear BETWEEN 1900 AND YEAR(GETDATE())),
    ResponsibleEmployeeID INT,
    FOREIGN KEY (ResponsibleEmployeeID) REFERENCES Employees(EmployeeID)
);
GO

-- Создание таблицы Билеты
CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY IDENTITY(1,1),
    TicketNumber NVARCHAR(20) NOT NULL UNIQUE,
    AttractionID INT NOT NULL,
    TicketPrice DECIMAL(10, 2) NOT NULL CHECK (TicketPrice > 0),
    EmployeeID INT,
    SaleDate DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AttractionID) REFERENCES Attractions(AttractionID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);
GO

-- Создание индексов для улучшения производительности
CREATE INDEX IX_Employees_Position ON Employees(Position);
CREATE INDEX IX_Attractions_Name ON Attractions(AttractionName);
CREATE INDEX IX_Tickets_SaleDate ON Tickets(SaleDate);
GO

-- =============================================
-- Хранимые процедуры для таблицы Employees
-- =============================================

CREATE PROCEDURE sp_GetAllEmployees
AS
BEGIN
    SELECT EmployeeID, FullName, Address, Phone, BirthYear, Position 
    FROM Employees;
END
GO

CREATE PROCEDURE sp_GetEmployeeById
    @EmployeeID INT
AS
BEGIN
    SELECT EmployeeID, FullName, Address, Phone, BirthYear, Position 
    FROM Employees
    WHERE EmployeeID = @EmployeeID;
END
GO

CREATE PROCEDURE sp_AddEmployee
    @FullName NVARCHAR(100),
    @Address NVARCHAR(200) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @BirthYear INT = NULL,
    @Position NVARCHAR(50)
AS
BEGIN
    INSERT INTO Employees (FullName, Address, Phone, BirthYear, Position)
    VALUES (@FullName, @Address, @Phone, @BirthYear, @Position);
    
    SELECT SCOPE_IDENTITY() AS EmployeeID;
END
GO

CREATE PROCEDURE sp_UpdateEmployee
    @EmployeeID INT,
    @FullName NVARCHAR(100),
    @Address NVARCHAR(200) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @BirthYear INT = NULL,
    @Position NVARCHAR(50)
AS
BEGIN
    UPDATE Employees
    SET FullName = @FullName,
        Address = @Address,
        Phone = @Phone,
        BirthYear = @BirthYear,
        Position = @Position
    WHERE EmployeeID = @EmployeeID;
END
GO

CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;
END
GO

-- =============================================
-- Хранимые процедуры для таблицы Attractions
-- =============================================

CREATE PROCEDURE sp_GetAllAttractions
AS
BEGIN
    SELECT AttractionID, AttractionName, InstallationYear, ResponsibleEmployeeID 
    FROM Attractions;
END
GO

CREATE PROCEDURE sp_GetAttractionById
    @AttractionID INT
AS
BEGIN
    SELECT AttractionID, AttractionName, InstallationYear, ResponsibleEmployeeID 
    FROM Attractions
    WHERE AttractionID = @AttractionID;
END
GO

CREATE PROCEDURE sp_AddAttraction
    @AttractionName NVARCHAR(100),
    @InstallationYear INT,
    @ResponsibleEmployeeID INT = NULL
AS
BEGIN
    INSERT INTO Attractions (AttractionName, InstallationYear, ResponsibleEmployeeID)
    VALUES (@AttractionName, @InstallationYear, @ResponsibleEmployeeID);
    
    SELECT SCOPE_IDENTITY() AS AttractionID;
END
GO

CREATE PROCEDURE sp_UpdateAttraction
    @AttractionID INT,
    @AttractionName NVARCHAR(100),
    @InstallationYear INT,
    @ResponsibleEmployeeID INT = NULL
AS
BEGIN
    UPDATE Attractions
    SET AttractionName = @AttractionName,
        InstallationYear = @InstallationYear,
        ResponsibleEmployeeID = @ResponsibleEmployeeID
    WHERE AttractionID = @AttractionID;
END
GO

CREATE PROCEDURE sp_DeleteAttraction
    @AttractionID INT
AS
BEGIN
    DELETE FROM Attractions WHERE AttractionID = @AttractionID;
END
GO

CREATE PROCEDURE sp_GetAttractionsByEmployee
    @EmployeeID INT
AS
BEGIN
    SELECT a.AttractionID, a.AttractionName, a.InstallationYear
    FROM Attractions a
    WHERE a.ResponsibleEmployeeID = @EmployeeID;
END
GO

-- =============================================
-- Хранимые процедуры для таблицы Tickets
-- =============================================

CREATE PROCEDURE sp_GetAllTickets
AS
BEGIN
    SELECT t.TicketID, t.TicketNumber, t.AttractionID, a.AttractionName, 
           t.TicketPrice, t.EmployeeID, e.FullName AS EmployeeName, t.SaleDate
    FROM Tickets t
    LEFT JOIN Attractions a ON t.AttractionID = a.AttractionID
    LEFT JOIN Employees e ON t.EmployeeID = e.EmployeeID;
END
GO

CREATE PROCEDURE sp_GetTicketById
    @TicketID INT
AS
BEGIN
    SELECT t.TicketID, t.TicketNumber, t.AttractionID, a.AttractionName, 
           t.TicketPrice, t.EmployeeID, e.FullName AS EmployeeName, t.SaleDate
    FROM Tickets t
    LEFT JOIN Attractions a ON t.AttractionID = a.AttractionID
    LEFT JOIN Employees e ON t.EmployeeID = e.EmployeeID
    WHERE t.TicketID = @TicketID;
END
GO

CREATE PROCEDURE sp_AddTicket
    @TicketNumber NVARCHAR(20),
    @AttractionID INT,
    @TicketPrice DECIMAL(10, 2),
    @EmployeeID INT = NULL,
    @SaleDate DATE = NULL
AS
BEGIN
    IF @SaleDate IS NULL
        SET @SaleDate = GETDATE();
    
    INSERT INTO Tickets (TicketNumber, AttractionID, TicketPrice, EmployeeID, SaleDate)
    VALUES (@TicketNumber, @AttractionID, @TicketPrice, @EmployeeID, @SaleDate);
    
    SELECT SCOPE_IDENTITY() AS TicketID;
END
GO

CREATE PROCEDURE sp_UpdateTicket
    @TicketID INT,
    @TicketNumber NVARCHAR(20),
    @AttractionID INT,
    @TicketPrice DECIMAL(10, 2),
    @EmployeeID INT = NULL,
    @SaleDate DATE
AS
BEGIN
    UPDATE Tickets
    SET TicketNumber = @TicketNumber,
        AttractionID = @AttractionID,
        TicketPrice = @TicketPrice,
        EmployeeID = @EmployeeID,
        SaleDate = @SaleDate
    WHERE TicketID = @TicketID;
END
GO

CREATE PROCEDURE sp_DeleteTicket
    @TicketID INT
AS
BEGIN
    DELETE FROM Tickets WHERE TicketID = @TicketID;
END
GO

CREATE PROCEDURE sp_GetTicketsByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT t.TicketID, t.TicketNumber, t.AttractionID, a.AttractionName, 
           t.TicketPrice, t.EmployeeID, e.FullName AS EmployeeName, t.SaleDate
    FROM Tickets t
    LEFT JOIN Attractions a ON t.AttractionID = a.AttractionID
    LEFT JOIN Employees e ON t.EmployeeID = e.EmployeeID
    WHERE t.SaleDate BETWEEN @StartDate AND @EndDate;
END
GO

-- =============================================
-- Создание представлений
-- =============================================

CREATE VIEW v_EmployeeAttractions AS
SELECT e.EmployeeID, e.FullName, e.Position, 
       COUNT(a.AttractionID) AS AttractionsCount,
       STRING_AGG(a.AttractionName, ', ') AS AttractionNames
FROM Employees e
LEFT JOIN Attractions a ON e.EmployeeID = a.ResponsibleEmployeeID
GROUP BY e.EmployeeID, e.FullName, e.Position;
GO

CREATE VIEW v_DailyRevenue AS
SELECT 
    CAST(SaleDate AS DATE) AS SaleDay,
    COUNT(TicketID) AS TicketsSold,
    SUM(TicketPrice) AS TotalRevenue
FROM Tickets
GROUP BY CAST(SaleDate AS DATE);
GO

CREATE VIEW v_AttractionPopularity AS
SELECT 
    a.AttractionID,
    a.AttractionName,
    COUNT(t.TicketID) AS TicketsSold,
    SUM(t.TicketPrice) AS TotalRevenue
FROM Attractions a
LEFT JOIN Tickets t ON a.AttractionID = t.AttractionID
GROUP BY a.AttractionID, a.AttractionName;
GO