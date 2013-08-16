CREATE PROCEDURE spMemberRemoveCategoryConnection
					@memberid INT,
					@categoryid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF @categoryid = 0 
			BEGIN
				DELETE FROM tblMemberCategoryConnection
				WHERE cMemberId = @memberid
			END
			ELSE
			BEGIN
				DELETE FROM tblMemberCategoryConnection
				WHERE cMemberId = @memberid
				AND cCategoryId = @categoryid
			END
		END

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
