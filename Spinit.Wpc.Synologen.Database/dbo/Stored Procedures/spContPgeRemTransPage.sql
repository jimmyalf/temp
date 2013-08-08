CREATE PROCEDURE spContPgeRemTransPage
					@id INT,					
					@status INT OUTPUT
	AS
		BEGIN	
			DECLARE	@statusRet INT
			SELECT @status = 0			
			BEGIN TRANSACTION DELETE_PAGE

			EXECUTE spContPgeRemPage @id, @statusRet OUTPUT
			
			IF (@statusRet <> 0)
				BEGIN
					SELECT @status = @statusRet
					ROLLBACK TRANSACTION DELETE_PAGE
					RETURN
				END

			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
					ROLLBACK TRANSACTION DELETE_PAGE
					RETURN
				END

			IF (@@ERROR = 0)
				BEGIN
					COMMIT TRANSACTION DELETE_PAGE
				END
		END
