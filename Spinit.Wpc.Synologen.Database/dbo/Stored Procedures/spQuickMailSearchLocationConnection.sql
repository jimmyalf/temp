create PROCEDURE spQuickMailSearchLocationConnection
					@type INT,
					@locId INT,
					@mlId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cLocId,
					cMlId
			FROM	tblQuickMailLocationConnection
			ORDER BY cMlId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cLocId,
					cMlId
			FROM	tblQuickMailLocationConnection
			WHERE	cMlId = @mlId
			ORDER BY cMlId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cLocId,
					cMlId
			FROM	tblQuickMailLocationConnection
			WHERE	cLocId = @locId
				AND	cMlId = @mlId
			ORDER BY cMlId ASC
		END

	SET @status = @@ERROR
			
END
