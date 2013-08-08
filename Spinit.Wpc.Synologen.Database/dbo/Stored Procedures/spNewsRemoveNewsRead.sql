CREATE PROCEDURE spNewsRemoveNewsRead
					@newsId INT,
					@user NVARCHAR(100),
					@status INT OUTPUT				
	AS
	BEGIN	
		DECLARE @count INT
		SET @count = 0
		
		SELECT @count= COUNT(cNewsId) FROM tblNewsRead
		WHERE cNewsId = @newsId
		AND cReadBy = @user
		
		IF @count > 0 
		BEGIN
			DELETE FROM tblNewsRead
			WHERE cNewsId = @newsId
			AND cReadBy = @user
			
		END		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
