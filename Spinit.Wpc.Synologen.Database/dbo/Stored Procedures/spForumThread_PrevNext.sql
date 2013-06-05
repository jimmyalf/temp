CREATE procedure spForumThread_PrevNext
(
	@ThreadID int,
	@ForumID int,
	@NextThreadID int OUTPUT,
	@PrevThreadID int OUTPUT
)
AS
DECLARE @ThreadDate datetime

SELECT 
	@ThreadDate = ThreadDate 
FROM 
	tblForumThreads
WHERE
	ThreadID = @ThreadID


SELECT TOP 1 
	@PrevThreadID = ThreadID 
FROM 
	tblForumThreads 
WHERE 
	ForumID = @ForumID 
	AND ThreadDate < @ThreadDate 
ORDER BY 
	IsSticky DESC, 
	ThreadDate DESC

IF @@ROWCOUNT < 1
	SELECT @PrevThreadID = 0


SELECT TOP 1 
	@NextThreadID = ThreadID 
FROM 
	tblForumThreads 
WHERE 
	ForumID = @ForumID 
	AND ThreadDate > @ThreadDate 
ORDER BY 
	IsSticky DESC, 
	ThreadDate DESC

IF @@ROWCOUNT < 1
	SELECT @NextThreadID = 0


