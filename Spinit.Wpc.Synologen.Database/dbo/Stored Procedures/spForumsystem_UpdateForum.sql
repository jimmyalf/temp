CREATE        procedure spForumsystem_UpdateForum
(
	@ForumID int,
	@ThreadID int,
	@PostID int
)
AS
BEGIN
DECLARE @UserID 		int
DECLARE @PostDate 		datetime
DECLARE @TotalThreads 		int
DECLARE @TotalPostsApproved	int
DECLARE @TotalPosts 		int
DECLARE @Subject		nvarchar(64)
DECLARE @User 			nvarchar(64)

-- Get values necessary to update the forum statistics
SELECT
	@UserID = U.UserID,
	@PostDate = PostDate,
	@TotalPostsApproved = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ForumID = P.ForumID AND P2.IsApproved=1),
	@TotalPosts = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID = P.ForumID),
	@TotalThreads = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ForumID = P.ForumID AND P2.IsApproved=1 AND P2.PostLevel=1),
        @Subject = P.Subject,
	@User = PostAuthor
FROM
	tblForumPosts P
	JOIN tblForumUsers U ON U.UserID = P.UserID
WHERE
	PostID = @PostID

-- Do the update within a transaction
BEGIN TRAN

	UPDATE 
		tblForumForums
	SET
		TotalPosts = @TotalPosts,
		TotalThreads = @TotalThreads,
		MostRecentPostID = @PostID,
		MostRecentThreadID = @ThreadID,
		MostRecentPostDate = @PostDate,
		MostRecentPostAuthorID = @UserID,
                MostRecentPostSubject = @Subject,
		MostRecentPostAuthor = @User,
		MostRecentThreadReplies = ISNULL((SELECT TotalReplies FROM tblForumThreads WHERE ThreadID = @ThreadID), 0)
	WHERE
		ForumID = @ForumID

COMMIT TRAN

END

