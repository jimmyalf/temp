CREATE PROCEDURE spNewsGetReadByList
					@newsId INT,
					@status INT OUTPUT				
	AS
	BEGIN	
		
		SELECT tblNewsRead.*,tblBaseUsers.* FROM tblNewsRead
		LEFT JOIN tblBaseUsers ON tblNewsRead.cReadBy=tblBaseUsers.cUserName
		WHERE cNewsId = @newsId
		ORDER BY cReadDate DESC
				
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
