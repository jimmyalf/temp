CREATE    PROCEDURE spForumModerate_Thread_Merge
(
	@ParentThreadID int,
	@ChildThreadID int,
	@JoinBy int
)
AS
DECLARE @ParentForumID int
DECLARE @LastPostInParent int
DECLARE @PostLevelInParent int
DECLARE @SortOrderInParent int
DECLARE @ChildForumID int
DECLARE @FirstPostInChild int
DECLARE @PostLevelInChild int
DECLARE @SortOrderInChild int
DECLARE @LastPostInChild int

-- Check to ensure we can perform this opertation
IF ((SELECT ThreadID FROM tblForumThreads WHERE ThreadID = @ChildThreadID) = @ParentThreadID)
	RETURN

-- Get details on the parent thread
SELECT TOP 1
	@ParentForumID = ForumID,
	@LastPostInParent = PostID,
	@PostLevelInParent = PostLevel,
	@SortOrderInParent = SortOrder
FROM
	tblForumPosts
WHERE
	ThreadID = @ParentThreadID
ORDER BY
	SortOrder DESC

-- Get details on the child thread
SELECT TOP 1
	@ChildForumID = ForumID,
	@FirstPostInChild = PostID,
	@PostLevelInChild = PostLevel,
	@SortOrderInChild = SortOrder
FROM
	tblForumPosts
WHERE
	ThreadID = @ChildThreadID

-- don't know why this is here
-- Get the last post in the child thread
--SELECT 
--	@LastPostInChild = MostRecentPostID
--FROM
--	tblForumThreads
--WHERE
--	ThreadID = @ChildThreadID

BEGIN TRAN

-- this is now done in the spForumsystem_UpdateThread sproc
-- Update the PostLevel and SortOrder for the Child posts before merging
--UPDATE 
--	tblForumPosts
--SET
--	PostLevel = PostLevel + @PostLevelInParent
--WHERE
--	ThreadID = @ChildThreadID
--
--UPDATE 
--	tblForumPosts
--SET
--	SortOrder = SortOrder + @SortOrderInParent
--WHERE
--	ThreadID = @ChildThreadID


-- Do the Updates
UPDATE
	tblForumPosts
SET
	ThreadID = @ParentThreadID,
	ForumID = @ParentForumID,
	PostLevel = PostLevel + @PostLevelInParent,
	SortOrder = SortOrder + @SortOrderInParent,
	ParentID = @LastPostInParent
WHERE
	ThreadID = @ChildThreadID

-- Now delete all of the old thread info
DELETE FROM 
	tblForumSearchBarrel
WHERE 
	ThreadID = @ChildThreadID

-- Delete all thread tracking data.	
DELETE FROM 
	tblForumTrackedThreads
WHERE 
	ThreadID = @ChildThreadID

-- Cleanup ThreadsRead
DELETE
	tblForumThreadsRead
WHERE
	ThreadID = @ChildThreadID 

-- Delete the child thread
DELETE 
	tblForumThreads
WHERE
	ThreadID = @ChildThreadID

-- Update thread statistics
EXEC spForumsystem_UpdateThread @ParentThreadID, 0

-- Update forum statistics
EXEC spForumsystem_UpdateForum @ParentForumID, @ParentThreadID, @ParentThreadID
EXEC spForumsystem_ResetForumStatistics @ChildForumID

-- Update moderation actions
EXEC spForumsystem_ModerationAction_AuditEntry 7, @JoinBy, @ChildThreadID

COMMIT TRAN



 
