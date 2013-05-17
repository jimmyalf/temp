CREATE PROCEDURE [dbo].[spCommerceSearchLocationConnection]
					@type INT,
					@locId INT,
					@prdCatId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cLocId,
					cPrdCatId
			FROM	tblCommerceLocationConnection
			ORDER BY cPrdCatId ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cLocId,
					cPrdCatId
			FROM	tblCommerceLocationConnection
			WHERE	cPrdCatId = @prdCatId
			ORDER BY cPrdCatId ASC
		END						

	IF @type = 2
		BEGIN
			SELECT	cLocId,
					cPrdCatId
			FROM	tblCommerceLocationConnection
			WHERE	cLocId = @locId
				AND	cPrdCatId = @prdCatId
			ORDER BY cPrdCatId ASC
		END

	SET @status = @@ERROR
			
END
