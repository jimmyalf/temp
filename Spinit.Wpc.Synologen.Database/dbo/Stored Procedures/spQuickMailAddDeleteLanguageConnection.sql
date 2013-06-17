create PROCEDURE spQuickMailAddDeleteLanguageConnection
@action INT, @lngId INT, @mlId INT, @status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN				
			SELECT	@dummy = 1
			FROM	tblQuickMailLanguageConnection
			WHERE	cLngId = @lngId
				AND	cMlId = @mlId
						
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
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

			SELECT	@dummy = 1
			FROM	tblQuickMailMail
			WHERE	cId = @mlId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -11
					RETURN
				END
			
			INSERT INTO tblQuickMailLanguageConnection
				(cLngId, cMlId)
			VALUES
				(@lngId, @mlId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailLanguageConnection
			WHERE	cLngId = @lngId
				AND	cMlId = @mlId
						
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			DELETE FROM tblQuickMailLanguageConnection
			WHERE		cLngId = @lngId
				AND		cMlId = @mlId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
