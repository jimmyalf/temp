CREATE PROCEDURE spForumsystem_UpdateSite 
(
	@UpdateUserPostRank bit = 1,
	@UpdateMostActiveUserList bit = 1
)
AS
	-- Get summary information - Total Users, Total Posts, TotalTopics, DaysPosts, and DaysTopics
	DECLARE @LastDateTimeUpdate datetime
	DECLARE @TotalUsers int
	DECLARE @TotalPosts int
	DECLARE @TotalTopics int
	DECLARE @TotalModerators int
	DECLARE @TotalModeratedPosts int
	DECLARE @NewThreadsInPast24Hours int
	DECLARE @NewPostsInPast24Hours int
	DECLARE @NewUsersInPast24Hours int
	DECLARE @MostViewsPostID int
	DECLARE @MostActivePostID int
	DECLARE @MostReadPostID int
	DECLARE @TotalAnonymousUsers int
	DECLARE @NewestUserID int
	DECLARE @MostActiveUserID int

	SET NOCOUNT ON

	SET @LastDateTimeUpdate = ISNULL( 
					(
						SELECT TOP 1
							DateCreated 
						FROM 
							tblForumstatistics_Site
					), '1/1/1979 12:00:00')

	-- Reset top posters
	IF @UpdateUserPostRank = 1
		exec spForumsystem_UpdateUserPostRank

	IF @UpdateMostActiveUserList = 1
		exec spForumsystem_UpdateMostActiveUsers

	-- Total Anonymous Users
	-- ***********************************************
	SET @TotalAnonymousUsers = ISNULL( 
					(
						SELECT 
							COUNT(UserID) 
						FROM 
							tblForumAnonymousUsers
					), 0 )

	-- Total Moderators, for this site only
	-- ***********************************************
	SET @TotalModerators = ISNULL(
					(
						SELECT 
							COUNT(jUR.UserID) 
						FROM 
							tblForumUsersInRoles jUR
							JOIN tblForumRoles jR ON jR.RoleID = jUR.RoleID
						WHERE 
							jUR.RoleID = 4
					), 0)

	-- Total Moderated Posts
	-- ***********************************************
	SET @TotalModeratedPosts = ISNULL( 
					(
						SELECT 
							COUNT(ModerationAction) 
						FROM 
							tblForumModerationAudit 
						WHERE 
							ModeratedOn >= @LastDateTimeUpdate
					), 0 )

	-- Most "Viewed" thread, by grabbing the first post
	-- ***********************************************
	SET @MostViewsPostID = ISNULL(
					(
						SELECT TOP 1 
							jP1.PostID
						FROM 
							tblForumThreads jT
							JOIN tblForumPosts jP1 ON jP1.ThreadID = jT.ThreadID
							JOIN tblForumForumPermissions jP ON jP.ForumID = jT.ForumID
						WHERE 
							jP.RoleID = 0 AND
							jP.[View] = 0x01 AND
							jT.ThreadDate > DateAdd(d, -3, GetDate()) AND
							jP1.IsApproved = 1 AND
							jT.ForumID > 4		-- excluding PM and hidden forums
						ORDER BY 
							jT.TotalViews DESC
					), 0)


	-- Most "Active" Thread, by grabbing the first post
	-- ***********************************************
	SET @MostActivePostID = ISNULL(
					(
						SELECT TOP 1 
							jP1.PostID
						FROM 
							tblForumThreads jT
							JOIN tblForumPosts jP1 ON jP1.ThreadID = jT.ThreadID
							JOIN tblForumForumPermissions jP ON jP.ForumID = jT.ForumID
						WHERE 
							jP.RoleID = 0 AND
							jP.[View] = 0x01 AND
							jT.ThreadDate > DateAdd(d, -3, GetDate()) AND
							jP1.IsApproved = 1 AND
							jT.ForumID > 4		-- excluding PM and hidden forums
						ORDER BY 
							jT.TotalReplies DESC
					), 0)

	-- Most "Read" thread, by grabbing the first post
	-- ***********************************************
	SET @MostReadPostID = ISNULL(
					(
						SELECT TOP 1 
							jP1.PostID
						FROM 
							tblForumThreads jT
							JOIN tblForumPosts jP1 ON jP1.ThreadID = jT.ThreadID
							JOIN tblForumForumPermissions jP ON jP.ForumID = jT.ForumID
						WHERE 
							jP.RoleID = 0 AND
							jP.[View] = 0x01 AND
							jT.ThreadDate > DateAdd(d, -3, GetDate()) AND
							jP1.IsApproved = 1 AND
							jT.ForumID > 4		-- excluding PM and hidden forums
						ORDER BY 
							( SELECT count(jTR.ThreadID) FROM tblForumThreadsRead jTR WHERE jP1.ThreadID = jTR.ThreadID ) DESC
					), 0)


	-- Most active user
	-- ***********************************************
	SET @MostActiveUserID = ISNULL(
					(
						SELECT TOP 1 
							jU.UserID

						FROM 
							tblForumUsers jU
							JOIN tblForumUserProfile jP ON jP.UserID = jU.UserID
						WHERE
							jP.EnableDisplayInMemberList = 1
						ORDER BY 
							jP.TotalPosts DESC
					), 0)
	-- Newest user
	-- ***********************************************
	SET @NewestUserID = ISNULL(
					(
						SELECT TOP 1 
							 jU.UserID
						FROM 
							tblForumUsers jU
							JOIN tblForumUserProfile jP ON jP.UserID = jU.UserID
						WHERE
							jP.EnableDisplayInMemberList = 1 AND
							jU.UserAccountStatus = 1
						ORDER BY 
							jU.DateCreated DESC
					), 0)


	-- Total Users
	-- ***********************************************
	SET @TotalUsers = ISNULL( 
					(
						SELECT 
							COUNT(UserID) 
						FROM 
							tblForumUserProfile 
						WHERE 
							EnableDisplayInMemberList = 1
					) ,0) 


	-- Total Posts
	-- ***********************************************
	SET @TotalPosts = 	ISNULL( 
					(
						SELECT TOP 1 
							TotalPosts 
						FROM 
							tblForumstatistics_Site
					), 0) +
				 ISNULL( 
					(
						SELECT 
							COUNT(PostID) 
						FROM 
							tblForumPosts 
						WHERE 
							ForumID > 4 AND 
						PostDate >= @LastDateTimeUpdate
					), 0)
	IF @TotalPosts = 0
	BEGIN
		-- there was no previous count.  this is mainly for clean installs
		SET @TotalPosts = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID > 4)
	END


	-- Total Topics
	-- ***********************************************
	SET @TotalTopics = 	ISNULL( 
					(
						SELECT TOP 1 
							TotalTopics 
						FROM 
							tblForumstatistics_Site
					), 0) + 
				ISNULL( 
					(
						SELECT 
							COUNT(ThreadID) 
						FROM 
							tblForumThreads 
						WHERE 
							ForumID > 4 AND 
							ThreadDate >= @LastDateTimeUpdate
					), 0)
	IF @TotalTopics = 0
	BEGIN
		-- there was no previous count.  this is mainly for clean installs
		SET @TotalTopics = (SELECT COUNT(ThreadID) FROM tblForumThreads WHERE ForumID > 4)
	END

	-- Total Posts in past 24 hours
	-- ***********************************************
	SET @NewPostsInPast24Hours = ISNULL( 
					(SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID > 4 And PostDate > DATEADD(dd,-1,getdate())
					), 0)

	-- Total Users in past 24 hours
	-- ***********************************************
	SET @NewUsersInPast24Hours = ISNULL(
						(SELECT COUNT(UserID) FROM tblForumUsers WHERE UserID > 0 And DateCreated > DATEADD(dd,-1,getdate())
					), 0)


	-- Total Topics in past 24 hours
	-- ***********************************************
	SET @NewThreadsInPast24Hours = ISNULL(
						(SELECT COUNT(ThreadID) FROM tblForumThreads WHERE ForumID > 4 AND PostDate > DATEADD(dd,-1,getdate())
					), 0)

	INSERT INTO tblForumstatistics_Site
	SELECT 
		DateCreated = GetDate(),
		TotalUsers = @TotalUsers,
		TotalPosts = @TotalPosts,
		TotalModerators = @TotalModerators,
		TotalModeratedPosts = @TotalModeratedPosts,
		TotalAnonymousUsers = @TotalAnonymousUsers,
		TotalTopics = @TotalTopics,
		DaysPosts = @NewPostsInPast24Hours, -- TODO remove
		DaysTopics = @NewThreadsInPast24Hours, -- TODO remove
		NewPostsInPast24Hours = @NewPostsInPast24Hours,
		NewThreadsInPast24Hours = @NewThreadsInPast24Hours,
		NewUsersInPast24Hours = @NewUsersInPast24Hours,
		MostViewsPostID = @MostViewsPostID,
		MostActivePostID = @MostActivePostID,
		MostActiveUserID = @MostActiveUserID,
		MostReadPostID = @MostReadPostID,
		NewestUserID = @NewestUserID


	SET NOCOUNT OFF


