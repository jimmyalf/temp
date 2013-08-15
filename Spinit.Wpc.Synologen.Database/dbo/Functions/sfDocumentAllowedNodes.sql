create FUNCTION sfDocumentAllowedNodes
( @userId int)
RETURNS @tmpList table
(
	cId int
)
AS
BEGIN
IF ((@userId = NULL) OR (@userId = -1) OR (@userId = 0))
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblDocumentNode
END
ELSE
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblDocumentNode
	WHERE cGroupId IS NULL AND cUserId IS NULL
	UNION
	SELECT	cId
	FROM	tblDocumentNode
	WHERE cUserId = @userId
	UNION
	SELECT	cId
	FROM	tblDocumentNode
	WHERE cUserId IS NULL AND cGroupId IN (Select cGroupId from tblBaseUsersGroups where cUserId = @userId)
END

RETURN
END
