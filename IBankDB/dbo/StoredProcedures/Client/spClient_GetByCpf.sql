CREATE PROCEDURE [dbo].[spClient_GetByCpf]
	@Cpf varchar(14)
AS
BEGIN
	SELECT CL.[Id], CL.[Name], CL.[Cpf], 
	AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], 
	AG.[Id], AG.[Number], AG.[Digit] 
	FROM [dbo].[Client] CL
	INNER JOIN [dbo].[Account] AC ON CL.[Id] = AC.[ClientId]
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	WHERE CL.[Cpf] = @Cpf AND CL.[IsActive] = 1
	AND AC.[IsActive] = 1 AND AG.[IsActive] = 1;
END
