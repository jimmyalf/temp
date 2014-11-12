CREATE PROCEDURE spContGetTreUp
--CREATE PROCEDURE spContGetTreUp
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
				cHideInMenu,
				cNeedsAuthentication,
				cCssClass,
				cPublish
		FROM	sfContGetTreUp (@id)
		
		SELECT @status = @@ERROR
