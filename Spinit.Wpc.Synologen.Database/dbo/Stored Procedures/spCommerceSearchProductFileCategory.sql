CREATE PROCEDURE spCommerceSearchProductFileCategory
					@categoryId INT = NULL,
					@status INT OUTPUT
AS
BEGIN
	IF (@categoryId IS NULL OR @categoryId <= 0) BEGIN
		SELECT	tblCommerceProductFileCategory.*
		FROM	tblCommerceProductFileCategory
		ORDER BY cName ASC
	END
		
	ELSE BEGIN
		SELECT	tblCommerceProductFileCategory.*
		FROM	tblCommerceProductFileCategory
		WHERE	cId = @categoryId
		ORDER BY cName ASC
	END						

	SET @status = @@ERROR
			
END
