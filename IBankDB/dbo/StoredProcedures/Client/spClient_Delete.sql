CREATE PROCEDURE [dbo].[spClient_Delete]
	@Id bigint
AS
BEGIN
	UPDATE [dbo].[Client] 
	SET [IsActive] = 0, [UpdatedAt] = CURRENT_TIMESTAMP
	WHERE [Id] = @Id;

	UPDATE [dbo].[Account] 
	SET [IsActive] = 0, [UpdatedAt] = CURRENT_TIMESTAMP
	WHERE [ClientId] = @Id;
END
