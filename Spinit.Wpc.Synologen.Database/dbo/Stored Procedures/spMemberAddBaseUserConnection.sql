
CREATE PROCEDURE spMemberAddBaseUserConnection
					@memberid INT,
					@userid INT,
					@status INT OUTPUT
					
	AS
	BEGIN	
		INSERT INTO tblMemberUserConnection
			(cMemberId, cUserId)
		VALUES
			(@memberid, @userid)				
	END
		
	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END

