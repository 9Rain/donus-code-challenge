/*
Deployment script for IBankDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "IBankDB"
:setvar DefaultFilePrefix "IBankDB"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating database $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating Table [dbo].[Account]...';


GO
CREATE TABLE [dbo].[Account] (
    [Id]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedAt]     DATETIME        NOT NULL,
    [UpdatedAt]     DATETIME        NOT NULL,
    [Number]        VARCHAR (10)    NOT NULL,
    [Digit]         TINYINT         NOT NULL,
    [ShortPassword] VARCHAR (100)   NOT NULL,
    [Password]      VARCHAR (100)   NOT NULL,
    [Balance]       DECIMAL (19, 2) NOT NULL,
    [IsActive]      BIT             NOT NULL,
    [ClientId]      BIGINT          NOT NULL,
    [AgencyId]      INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Account_Number] UNIQUE NONCLUSTERED ([Number] ASC)
);


GO
PRINT N'Creating Table [dbo].[Agency]...';


GO
CREATE TABLE [dbo].[Agency] (
    [Id]        INT         IDENTITY (1, 1) NOT NULL,
    [CreatedAt] DATETIME    NOT NULL,
    [UpdatedAt] DATETIME    NOT NULL,
    [Number]    VARCHAR (4) NOT NULL,
    [Digit]     TINYINT     NOT NULL,
    [IsActive]  BIT         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Agency_Number] UNIQUE NONCLUSTERED ([Number] ASC)
);


GO
PRINT N'Creating Table [dbo].[Client]...';


GO
CREATE TABLE [dbo].[Client] (
    [Id]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [CreatedAt] DATETIME       NOT NULL,
    [UpdatedAt] DATETIME       NOT NULL,
    [Name]      NVARCHAR (100) NOT NULL,
    [Cpf]       VARCHAR (14)   NOT NULL,
    [IsActive]  BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Client_Cpf] UNIQUE NONCLUSTERED ([Cpf] ASC)
);


GO
PRINT N'Creating Table [dbo].[Transaction]...';


GO
CREATE TABLE [dbo].[Transaction] (
    [Id]                    BIGINT          IDENTITY (1, 1) NOT NULL,
    [CreatedAt]             DATETIME        NOT NULL,
    [UpdatedAt]             DATETIME        NOT NULL,
    [CompletedAt]           DATETIME        NULL,
    [DesiredCompletionDate] DATETIME        NOT NULL,
    [ReferenceId]           VARCHAR (100)   NOT NULL,
    [Amount]                DECIMAL (19, 2) NOT NULL,
    [IsIncome]              BIT             NOT NULL,
    [IsCompleted]           BIT             NOT NULL,
    [IsActive]              BIT             NOT NULL,
    [ActionId]              INT             NOT NULL,
    [FromAccountId]         BIGINT          NULL,
    [ToAccountId]           BIGINT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Transaction_ReferenceId] UNIQUE NONCLUSTERED ([ReferenceId] ASC)
);


GO
PRINT N'Creating Table [dbo].[TransactionAction]...';


GO
CREATE TABLE [dbo].[TransactionAction] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [CreatedAt] DATETIME     NOT NULL,
    [UpdatedAt] DATETIME     NOT NULL,
    [Name]      VARCHAR (50) NOT NULL,
    [IsActive]  BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Account]...';


GO
ALTER TABLE [dbo].[Account]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Account]...';


GO
ALTER TABLE [dbo].[Account]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [UpdatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Account]...';


GO
ALTER TABLE [dbo].[Account]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Agency]...';


GO
ALTER TABLE [dbo].[Agency]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Agency]...';


GO
ALTER TABLE [dbo].[Agency]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [UpdatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Agency]...';


GO
ALTER TABLE [dbo].[Agency]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [UpdatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Client]...';


GO
ALTER TABLE [dbo].[Client]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [UpdatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT NULL FOR [CompletedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [DesiredCompletionDate];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT 0 FOR [IsCompleted];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT NULL FOR [FromAccountId];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Transaction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD DEFAULT NULL FOR [ToAccountId];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[TransactionAction]...';


GO
ALTER TABLE [dbo].[TransactionAction]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [CreatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[TransactionAction]...';


GO
ALTER TABLE [dbo].[TransactionAction]
    ADD DEFAULT CURRENT_TIMESTAMP FOR [UpdatedAt];


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[TransactionAction]...';


GO
ALTER TABLE [dbo].[TransactionAction]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Creating Foreign Key [dbo].[FK_Account_Client]...';


GO
ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [FK_Account_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Account_Agency]...';


GO
ALTER TABLE [dbo].[Account]
    ADD CONSTRAINT [FK_Account_Agency] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Transaction_TransactionsAction]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD CONSTRAINT [FK_Transaction_TransactionsAction] FOREIGN KEY ([ActionId]) REFERENCES [dbo].[TransactionAction] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Transaction_Account_From]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD CONSTRAINT [FK_Transaction_Account_From] FOREIGN KEY ([FromAccountId]) REFERENCES [dbo].[Account] ([Id]);


GO
PRINT N'Creating Foreign Key [dbo].[FK_Transaction_Account_To]...';


GO
ALTER TABLE [dbo].[Transaction]
    ADD CONSTRAINT [FK_Transaction_Account_To] FOREIGN KEY ([ToAccountId]) REFERENCES [dbo].[Account] ([Id]);


GO
PRINT N'Creating Procedure [dbo].[spAccount_Create]...';


GO
CREATE PROCEDURE [dbo].[spAccount_Create]
	@Number varchar(10),
	@Digit tinyint,
	@ShortPassword varchar(100),
	@Password varchar(100),
	@Balance decimal(19,2) = 0,
	@ClientId bigint,
	@AgencyId int
AS
BEGIN
	INSERT INTO [dbo].[Account] ([Number], [Digit], [ShortPassword], [Password], 
	[Balance], [IsActive], [ClientId], [AgencyId]) OUTPUT INSERTED.[Id] 
	VALUES (@Number, @Digit, @ShortPassword, @Password, @Balance, 1, @ClientId, @AgencyId);
END
GO
PRINT N'Creating Procedure [dbo].[spAccount_GetBalance]...';


GO
CREATE PROCEDURE [dbo].[spAccount_GetBalance]
	@ClientId bigint
AS
BEGIN
	SELECT AC.[Balance]
	FROM [dbo].[Account] AC 
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	INNER JOIN [dbo].[Client] CL ON AC.[ClientId] = CL.[Id]
	WHERE AC.[ClientId] = @ClientId AND AC.[IsActive] = 1
	AND AG.[IsActive] = 1 AND CL.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spAccount_GetByClientId]...';


GO
CREATE PROCEDURE [dbo].[spAccount_GetByClientId]
	@ClientId bigint
AS
BEGIN
	SELECT AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], AC.[ClientId],
	AG.[Id], AG.[Number], AG.[Digit]
	FROM [dbo].[Account] AC 
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	INNER JOIN [dbo].[Client] CL ON AC.[ClientId] = CL.[Id]
	WHERE AC.[ClientId] = @ClientId AND AC.[IsActive] = 1
	AND AG.[IsActive] = 1 AND CL.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spAccount_GetByNumber]...';


GO
CREATE PROCEDURE [dbo].[spAccount_GetByNumber]
	@Number varchar(10)
AS
BEGIN
	SELECT AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], AC.[ClientId],
	AG.[Id], AG.[Number], AG.[Digit]
	FROM [dbo].[Account] AC 
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	INNER JOIN [dbo].[Client] CL ON AC.[ClientId] = CL.[Id]
	WHERE AC.[Number] = @Number AND AC.[IsActive] = 1 
	AND AG.[IsActive] = 1 AND CL.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spAccount_GetForTransaction]...';


GO
CREATE PROCEDURE [dbo].[spAccount_GetForTransaction]
	@Name nvarchar(100),
	@Cpf varchar(14),
	@AgencyNumber varchar(4),
	@AgencyDigit tinyint,
	@AccountNumber varchar(10),
	@AccountDigit tinyint
AS
BEGIN
	SELECT AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], AC.[ClientId],
	AG.[Id], AG.[Number], AG.[Digit]
	FROM [dbo].[Account] AC 
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	INNER JOIN [dbo].[Client] CL ON AC.[ClientId] = CL.[Id]
	WHERE CL.[Name] = @Name AND CL.[Cpf] = @Cpf 
	AND AG.[Number] = @AgencyNumber AND AG.[Digit] = @AgencyDigit
	AND AC.[Number] = @AccountNumber AND AC.[Digit] = @AccountDigit
	AND AC.[IsActive] = 1 AND AG.[IsActive] = 1 AND CL.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spAgency_Get]...';


GO
CREATE PROCEDURE [dbo].[spAgency_Get]
	@Id int
AS
BEGIN
	SELECT AG.[Id], AG.[Number], AG.[Digit]
	FROM [dbo].[Agency] AG
	WHERE AG.[Id] = @Id AND AG.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spClient_Create]...';


GO
CREATE PROCEDURE [dbo].[spClient_Create]
	@Name nvarchar(100),
	@Cpf varchar(14)
AS
BEGIN
	INSERT INTO [dbo].[Client] ([Name], [Cpf], [IsActive]) OUTPUT INSERTED.[Id] 
	VALUES (@Name, @Cpf, 1);
END
GO
PRINT N'Creating Procedure [dbo].[spClient_Delete]...';


GO
CREATE PROCEDURE [dbo].[spClient_Delete]
	@Id bigint
AS
BEGIN TRANSACTION
BEGIN
	UPDATE [dbo].[Client] 
	SET [IsActive] = 0, [UpdatedAt] = CURRENT_TIMESTAMP
	WHERE [Id] = @Id;

	UPDATE [dbo].[Account] 
	SET [IsActive] = 0, [UpdatedAt] = CURRENT_TIMESTAMP
	WHERE [ClientId] = @Id;
END
IF @@ERROR = 0
	COMMIT
ELSE
	ROLLBACK
GO
PRINT N'Creating Procedure [dbo].[spClient_Get]...';


GO
CREATE PROCEDURE [dbo].[spClient_Get]
	@Id bigint
AS
BEGIN
	SELECT CL.[Id], CL.[Name], CL.[Cpf], 
	AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], 
	AG.[Id], AG.[Number], AG.[Digit] 
	FROM [dbo].[Client] CL
	INNER JOIN [dbo].[Account] AC ON CL.[Id] = AC.[ClientId]
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	WHERE CL.[Id] = @Id AND CL.[IsActive] = 1
	AND AC.[IsActive] = 1 AND AG.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spClient_GetByCpf]...';


GO
CREATE PROCEDURE [dbo].[spClient_GetByCpf]
	@Cpf varchar(14)
AS
BEGIN
	SELECT CL.[Id], CL.[Name], CL.[Cpf], 
	AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], 
	AG.[Id], AG.[Number], AG.[Digit] 
	FROM [dbo].[Client] CL
	INNER JOIN [dbo].[Account] AC ON CL.[Id] = AC.[ClientId]
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	WHERE CL.[Cpf] = @Cpf AND CL.[IsActive] = 1
	AND AC.[IsActive] = 1 AND AG.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spClient_HasActiveOrDisabledAccount]...';


GO
CREATE PROCEDURE [dbo].[spClient_HasActiveOrDisabledAccount]
	@Cpf varchar(14)
AS
BEGIN
	SELECT 1 
	FROM [dbo].[Client]
	WHERE [Cpf] = @Cpf;
END
GO
PRINT N'Creating Procedure [dbo].[spClient_Login]...';


GO
CREATE PROCEDURE [dbo].[spClient_Login]
	@AgencyNumber varchar(4),
	@AgencyDigit tinyint,
	@AccountNumber varchar(10),
	@AccountDigit tinyint
AS
BEGIN
	SELECT CL.[Id], CL.[Name], 
	AC.[Id], AC.[ShortPassword] 
	FROM [dbo].[Client] CL
	INNER JOIN [dbo].[Account] AC ON CL.[Id] = AC.[ClientId]
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	WHERE AG.[Number] = @AgencyNumber AND AG.[Digit] = @AgencyDigit AND AG.[IsActive] = 1
	AND AC.[Number] = @AccountNumber AND AC.[Digit] = @AccountDigit 
	AND AC.[IsActive] = 1 AND CL.[IsActive] = 1;
END
GO
PRINT N'Creating Procedure [dbo].[spTransaction_Deposit]...';


GO
CREATE PROCEDURE [dbo].[spTransaction_Deposit]
	@Amount decimal(19,2),
    @ReferenceId varchar(100),
    @DesiredCompletionDate datetime,
    @IsCompleted bit,
    @CompletedAt datetime,
    @ToAccountId bigint,
    @AccountNewBalance decimal(19,2)
AS

IF EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [Id] = @ToAccountId AND [IsActive] = 1)
    BEGIN
		BEGIN TRANSACTION
	    
		INSERT INTO [dbo].[Transaction] ([CompletedAt], [DesiredCompletionDate], [ReferenceId],
        [Amount], [IsIncome], [IsCompleted], [IsActive], [ActionId], [ToAccountId]) OUTPUT INSERTED.[Id], INSERTED.[ReferenceId]
        VALUES (@CompletedAt, @DesiredCompletionDate, @ReferenceId, @Amount, 1, @IsCompleted, 1, 2, @ToAccountId);
    
        IF(@IsCompleted = 1)
            UPDATE [dbo].[Account] 
            SET [Balance] = @AccountNewBalance, [UpdatedAt] = CURRENT_TIMESTAMP
            WHERE [Id] = @ToAccountId;
		
		IF @@ERROR = 0
			COMMIT
		ELSE
			ROLLBACK
	END
GO
PRINT N'Creating Procedure [dbo].[spTransaction_GetByReferenceId]...';


GO
CREATE PROCEDURE [dbo].[spTransaction_GetByReferenceId]
	@ReferenceId varchar(100)
AS
BEGIN
	SELECT TRA.[Id], TRA.[CompletedAt], TRA.[DesiredCompletionDate],
	TRA.[ReferenceId], TRA.[Amount], TRA.[IsIncome], TRA.[IsCompleted],
	TRT.[Id], TRT.[Name], FAC.[Id], TAC.[Id]
	FROM [dbo].[Transaction] TRA
	INNER JOIN [dbo].[TransactionAction] TRT ON TRA.[ActionId] = TRT.[Id]
	LEFT JOIN [dbo].[Account] FAC ON TRA.[FromAccountId] = FAC.[Id]
	LEFT JOIN [dbo].[Account] TAC ON TRA.[ToAccountId] = TAC.[Id]
	WHERE TRA.[ReferenceId] = @ReferenceId AND TRA.[IsActive] = 1
	AND TRT.[IsActive] = 1 AND (FAC.[IsActive] IS NULL OR FAC.[IsActive] = 1)
	AND (TAC.[IsActive] IS NULL OR TAC.[IsActive] = 1);
END
GO
PRINT N'Creating Procedure [dbo].[spTransaction_List]...';


GO
CREATE PROCEDURE [dbo].[spTransaction_List]
	@ClientId bigint,
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	SELECT TRA.[Id], TRA.[CreatedAt], TRA.[CompletedAt], TRA.[DesiredCompletionDate],
	TRA.[ReferenceId], TRA.[Amount], TRA.[IsIncome], TRA.[IsCompleted],
	TRT.[Id], TRT.[Name], FAC.[Id], FAC.[ClientId], TAC.[Id], TAC.[ClientId] 
	FROM [dbo].[Transaction] TRA
	INNER JOIN [dbo].[TransactionAction] TRT ON TRA.[ActionId] = TRT.[Id]
	LEFT JOIN [dbo].[Account] FAC ON TRA.[FromAccountId] = FAC.[Id]
	LEFT JOIN [dbo].[Account] TAC ON TRA.[ToAccountId] = TAC.[Id]
	WHERE (FAC.[ClientId] = @ClientId OR TAC.[ClientId] = @ClientId) 
	AND TRA.[CreatedAt] BETWEEN @StartDate AND @EndDate 
	AND TRA.[IsActive] = 1 AND TRT.[IsActive] = 1 
	AND (FAC.[IsActive] IS NULL OR FAC.[IsActive] = 1)
	AND (TAC.[IsActive] IS NULL OR TAC.[IsActive] = 1);
END
GO
PRINT N'Creating Procedure [dbo].[spTransaction_Transfer]...';


GO
CREATE PROCEDURE [dbo].[spTransaction_Transfer]
	@Amount decimal(19,2),
    @ReferenceId varchar(100),
    @DesiredCompletionDate datetime,
    @IsCompleted bit,
    @CompletedAt datetime,
    @FromAccountId bigint,
    @ToAccountId bigint,
    @OriginNewBalance decimal(19,2),
    @DestinationNewBalance decimal(19,2)
AS

IF EXISTS (
    SELECT 1 
    FROM [dbo].[Account] 
    WHERE [Id] = @FromAccountId AND [IsActive] = 1 AND [Balance] >= @Amount
) 
AND EXISTS (
    SELECT 1 FROM 
    [dbo].[Account] 
    WHERE [Id] = @ToAccountId AND [IsActive] = 1
)
	BEGIN
		BEGIN TRANSACTION
			
			INSERT INTO [dbo].[Transaction] ([CompletedAt], [DesiredCompletionDate], [ReferenceId],
			[Amount], [IsIncome], [IsCompleted], [IsActive], [ActionId], [FromAccountId], [ToAccountId]) OUTPUT INSERTED.[Id], INSERTED.[ReferenceId]
			VALUES (@CompletedAt, @DesiredCompletionDate, @ReferenceId, @Amount, 1, @IsCompleted, 1, 3, @FromAccountId, @ToAccountId);
    
			IF(@IsCompleted = 1)
				UPDATE [dbo].[Account] 
				SET [Balance] = @OriginNewBalance, [UpdatedAt] = CURRENT_TIMESTAMP
				WHERE [Id] = @FromAccountId;
		
			IF(@IsCompleted = 1)
				UPDATE [dbo].[Account] 
				SET [Balance] = @DestinationNewBalance, [UpdatedAt] = CURRENT_TIMESTAMP
				WHERE [Id] = @ToAccountId;
		
		IF @@ERROR = 0
			COMMIT
		ELSE
			ROLLBACK
	END
GO
PRINT N'Creating Procedure [dbo].[spTransaction_Withdraw]...';


GO
CREATE PROCEDURE [dbo].[spTransaction_Withdraw]
	@Amount decimal(19,2),
    @ReferenceId varchar(100),
    @DesiredCompletionDate datetime,
    @IsCompleted bit,
    @CompletedAt datetime,
    @FromAccountId bigint,
    @AccountNewBalance decimal(19,2)
AS

IF EXISTS (SELECT 1 FROM [dbo].[Account] WHERE [Id] = @FromAccountId AND [IsActive] = 1)
	BEGIN
		BEGIN TRANSACTION
			
			INSERT INTO [dbo].[Transaction] ([CompletedAt], [DesiredCompletionDate], [ReferenceId],
			[Amount], [IsIncome], [IsCompleted], [IsActive], [ActionId], [FromAccountId]) OUTPUT INSERTED.[Id], INSERTED.[ReferenceId]
			VALUES (@CompletedAt, @DesiredCompletionDate, @ReferenceId, @Amount, 0, @IsCompleted, 1, 1, @FromAccountId);
    
			IF(@IsCompleted = 1)
				UPDATE [dbo].[Account] 
				SET [Balance] = @AccountNewBalance, [UpdatedAt] = CURRENT_TIMESTAMP
				WHERE [Id] = @FromAccountId;
		
		IF @@ERROR = 0
			COMMIT
		ELSE
			ROLLBACK
	END
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4f7d1f2a-6ffb-4bda-ad57-d37c8aea3d30')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4f7d1f2a-6ffb-4bda-ad57-d37c8aea3d30')

GO

GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[TransactionAction])
BEGIN
	INSERT INTO [dbo].TransactionAction(Name) 
	VALUES ('Withdrawal'), ('Deposit'), ('Transference');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Agency])
BEGIN
	INSERT INTO [dbo].Agency(Number, Digit) 
	VALUES ('0001', '0');
END
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
