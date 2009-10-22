CREATE TRIGGER [InsteadOfInsert] 
ON [SynologenOpqNode].[SomeTableOrView]
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON
END