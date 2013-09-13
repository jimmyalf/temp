

CREATE PROCEDURE spMemberRemoveNewsConnection
					@memberid INT,
					@newsid INT,
					@status INT OUTPUT
	AS
		BEGIN
			DELETE FROM tblMemberNews
			WHERE cMemberId = @memberid
			AND cNewsId = @newsid
		END

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END

