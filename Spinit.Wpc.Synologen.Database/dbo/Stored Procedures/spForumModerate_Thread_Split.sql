CREATE       PROCEDURE spForumModerate_Thread_Split
(
	@PostID INT,
	@MoveToForum INT,
	@SplitBy INT
)
AS
DECLARE @IsSticky BIT
DECLARE @StickyDate DATETIME
DECLARE @IsLocked BIT
DECLARE @NewThreadID INT
DECLARE @OldThreadID INT
DECLARE @UserID INT
DECLARE @PostAuthor NVARCHAR(64)
DECLARE @PostDate DATETIME
DECLARE @EmoticonID INT
DECLARE @TopPostLevel INT
DECLARE @TopSortOrder INT
DECLARE @TotalReplies INT
DECLARE	@MostRecentPostAuthor NVARCHAR(64)
DECLARE	@MostRecentPostAuthorID INT
DECLARE	@MostRecentPostID INT

-- Get details on the post
SELECT 
	@PostDate = PostDate,
	@UserID = UserID,
	@PostAuthor = PostAuthor,
	@IsSticky = 0,			-- shouldn't be a stickie when splitting
	@IsLocked = IsLocked,
	@StickyDate = GetDate(),
	@EmoticonID = EmoticonID,
	@OldThreadID = ThreadID		-- to delete later if no more replies
FROM 
	tblForumPosts 
WHERE 
	PostID = @PostID

BEGIN TRAN

-- Create a new thread by inserting
INSERT tblForumThreads 	
	( ForumID,
	PostDate, 
	UserID, 
	PostAuthor, 
	ThreadDate, 
	MostRecentPostAuthor, 
	MostRecentPostAuthorID, 	
	MostRecentPostID, 
	IsLocked, 
	IsApproved,
	IsSticky, 
	StickyDate, 
	ThreadEmoticonID )
VALUES
	( @MoveToForum, 	-- the forum we are moving to
	@PostDate, 
	@UserID, 
	@PostAuthor,
	@PostDate,
	@PostAuthor,	-- Dummy data until we move all posts below
	@UserID, 	-- Dummy data until we move all posts below
	0,		-- MostRecentPostID, which we don't know yet.
	@IsLocked,
	1,		-- Wouldn't be shown in the forum unless it wasn't approved already.
	@IsSticky,
	@StickyDate,
	@EmoticonID )

SELECT 
	@NewThreadID = @@IDENTITY
FROM
	tblForumThreads

-- Update the post and it's childred (if any) with the new threadid
UPDATE 
	tblForumPosts 
SET 
	ThreadID = @NewThreadID,
	ForumID = @MoveToForum,
	ParentID = @PostID	-- the toplevel post should now reference itself.
--	PostDate = GetDate()	-- We're not going to reset the DATETIME for the posts
WHERE
	ThreadID = @OldThreadID
	AND (PostID = @PostID OR ParentID = @PostID)

-- this is now controlled in the spForumsystem_UpdateThread sproc
-- Fix the PostLevel and SortOrder details of the new thread
--SELECT 
--	@TopPostLevel = PostLevel,
--	@TopSortOrder = SortOrder
--FROM 
--	spForumPosts 
--WHERE 
--	PostID = @PostID
--
--UPDATE 
--	spForumPosts 
--SET 
---	PostLevel = (PostLevel - @TopPostLevel) + 1,
--	SortOrder = (SortOrder - @TopSortOrder) + 1
--WHERE
--	ThreadID = @NewThreadID

-- Update the threads...
EXEC spForumsystem_UpdateThread @NewThreadID, 0
EXEC spForumsystem_UpdateThread @OldThreadID, 0

-- Update forum statistics
EXEC spForumsystem_UpdateForum @MoveToForum, @NewThreadID, @PostID

-- #7. Update moderation actions
EXEC spForumsystem_ModerationAction_AuditEntry 8, @SplitBy, @PostID

COMMIT TRAN



