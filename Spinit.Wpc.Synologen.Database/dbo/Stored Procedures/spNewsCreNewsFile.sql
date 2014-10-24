create PROCEDURE spNewsCreNewsFile
					@newsId INT,
					@fleId INT,
					@status INT OUTPUT
	AS
		INSERT INTO	tblNewsFileConnection
			(cNewsId, cFileId)
		VALUES
			(@newsId, @fleId)
			
		SELECT @status = @@ERROR
