CREATE PROCEDURE [dbo].[spClient_Get]
	@Id bigint
AS
BEGIN
	SELECT CL.[Id], CL.[Name], CL.[Cpf], 
	AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], 
	AG.[Id], AG.[Number], AG.[Digit] 
	FROM [dbo].[Client] CL
	INNER JOIN [dbo].[Account] AC ON CL.[Id] = AC.[ClientId]
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	WHERE CL.[Id] = @Id AND CL.[IsActive] = 1
	AND AC.[IsActive] = 1 AND AG.[IsActive] = 1;
END
