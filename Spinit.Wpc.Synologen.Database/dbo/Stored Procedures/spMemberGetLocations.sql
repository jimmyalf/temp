

CREATE PROCEDURE spMemberGetLocations
					@status INT OUTPUT
	AS
		BEGIN
				BEGIN
					SELECT	*
					FROM	tblBaseLocations
				END
			SELECT @status = @@ERROR
		END
