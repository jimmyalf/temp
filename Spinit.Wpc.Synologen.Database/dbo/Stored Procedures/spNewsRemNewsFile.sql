create PROCEDURE spNewsRemNewsFile
					@newsId INT,
					@fleId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblNewsFileConnection
		WHERE		cNewsId = @newsId
			AND		cFileId = @fleId
					
		SELECT @status = @@ERROR
