CREATE PROCEDURE [dbo].[spCommerceAddDeleteLanguageConnection]
					@action INT,
					@lngId INT,
					@prdCatId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN				
			SELECT	@dummy = 1
			FROM	tblBaseLanguages
			WHERE	cId = @lngId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -5
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
			
			INSERT INTO tblCommerceLanguageConnection
				(cLngId, cPrdCatId)
			VALUES
				(@lngId, @prdCatId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceLanguageConnection
			WHERE	cLngId = @lngId
				AND	cPrdCatId = @prdCatId
						
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			DELETE FROM tblCommerceLanguageConnection
			WHERE		cLngId = @lngId
				AND		cPrdCatId = @prdCatId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
