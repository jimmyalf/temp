create procedure spForumReverseTrackingOption 
(
	@UserID int,
	@PostID	int
)
AS
	-- reverse the user's tracking options for a particular thread
	-- first get the threadID of the Post
	DECLARE @ThreadID int
	SELECT @ThreadID = ThreadID FROM tblForumPosts WHERE PostID = @PostID

	IF EXISTS(SELECT ThreadID FROM tblForumTrackedThreads WHERE ThreadID = @ThreadID AND UserID = @UserID)
		-- the user is tracking this thread, delete this row
		DELETE FROM tblForumTrackedThreads
		WHERE ThreadID = @ThreadID AND UserID = @UserID
	ELSE
		-- this user isn't tracking the thread, so add her
		INSERT INTO tblForumTrackedThreads (ThreadID, UserID, DateCreated)
		VALUES(@ThreadID, @UserID, GetDate())


