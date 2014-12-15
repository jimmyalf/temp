CREATE PROCEDURE spContTreCreateLocation
					@locId INT,
					@name NVARCHAR (255),
					@fileName NTEXT,
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
				@approvedDate SMALLDATETIME,
				@lockedDate SMALLDATETIME
		
		DECLARE chk_nameExist CURSOR FOR 
			SELECT	1
			FROM	tblContTree
			WHERE	cName = @name
				AND	cParent IS NULL
				
		DECLARE chk_locExist CURSOR FOR
			SELECT	1
			FROM	tblContTree
			WHERE	cLocId = @locId
				
		DECLARE get_order CURSOR FOR
			SELECT	COUNT (cId)
			FROM	tblContTree
			WHERE	cParent IS NULL
								
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
			
		OPEN chk_locExist
		FETCH NEXT FROM chk_locExist INTO @dummy
		
		IF (@@FETCH_STATUS <> -1)
			BEGIN
				CLOSE chk_locExist
				DEALLOCATE chk_locExist
				
				SELECT @status = -1
				SELECT @id = 0
				RETURN
			END
			
		CLOSE chk_locExist
		DEALLOCATE chk_locExist

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
			
		CLOSE chk_nameExist
		DEALLOCATE chk_nameExist

		INSERT INTO tblContTree
			(cLocId, cTreTpeId, cName, cFileName, cLockFileName, cDescription,
			 cNote, cKeywords, cLink, cTarget, cHeader,
			 cHideInMenu, cExcludeFromSearch, cTemplate, cStylesheet,
			 cOrder, cCreatedBy, cCreatedDate, cChangedBy,
			 cChangedDate, cApprovedBy, cApprovedDate, cPublishDate,
			 cPublishedDate, cUnPublishDate, cUnPublishedDate,
			 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
		VALUES
			(@locId, 1, @name, @fileName, @lockFileName, @description, 
			 @note, @keywords, @link, @target, @header, 
			 @hideInMenu, @excludeFromSearch, @template, @stylesheet,
			 @orderNo, @createdBy, GETDATE (), @createdBy,
			 GETDATE (), @approvedBy,  @approvedDate, @publishDate, 
			 NULL, @unPublishDate, NULL,  
			 @lockedBy, @lockedDate, 1, 0)
			
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
