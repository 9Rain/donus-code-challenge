CREATE PROCEDURE [dbo].[spClient_Create]
	@Name nvarchar(100),
	@Cpf varchar(14)
AS
BEGIN
	INSERT INTO [dbo].[Client] ([Name], [Cpf], [IsActive]) OUTPUT INSERTED.[Id] 
	VALUES (@Name, @Cpf, 1);
END
