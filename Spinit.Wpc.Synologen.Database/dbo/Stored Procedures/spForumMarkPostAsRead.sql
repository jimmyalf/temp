CREATE        PROCEDURE spForumMarkPostAsRead
(
	@PostID	int,
	@UserName nvarchar (50)
)
 AS
BEGIN

	-- If @UserName is null it is an anonymous user
	IF @UserName IS NOT NULL
	BEGIN
		DECLARE @ForumID int
		DECLARE @PostDate datetime

		-- Mark the post as read
		-- *********************

		-- Only for PostLevel = 1
		IF EXISTS (SELECT PostID FROM tblForumPosts WHERE PostID = @PostID AND PostLevel = 1)
			IF NOT EXISTS (SELECT HasRead FROM PostsRead WHERE UserName = @UserName and PostID = @PostID)
				INSERT INTO PostsRead (UserName, PostID) VALUES (@UserName, @PostID)

	END

END


