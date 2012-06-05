
CREATE PROCEDURE spNewsConnectToCategory
					@newsId INT,
					@categoryId INT,
					@status INT OUTPUT				
	AS
	BEGIN	
		INSERT INTO tblNewsCategoryConnection
			(cCategoryId, cNewsId)
		VALUES
			(@categoryId, @newsId)		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
