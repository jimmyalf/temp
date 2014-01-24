create FUNCTION sfBaseGetNrOf (@search NVARCHAR (256))
RETURNS INT
AS
	BEGIN
		DECLARE @ret INT
		SELECT	@ret = COUNT (DISTINCT cId)
		FROM	tblBaseFile	
		WHERE	cName LIKE '%' + @search + '%'
			OR	cKeyWords LIKE '%' + @search + '%'
			OR	cDescription LIKE '%' + @search + '%'

		RETURN @ret
	END
