CREATE PROCEDURE spContPgeRemPage
@id INT, @status INT OUTPUT
AS
BEGIN	
			DECLARE	@statusRet INT
			
			-- Set cPgeId, cTemplate, cStylesheet, cFrameset to null if linked to @id
			
			UPDATE tblContTree
			SET cPgeId = null
			WHERE cPgeId = @id
			
			UPDATE tblContTree
			SET cTemplate = null
			WHERE cTemplate = @id

			UPDATE tblContTree
			SET cStylesheet = null
			WHERE cStylesheet = @id
																						
			DELETE FROM	tblContPageComponents
			WHERE cPgeId = @id
			
			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
					RETURN
				END
			
			DELETE FROM	tblContPageFile
			WHERE cPgeId = @id
			
			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
					RETURN
				END

			DELETE FROM tblContPagePage
			WHERE cPgeId = @id
			
			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
					RETURN
				END

			DELETE FROM tblContPage
			WHERE		cId = @id
			
			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
				END
			ELSE
				BEGIN
					SELECT @status = 0
				END
	END
