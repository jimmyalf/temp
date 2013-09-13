create procedure spForumForum_RssPingback_Update (
	@ForumID int,
	@Pingback nvarchar(512),
	@Count int
)
AS
BEGIN

	IF EXISTS (SELECT ForumID FROM tblForumForumPingback WHERE ForumID = @ForumID AND Pingback = @Pingback)
		UPDATE
			tblForumForumPingback
		SET
			[Count] = [Count] + @Count,
			LastUpdated = GetDate()
		WHERE
			ForumID = @ForumID AND
			Pingback = @Pingback
	ELSE
		INSERT INTO
			tblForumForumPingback
		VALUES
			(@ForumID, @Pingback, @Count, GetDate())
			

END



