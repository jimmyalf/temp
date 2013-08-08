CREATE PROCEDURE spCommerceSearchDisplayType
					@type INT,
					@id INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cName
			FROM	tblCommerceDisplayType
			ORDER BY cName ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cName
			FROM	tblCommerceDisplayType
			WHERE	cId = @id
			ORDER BY cName ASC
		END
						
	SET @status = @@ERROR
			
END
