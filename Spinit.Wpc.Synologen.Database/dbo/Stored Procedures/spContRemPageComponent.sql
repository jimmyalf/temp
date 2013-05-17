
CREATE PROCEDURE spContRemPageComponent
					@pgeId INT,
					@cmpId INT,
					@status INT OUTPUT
	AS
		DELETE FROM tblContPageComponents
		WHERE		cPgeId = @pgeId
			AND		cCmpId = @cmpId
			
		SELECT @status = @@ERROR
