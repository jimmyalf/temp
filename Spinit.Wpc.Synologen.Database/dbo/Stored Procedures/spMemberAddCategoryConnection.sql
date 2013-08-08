

CREATE PROCEDURE spMemberAddCategoryConnection
					@memberid INT,
					@categoryid INT,
					@status INT OUTPUT
					
	AS
	BEGIN	
		INSERT INTO tblMemberCategoryConnection
			(cMemberId, cCategoryId)
		VALUES
			(@memberid, @categoryid)				
	END
		
	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END

