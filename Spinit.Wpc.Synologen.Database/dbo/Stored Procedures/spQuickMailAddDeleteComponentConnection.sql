create PROCEDURE spQuickMailAddDeleteComponentConnection
					@action INT,
					@cmpId INT,
					@mlId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN				
			SELECT	@dummy = 1
			FROM	tblQuickMailComponentConnection
			WHERE	cCmpId = @cmpId
				AND	cMlId = @mlId
						
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
					RETURN
				END

			SELECT	@dummy = 1
			FROM	tblBaseComponents
			WHERE	cId = @cmpId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -7
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
			
			INSERT INTO tblQuickMailComponentConnection
				(cCmpId, cMlId)
			VALUES
				(@cmpId, @mlId)
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailComponentConnection
			WHERE	cCmpId = @cmpId
				AND	cMlId = @mlId
						
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			DELETE FROM tblQuickMailComponentConnection
			WHERE		cCmpId = @cmpId
				AND		cMlId = @mlId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
