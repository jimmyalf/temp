create PROCEDURE spContGetConnectedNodes
	@type INT,
	@pageId INT,
	@status INT OUTPUT
	AS
BEGIN
	IF (@type = 2)
	BEGIN
		SELECT	cId,
				cTreTpeId,
				cParent,
				cName,
				cFileName,
				cLocId,
				cLngId,
				cHideInMenu,
				cNeedsAuthentication,
				cCssClass
		FROM	tblContTree
		WHERE cTemplate = @pageId
	END
		
	IF (@type = 4)
	BEGIN
		SELECT	cId,
				cTreTpeId,
				cParent,
				cName,
				cFileName,
				cLocId,
				cLngId,
				cHideInMenu,
				cNeedsAuthentication,
				cCssClass
		FROM	tblContTree
		WHERE cStylesheet = @pageId
	END

	SELECT @status = @@ERROR

END
