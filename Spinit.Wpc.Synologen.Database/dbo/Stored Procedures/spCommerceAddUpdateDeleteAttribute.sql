CREATE PROCEDURE spCommerceAddUpdateDeleteAttribute
					@action INT,
					@id INT OUTPUT,
					@order INT,
					@name NVARCHAR (512),
					@description NTEXT,
					@defaultValue NVARCHAR (1024),
					@userName NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT,
			@max INT,
			@pOrder INT,
			@maxOrder INT
							
	IF @action = 0
		BEGIN				
			SELECT	@max = MAX (cOrder)
			FROM	tblCommerceAttribute
				
			IF @max IS NULL 
				BEGIN
					SET @max = 1
				END

			INSERT INTO tblCommerceAttribute
				(cOrder, cName, cDescription, cDefaultValue, cCreatedBy, cCreatedDate)
			VALUES
				(@max + 1, @name, @description, @defaultValue, @userName, GETDATE ())
				
			SET @id = @@IDENTITY
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceAttribute
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			IF @order IS NOT NULL
				BEGIN
					IF @order != @pOrder
						BEGIN
									SELECT	@maxOrder = MAX (cOrder)
									FROM	dbo.tblCommerceProduct
						
							IF (@order > @pOrder) AND (@pOrder < @maxOrder)
								BEGIN
									UPDATE	dbo.tblCommerceAttribute
									SET		cOrder = cOrder - 1
									WHERE	cOrder = (@pOrder + 1)
									
									SET @pOrder = @pOrder + 1
								END
							ELSE
								BEGIN
									IF @pOrder > 1
										BEGIN
											UPDATE	dbo.tblCommerceAttribute
											SET		cOrder = cOrder + 1
											WHERE	cOrder = (@pOrder - 1)
											
											SET @pOrder = @pOrder - 1
										END
								END
						END
				END

			UPDATE	tblCommerceAttribute
			SET		cOrder = @order,
					cName = @name,
					cDescription = @description,
					cDefaultValue = @defaultValue,
					cChangedBy = @userName,
					cChangedDate = GETDATE ()
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceAttribute
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductCategoryAttribute
			WHERE		cAttId = @id
			
			DELETE FROM tblCommerceProductAttribute
			WHERE		cAttId = @id
				
			DELETE FROM tblCommerceAttribute
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
