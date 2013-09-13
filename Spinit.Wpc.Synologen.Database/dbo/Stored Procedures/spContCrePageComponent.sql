
CREATE PROCEDURE spContCrePageComponent
					@pgeId INT,
					@cmpId INT,
					@status INT OUTPUT
	AS
		INSERT INTO	tblContPageComponents
			(cPgeId, cCmpId)
		VALUES
			(@pgeId, @cmpId)
			
		SELECT @status = @@ERROR
