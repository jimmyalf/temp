
CREATE PROCEDURE spNewsDisconnectFromCategory
					@newsId INT,
					@categoryId INT,
					@status INT OUTPUT
	AS

		BEGIN
			DELETE FROM tblNewsCategoryConnection
			WHERE cNewsId = @newsId AND cCategoryId = @categoryId
		END
				
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
