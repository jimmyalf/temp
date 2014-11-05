create PROCEDURE spQuickMailSearchPageConnection
					@type INT,
					@mlId INT,
					@pgeId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cMlId,
					cPgeId
			FROM	tblQuickMailPageConnection
			ORDER BY cMlId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cMlId,
					cPgeId
			FROM	tblQuickMailPageConnection
			WHERE	cMlId = @mlId
			ORDER BY cMlId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cMlId,
					cPgeId
			FROM	tblQuickMailPageConnection
			WHERE	cPgeId = @pgeId
			ORDER BY cMlId ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cMlId,
					cPgeId
			FROM	tblQuickMailPageConnection
			WHERE	cMlId = @mlId
				AND	cPgeId = @pgeId
			ORDER BY cMlId ASC
		END

	SET @status = @@ERROR
			
END
