CREATE   PROCEDURE spForumsystem_ResetThreadStatistics
(
	@ThreadID int
)
AS

DECLARE @PostID int
DECLARE @UserID int
DECLARE @PostDate datetime
DECLARE @PostAuthor varchar(64)
DECLARE @Subject varchar(256)

-- Select the most recent post in the thread.
SELECT TOP 1
	@PostID = PostID,
	@UserID = UserID,
	@PostDate = PostDate,
	@PostAuthor = PostAuthor
FROM
	tblForumPosts
WHERE
	ThreadID = @ThreadID
	AND IsApproved = 1
ORDER BY
	PostID DESC

-- Update the thread.	
UPDATE 
	tblForumThreads
SET
	TotalReplies = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ThreadID = @ThreadID AND IsApproved = 1 AND PostLevel > 1),
	MostRecentPostAuthorID = @UserID,
	MostRecentPostAuthor = @PostAuthor,	
	MostRecentPostID = @PostID
WHERE
	ThreadID = @ThreadID




