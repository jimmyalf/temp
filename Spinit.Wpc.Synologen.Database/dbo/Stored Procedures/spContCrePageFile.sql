
CREATE PROCEDURE spContCrePageFile
					@pgeId INT,
					@fleId INT,
					@status INT OUTPUT
	AS
		INSERT INTO	tblContPageFile
			(cPgeId, cFleId)
		VALUES
			(@pgeId, @fleId)
			
		SELECT @status = @@ERROR
