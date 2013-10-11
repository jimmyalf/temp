CREATE PROCEDURE spContTreCreateNode
--CREATE PROCEDURE spContTreCreateNode
					@locIdTemp INT,
					@lngIdTemp INT,
					@pgeId INT,
					@treTpeId INT,
					@parent INT,
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
					@needsAuthentication BIT,
					@cssClass NVARCHAR(255),
					@publish BIT,
					@status INT OUTPUT,
					@id INT OUTPUT
	AS
		DECLARE @dummy INT,
				@locId INT,
				@lngId INT,
				@orderNo INT,
				@parentType INT,
				@parentLink NVARCHAR (256),
				@approvedDate SMALLDATETIME,
				@lockedDate SMALLDATETIME,
				@reOrdId INT,
				@reOrdTreTpeId INT,
				@reOrdOrder INT,
				@reOrdOrderLast INT,
				@reOrdOrderArhLast INT
		
		DECLARE chk_nameExist CURSOR LOCAL FOR
			SELECT	1
			FROM	tblContTree
			WHERE	cName = @name
				AND	cParent = @parent
												
		DECLARE get_parent CURSOR LOCAL FOR
			SELECT	cLocId,
					cLngId,
					cTreTpeId
			FROM	tblContTree
			WHERE	cId = @parent
			
		DECLARE get_order CURSOR LOCAL FOR
			SELECT	COUNT (cId)
			FROM	tblContTree
			WHERE	cParent = @parent
			
		DECLARE chk_defaultPage CURSOR LOCAL FOR
			SELECT	cId
			FROM	tblContTree
			WHERE	cParent = @parent
				AND	cTreTpeId = 5
				
		DECLARE chk_defaultPageLocLng CURSOR LOCAL FOR
			SELECT	cId
			FROM	tblContTree
			WHERE	cParent = @parent
				AND	(cTreTpeId = 5
						OR cTreTpeId = 4)

		DECLARE chk_defaultLink CURSOR LOCAL FOR
			SELECT	cLink
			FROM	tblContTree
			WHERE	cId = @parent

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
			
		IF (@parent IS NULL)
			BEGIN
				SELECT @id = 0
				SELECT @status = -6
				RETURN
			END
			
		
		
			
		IF (@treTpeId = 5)
			BEGIN	
				OPEN chk_defaultPage
				FETCH NEXT FROM chk_defaultPage INTO @dummy
				
				IF (@@FETCH_STATUS <> -1)
					BEGIN
						CLOSE chk_defaultPage
						DEALLOCATE chk_defaultPage
						
						UPDATE	tblContTree
						SET		cTreTpeId = 4
						WHERE	cId = @dummy				
					END
				ELSE
					BEGIN
						CLOSE chk_defaultPage
						DEALLOCATE chk_defaultPage
					END
					
				OPEN chk_defaultLink
				FETCH NEXT FROM chk_defaultLink INTO @parentLink
				
				IF (@@FETCH_STATUS <> -1)
					BEGIN
						IF ((@parentLink IS NOT NULL)
							 AND (LEN (@parentLink) != 0))
							BEGIN
								CLOSE chk_defaultLink
								DEALLOCATE chk_defaultLink
						
								SELECT @id = 0
								SELECT @status = -7
								RETURN
							END
					END
					
				CLOSE chk_defaultLink
				DEALLOCATE chk_defaultLink
								
				UPDATE	tblContTree
				SET		cOrder = cOrder + 1
				WHERE	cParent = @parent	
			END
		
		
		DECLARE get_parenttype CURSOR LOCAL FOR
			SELECT	cLocId,
					cLngId,
					cTreTpeId
			FROM	tblContTree
			WHERE	cId = @parent
				
		OPEN get_parenttype
		FETCH NEXT FROM get_parenttype INTO	@locId,
										@lngId,
										@parentType
										
		IF (@@FETCH_STATUS = -1)
			BEGIN
				CLOSE get_parenttype
				DEALLOCATE get_parenttype
				
				SELECT @status = -6
				SELECT @id = 0
				RETURN
			END
			
		CLOSE get_parenttype
		DEALLOCATE get_parenttype
		
		
		IF (@treTpeId = 4 AND (@parentType <> 18))
			BEGIN
				DECLARE chk_other_pages CURSOR LOCAL FOR
					SELECT	1
					FROM	tblContTree
					WHERE	cParent = @parent
					
				OPEN chk_other_pages
				FETCH NEXT FROM chk_other_pages INTO @dummy
				
				IF (@@FETCH_STATUS = -1)
					BEGIN
						SET @treTpeId = 5
					END
					
				CLOSE chk_other_pages
				DEALLOCATE chk_other_pages				
			END
						
		OPEN chk_nameExist
		FETCH NEXT FROM chk_nameExist INTO @dummy
		
		IF (@@FETCH_STATUS <> -1) 
			BEGIN
				CLOSE chk_nameExist
				DEALLOCATE chk_nameExist
				
				SET @status = -1
				SET @id = 0
				RETURN
			END
		ELSE
			BEGIN
				CLOSE chk_nameExist
				DEALLOCATE chk_nameExist
				
				IF ((@locIdTemp IS NOT NULL) AND (@lngIdTemp IS NOT NULL))
					BEGIN
						SET @locId = @locIdTemp
						SET @lngId = @lngIdTemp
					END
				ELSE
					BEGIN
						OPEN get_parent
						FETCH NEXT FROM get_parent INTO	@locId,
														@lngId,
														@parentType
														
						IF (@@FETCH_STATUS = -1)
							BEGIN
								CLOSE get_parent
								DEALLOCATE get_parent
								
								SELECT @status = -6
								SELECT @id = 0
								RETURN
							END
							
						CLOSE get_parent
						DEALLOCATE get_parent
					END
									
				IF ((@treTpeId = 3)
					 AND (@parentType <> 2) 
					 AND (@parentType <> 3)
					 AND (@parentType <> 18))
					BEGIN
						SELECT @status = -6
						SELECT @id = 0
						RETURN
					END
										
				IF ((@treTpeId = 4)
					AND ((@parentType = 1) OR (@parentType = 2)))
					BEGIN
						SET @treTpeId = 5
						
						OPEN chk_defaultPageLocLng
						FETCH NEXT FROM chk_defaultPageLocLng INTO @dummy
						
						IF (@@FETCH_STATUS <> -1)
							BEGIN
								CLOSE chk_defaultPageLocLng
								DEALLOCATE chk_defaultPageLocLng
								
								SET @status = -6
								SET @id = 0
								RETURN			
							END
							
							CLOSE chk_defaultPageLocLng
							DEALLOCATE chk_defaultPageLocLng
					END

					
				IF ((@treTpeId = 4) 
					AND (@parentType <> 3)
					AND (@parentType <> 18))
					BEGIN
						SELECT @status = -6
						SELECT @id = 0
						RETURN
					END
					
				IF ((@treTpeId = 5) 
						AND (@parentType <> 2) 
						AND (@parentType <> 3)
						AND (@parentType <> 18))
					BEGIN
						SELECT @status = -6
						SELECT @id = 0
						RETURN
					END
										
				IF ((@treTpeId = 12)
					AND (@parentType <> 2)
					AND (@parentType <> 3))
					BEGIN
						SET @status = -6
						SET @id = 0
						RETURN
					END
					
				IF ((@treTpeId = 16)
					AND (@parentType <> 17))
					BEGIN
						SET @status = -6
						SET @id = 0
						RETURN
					END	
					
				SET @reOrdOrderLast = 1
				SET @reOrdOrderArhLast = 2147483640

				DECLARE reorder CURSOR LOCAL FOR
					SELECT	cId,
							cTreTpeId,
							cOrder
					FROM	tblContTree
					WHERE	cParent = @parent
					ORDER BY cOrder ASC		
					
				OPEN reorder 
				FETCH NEXT FROM reorder INTO	@reOrdId,
												@reOrdTreTpeId,
												@reOrdOrder
												
				WHILE (@@FETCH_STATUS <> -1)
					BEGIN
						IF ((@reOrdTreTpeId = 10)
							OR (@reOrdTreTpeId = 17))
							BEGIN
								FETCH NEXT FROM reorder INTO	@reOrdId,
																@reOrdTreTpeId,
																@reOrdOrder
								CONTINUE
							END
					
						IF (@reOrdTreTpeId = 9)
							BEGIN
								UPDATE	tblContTree
								SET		cOrder = @reOrdOrderArhLast
								WHERE	cId = @reOrdId
								
								SET @reOrdOrderArhLast = @reOrdOrderArhLast + 1
							END
						ELSE
							BEGIN
								UPDATE	tblContTree
								SET		cOrder = @reOrdOrderLast
								WHERE	cId = @reOrdId
								
								SET @reOrdOrderLast = @reOrdOrderLast + 1
							END
							
						FETCH NEXT FROM reorder INTO	@reOrdId,
														@reOrdTreTpeId,
														@reOrdOrder
					END
					
				CLOSE reorder
				DEALLOCATE reorder
														
				IF (@treTpeId = 9)
					BEGIN
						SET @orderNo = @reOrdOrderArhLast
					END
				ELSE
					BEGIN
						SET @orderNo = @reOrdOrderLast
						
						IF (@treTpeId = 5)
							BEGIN
								SET @orderNo = 1
								
								UPDATE	tblContTree
								SET		cOrder = cOrder + 1
								WHERE	cParent = @parent
									AND	cTreTpeId <> 9
							END
						
					END

				INSERT INTO tblContTree
					(cPgeId, cLocId, cLngId, cTreTpeId, cParent, cName,
					 cFileName, cLockFileName, cDescription, cNote, cKeywords, 
					 cLink, cTarget, cHeader, cHideInMenu,
					 cExcludeFromSearch, cTemplate, cStylesheet,
					 cOrder, cCreatedBy, cCreatedDate, cChangedBy, cChangedDate,
					 cApprovedBy, cApprovedDate, cPublishDate, cPublishedDate,
					 cUnPublishDate, cUnPublishedDate, cLockedBy,
					 cLockedDate, cCrsTpeId, cNeedsAuthentication, cCssClass, cPublish)
				VALUES
					(@pgeId, @locId, @lngId, @treTpeId, @parent, @name,
					 @fileName, @lockFileName, @description, @note, @keywords, 
					 @link, @target, @header, @hideInMenu,
					 @excludeFromSearch, @template, @stylesheet,
					 @orderNo, @createdBy, GETDATE (), @createdBy, GETDATE (),
					 @approvedBy, @approvedDate, @publishDate, NULL,
					 @unPublishDate, NULL, @lockedBy,  
					 @lockedDate, 1, @needsAuthentication, @cssClass, @publish)
			END
			
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
				SELECT @id = @@IDENTITY
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
				SELECT @id = 0
			END
