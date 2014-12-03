create PROCEDURE spCourseMainConnectDisconnectCategory
					@action INT=-1, --ConnectDisconnect
					@mainId INT=0,
					@categoryId INT=0,
					@status INT OUTPUT				
	AS
	BEGIN
		IF (@action = 0) BEGIN --Connect
			INSERT INTO tblCourseMainCategoryConnection (cCategoryId, cCourseMainId)
			VALUES (@categoryId, @mainId)
		END
		ELSE BEGIN --Disconnect
			DELETE FROM tblCourseMainCategoryConnection
			WHERE cCourseMainId = @mainId AND cCategoryId = @categoryId
		END
		
		IF (@@ERROR = 0) BEGIN
			SELECT @status = 0
		END
		ELSE BEGIN
			SELECT @status = @@ERROR
		END
	END
