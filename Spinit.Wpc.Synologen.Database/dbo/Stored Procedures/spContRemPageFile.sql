
CREATE PROCEDURE spContRemPageFile
					@pgeId INT,
					@fleId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblContPageFile
		WHERE		cPgeId = @pgeId
			AND		cFleId = @fleId
					
		SELECT @status = @@ERROR
