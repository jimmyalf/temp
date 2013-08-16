create FUNCTION sfDocumentAllowedDocuments
( @userId int)
RETURNS @tmpList table
(
	cId int
)
AS
BEGIN
IF ((@userId = NULL) OR (@userId = -1))
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblDocuments
END
ELSE
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblDocuments
	WHERE cGroupId IS NULL AND cUserId IS NULL
	UNION
	SELECT	cId
	FROM	tblDocuments
	WHERE cUserId = @userId
	UNION
	SELECT	cId
	FROM	tblDocuments
	WHERE cUserId IS NULL AND cGroupId IN (Select cGroupId from tblBaseUsersGroups where cuserId = @userId)
END

RETURN
END
