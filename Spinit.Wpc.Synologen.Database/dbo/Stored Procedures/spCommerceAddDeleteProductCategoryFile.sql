CREATE PROCEDURE [dbo].[spCommerceAddDeleteProductCategoryFile]
@action INT, @prdCatId INT, @fleId INT, @lngId INT, @status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategoryFile
			WHERE	cPrdCatId = @prdCatId
				AND	cFleId = @fleId
				AND	cLngId = @lngId
			
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
					RETURN
				END		
			
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategory
			WHERE	cId = @prdCatId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -13
					RETURN
				END
			
			SELECT	@dummy = 1
			FROM	tblBaseFile
			WHERE	cId = @fleId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -8
					RETURN
				END
			
			SELECT	@dummy = 1
			FROM	tblBaseLanguages
			WHERE	cId = @fleId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -5
					RETURN
				END

			INSERT INTO tblCommerceProductCategoryFile
				(cPrdCatId, cFleId, cLngId)
			VALUES
				(@prdCatId, @fleId, @lngId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductCategoryFile
			WHERE	cPrdCatId = @prdCatId
				AND	cFleId = @fleId
				AND	cLngId = @lngId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductCategoryFile
			WHERE	cPrdCatId = @prdCatId
				AND	cFleId = @fleId
				AND	cLngId = @lngId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
