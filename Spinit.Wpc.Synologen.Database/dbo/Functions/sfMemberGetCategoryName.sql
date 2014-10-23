CREATE FUNCTION sfMemberGetCategoryName ( @categoryId INT)
	RETURNS  NVARCHAR(50) 
AS
	BEGIN
		DECLARE @ret NVARCHAR(50)
		set @ret = ''

			IF ((@categoryId IS NOT NULL) AND (@categoryId > 0)) BEGIN
				SELECT @ret = cString 
				FROM tblMemberCategories
				INNER JOIN tblMemberLanguageStrings ON tblMemberLanguageStrings.cId = tblMemberCategories.cNameStringId
				WHERE tblMemberCategories.cId = @categoryId
			END
			return @ret
	END
