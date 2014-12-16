create PROCEDURE spContGetTreUpAll
					@id INT,
					@status INT OUTPUT
	AS
		SELECT	cId,
				cTreTpeId,
				cParent,
				cName,
				cFileName,
				cLocId,
				cLngId,
				cHideInMenu
		FROM	sfContGetTreUpAll (@id)
		
		SELECT @status = @@ERROR
