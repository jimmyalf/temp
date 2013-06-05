create FUNCTION sfGalleryAllowedGalleries
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
	FROM	tblGallery
END
ELSE
BEGIN
	INSERT INTO @tmpList
	SELECT	cId
	FROM	tblGallery
	WHERE (SELECT COUNT(cGroupId) FROM tblGalleryGroupConnection
			WHERE tblGalleryGroupConnection.cGalleryId = tblGallery.cId) = 0
	UNION
	SELECT	cId
	FROM	tblGallery
	WHERE cOwner = @userId
	UNION
	SELECT	cId
	FROM	tblGallery
	INNER JOIN tblGalleryGroupConnection 
		ON tblGallery.cId = tblGalleryGroupConnection.cGalleryId
	WHERE tblGalleryGroupConnection.cGroupId 
		IN (SELECT cGroupId FROM tblBaseUsersGroups WHERE cUserId = @userId)


END

RETURN
END
