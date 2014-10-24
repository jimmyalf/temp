create PROCEDURE spMemberDisconnectFile
					@memberId INT,
					@fileId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblMemberFileConnection
		WHERE		cMemberId = @memberId
			AND		cFileId = @fileId
					
		SELECT @status = @@ERROR
