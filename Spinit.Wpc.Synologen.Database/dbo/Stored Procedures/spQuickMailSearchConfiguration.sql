﻿CREATE PROCEDURE spQuickMailSearchConfiguration
					@type INT,
					@id INT,
					@name NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cName,
					cValue
			FROM	tblQuickMailConfiguration
			ORDER BY cName ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cName,
					cValue
			FROM	tblQuickMailConfiguration
			WHERE	cId = @id
			ORDER BY cName ASC
		END
							
	IF @type = 2
		BEGIN
			SELECT	cId,
					cName,
					cValue
			FROM	tblQuickMailConfiguration
			WHERE	cName = @name
			ORDER BY cName ASC
		END

	SET @status = @@ERROR
			
END
