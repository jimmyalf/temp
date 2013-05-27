CREATE PROCEDURE [dbo].[spCommerceAddDeleteProductFileConnection]
					@action INT,
					@prdId INT,
					@fleId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblCommerceProductFileConnection
			WHERE	cPrdId = @prdId
				AND	cFleId = @fleId
			
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
			
			INSERT INTO tblCommerceProductFileConnection
				(cPrdId, cFleId)
			VALUES
				(@prdId, @fleId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceProductFileConnection
			WHERE	cPrdId = @prdId
				AND	cFleId = @fleId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceProductFileConnection
			WHERE	cPrdId = @prdId
				AND	cFleId = @fleId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
