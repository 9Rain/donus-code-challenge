IF NOT EXISTS (SELECT 1 FROM [dbo].[TransactionAction])
BEGIN
	INSERT INTO [dbo].TransactionAction(Name) 
	VALUES ('Withdrawal'), ('Deposit'), ('Transference');
END

IF NOT EXISTS (SELECT 1 FROM [dbo].[Agency])
BEGIN
	INSERT INTO [dbo].Agency(Number, Digit) 
	VALUES ('0001', '0');
END
