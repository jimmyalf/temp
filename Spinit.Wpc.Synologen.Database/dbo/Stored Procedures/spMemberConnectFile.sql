create PROCEDURE spMemberConnectFile
					@memberId INT,
					@fileId INT,
					@status INT OUTPUT
	AS
		INSERT INTO	tblMemberFileConnection
			(cMemberId, cFileId)
		VALUES
			(@memberId, @fileId)
			
		SELECT @status = @@ERROR
