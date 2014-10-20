create FUNCTION sfNewsAllowedNews
( @userId INT)
RETURNS @tmpList TABLE
(
	cId INT
)
AS
BEGIN
IF ((@userId = NULL) OR (@userId = -1))
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblNews
END
ELSE
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblNews
	WHERE (SELECT COUNT(cGroupId) FROM tblNewsGroupConnection
			WHERE tblNewsGroupConnection.cNewsId = tblNews.cId) = 0
	UNION
	SELECT	cId
	FROM	tblNews
	INNER JOIN tblNewsGroupConnection 
		ON tblNews.cId = tblNewsGroupConnection.cNewsId
	WHERE tblNewsGroupConnection.cGroupId 
		IN (SELECT cGroupId FROM tblBaseUsersGroups WHERE cUserId = @userId)


END

RETURN
END
