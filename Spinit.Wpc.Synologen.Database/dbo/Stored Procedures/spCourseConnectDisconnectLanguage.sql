create PROCEDURE spCourseConnectDisconnectLanguage
					@action INT=-1, --ConnectDisconnect
					@courseId INT=0,
					@languageId INT=0,
					@status INT OUTPUT				
	AS
	BEGIN
		IF (@action = 0) BEGIN --Connect
			INSERT INTO tblCourseLanguageConnection (cLanguageId, cCourseId)
			VALUES (@languageId, @courseId)
		END
		ELSE BEGIN --Disconnect
			DELETE FROM tblCourseLanguageConnection
			WHERE cCourseId = @courseId AND cLanguageId = @languageId
		END
		
		IF (@@ERROR = 0) BEGIN
			SELECT @status = 0
		END
		ELSE BEGIN
			SELECT @status = @@ERROR
		END
	END
