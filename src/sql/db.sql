DROP TABLE LSCATransaction
CREATE TABLE LSCATransaction (
    CompanyID int NOT NULL,
    TransactionType char(1) NOT NULL,
    ReferenceNumber nvarchar(15) NOT NULL,
    TranDate smalldatetime NULL,
    Descr nvarchar(125) NULL,
    RefNoteID uniqueidentifier NULL,
    LastLineNbr int NULL DEFAULT 0,
    PRIMARY KEY (CompanyID, TransactionType, ReferenceNumber)
);

DROP TABLE LSCATransactionDetail
CREATE TABLE LSCATransactionDetail (
    CompanyID int NOT NULL,
    TransactionType char(1) NOT NULL,
    ReferenceNumber nvarchar(15) NOT NULL,
    LineNbr int NOT NULL,
    Qty decimal(25, 6) NULL,
    ReasonCode nvarchar(15) NULL,
    TranDescr nvarchar(125) NULL,
    PRIMARY KEY (CompanyID, TransactionType, ReferenceNumber, LineNbr)
);

DROP TABLE LSCASetup
CREATE TABLE LSCASetup (
    CompanyID int NOT NULL PRIMARY KEY,
    TransactionNumberingID nvarchar(15) NULL,
	CarbonInventoryID int NULL
);
