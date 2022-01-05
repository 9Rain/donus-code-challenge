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

