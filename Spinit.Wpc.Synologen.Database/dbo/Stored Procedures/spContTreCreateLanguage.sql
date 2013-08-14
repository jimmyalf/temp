CREATE PROCEDURE spContTreCreateLanguage
					@locId INT,
					@lngId INT,
					@parent INT,
					@name NVARCHAR (255),
					@fileName NVARCHAR (50),
					@lockFileName BIT,
					@description NTEXT,
					@note NVARCHAR (256),
					@keywords NTEXT,
					@link NVARCHAR (256),
					@target NVARCHAR (256),
					@header NTEXT,
					@hideInMenu BIT,
					@excludeFromSearch BIT,
					@template INT,
					@stylesheet INT,
					@createdBy NVARCHAR (100),
					@approvedBy NVARCHAR (100),
					@publishDate SMALLDATETIME,
					@unPublishDate SMALLDATETIME,
					@lockedBy NVARCHAR (100),
					@status INT OUTPUT,
					@id INT OUTPUT
	AS
		DECLARE @dummy INT,
				@orderNo INT,
				@parentType INT,
				@approvedDate SMALLDATETIME,
				@lockedDate SMALLDATETIME,
				@frames BIT
		
		DECLARE chk_nameExist CURSOR FOR 
			SELECT	1
			FROM	tblContTree
			WHERE	cName = @name
				AND	cParent = @parent
				
		DECLARE chk_lngExist CURSOR FOR
			SELECT	1
			FROM	tblContTree
			WHERE	cLocId = @locId
				AND	cLngId = @lngId
				
		DECLARE get_order CURSOR FOR
			SELECT	COUNT (cId)
			FROM	tblContTree
			WHERE	cParent = @parent
			
		DECLARE get_parentType CURSOR FOR
			SELECT	cTreTpeId
			FROM	tblContTree
			WHERE	cId = @parent
			
		IF (@lngId IS NULL)
			BEGIN
				SELECT @id = 0
				SELECT @status = -3
				RETURN
			END
								
		IF (@approvedBy IS NOT NULL)
			BEGIN
				SELECT @approvedDate = GETDATE ()
			END
		ELSE
			BEGIN
				SELECT @approvedDate = NULL
			END
			
		IF (@lockedBy IS NOT NULL)
			BEGIN 
				SELECT @lockedDate = GETDATE ()
			END
		ELSE
			BEGIN
				SELECT @lockedDate = NULL
			END
			
			OPEN get_order
			FETCH NEXT FROM get_order INTO @orderNo
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					SELECT @orderNo = 1
				END
			ELSE
				BEGIN
					SELECT @orderNo = @orderNo + 1
				END
			
			CLOSE get_order
			DEALLOCATE get_order
			
		IF (@parent IS NULL)
			BEGIN
				SELECT @id = 0
				SELECT @status = -6
				RETURN
			END
		ELSE
			BEGIN
				OPEN get_parentType
				FETCH NEXT FROM get_parentType INTO @parentType
				
				IF (@@FETCH_STATUS = -1)
					BEGIN
						CLOSE get_parentType
						DEALLOCATE get_parentType
						
						SELECT @id = 0
						SELECT @status = -6
						RETURN
					END
				ELSE
					BEGIN
						CLOSE get_parentType
						DEALLOCATE get_parentType
						
						IF (@parentType <> 1)
							BEGIN
								SELECT @id = 0
								SELECT @status = -6
								RETURN
							END
					END
			END
			
		OPEN chk_lngExist
		FETCH NEXT FROM chk_lngExist INTO @dummy
		
		IF (@@FETCH_STATUS <> -1)
			BEGIN
				CLOSE chk_lngExist
				DEALLOCATE chk_lngExist
				
				SELECT @status = -1
				SELECT @id = 0
				RETURN
			END
			
		CLOSE chk_lngExist
		DEALLOCATE chk_lngExist

		OPEN chk_nameExist
		FETCH NEXT FROM chk_nameExist INTO @dummy
	
		IF (@@FETCH_STATUS <> -1)
			BEGIN
				CLOSE chk_nameExist
				DEALLOCATE chk_nameExist
				
				SELECT @status = -1
				SELECT @id = 0
				RETURN
			END
		ELSE
			BEGIN					
				CLOSE chk_nameExist
				DEALLOCATE chk_nameExist

				INSERT INTO tblContTree
					(cLocId, cLngId, cTreTpeId, cParent, cName, cFileName,
					 cLockFileName, cDescription, cNote, cKeywords, cLink, 
					 cTarget, cHeader, cHideInMenu, cExcludeFromSearch,
					 cTemplate, cStylesheet, cOrder, cCreatedBy,
					 cCreatedDate, cChangedBy, cChangedDate, cApprovedBy, 
					 cApprovedDate, cPublishDate, cPublishedDate, 
					 cUnPublishDate, cUnPublishedDate, 
					 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
				VALUES
					(@locId, @lngId, 2, @parent,  @name, @fileName,
					 @lockFileName, @description, @note, @keywords, @link, 
					 @target,@header, @hideInMenu, @excludeFromSearch,
					 @template, @stylesheet, @orderNo, @createdBy, 
					 GETDATE (), @createdBy, GETDATE (), @approvedBy, 
					 @approvedDate, @publishDate, NULL, @unPublishDate, NULL, 
					 @lockedBy, @lockedDate, 1, 0)
			END
			
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
				SELECT @id = @@identity
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
				SELECT @id = 0
			END
	
	INSERT INTO tblContTree
		(cLocId, cLngId, cTreTpeId, cParent, cName, cFileName,
		 cLockFileName, cDescription, cNote, cKeywords, cLink, cTarget,
		 cHeader, cHideInMenu, cExcludeFromSearch,
		 cTemplate, cStylesheet, cOrder, cCreatedBy,
		 cCreatedDate, cChangedBy, cChangedDate, cApprovedBy, 
		 cApprovedDate, cPublishDate, cPublishedDate, 
		 cUnPublishDate, cUnPublishedDate, 
		 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
	VALUES
		(@locId, @lngId, 18, @id,  'CrossPublished', ' ',
		 1, NULL, NULL, NULL, NULL, NULL, 
		 NULL, 1, 1,
		 NULL, NULL, 2147483637, @createdBy, 
		 GETDATE (), @createdBy, GETDATE (), @createdBy, 
		 GETDATE (), NULL, GETDATE (), NULL, NULL, 
		 NULL, NULL, 1, 0)	
			
	INSERT INTO tblContTree
		(cLocId, cLngId, cTreTpeId, cParent, cName, cFileName,
		 cLockFileName, cDescription, cNote, cKeywords, cLink, cTarget,
		 cHeader, cHideInMenu, cExcludeFromSearch,
		 cTemplate, cStylesheet, cOrder, cCreatedBy,
		 cCreatedDate, cChangedBy, cChangedDate, cApprovedBy, 
		 cApprovedDate, cPublishDate, cPublishedDate, 
		 cUnPublishDate, cUnPublishedDate, 
		 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
	VALUES
		(@locId, @lngId, 17, @id,  'Includes', 'Includes',
		 1, NULL, NULL, NULL, NULL, NULL, 
		 NULL, 1, 1,
		 NULL, NULL, 2147483638, @createdBy, 
		 GETDATE (), @createdBy, GETDATE (), @createdBy, 
		 GETDATE (), NULL, GETDATE (), NULL, NULL, 
		 NULL, NULL, 1, 0)

	INSERT INTO tblContTree
		(cLocId, cLngId, cTreTpeId, cParent, cName, cFileName,
		 cLockFileName, cDescription, cNote, cKeywords, cLink, cTarget,
		 cHeader, cHideInMenu, cExcludeFromSearch,
		 cTemplate, cStylesheet, cOrder, cCreatedBy,
		 cCreatedDate, cChangedBy, cChangedDate, cApprovedBy, 
		 cApprovedDate, cPublishDate, cPublishedDate, 
		 cUnPublishDate, cUnPublishedDate, 
		 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
	VALUES
		(@locId, @lngId, 9, @id,  'Archive', ' ',
		 1,NULL, NULL, NULL, NULL, NULL, 
		 NULL, 1, 1,
		 NULL, NULL, 2147483639, @createdBy, 
		 GETDATE (), @createdBy, GETDATE (), @createdBy, 
		 GETDATE (), NULL, GETDATE (), NULL, NULL, 
		 NULL, NULL, 1, 0)

	INSERT INTO tblContTree
		(cLocId, cLngId, cTreTpeId, cParent, cName, cFileName,
		 cLockFileName, cDescription, cNote, cKeywords, cLink, cTarget, 
		 cHeader,cHideInMenu, cExcludeFromSearch,
		 cTemplate, cStylesheet, cOrder, cCreatedBy,
		 cCreatedDate, cChangedBy, cChangedDate, cApprovedBy, 
		 cApprovedDate, cPublishDate, cPublishedDate, 
		 cUnPublishDate, cUnPublishedDate,  
		 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
	VALUES
		(@locId, @lngId, 10, @id,  'TrashCan', ' ',
		 1, NULL, NULL, NULL, NULL, NULL, 
		 NULL, 1, 1,
		 NULL, NULL, 2147483640, @createdBy, 
		 GETDATE (), @createdBy, GETDATE (), @createdBy, 
		 GETDATE (), NULL, GETDATE (), NULL, NULL, 
		 NULL, NULL, 1, 0)
			  	
	SELECT @status = @@ERROR
