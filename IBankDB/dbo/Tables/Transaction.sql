CREATE TABLE [dbo].[Transaction]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[CreatedAt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[UpdatedAt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[CompletedAt] DATETIME DEFAULT NULL,
	[DesiredCompletionDate] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[ReferenceId] VARCHAR(100) NOT NULL,
	[Amount] DECIMAL(19,2) NOT NULL,
    [IsIncome] BIT NOT NULL,
    [IsCompleted] BIT NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1,
	[ActionId] INT NOT NULL,
	[FromAccountId] BIGINT DEFAULT NULL,
	[ToAccountId] BIGINT DEFAULT NULL,
    CONSTRAINT [FK_Transaction_TransactionsAction] FOREIGN KEY ([ActionId]) REFERENCES [TransactionAction]([Id]),
    CONSTRAINT [FK_Transaction_Account_From] FOREIGN KEY ([FromAccountId]) REFERENCES [Account]([Id]),
    CONSTRAINT [FK_Transaction_Account_To] FOREIGN KEY ([ToAccountId]) REFERENCES [Account]([Id]),
	CONSTRAINT UQ_Transaction_ReferenceId UNIQUE ([ReferenceId]),
)

GO

