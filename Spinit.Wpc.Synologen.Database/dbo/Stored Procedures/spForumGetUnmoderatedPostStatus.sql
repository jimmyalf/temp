CREATE  PROCEDURE spForumGetUnmoderatedPostStatus
(
	@ForumID int = null,
	@UserName nvarchar (50) = null
)
AS
BEGIN
DECLARE @DateDiff int
DECLARE @TotalCount int

	-- Is the user allowed to retrieve this data?
	IF NOT EXISTS(SELECT UserName FROM Moderators WHERE UserName = @UserName)
		RETURN
		
	IF @ForumID = 0
		SET @ForumID = null

	SELECT 
		OldestPostAgeInMinutes = datediff(mi, isnull(min(postdate),getdate()),getdate()),
		TotalPostsInModerationQueue = count(PostID)
	FROM 
		POSTS P 
	WHERE 
		ForumID = isnull(@ForumID,ForumID) AND 
		Approved = 0


END



