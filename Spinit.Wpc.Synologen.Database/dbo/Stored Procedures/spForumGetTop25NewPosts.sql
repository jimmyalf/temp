CREATE  procedure spForumGetTop25NewPosts
(
	@UserName nvarchar(50) = null
)
AS

IF @UserName IS NULL

SELECT TOP 5
		Subject,
		Body,
		P.PostID,
		ThreadID,
		ParentID,
		PostDate = (SELECT Max(PostDate) FROM tblForumPosts WHERE P.ThreadID = ThreadID),
		PostAuthor,
		Replies = (SELECT COUNT(*) FROM tblForumPosts WHERE P.ThreadID = ThreadID AND PostLevel != 1 AND IsApproved = 1),
		Body,
		TotalViews,
		IsLocked,
		HasRead = 0,
		MostRecentPostAuthor = (SELECT TOP 1 PostAuthor FROM tblForumPosts WHERE P.ThreadID = ThreadID AND IsApproved = 1 ORDER BY PostDate DESC),
		MostRecentPostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE P.ThreadID = ThreadID AND IsApproved = 1 ORDER BY PostDate DESC)
	FROM 
		tblForumPosts P 
	WHERE 
		PostLevel = 1 AND 
		IsApproved = 1 AND
		ForumID NOT IN (SELECT ForumID from tblForumForums where EnablePostStatistics = 0)
	ORDER BY 
		PostDate DESC
ELSE	
	
SELECT TOP 5
		Subject,
		Body,
		P.PostID,
		ThreadID,
		ParentID,
		PostDate = (SELECT Max(PostDate) FROM tblForumPosts WHERE P.ThreadID = ThreadID),
		PostAuthor,
		Replies = (SELECT COUNT(*) FROM tblForumPosts WHERE P.ThreadID = ThreadID AND PostLevel != 1 AND IsApproved = 1),
		Body,
		TotalViews,
		IsLocked,
		HasRead = 0,
		MostRecentPostAuthor = (SELECT TOP 1 PostAuthor FROM tblForumPosts WHERE P.ThreadID = ThreadID AND IsApproved = 1 ORDER BY PostDate DESC),
		MostRecentPostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE P.ThreadID = ThreadID AND IsApproved = 1 ORDER BY PostDate DESC)
	FROM 
		tblForumPosts P 
	WHERE 
		PostLevel = 1 AND 
		IsApproved = 1 AND
		ForumID NOT IN (SELECT ForumID from tblForumForums where EnablePostStatistics = 0)
		AND dbo.sfForumGetUserHasViewRightsOnForum(@UserName,ForumID) = 1
	ORDER BY 
		PostDate DESC
/*

	SELECT TOP 25
		Subject,
		Body,
		P.PostID,
		ThreadID,
		ParentID,
		PostDate = (SELECT Max(PostDate) FROM Posts WHERE P.ThreadID = ThreadID),
		ThreadDate,
		UserName,
		Replies = (SELECT COUNT(*) FROM Posts WHERE P.ThreadID = ThreadID AND PostLevel != 1 AND Approved = 1),
		Body,
		TotalViews,
		IsLocked,
		HasRead = 0,
		MostRecentPostAuthor =  (SELECT TOP 1 Username FROM tblForumPosts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC),
		MostRecentPostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC)
	FROM 
		tblForumPosts P 
	WHERE 
		PostLevel = 1 AND 
		Approved = 1 AND
		ForumID NOT IN (SELECT ForumID from PrivateForums)
	ORDER BY 
		ThreadDate DESC*/
/*		
ELSE
	SELECT TOP 5 
		Subject,
		Body,
		P.PostID,
		ThreadID,
		ParentID,
		PostDate = (SELECT Max(PostDate) FROM Posts WHERE P.ThreadID = ThreadID),
		ThreadDate,
		UserName,
		Replies = (SELECT COUNT(*) FROM Posts WHERE P.ThreadID = ThreadID AND PostLevel != 1 AND Approved = 1),
		Body,
		TotalViews,
		IsLocked,
		HasRead = 0,
		MostRecentPostAuthor = '',/* (SELECT TOP 1 Username FROM tblForumPosts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC),*/
		MostRecentPostID = ''/* (SELECT TOP 1 PostID FROM tblForumPosts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC)*/
	FROM 
		tblForumPosts P
	WHERE 
		PostLevel = 1 AND 
		Approved = 1 /*AND
		(ForumID NOT IN (SELECT ForumID from PrivateForums) OR
		ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName)))*/
	ORDER BY 
		ThreadDate DESC
		*/
