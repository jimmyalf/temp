CREATE procedure spForumSearch_PostReindex
(
	@RowCount int = 25
)
AS
BEGIN
SET ROWCOUNT @RowCount

SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	UserName,
	EditNotes = '',
	AttachmentFilename = '',
	Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	IsModerator = 0,
	LastUserActivity = '1/1/1989',
	HasRead = 0 -- not used
FROM 
	tblForumPosts P,
	tblForumUsers U, 
	tblForumThreads T,
	tblForumUserProfile UP,
	tblForumForums F
WHERE 
	F.ForumID = P.ForumID AND 
	P.ThreadID = T.ThreadID AND
	P.UserID = U.UserID AND
        U.UserID = UP.UserID AND
	F.IsActive = 1 AND
	F.IsSearchable = 1 AND 
	P.IsApproved = 1 AND
	P.IsIndexed = 0
ORDER BY
	T.ThreadDate DESC
END



