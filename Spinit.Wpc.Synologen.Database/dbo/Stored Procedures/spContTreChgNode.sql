CREATE PROCEDURE spContTreChgNode
--CREATE PROCEDURE [dbo].[spContTreChgNode]
					@id INT,
					@treTpeId INT,
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
					@publishDate SMALLDATETIME,
					@unPublishDate SMALLDATETIME,
					@changedBy NVARCHAR (100),
					@approvedBy NVARCHAR (100),
					@needsAuthentication BIT,
					@cssClass NVARCHAR(255),
					@publish BIT,
					@status INT OUTPUT
	AS
		DECLARE	@dummy INT,
				@pgeId INT,
				@parent INT,
				@treTreTpeId INT,
				@treName NVARCHAR (255),
				@treLockFileName BIT,
				@treNote NVARCHAR (256),
				@treLink NVARCHAR (256),
				@treTarget NVARCHAR (256),
				@treHideInMenu BIT,
				@treExcludeFromSearch BIT,
				@treTemplate INT,
				@treStylesheet INT,
				@trePublishDate SMALLDATETIME,
				@treUnPublishDate SMALLDATETIME,
				@approvedDate SMALLDATETIME
				
		DECLARE get_tree CURSOR FOR
			SELECT	cPgeId,
					cParent,
					cTreTpeId,
					cName,
					cLockFileName,
					cNote,
					cLink,
					cTarget,
					cHideInMenu,
					cExcludeFromSearch,
					cTemplate,
					cStylesheet,
					cPublishDate,
					cUnPublishDate
			FROM	tblContTree
			WHERE	cId = @id
			
		DECLARE chk_defaultPage CURSOR FOR
			SELECT	1
			FROM	tblContTree
			WHERE	cTreTpeId = 5
				AND	cParent = @id
				
		IF (@approvedBy IS NULL)
			BEGIN
				SELECT @approvedDate = NULL
			END
		ELSE
			BEGIN
				SELECT @approvedDate = GETDATE ()
			END
			
		OPEN get_tree
		FETCH NEXT FROM get_tree INTO	@pgeId,
										@parent,
										@treTreTpeId,
										@treName,
										@treLockFileName,
										@treNote,
										@treLink,
										@treTarget,
										@treHideInMenu,
										@treExcludeFromSearch,
										@treTemplate,
										@treStylesheet,
										@trePublishDate,
										@treUnPublishDate
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_tree
				DEALLOCATE get_tree
				
				SELECT @status = -2
				RETURN
			END
			
		CLOSE get_tree
		DEALLOCATE get_tree		
		
		IF (@treTpeId IS NOT NULL)
			BEGIN				
				IF ((@treTpeId = 5) AND (@treTreTpeId <> 5))
					BEGIN
--						UPDATE	tblContTree
--						SET		cTreTpeId = 4,
--								cPublishedDate = NULL
--						WHERE	cTreTpeId = 5
--							AND	cParent = @parent
							
						UPDATE 	tblContTree
						SET		cOrder = cOrder + 1
						WHERE	cParent = @parent
							AND	cOrder < 2147483628
						
						UPDATE	tblContTree
						SET		cOrder = 1
						WHERE	cId = @id
					END		
				
				SELECT @treTreTpeId = @treTpeId
			END	
		
		IF (@name IS NOT NULL)
			BEGIN	
				DECLARE chk_nameExist CURSOR FOR
					SELECT	1
					FROM	tblContTree
					WHERE	cName = @name
						AND	cParent = @parent
						AND	cId <> @id

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
				
				SELECT @treName = @name
			END
			
		IF (@fileName IS NOT NULL)
			BEGIN
				UPDATE	tblContTree
				SET		cFileName = @fileName
				WHERE	cId = @id
			END
			
		IF (@lockFileName IS NOT NULL)
			BEGIN
				SET @treLockFileName = @lockFileName
			END
			
		IF (@description IS NOT NULL)
			BEGIN
				UPDATE	tblContTree
				SET		cDescription = @description
				WHERE	cId = @id
			END
			
		IF (@note IS NOT NULL)
			BEGIN
				IF (LEN (@note) = 0)
					BEGIN
						SELECT @treNote = NULL
					END
				ELSE
					BEGIN 
						SELECT @treNote = @note
					END
			END
		
		IF (@keywords IS NOT NULL)
			BEGIN
				UPDATE	tblContTree
				SET		cKeywords = @keywords
				WHERE	cId = @id
			END
			
		IF ((@link IS NOT NULL)
			 AND (LEN (@link) != 0))
			BEGIN
				IF (@treTreTpeId <> 12)
					BEGIN
						OPEN chk_defaultPage
						FETCH NEXT FROM chk_defaultPage INTO @dummy
						
						IF (@@FETCH_STATUS <> -1)
							BEGIN
								CLOSE chk_defaultPage
								DEALLOCATE chk_defaultPage
								
								SELECT @status = -7
								RETURN
							END
							
						CLOSE chk_defaultPage
						DEALLOCATE chk_defaultPage
					END
				SELECT @treLink = @link
			END
		ELSE
			BEGIN
				IF (LEN (@link) = 0)
					BEGIN
						SELECT @treLink = NULL
					END
			END
			
		IF (@target IS NOT NULL)
			BEGIN
				IF (LEN (@target) = 0)
					BEGIN
						SELECT @treTarget = NULL
					END
				ELSE
					BEGIN
						SELECT @treTarget = @target
					END
			END
			
		IF (@hideInMenu IS NOT NULL)
			BEGIN
				SELECT @treHideInMenu = @hideInMenu
			END
									
		IF (@excludeFromSearch IS NOT NULL)
			BEGIN
				SELECT @treExcludeFromSearch = @excludeFromSearch
			END
			
		IF (@template IS NULL OR @template = -1)
			BEGIN
				SELECT @treTemplate = NULL
			END
		ELSE
			BEGIN
				SELECT @treTemplate = @template
			END
			
		IF (@stylesheet IS NULL OR @stylesheet = -1)
			BEGIN
				SELECT @treStylesheet = NULL
			END
		ELSE
			BEGIN
				SELECT @treStylesheet = @stylesheet
			END
			
		IF (@publishDate IS NOT NULL)
			BEGIN
				SET @trePublishDate = @publishDate
			END
		
		IF (@unPublishDate IS NOT NULL)
			BEGIN
				SET @treUnPublishDate = @unPublishDate
			END
						
		UPDATE	tblContTree
		SET		cTreTpeId = @treTreTpeId,
				cName = @treName,
				cLockFileName = @treLockFileName,
				cNote = @treNote,
				cLink = @treLink,
				cTarget = @treTarget,
				cHeader = @header,
				cHideInMenu = @treHideInMenu,
				cExcludeFromSearch = @treExcludeFromSearch,
				cTemplate = @treTemplate,
				cStylesheet = @treStylesheet,
				cChangedBy = @changedBy,
				cChangedDate = GETDATE (),
				cApprovedBy = @approvedBy,
				cApprovedDate = @approvedDate,
				cPublishDate = @trePublishDate,
				--cPublishedDate = NULL,
				cUnPublishDate = @trePublishDate,
				cNeedsAuthentication = @needsAuthentication,
				cCssClass = @cssClass,
				cPublish = @publish
				--cUnPublishedDate = NULL

		WHERE	cId = @id
		
		SELECT @status = @@ERROR
