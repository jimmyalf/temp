create procedure spForumGetModeratorsForEmailNotification
(
	@PostID	int
)
 AS
	-- get the ForumID
	DECLARE @ForumID int
	SELECT @ForumID = ForumID FROM Posts (nolock) WHERE PostID = @PostID
	SELECT
		U.Username,
		Email
	FROM Users U (nolock)
		INNER JOIN Moderators M (nolock) ON
			M.UserName = U.UserName
	WHERE (M.ForumID = @ForumID OR M.ForumID = 0) AND M.EmailNotification = 1


