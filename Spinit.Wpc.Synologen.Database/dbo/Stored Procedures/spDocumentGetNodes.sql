CREATE PROCEDURE spDocumentGetNodes
					@type INT,
					@id INT,
					@parentId INT,
					@locationId INT,
					@languageId INT,
					@name NVARCHAR (100),
					@userId INT = 0,
					@status INT OUTPUT
AS
BEGIN
	IF (@type = 0)  /*specific*/
		BEGIN
			SELECT	*
			FROM	tblDocumentNode
			WHERE cId = @id
			AND cId IN (SELECT cId FROM sfDocumentAllowedNodes(@userId))
			
		END
	
	IF (@type = 1) /*all */
		BEGIN
			SELECT	*
			FROM	tblDocumentNode
			WHERE cId IN (SELECT cId FROM sfDocumentAllowedNodes(@userId))
			
		END
	
	IF (@type = 5)  /*Children */
		BEGIN
			SELECT	*
			FROM	tblDocumentNode
			WHERE cParentId = @parentId
			AND cId IN (SELECT cId FROM sfDocumentAllowedNodes(@userId))
		END
	
	IF (@type = 4) /*only roots */
		BEGIN
			SELECT	*
			FROM	tblDocumentNode
			INNER JOIN tblDocumentNodeLocationConnection
				ON tblDocumentNodeLocationConnection.cDocumentNodeId = tblDocumentNode.cId
			INNER JOIN tblDocumentNodeLanguageConnection
				ON tblDocumentNodeLanguageConnection.cDocumentNodeId = tblDocumentNode.cId
			WHERE tblDocumentNodeLocationConnection.cLocationId = @locationId
			AND tblDocumentNodeLanguageConnection.cLanguageId = @languageId
			AND cParentId IS NULL
			AND cId IN (SELECT cId FROM sfDocumentAllowedNodes(@userId))
		END
	
	IF @type = 9
		BEGIN
			IF @parentId IS NULL
				BEGIN
					SELECT	*
					FROM	dbo.tblDocumentNode
						INNER JOIN tblDocumentNodeLocationConnection ON tblDocumentNodeLocationConnection.cDocumentNodeId = tblDocumentNode.cId
						INNER JOIN tblDocumentNodeLanguageConnection ON tblDocumentNodeLanguageConnection.cDocumentNodeId = tblDocumentNode.cId
					WHERE tblDocumentNodeLocationConnection.cLocationId = @locationId
						AND tblDocumentNodeLanguageConnection.cLanguageId = @languageId
						AND	cParentId IS NULL
						AND	cName = @name
				END
			ELSE
				BEGIN
					SELECT	*
					FROM	dbo.tblDocumentNode
					WHERE	cParentId = @parentId
						AND	cName = @name
				END
		END
	
	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
END
