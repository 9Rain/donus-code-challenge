CREATE TABLE [dbo].[TransactionAction]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CreatedAt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[UpdatedAt] DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	[Name] VARCHAR(50) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
)

GO
