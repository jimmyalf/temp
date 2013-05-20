create  procedure spForumsystem_UpdateUserPostCount
(
	@ForumID 	int,
	@UserID		int
)
AS
BEGIN

	-- Does the forum track post statistics?
	IF (SELECT EnablePostStatistics FROM tblForumForums WHERE ForumID = @ForumID) = 0
		RETURN

	UPDATE tblForumUserProfile SET TotalPosts = TotalPosts + 1 WHERE UserID = @UserID

END

