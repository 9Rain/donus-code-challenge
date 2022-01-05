CREATE PROCEDURE [dbo].[spClient_HasActiveOrDisabledAccount]
	@Cpf varchar(14)
AS
BEGIN
	SELECT 1 
	FROM [dbo].[Client]
	WHERE [Cpf] = @Cpf;
END
