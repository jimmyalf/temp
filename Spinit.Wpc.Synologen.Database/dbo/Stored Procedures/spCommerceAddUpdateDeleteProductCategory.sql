CREATE PROCEDURE spCommerceAddUpdateDeleteProductCategory
					@action INT,
					@id INT OUTPUT,
					@order INT,
					@parent INT,
					@name NVARCHAR (512),
					@description NTEXT,
					@picture INT,
					@active BIT,
					@approvedBy NVARCHAR (100),
					@approvedDate DATETIME,
					@lockedBy NVARCHAR (100),
					@lockedDate DATETIME,
					@userName NVARCHAR (100),
					@currentLocationId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT,
			--@max INT,
			@pOrder INT,
			@maxOrder INT
							
	IF @action = 0 BEGIN	
		IF @parent IS NOT NULL BEGIN		
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategory
			WHERE	cId = @parent
			
			IF @@ROWCOUNT = 0 BEGIN
				SET @status = -7
				RETURN
			END		
			/*
			SELECT	@max = MAX (cOrder)
			FROM	tblCommerceProductCategory	
			WHERE	cParent = @parent*/
		END	
/*		
		ELSE BEGIN
			SELECT	@max = MAX (cOrder)
			FROM	tblCommerceProductCategory
			WHERE	cParent IS NULL
		END
			
		IF @max IS NULL BEGIN
			SET @max = 1
		END
		 */
		IF @picture IS NOT NULL BEGIN
			SELECT	@dummy = 1 FROM	tblBaseFile WHERE cId = @picture
			IF @@ROWCOUNT = 0 BEGIN
				SET @status = -8
				RETURN
			END
		END		
		
		INSERT INTO tblCommerceProductCategory
			(cOrder, cParent, cName, cDescription, cPicture,
			 cActive, cApprovedBy, cApprovedDate, cLockedBy,
			 cLockedDate, cCreatedBy, cCreatedDate)
		VALUES
			(0, @parent, @name, @description, @picture, 
			 @active, @approvedBy, @approvedDate, @lockedBy,
			 @lockedDate, @userName, GETDATE ())
			
		SET @id = @@IDENTITY
	END
	ELSE IF @action = 1 BEGIN
			SELECT @dummy = 1 FROM tblCommerceProductCategory WHERE cId = @id
			IF @@ROWCOUNT = 0 BEGIN
				SET @status = -3
				RETURN
			END

			IF @parent IS NOT NULL BEGIN		
				SELECT @dummy = 1 FROM tblCommerceProductCategory WHERE cId = @parent
				IF @@ROWCOUNT = 0 BEGIN
					SET @status = -7
					RETURN
				END		
			END	
			
			IF @picture IS NOT NULL BEGIN
				SELECT @dummy = 1 FROM	tblBaseFile WHERE cId = @picture
				IF @@ROWCOUNT = 0 BEGIN
					SET @status = -8
					RETURN
				END
			END		

			--IF @order IS NOT NULL BEGIN
			--	IF @order != @pOrder
			--		BEGIN
			--			IF @parent IS NULL
			--				BEGIN
			--					SELECT	@maxOrder = MAX (cOrder)
			--					FROM	dbo.tblCommerceProductCategory
			--					WHERE	cParent IS NULL
			--				END
			--			ELSE
			--				BEGIN
			--					SELECT	@maxOrder = MAX (cOrder)
			--					FROM	dbo.tblCommerceProductCategory
			--					WHERE	cParent = @parent
			--				END
					
			--			IF (@order > @pOrder) AND (@pOrder < @maxOrder)
			--				BEGIN
			--					IF @parent IS NULL
			--						BEGIN
			--							UPDATE	dbo.tblCommerceProductCategory
			--							SET		cOrder = cOrder - 1
			--							WHERE	cParent IS NULL
			--								AND	cOrder = (@pOrder + 1)
			--						END
			--					ELSE
			--						BEGIN
			--							UPDATE	dbo.tblCommerceProductCategory
			--							SET		cOrder = cOrder - 1
			--							WHERE	cParent = @parent
			--								AND	cOrder = (@pOrder + 1)
			--						END
								
			--					SET @pOrder = @pOrder + 1
			--				END
			--			ELSE
			--				BEGIN
			--					IF @pOrder > 1
			--						BEGIN
			--							IF @parent IS NULL
			--								BEGIN
			--									UPDATE	dbo.tblCommerceProductCategory
			--									SET		cOrder = cOrder + 1
			--									WHERE	cParent IS NULL
			--										AND	cOrder = (@pOrder - 1)
			--								END
			--							ELSE
			--								BEGIN
			--									UPDATE	dbo.tblCommerceProductCategory
			--									SET		cOrder = cOrder + 1
			--									WHERE	cParent = @parent
			--										AND	cOrder = (@pOrder - 1)
			--								END
										
			--							SET @pOrder = @pOrder - 1
			--						END
			--				END
			--		END
			--END
			
		--Sorting update (trigger sorting)
		UPDATE tblCommerceLocationConnection SET cOrder = @order
		WHERE cPrdCatId = @id AND cLocId = @currentLocationId
			

		UPDATE	tblCommerceProductCategory
		SET		cOrder = @order,
				cParent = @parent,
				cName = @name,
				cDescription = @description,
				cPicture = @picture,
				cActive = @active,
				cApprovedBy = @approvedBy,
				cApprovedDate = @approvedDate,
				cLockedBy = @lockedBy,
				cLockedDate = @lockedDate,
				cChangedBy = @userName,
				cChangedDate = GETDATE ()
		WHERE	cId = @id
	END
	ELSE IF @action = 2 BEGIN
		SELECT	@dummy = 1
		FROM	tblCommerceProductCategory
		WHERE	cId = @id
		
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -3
			RETURN
		END		

		DECLARE @statusRet INT	
		EXECUTE spCommerceDeleteProductCategoryRecursive @id, @statusRet OUTPUT

		IF (@statusRet <> 0) BEGIN
			SELECT @status = @statusRet
		END

	END
	ELSE BEGIN
		SET @status = -1
		RETURN
	END
	
	SET @status = @@ERROR
END
