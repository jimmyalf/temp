create PROCEDURE spBaseRemoveFile
					@id INT,
					@status INT OUTPUT
	AS
	BEGIN
		DECLARE	@dummy INT
		
		DECLARE chkExs CURSOR LOCAL FOR
			SELECT	1
			FROM	tblContPageFile
			WHERE	cFleId = @id
			
		OPEN chkExs
		FETCH NEXT FROM chkExs INTO @dummy
		
		IF (@@FETCH_STATUS <> -1)
			BEGIN
				CLOSE chkExs
				DEALLOCATE chkExs
				
				SET @status = -1
				RETURN
			END
			
		CLOSE chkExs
		DEALLOCATE chkExs
	
		DELETE FROM tblBaseFile
		WHERE cId = @id
		

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
	END