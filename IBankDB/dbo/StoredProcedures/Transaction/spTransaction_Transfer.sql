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