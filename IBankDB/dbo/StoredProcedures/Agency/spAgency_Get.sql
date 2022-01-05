CREATE PROCEDURE [dbo].[spAgency_Get]
	@Id int
AS
BEGIN
	SELECT AG.[Id], AG.[Number], AG.[Digit]
	FROM [dbo].[Agency] AG
	WHERE AG.[Id] = @Id AND AG.[IsActive] = 1;
END
