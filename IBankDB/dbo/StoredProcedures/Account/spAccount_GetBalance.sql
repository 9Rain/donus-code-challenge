CREATE PROCEDURE [dbo].[spAccount_GetBalance]
	@ClientId bigint
AS
BEGIN
	SELECT AC.[Balance]
	FROM [dbo].[Account] AC 
	INNER JOIN [dbo].[Agency] AG ON AC.[AgencyId] = AG.[Id]
	INNER JOIN [dbo].[Client] CL ON AC.[ClientId] = CL.[Id]
	WHERE AC.[ClientId] = @ClientId AND AC.[IsActive] = 1
	AND AG.[IsActive] = 1 AND CL.[IsActive] = 1;
END
