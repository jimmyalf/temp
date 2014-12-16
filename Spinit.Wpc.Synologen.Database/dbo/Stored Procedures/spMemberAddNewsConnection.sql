
CREATE PROCEDURE spMemberAddNewsConnection
					@memberid INT,
					@newsid INT,
					@status INT OUTPUT
					
	AS
	BEGIN	
		INSERT INTO tblMemberNews
			(cMemberId, cNewsId)
		VALUES
			(@memberid, @newsid)		
		

			END
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END

