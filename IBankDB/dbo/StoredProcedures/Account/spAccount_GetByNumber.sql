CREATE PROCEDURE [dbo].[spAccount_GetByNumber]
	@Number varchar(10)
AS
BEGIN
	SELECT AC.[Id], AC.[Number], AC.[Digit], AC.[Balance], AC.[ClientId],
	AG.[Id], AG.[Number], AG.[Digit]
	FROM [dbo].[Account] AC 
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	INNER JOIN [dbo].[Client] CL ON AC.[ClientId] = CL.[Id]
	WHERE AC.[Number] = @Number AND AC.[IsActive] = 1 
	AND AG.[IsActive] = 1 AND CL.[IsActive] = 1;
END
