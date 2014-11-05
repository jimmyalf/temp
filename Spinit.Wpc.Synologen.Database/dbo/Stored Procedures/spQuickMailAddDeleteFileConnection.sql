create PROCEDURE spQuickMailAddDeleteFileConnection
					@action INT,
					@mlId INT,
					@fleId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblQuickMailFileConnection
			WHERE	cMlId = @mlId
				AND	cFleId = @fleId
			
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
			FROM	tblBaseFile
			WHERE	cId = @fleId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -8
					RETURN
				END
			
			INSERT INTO tblQuickMailFileConnection
				(cMlId, cFleId)
			VALUES
				(@mlId, @fleId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailFileConnection
			WHERE	cMlId = @mlId
				AND	cFleId = @fleId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblQuickMailFileConnection
			WHERE	cMlId = @mlId
				AND	cFleId = @fleId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
