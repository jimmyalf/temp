
CREATE PROCEDURE spContRemPagePage
					@pgeId INT,
					@lnkId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblContPagePage
		WHERE		cPgeId = @pgeId
			AND		cLnkId = @lnkId
			
		SELECT @status = @@ERROR
