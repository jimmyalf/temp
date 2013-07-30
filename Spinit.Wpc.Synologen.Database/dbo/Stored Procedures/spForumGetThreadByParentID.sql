CREATE    PROCEDURE spForumGetThreadByParentID
(
	@ParentID	int
)
 AS
BEGIN
	SELECT 
		PostID,
		ThreadID,
		ForumID,
		Subject,
		ParentID,
		PostLevel,
		SortOrder,
		PostDate,
		ThreadDate,
		UserName,
		Approved,
		Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
		Body,
		TotalMessagesInThread = 0, -- not used
		TotalViews,
		IsLocked
	FROM
		Posts P
	WHERE
		Approved = 1 AND
		ParentID = @ParentID
END


