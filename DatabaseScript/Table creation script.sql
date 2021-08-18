USE [STDemoAdmin]
GO

BEGIN TRY
	BEGIN TRANSACTION
		IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
		BEGIN
			DROP TABLE [dbo].[Person]
		END

		CREATE TABLE [dbo].[Person]
		(
			Id						INT IDENTITY(1, 1) CONSTRAINT [PK_Person_Id] PRIMARY KEY CLUSTERED
			, AccountNumber			VARCHAR(10) NOT NULL
			, FirstName				VARCHAR(50) NOT NULL
			, MiddleName			VARCHAR(50) NULL
			, LastName				VARCHAR(50) NOT NULL
			, DisplayName			VARCHAR(100) NOT NULL
			, SSN					VARCHAR(12)	NOT NULL
			, IsActive				BIT NOT NULL CONSTRAINT DF_AccountInformation_IsActive DEFAULT (1)
			, TerminationDate		DATETIME NULL
			, CreatedBy				VARCHAR(50) NULL
			, CreatedDate			DATETIME NOT NULL CONSTRAINT DF_AccountInformation_CreatedDate DEFAULT (GETDATE())
			, ModifiedDate			DATETIME NULL
			, ModifiedBy			VARCHAR(50) NULL
		)

		--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bank]') AND type in (N'U'))
		--BEGIN
		--	DROP TABLE [dbo].[Bank]
		--END

		--CREATE TABLE [dbo].[Bank]
		--(
		--	Id						INT IDENTITY(1, 1) CONSTRAINT [PK_Bank_Id] PRIMARY KEY CLUSTERED
		--	, PersonId				INT NOT NULL CONSTRAINT [FK_Person_Id] FOREIGN KEY REFERENCES [dbo].[Person](Id) 
		--	, [Name]				VARCHAR(20) NOT NULL
		--	, AccountNumber			VARCHAR(10) NOT NULL
		--	, RoutingNumber			VARCHAR(15) NOT NULL
		--	, AccountType			VARCHAR(15) NOT NULL
		--	, IsActive				BIT NOT NULL CONSTRAINT DF_Bank_IsActive DEFAULT (1)
		--	, CreatedBy				VARCHAR(50) NULL
		--	, CreatedDate			DATETIME NOT NULL CONSTRAINT DF_Bank_CreatedDate DEFAULT (GETDATE())
		--	, ModifiedDate			DATETIME NULL
		--	, ModifiedBy			VARCHAR(50) NULL
		--)
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	SELECT	ERROR_NUMBER() AS ErrorNumber
			, ERROR_SEVERITY() AS ErrorSeverity
			, ERROR_STATE() AS ErrorState
			, ERROR_PROCEDURE() AS ErrorProcedure
			, ERROR_LINE() AS ErrorLine
			, ERROR_MESSAGE() AS ErrorMessage;
	RETURN;
END CATCH
GO