CREATE PROCEDURE [dbo].[spClient_Login]
	@AgencyNumber varchar(4),
	@AgencyDigit tinyint,
	@AccountNumber varchar(10),
	@AccountDigit tinyint
AS
BEGIN
	SELECT CL.[Id], CL.[Name], 
	AC.[Id], AC.[ShortPassword] 
	FROM [dbo].[Client] CL
	INNER JOIN [dbo].[Account] AC ON CL.[Id] = AC.[ClientId]
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	WHERE AG.[Number] = @AgencyNumber AND AG.[Digit] = @AgencyDigit AND AG.[IsActive] = 1
	AND AC.[Number] = @AccountNumber AND AC.[Digit] = @AccountDigit 
	AND AC.[IsActive] = 1 AND CL.[IsActive] = 1;
END

