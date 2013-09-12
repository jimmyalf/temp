CREATE    procedure spForumForum_MarkRead
(
	@UserID int,
	@ForumID int = 0,
	@ForumGroupID int = 0,
	@MarkAllThreadsRead bit = 0
)
AS
BEGIN
DECLARE @LastReadThread int

	SET NOCOUNT ON

	IF @UserID = 0
		RETURN

	-- Are we marking all forums as read?
	IF @ForumGroupID = 0 AND @ForumID = 0
	BEGIN

		-- 1. Delete any entries for this user
		DELETE tblForumForumsRead WHERE UserID = @UserID
		DELETE tblForumThreadsRead WHERE UserID = @UserID

		-- 2. INSERT into tblForumForumsRead
		INSERT INTO tblForumForumsRead
		SELECT ForumGroupID, ForumID, @UserID, 0, 0, GetDate() FROM tblForumForums F

		RETURN
	END

	-- Are we marking a particular forum group as read?
	IF @ForumGroupID > 0 AND @ForumID = 0
	BEGIN

		-- 1. Delete any entries for this user
		DELETE tblForumForumsRead WHERE UserID = @UserID AND ForumGroupID = @ForumGroupID
		DELETE tblForumThreadsRead WHERE UserID = @UserID AND ForumGroupID = @ForumGroupID

		-- 2. Insert into tblForumForums Read
		INSERT INTO tblForumForumsRead
		SELECT ForumGroupID, ForumID, @UserID, 0, 0, GetDate() FROM tblForumForums F WHERE ForumGroupID = @ForumGroupID

		RETURN
	END

	-- Are we marking an individual forum as read?
	IF @ForumID > 0
	BEGIN
		IF @MarkAllThreadsRead = 1
			IF EXISTS (SELECT UserID FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = @ForumID)
				UPDATE 
					tblForumForumsRead 
				SET 
					NewPosts = 0,
					MarkReadAfter = (SELECT (MostRecentPostID + 1) FROM tblForumForums F WHERE ForumID = @ForumID),
					LastActivity = GetDate()
				WHERE
					UserID = @UserID AND
					ForumID = @ForumID
			ELSE
				INSERT INTO 
					tblForumForumsRead
				SELECT ForumGroupID, ForumID, @UserID, (MostRecentPostID + 1), 0, GetDate() FROM tblForumForums F WHERE ForumID = @ForumID
		ELSE
			IF (SELECT NewPosts FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = @ForumID) = 1
				UPDATE
					tblForumForumsRead							
				SET 
					NewPosts = 0
				WHERE
					UserID = @UserID AND
					ForumID = @ForumID

	END
END


