create PROCEDURE spContPgeRemPageLocLng
					@id INT,	
					@locId INT,
					@lngId INT,				
					@status INT OUTPUT
	AS
		BEGIN	
			DECLARE	@statusRet INT
			
			-- Set cPgeId, cTemplate, cStylesheet, cFrameset to null if linked to @id
			
			UPDATE tblContTree
			SET cPgeId = null
			WHERE cPgeId = @id
				AND	cLocId = @locId
				AND cLngId = @lngId
			
			UPDATE tblContTree
			SET cTemplate = null
			WHERE cTemplate = @id
				AND	cLocId = @locId
				AND cLngId = @lngId

			UPDATE tblContTree
			SET cStylesheet = null
			WHERE cStylesheet = @id
				AND	cLocId = @locId
				AND cLngId = @lngId
																										
			DELETE FROM tblContPageLocationLanguage
			WHERE cPgeId = @id
				AND	cLocId = @locId
				AND cLngId = @lngId
						
			IF (@@ERROR <> 0)
				BEGIN
					SELECT @status = @@ERROR
				END
			ELSE
				BEGIN
					SELECT @status = 0
				END
	END
