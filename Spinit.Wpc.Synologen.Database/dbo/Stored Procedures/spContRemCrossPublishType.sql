
CREATE PROCEDURE spContRemCrossPublishType
					@id INT,
					@status INT OUTPUT
	AS
		BEGIN
			DELETE FROM tblContCrossPublishType
			WHERE		cId = @id
			
			SELECT @status = @@ERROR		
		END
