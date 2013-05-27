create PROCEDURE spQuickMailSearchFileConnection
					@type INT,
					@mlId INT,
					@fleId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cMlId,
					cFleId
			FROM	tblQuickMailFileConnection
			ORDER BY cMlId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cMlId,
					cFleId
			FROM	tblQuickMailFileConnection
			WHERE	cMlId = @mlId
			ORDER BY cMlId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cMlId,
					cFleId
			FROM	tblQuickMailFileConnection
			WHERE	cFleId = @fleId
			ORDER BY cMlId ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cMlId,
					cFleId
			FROM	tblQuickMailFileConnection
			WHERE	cMlId = @mlId
				AND	cFleId = @fleId
			ORDER BY cMlId ASC
		END

	SET @status = @@ERROR
			
END
