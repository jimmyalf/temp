CREATE PROCEDURE spDocumentGetDocuments
					@type INT,
					@id INT,
					@nodeId INT,
					@userId INT,
					@status INT OUTPUT
	AS
		DECLARE @sortType INT
		
		IF (@type = 0) /*Specific*/
		BEGIN
			SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
					NULL AS cContent, cDescription, cCreatedDate, 
					cCreatedBy, cChangedDate, cChangedBy, 
					cGroupId, cUserId
			FROM	tblDocuments
			WHERE cId = @id AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
		END
		
		IF (@type = 1) /*All*/
		BEGIN
			SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
					NULL AS cContent, cDescription, cCreatedDate, 
					cCreatedBy, cChangedDate, cChangedBy, 
					cGroupId, cUserId
			FROM	tblDocuments 
			WHERE cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
		END
		
		IF (@type = 2) /*All for node*/
		BEGIN
			SELECT	@sortType=cSortType 
			FROM	tblDocumentNode
			WHERE	cId = @nodeId

			IF @sortType IS NULL
			BEGIN
				SET @sortType = 2
			END
			
			IF(@sortType = 1)
			BEGIN
				SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
						NULL AS cContent, cDescription, cCreatedDate, 
						cCreatedBy, cChangedDate, cChangedBy, 
						cGroupId, cUserId
				FROM	tblDocuments
				WHERE cNodeId = @nodeId 
				AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
				ORDER BY cOrder
			END
			
			IF(@sortType = 2)
			BEGIN
				SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
						NULL AS cContent, cDescription, cCreatedDate, 
						cCreatedBy, cChangedDate, cChangedBy, 
						cGroupId, cUserId
				FROM	tblDocuments
				WHERE cNodeId = @nodeId 
				AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
				ORDER BY cName
			END
			
			IF(@sortType = 3)
			BEGIN
				SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
						NULL AS cContent, cDescription, cCreatedDate, 
						cCreatedBy, cChangedDate, cChangedBy, 
						cGroupId, cUserId
				FROM	tblDocuments
				WHERE cNodeId = @nodeId 
				AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
				ORDER BY cCreatedDate DESC
			END
			
		END
		
		IF (@type = 6) /*Latest*/
		BEGIN
			If @nodeId > 0 
			BEGIN
				SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
						NULL AS cContent, cDescription, cCreatedDate, 
						cCreatedBy, cChangedDate, cChangedBy,
						cGroupId, cUserId
				FROM	tblDocuments
				WHERE cNodeId = @nodeId AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
				ORDER BY cCreatedDate DESC
			END
			ELSE
			BEGIN
				SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
						NULL AS cContent, cDescription, cCreatedDate, 
						cCreatedBy, cChangedDate, cChangedBy,
						cGroupId, cUserId
				FROM	tblDocuments
				WHERE cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
				ORDER BY cCreatedDate DESC
			END
			
		END
		
		IF (@type = 7) /*Specific with content*/
		BEGIN
			SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
					cContent, cDescription, cCreatedDate, 
					cCreatedBy, cChangedDate, cChangedBy, 
					cGroupId, cUserId
			FROM	tblDocuments
			WHERE cId = @id AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
		END
		
		IF (@type = 8) /*All for a node by order*/
		BEGIN		
			SELECT	cId, cNodeId, cName, cContentInfo, cSize, 
					NULL AS cContent, cDescription, cCreatedDate, 
					cCreatedBy, cChangedDate, cChangedBy, 
					cGroupId, cUserId
			FROM	tblDocuments
			WHERE cNodeId = @nodeId 
			AND cId IN (SELECT cId FROM sfDocumentAllowedDocuments(@userId))
			ORDER BY cOrder
		END
		

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
