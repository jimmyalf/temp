

CREATE PROCEDURE spMemberRemoveLocation
					@memberid INT,
					@locationid INT,
					@status INT OUTPUT
	AS

				BEGIN
					DELETE FROM tblMemberLocationConnection
					WHERE cMemberId = @memberid
					AND cLocationId = @locationid
				END

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END

