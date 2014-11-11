CREATE       PROCEDURE spForumPost
/*

	Procedure for getting basic information on a single post.

*/
(
	@PostID int,
	@UserID int,
	@TrackViews bit
) AS
DECLARE @NextThreadID int
DECLARE @PrevThreadID int
DECLARE @ThreadID int 
DECLARE @ForumID int
DECLARE @SortOrder int
DECLARE @IsApproved bit

SELECT 
	@ThreadID = ThreadID, 
	@ForumID = ForumID, 
	@SortOrder=SortOrder,
	@IsApproved = IsApproved
FROM 
	tblForumPosts (nolock) 
WHERE 
	PostID = @PostID

-- Is the Forum 0 (If so this is a private message and we need to verify the user can view it
IF @ForumID = 0
BEGIN
	IF NOT EXISTS (SELECT UserID FROM tblForumPrivateMessages WHERE UserID = @UserID AND ThreadID = @ThreadID)
		RETURN
END

-- Get the previous and next thread id
EXEC spForumThread_PrevNext @ThreadID, @ForumID, @NextThreadID OUTPUT, @PrevThreadID OUTPUT

DECLARE @TrackingThread bit

IF @TrackViews = 1
BEGIN
	-- Update the counter for the number of times this post is viewed
	UPDATE tblForumPosts SET TotalViews = (TotalViews + 1) WHERE PostID = @PostID
	UPDATE tblForumThreads SET TotalViews = (TotalViews + 1) WHERE ThreadID = @ThreadID
END

-- If @UserID is 0 the user is anonymous
IF @UserID > 0 AND @IsApproved = 1
BEGIN

	-- Mark the post as read
	-- *********************
	IF NOT EXISTS (SELECT ThreadID FROM tblForumThreadsRead WHERE ThreadID = @ThreadID AND UserID = @UserID)
		INSERT INTO tblForumThreadsRead (UserID, ThreadID) VALUES (@UserID, @ThreadID)

END


IF EXISTS(SELECT ThreadID FROM tblForumTrackedThreads (nolock) WHERE ThreadID = @ThreadID AND UserID=@UserID)
	SELECT @TrackingThread = 1
ELSE
	SELECT @TrackingThread = 0

SELECT
	UserName = dbo.sfForumGetUserDisplayName(P.UserID),
	*,
	T.ThreadDate,
	T.StickyDate,
	T.IsLocked,
	T.IsSticky,
	HasRead = 0,
	EditNotes = (SELECT EditNotes FROM tblForumPostEditNotes WHERE PostID = P.PostID),
	IndexInThread = (SELECT Count(PostID) FROM tblForumPosts P1 WHERE IsApproved = 1 AND ThreadID = @ThreadID AND SortOrder <= (SELECT SortOrder FROM tblForumPosts where PostID = @PostID)),
	AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
	IsModerator = (SELECT Count(*) FROM tblForumModerators WHERE UserID = U.UserID),
	Replies = (SELECT COUNT(*) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	PrevThreadID = @PrevThreadID,
	NextThreadID = @NextThreadID,
	UserIsTrackingThread = @TrackingThread
FROM 
	tblForumPosts P,
	tblForumThreads T,
	tblForumUsers U,
	tblForumUserProfile UP
WHERE 
	P.PostID = @PostID AND
	P.ThreadID = T.ThreadID AND
	P.UserID = U.UserID AND
	U.UserID = UP.UserID
