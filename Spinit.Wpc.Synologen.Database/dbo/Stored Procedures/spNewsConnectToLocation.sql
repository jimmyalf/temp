
CREATE PROCEDURE spNewsConnectToLocation
					@newsId INT,
					@locationId INT,
					@status INT OUTPUT
					
	AS
	BEGIN	
		INSERT INTO tblNewsLocationConnection
			(cNewsId, cLocationId)
		VALUES
			(@newsId, @locationId)		

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
