create PROCEDURE spQuickmailSearchComponentConnection
					@type INT,
					@cmpId INT,
					@mlId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cCmpId,
					cMlId
			FROM	tblQuickMailComponentConnection
			ORDER BY cMlId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cCmpId,
					cMlId
			FROM	tblQuickMailComponentConnection
			WHERE	cMlId = @mlId
			ORDER BY cMlId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cCmpId,
					cMlId
			FROM	tblQuickMailComponentConnection
			WHERE	cCmpId = @cmpId
				AND	cMlId = @mlId
			ORDER BY cMlId ASC
		END

	SET @status = @@ERROR
			
END
