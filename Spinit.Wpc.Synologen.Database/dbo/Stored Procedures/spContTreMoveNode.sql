CREATE PROCEDURE spContTreMoveNode
					@type INT,
					@id INT,
					@destId INT,
					@status INT OUTPUT
AS
BEGIN
	IF (@type = 0)
		BEGIN
			EXECUTE spContTreMoveNodeUpDown	1, @id, @status OUTPUT
		END
		
	IF (@type = 1)
		BEGIN
			EXECUTE	spContTreMoveNodeUpDown 0, @id, @status OUTPUT
		END
		
	IF (@type = 2)
		BEGIN
			EXECUTE spContTreMoveNodeTo @id, @destId, @status OUTPUT
		END

END
