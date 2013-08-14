CREATE PROCEDURE spContTreCopyNodeRec
					@oldId INT,
					@parent INT,
					@order INT,
					@user NVARCHAR (100),
					@inName NVARCHAR (50),
					@inFileName NVARCHAR(256),
					@newId INT OUTPUT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @pgeId INT,
					@locId INT,
					@lngId INT,
					@parTreTpeId INT,
					@pgeCount INT,
					@treTpeId INT,
					@name NVARCHAR (255),
					@fileName NVARCHAR (256),
					@lockFileName BIT,
					@note NVARCHAR (256),
					@link NVARCHAR (256),
					@target NVARCHAR (256),
					@hideInMenu BIT,
					@excludeFromSearch BIT,
					@template INT,
					@stylesheet NVARCHAR (100),
					@approvedBy NVARCHAR (100),
					@approvedDate SMALLDATETIME,
					@publishDate SMALLDATETIME,
					@unPublishDate SMALLDATETIME,
					@lockedBy NVARCHAR (100),
					@lockedDate SMALLDATETIME,
					@orderCount INT,
					@bseGrpId INT,
					@crsId INT,
					@childId INT,
					@newChildId INT
					
			DECLARE get_newParent CURSOR LOCAL FOR
				SELECT	cLocId,
						cLngId,
						cTreTpeId
				FROM	tblContTree
				WHERE	cId = @parent
			
			DECLARE get_tree CURSOR LOCAL FOR
				SELECT	cPgeId,
						cTreTpeId,
						cName,
						cFileName,
						cLockFileName,
						cNote,
						cLink,
						cTarget,
						cHideInMenu,
						cExcludeFromSearch,
						cTemplate,
						cStylesheet,
						cApprovedBy,
						cApprovedDate,
						cPublishDate,
						cUnPublishDate,
						cLockedBy
				FROM	tblContTree
				WHERE	cId = @oldId
				
			
			OPEN get_newParent
			FETCH NEXT FROM get_newParent INTO	@locId,
												@lngId,
												@parTreTpeId
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					PRINT 'spContTreCopyNodeRec: Couldnt fetch new parent. Aborting...' 
					CLOSE get_newParent
					DEALLOCATE get_newParent
					
					SELECT @newId = 0
					SELECT @status = -2
					RETURN
				END
				
			CLOSE get_newParent
			DEALLOCATE get_newParent
			
			SELECT	@pgeCount = COUNT (cId)
			FROM	tblContTree
			WHERE	cParent = @parent
				AND	cTreTpeId = 5
									
			OPEN get_tree
			FETCH NEXT FROM get_tree INTO	@pgeId,
											@treTpeId,
											@name,
											@fileName,
											@lockFileName,
											@note,
											@link,
											@target,
											@hideInMenu,
											@excludeFromSearch,
											@template,
											@stylesheet,
											@approvedBy,
											@approvedDate,
											@publishDate,
											@unPublishDate,
											@lockedBy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					PRINT 'spContTreCopyNodeRec: Couldnt fetch parent tree. Aborting...' 
					CLOSE get_tree
					DEALLOCATE get_tree
					
					SELECT @newId = 0
					SELECT @status = -2
					RETURN
				END
				
			CLOSE get_tree
			DEALLOCATE get_tree
			
			-- Only allow One defaultpage under the languageNode. 
			-- Any additional page there will abort procedure.
			IF(	(@parTreTpeId = 2) AND (@pgeCount > 0) 
				AND ((@treTpeId =4) OR (@treTpeId =5) OR (@treTpeId =12)) )
				BEGIN 	
					SELECT @newId = 0
					SELECT @status = -2
					RETURN
				END
				
			IF (@parTreTpeId = 2 AND @treTpeId = 4)
				BEGIN
					PRINT 'spContTreCopyNodeRec: ParentType = Language AND nodeType = Page. Setting nodeType = DefaultPage.' 	
					SET @treTpeId = 5
				END

			IF (@pgeId IS NOT NULL)
				BEGIN
					PRINT 'spContTreCopyNodeRec: Inserting into tblContPage.' 	
					INSERT INTO tblContPage
						(cPgeTpeId, cName, cSize, cContent, 
						 cCreatedBy, cCreatedDate)
						SELECT	cPgeTpeId,
								cName,
								cSize,
								cContent,
								cCreatedBy,
								cCreatedDate
						FROM	tblContPage
						WHERE	cId = @pgeId
						
					SELECT @pgeId = @@IDENTITY
					
					UPDATE	tblContPage
					SET		cCreatedBy = @user,
							cCreatedDate = GETDATE (),
							cChangedBy = @user,
							cChangedDate = GETDATE ()
					WHERE	cId = @pgeId
				END
				
			IF (@lockedBy IS NOT NULL)
				BEGIN
					SELECT @lockedBy = @user
					SELECT @lockedDate = NULL
				END
			ELSE
				BEGIN
					SELECT @lockedDate = NULL
				END
				
			IF (@inName IS NOT NULL) BEGIN
				SELECT @name = @inName
			END

			IF (@inFileName IS NOT NULL) BEGIN
				SELECT @fileName = @inFileName
			END

			INSERT INTO tblContTree
				(cPgeId, cLocId, cLngId, cTreTpeId, cParent, cName, cFileName,
				 cLockFileName, cDescription, cNote, cKeywords, cLink, cTarget,
				 cHeader, cHideInMenu, cExcludeFromSearch,
				 cTemplate, cStylesheet, cOrder, cCreatedBy,
				 cCreatedDate, cChangedBy, cChangedDate, cApprovedBy,
				 cApprovedDate, cPublishDate, cUnPublishDate,
				 cLockedBy, cLockedDate, cCrsTpeId, cNeedsAuthentication)
			SELECT	@pgeId, @locId, @lngId, @treTpeId, @parent, @name,
					@fileName, @lockFileName, cDescription, @note, cKeywords,
					@link, @target, cHeader, @hideInMenu, @excludeFromSearch, 
					--@template, @stylesheet,
					NULL, NULL, 
					@order, @user, GETDATE (), @user, 
					GETDATE (), NULL, NULL, @publishDate, @unPublishDate, 
					NULL, NULL, 1, 0
			FROM	tblContTree
			WHERE	cId = @oldId
				 
			SELECT @newId = @@IDENTITY
			PRINT 'spContTreCopyNodeRec: Got new id='+CAST(@newId AS NVARCHAR(30))+'.' 
			
			DECLARE get_bseGrp CURSOR LOCAL FOR
				SELECT	cBseGrpId
				FROM	tblContTreeBaseGroups
				WHERE	cTreId = @oldId
				
			OPEN get_bseGrp			
			FETCH NEXT FROM get_bseGrp INTO @bseGrpId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
					PRINT 'spContTreCopyNodeRec: Updating tblContTreeBaseGroups.' 
					INSERT INTO tblContTreeBaseGroups
						(cTreId, cBseGrpId)
					VALUES
						(@newId, @bseGrpId)
					
					FETCH NEXT FROM get_bseGrp INTO @bseGrpId
				END
				
			CLOSE get_bseGrp
			DEALLOCATE get_bseGrp
			
			DECLARE get_leafs CURSOR LOCAL FOR
				SELECT		cId
				FROM		tblContTree
				WHERE		cParent = @oldId
				ORDER BY	cOrder ASC
				
			SELECT @orderCount = 0
			
			OPEN get_leafs
			FETCH NEXT FROM get_leafs INTO @childId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
					SELECT @orderCount = @orderCount + 1
					PRINT 'spContTreCopyNodeRec: Execute recursively for leafs/children.' 
					EXECUTE spContTreCopyNodeRec @childId, 
												 @newId, 
												 @orderCount,
												 @user,
												 NULL,
												 NULL,
												 @newChildId OUTPUT,
												 @status OUTPUT
					FETCH NEXT FROM get_leafs INTO @childId
				END
				
			CLOSE get_leafs
			DEALLOCATE get_leafs
			

			IF (@status = 0)
				BEGIN		
					SELECT @status = @@ERROR			
				END

			--//CBER
			IF (@status LIKE NULL) BEGIN
				SELECT @status = ISNULL(@status,@@ERROR)
			END
			IF (@status LIKE NULL) BEGIN
				SELECT @status = ISNULL(@status,0)
			END

		END
