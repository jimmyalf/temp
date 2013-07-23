create procedure spForumGetThread
(
	@ThreadID int
) AS
SELECT
	PostID,
	ForumID,
	Subject,
	ParentID,
	ThreadID,
	PostLevel,
	SortOrder,
	PostDate,
	ThreadDate,
	UserName,
	Approved,
	Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	Body
FROM Posts P (nolock)
WHERE Approved = 1 AND ThreadID = @ThreadID
ORDER BY SortOrder


