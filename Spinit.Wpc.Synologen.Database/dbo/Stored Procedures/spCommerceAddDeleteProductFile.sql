CREATE PROCEDURE spCommerceAddDeleteProductFile
@action INT, @prdId INT, @fleId INT, @lngId INT, @status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblCommerceProductFile
			WHERE	cPrdId = @prdId
				AND	cFleId = @fleId
				AND	cLngId = @lngId
			
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
					RETURN
				END		
			
			SELECT	@dummy = 1
			FROM	tblCommerceProduct
			WHERE	cId = @prdId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -10
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
			WHERE	cId = @lngId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -5
					RETURN
				END

			INSERT INTO tblCommerceProductFile
				(cPrdId, cFleId, cLngId)
			VALUES
				(@prdId, @fleId, @lngId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductFile
			WHERE	cPrdId = @prdId
				AND	cFleId = @fleId
				AND	cLngId = @lngId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductFile
			WHERE	cPrdId = @prdId
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
