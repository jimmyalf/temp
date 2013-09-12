
CREATE PROCEDURE spMemberAddLocation
					@memberid INT,
					@locationid INT,
					@status INT OUTPUT
					
	AS
	BEGIN	
		INSERT INTO tblMemberLocationConnection
			(cMemberId, cLocationId)
		VALUES
			(@memberid, @locationid)		
		

			END
		
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END

