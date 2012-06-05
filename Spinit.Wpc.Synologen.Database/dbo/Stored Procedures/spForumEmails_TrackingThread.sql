CREATE PROCEDURE spForumEmails_TrackingThread
(
	@PostID INT
)
AS

DECLARE @ForumID INT
DECLARE @UserID INT
DECLARE @PostLevel INT
DECLARE @ThreadID INT

-- First get the post info
SELECT 
	@ForumID = ForumID, 
	@UserID = UserID,
	@PostLevel = PostLevel,
	@ThreadID = ThreadID
FROM 
	tblForumPosts (nolock) 
WHERE 
	PostID = @PostID


-- Check if this is a PM message
IF (@ForumID = 0)
BEGIN
	
	-- we have to bind to the PM users for this ThreadID
	SELECT
		U.Email,
		UP.EnableHtmlEmail
	FROM
		tblForumTrackedThreads T
		JOIN tblForumUsers U (nolock) ON U.UserID = T.UserID
		JOIN tblForumUserProfile UP ON UP.UserID = U.UserID
		JOIN tblForumPrivateMessages PM ON PM.UserID = T.UserID AND PM.ThreadID = @ThreadID
	WHERE
		T.ThreadID = @ThreadID

END
ELSE BEGIN

	SELECT
		U.Email, 
		UP.EnableHtmlEmail
	FROM 
		tblForumTrackedThreads T
		JOIN tblForumUsers U (nolock) ON U.UserID = T.UserID
		JOIN tblForumUserProfile UP ON UP.UserID = U.UserID				
	WHERE
		T.ThreadID = @ThreadID
END

