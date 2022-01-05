CREATE PROCEDURE [dbo].[spTransaction_GetByReferenceId]
	@ReferenceId varchar(100)
AS
BEGIN
	SELECT TRA.[Id], TRA.[CompletedAt], TRA.[DesiredCompletionDate],
	TRA.[ReferenceId], TRA.[Amount], TRA.[IsIncome], TRA.[IsCompleted],
	TRT.[Id], TRT.[Name], FAC.[Id], TAC.[Id]
	FROM [dbo].[Transaction] TRA
	INNER JOIN [dbo].[TransactionAction] TRT ON TRA.[ActionId] = TRT.[Id]
	LEFT JOIN [dbo].[Account] FAC ON TRA.[FromAccountId] = FAC.[Id]
	LEFT JOIN [dbo].[Account] TAC ON TRA.[ToAccountId] = TAC.[Id]
	WHERE TRA.[ReferenceId] = @ReferenceId AND TRA.[IsActive] = 1
	AND TRT.[IsActive] = 1 AND (FAC.[IsActive] IS NULL OR FAC.[IsActive] = 1)
	AND (TAC.[IsActive] IS NULL OR TAC.[IsActive] = 1);
END