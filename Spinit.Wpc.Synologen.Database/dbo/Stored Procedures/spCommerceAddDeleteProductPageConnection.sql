CREATE PROCEDURE [dbo].[spCommerceAddDeleteProductPageConnection]
					@action INT,
					@prdId INT,
					@pgeId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblCommerceProductPageConnection
			WHERE	cPrdId = @prdId
				AND	cPgeId = @pgeId
			
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
			FROM	tblContPage
			WHERE	cId = @pgeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -20
					RETURN
				END
			
			INSERT INTO tblCommerceProductPageConnection
				(cPrdId, cPgeId)
			VALUES
				(@prdId, @pgeId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductPageConnection
			WHERE	cPrdId = @prdId
				AND	cPgeId = @pgeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductPageConnection
			WHERE	cPrdId = @prdId
				AND	cPgeId = @pgeId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
