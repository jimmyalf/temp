CREATE    PROCEDURE spForumGetTotalPostsForThread
(
	@PostID	int
)
 AS
	DECLARE @ThreadID int

	-- Make sure we're working with the threadid
	SELECT 
		@ThreadID = ThreadID
	FROM 
		tblForumPosts
	WHERE
		PostID = @PostID

	-- Get the count of posts for a given thread
	SELECT 
		TotalPostsForThread = COUNT(PostID)
	FROM 
		tblForumPosts (nolock)
	WHERE 
		ThreadID = @ThreadID



