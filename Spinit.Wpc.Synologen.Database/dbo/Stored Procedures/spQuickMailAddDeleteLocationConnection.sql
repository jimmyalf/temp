create PROCEDURE spQuickMailAddDeleteLocationConnection
					@action INT,
					@locId INT,
					@mlId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN				
			SELECT	@dummy = 1
			FROM	tblQuickMailLocationConnection
			WHERE	cLocId = @locId
				AND	cMlId = @mlId
						
			IF @@ROWCOUNT > 0
				BEGIN
					SET @status = -2
					RETURN
				END

			SELECT	@dummy = 1
			FROM	tblBaseLocations
			WHERE	cId = @locId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -6
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
			
			INSERT INTO tblQuickMailLocationConnection
				(cLocId, cMlId)
			VALUES
				(@locId, @mlId)				
		 END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailLocationConnection
			WHERE	cLocId = @locId
				AND	cMlId = @mlId
						
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			DELETE FROM tblQuickMailLocationConnection
			WHERE		cLocId = @locId
				AND		cMlId = @mlId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
