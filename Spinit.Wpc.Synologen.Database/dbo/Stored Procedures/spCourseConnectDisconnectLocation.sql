create PROCEDURE spCourseConnectDisconnectLocation
					@action INT=-1, --ConnectDisconnect
					@courseId INT=0,
					@locationId INT=0,
					@status INT OUTPUT				
	AS
	BEGIN
		IF (@action = 0) BEGIN --Connect
			INSERT INTO tblCourseLocationConnection (cLocationId, cCourseId)
			VALUES (@locationId, @courseId)
		END
		ELSE BEGIN --Disconnect
			DELETE FROM tblCourseLocationConnection
			WHERE cCourseId = @courseId AND cLocationId = @locationId
		END
		
		IF (@@ERROR = 0) BEGIN
			SELECT @status = 0
		END
		ELSE BEGIN
			SELECT @status = @@ERROR
		END
	END
