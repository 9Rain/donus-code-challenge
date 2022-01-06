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