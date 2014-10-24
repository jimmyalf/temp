CREATE PROCEDURE spForumEmails_TrackingForum
(
	@PostID    INT
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

-- Check if its a new thread or not
IF (@PostLevel = 1)
BEGIN
	-- this is a new thread (1 & 2)
	
	-- Check if this is a PM message
	IF (@ForumID = 0)
	BEGIN
		
		-- we have to bind to the PM users for this ThreadID
		SELECT
			U.Email,
			UP.EnableHtmlEmail
		FROM
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID
			JOIN tblForumPrivateMessages PM ON PM.UserID = F.UserID AND PM.ThreadID = @ThreadID
		WHERE
			F.ForumID IN (-1, 0) AND
			F.SubscriptionType <> 0

	END
	ELSE BEGIN

		SELECT
			U.Email, 
			UP.EnableHtmlEmail
		FROM 
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID				
		WHERE
			F.ForumID = @ForumID AND
			F.SubscriptionType <> 0
	END
END
ELSE BEGIN
	-- this is a reply to an existing post (2)

	-- Check if this is a PM message
	IF (@ForumID = 0)
	BEGIN
		
		-- we have to bind to the PM users for this ThreadID
		SELECT
			U.Email,
			UP.EnableHtmlEmail
		FROM
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID
			JOIN tblForumPrivateMessages PM ON PM.UserID = F.UserID AND PM.ThreadID = @ThreadID
		WHERE
			F.ForumID IN (-1, 0) AND
			F.SubscriptionType = 2

	END
	ELSE BEGIN

		SELECT
			U.Email, 
			UP.EnableHtmlEmail
		FROM 
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID				
		WHERE
			F.ForumID = @ForumID AND
			F.SubscriptionType = 2
	END
END

