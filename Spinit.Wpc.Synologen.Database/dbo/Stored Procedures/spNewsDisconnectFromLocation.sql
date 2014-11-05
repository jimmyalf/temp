
CREATE PROCEDURE spNewsDisconnectFromLocation
					@newsId INT,
					@locationId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DELETE FROM tblNewsLocationConnection
			WHERE cNewsId = @newsId AND cLocationId = @locationId
		END

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
