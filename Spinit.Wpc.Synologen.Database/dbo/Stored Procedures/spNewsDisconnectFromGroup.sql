﻿create PROCEDURE spNewsDisconnectFromGroup
					@newsId INT,
					@groupId INT,
					@status INT OUTPUT
	AS

		BEGIN
			DELETE FROM tblNewsGroupConnection
			WHERE cNewsId = @newsId AND cGroupId = @groupId
		END
				
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
