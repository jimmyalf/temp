create PROCEDURE spNewsConnectToGroup
					@newsId INT,
					@groupId INT,
					@status INT OUTPUT				
	AS
	BEGIN	
		INSERT INTO tblNewsGroupConnection
			(cGroupId, cNewsId)
		VALUES
			(@groupId, @newsId)		
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END
