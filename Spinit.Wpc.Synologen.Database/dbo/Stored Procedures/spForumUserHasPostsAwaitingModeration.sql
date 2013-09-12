create  procedure spForumUserHasPostsAwaitingModeration
(
	@UserName nvarchar(50)
)
AS
BEGIN
	-- Can the user moderate all forums?
	IF EXISTS(SELECT NULL FROM Moderators (nolock) WHERE UserName=@UserName AND ForumID=0)

		-- return ALL posts awaiting moderation
		IF EXISTS(SELECT TOP 1 PostID FROM Posts P (nolock) INNER JOIN Forums F (nolock) ON F.ForumID = P.ForumID WHERE Approved = 0)
		  SELECT 1
		ELSE
		  SELECT 0
	ELSE
		-- return only those posts in the forum this user can moderate
		IF EXISTS (SELECT TOP 1 PostID FROM Posts P (nolock) INNER JOIN Forums F (nolock) ON F.ForumID = P.ForumID WHERE Approved = 0 AND P.ForumID IN (SELECT ForumID FROM Moderators (nolock) WHERE UserName=@UserName))
		  SELECT 1
		ELSE
		  SELECT 0
	
END


