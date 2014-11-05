create PROCEDURE spQuickMailAddDeletePageConnection
					@action INT,
					@mlId INT,
					@pgeId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblQuickMailPageConnection
			WHERE	cMlId = @mlId
				AND	cPgeId = @pgeId
			
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
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
			
			SELECT	@dummy = 1
			FROM	tblContPage
			WHERE	cId = @pgeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -9
					RETURN
				END
			
			INSERT INTO tblQuickMailPageConnection
				(cMlId, cPgeId)
			VALUES
				(@mlId, @pgeId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailPageConnection
			WHERE	cMlId = @mlId
				AND	cPgeId = @pgeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblQuickMailPageConnection
			WHERE	cMlId = @mlId
				AND	cPgeId = @pgeId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
