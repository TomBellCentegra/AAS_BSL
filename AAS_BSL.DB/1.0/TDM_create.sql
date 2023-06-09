CREATE TABLE [TDM_Transaction](
    TDMTransactionID NVARCHAR(150) NOT NULL,
    BusinessDay DATETIME NULL,
    CloseDate DATETIME NULL,
    OpenDate DATETIME NULL,
    IsTraining BIT DEFAULT 0 NOT NULL,
    SiteInfoId NVARCHAR(100) NULL,
    SiteInfoName NVARCHAR(100)  NULL,
    SiteInfoTimeZone NVARCHAR(100)  NULL,
    EmployeeName NVARCHAR(150)  NULL,
    Employees NVARCHAR(255)  NULL,
    EmployeeShiftId NVARCHAR(100)  NULL,
    IsDeleted BIT DEFAULT 0 NOT NULL,
    IsOpen BIT DEFAULT 0 NOT NULL,
    IsVoided BIT DEFAULT 0 NOT NULL,
    LocalCurrency NVARCHAR(20)  NULL,
    Location NVARCHAR(100)  NULL,
    LocationId NVARCHAR(10)  NULL,
    ReceiptId NVARCHAR(100)  NULL,
    TransactionType NVARCHAR(100)  NULL,
    CreatedDate DATETIME DEFAULT GETDATE() NOT NULL,
    Batched INT DEFAULT 0 NOT NULL,
    CONSTRAINT PK_TDM_Transaction_TDMTransactionID PRIMARY KEY (TDMTransactionID)
)
CREATE TABLE [TDM_Transaction_Payload]
(
    TDMTransactionID NVARCHAR(150) NOT NULL ,
    Payload NVARCHAR(MAX) NOT NULL,
)
CREATE TABLE [TDM_Item](
    ItemID INT NOT NULL PRIMARY KEY IDENTITY,
    Discount DECIMAL(24,6) NULL,
    UnitPrice DECIMAL(24,6) NULL,
    Quantity INT NOT NULL,
    Measurement VARCHAR(50) NULL,
    ProductId VARCHAR(100) NULL,
    ProductName VARCHAR(200) NULL,
    ProductPrice DECIMAL(24,6) NULL,
    ParentItemId VARCHAR(150) NULL,
    TDMTransactionID NVARCHAR(150) NOT NULL FOREIGN KEY REFERENCES [TDM_Transaction] (TDMTransactionID),
)
CREATE TABLE [TDM_Payment](
    PaymentID INT NOT NULL PRIMARY KEY IDENTITY,
    ExternalPaymentID IN NOT NULL,
    [Type] VARCHAR(100) NULL,
    Amount DECIMAL(24,6) NOT NULL,
    Currency VARCHAR(50) NOT NULL,
    Change DECIMAL(24,6)  NULL,
    GrandAmount DECIMAL(24,6)  NULL,
    NetAmount DECIMAL(24,6)  NULL,
    GrossAmount DECIMAL(24,6)  NULL,
    VoidsAmount DECIMAL(24,6)  NULL,
    DiscountAmount DECIMAL(24,6)  NULL,
    TaxExclusive DECIMAL(24,6)  NULL,
    TDMTransactionID NVARCHAR(150) NOT NULL FOREIGN KEY REFERENCES [TDM_Transaction] (TDMTransactionID),
)