CREATE procedure spForumUser_Avatar
(
	@UserID	int
)
AS
BEGIN
	IF EXISTS(SELECT UserID FROM tblForumUserAvatar WHERE UserID = @UserID)
		SELECT
			U.UserID,
			U.ImageID,
			Length,
			ContentType,
			Content,
			DateLastUpdated
		FROM
			tblForumImages I,
			tblForumUserAvatar U
		WHERE
			I.ImageID = U.ImageID AND
			U.UserID = @UserID
END


