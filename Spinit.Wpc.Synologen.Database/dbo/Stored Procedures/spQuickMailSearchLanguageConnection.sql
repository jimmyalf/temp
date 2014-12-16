create PROCEDURE spQuickMailSearchLanguageConnection
					@type INT,
					@lngId INT,
					@mlId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cLngId,
					cMlId
			FROM	tblQuickMailLanguageConnection
			ORDER BY cMlId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cLngId,
					cMlId
			FROM	tblQuickMailLanguageConnection
			WHERE	cMlId = @mlId
			ORDER BY cMlId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cLngId,
					cMlId
			FROM	tblQuickMailLanguageConnection
			WHERE	cLngId = @lngId
				AND	cMlId = @mlId
			ORDER BY cMlId ASC
		END

	SET @status = @@ERROR
			
END
