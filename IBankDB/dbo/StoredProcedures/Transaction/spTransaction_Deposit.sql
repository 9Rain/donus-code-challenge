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
	    INSERT INTO [dbo].[Transaction] ([CompletedAt], [DesiredCompletionDate], [ReferenceId],
        [Amount], [IsIncome], [IsCompleted], [IsActive], [ActionId], [ToAccountId]) OUTPUT INSERTED.[Id], INSERTED.[ReferenceId]
        VALUES (@CompletedAt, @DesiredCompletionDate, @ReferenceId, @Amount, 1, @IsCompleted, 1, 2, @ToAccountId);
    
        IF(@IsCompleted = 1)
            UPDATE [dbo].[Account] 
            SET [Balance] = @AccountNewBalance, [UpdatedAt] = CURRENT_TIMESTAMP
            WHERE [Id] = @ToAccountId;
    END
