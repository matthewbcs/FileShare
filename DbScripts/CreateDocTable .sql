IF (OBJECT_ID('dbo.Document', 'U') IS NULL)
BEGIN
	CREATE TABLE [dbo].[Document](
		DocumentId int IDENTITY(1,1) NOT NULL,
		DocumentName varchar(200) NOT NULL,
		DocumentData varbinary(max) NULL,
		DocumentType varchar(50),
		DateAddedOnUtc datetime2(4) NOT NULL,
		DateToDeleteByUtc datetime2(4) NOT NULL,
		AccessCode int NOT NULL
		CONSTRAINT PK_Document PRIMARY KEY CLUSTERED (DocumentId)
	)
END
GO

IF (OBJECT_ID('dbo.TransportText', 'U') IS NULL)
BEGIN
	CREATE TABLE [dbo].[TransportText](
		TransportTextId int IDENTITY(1,1) NOT NULL,
		TransportTextData varchar(max) NULL,
		TransportTextDataEncypted varchar(max) NULL,
		DateAddedOnUtc datetime2(4) NOT NULL,
		DateToDeleteByUtc datetime2(4) NOT NULL,
		AccessCode int NOT NULL
		CONSTRAINT PK_TransportText PRIMARY KEY CLUSTERED (TransportTextId)
	)
END
GO