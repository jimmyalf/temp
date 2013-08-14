CREATE procedure spForumModerate_Forums
	(
		@SiteID	int,
		@UserID int
	)
	AS
	BEGIN
	
		SELECT
			ForumID, ParentID ,ForumGroupID, DateCreated, [Description], [Name], NewsgroupName, IsModerated, DaysToView , IsActive ,SortOrder,
			DisplayMask,TotalPosts, TotalThreads, ForumGroupID, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostID,  MostRecentPostSubject,
			MostRecentThreadID, MostRecentThreadReplies, MostRecentPostDate, EnableAutoDelete, EnablePostStatistics,
			AutoDeleteThreshold, EnableAnonymousPosting,
			PostsToModerate = (SELECT Count(PostID) FROM tblForumPosts P WHERE ForumID = F.ForumID AND P.IsApproved = 0),
			LastUserActivity = '1/1/1797'
		FROM
			tblForumForums F
		WHERE
			F.IsActive = 1 AND
			(SELECT Count(PostID) FROM tblForumPosts P WHERE ForumID = F.ForumID AND P.IsApproved = 0) > 0 AND
			(F.SiteID = 0)

	END


