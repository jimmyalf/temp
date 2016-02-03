create PROCEDURE spNewsDisconnectCategoryFromGroup
					@categoryId INT,
					@groupId INT,
					@status INT OUTPUT
	AS

		BEGIN
			DELETE FROM tblNewsCategoryGroupConnection
			WHERE cCategoryId = @categoryId AND cGroupId = @groupId
		END
				
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
