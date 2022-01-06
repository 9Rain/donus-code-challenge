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
