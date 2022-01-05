CREATE PROCEDURE [dbo].[spAccount_Create]
	@Number varchar(10),
	@Digit tinyint,
	@ShortPassword varchar(100),
	@Password varchar(100),
	@Balance decimal(19,2) = 0,
	@ClientId bigint,
	@AgencyId int
AS
BEGIN
	INSERT INTO [dbo].[Account] ([Number], [Digit], [ShortPassword], [Password], 
	[Balance], [IsActive], [ClientId], [AgencyId]) OUTPUT INSERTED.[Id] 
	VALUES (@Number, @Digit, @ShortPassword, @Password, @Balance, 1, @ClientId, @AgencyId);
END