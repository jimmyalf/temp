create PROCEDURE spNewsConnectCategoryToGroup
					@categoryId INT,
					@groupId INT,
					@status INT OUTPUT				
	AS
	BEGIN	
		INSERT INTO tblNewsCategoryGroupConnection
			(cGroupId, cCategoryId)
		VALUES
			(@groupId, @categoryId)		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
