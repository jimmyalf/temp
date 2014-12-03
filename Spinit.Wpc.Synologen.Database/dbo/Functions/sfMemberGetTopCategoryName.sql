CREATE FUNCTION sfMemberGetTopCategoryName ( @memberId INT)
	RETURNS  NVARCHAR(50) 
AS
	BEGIN
		DECLARE @ret NVARCHAR(50)
		set @ret = ''

			IF ((@memberId IS NOT NULL) AND (@memberId > 0)) BEGIN
				SELECT TOP 1 @ret = cString 
				FROM tblMemberCategories
				INNER JOIN tblMemberLanguageStrings ON tblMemberLanguageStrings.cId = tblMemberCategories.cNameStringId
				INNER JOIN tblMemberCategoryConnection ON tblMemberCategoryConnection.cCategoryId = tblMemberCategories.cId
				WHERE tblMemberCategoryConnection.cMemberId = @memberId
			END
			return @ret
	END
