CREATE              PROCEDURE spForumSite_Statistics 
(
	@UpdateWindow int = 3
)
AS

-- Do we need to update the statistics?
DECLARE @LastUpdate datetime
DECLARE @DateWindow datetime

SET @LastUpdate = ISNULL((SELECT MAX(DateCreated) FROM tblForumstatistics_Site), '1/1/1797')
SET @DateWindow = DATEADD(hh, -@UpdateWindow, GetDate())

if (@LastUpdate <  @DateWindow)
	BEGIN
		exec spForumsystem_UpdateSite
	END

-- SELECT current statistics
SELECT 
	S.*,
	CurrentAnonymousUsers = (SELECT Count(*) FROM tblForumAnonymousUsers),
	MostReadSubject = (SELECT Subject FROM tblForumPosts WHERE PostID = S.MostReadPostID),
	MostViewsSubject = (SELECT Subject FROM tblForumPosts WHERE PostID = S.MostViewsPostID),
	MostActiveSubject = (SELECT Subject FROM tblForumPosts WHERE PostID = S.MostActivePostID),
	MostActiveUser = (SELECT UserName FROM tblForumUsers WHERE UserID = S.MostActiveUserID),
	NewestUser = (SELECT UserName FROM tblForumUsers WHERE UserID = S.NewestUserID)
FROM
	tblForumstatistics_Site S
WHERE
	DateCreated = @LastUpdate

-- SELECT TOP 100 Users
SELECT TOP 100
	U.UserID,
	U.UserName,
	U.PasswordFormat,
	U.Email,
	U.DateCreated,
	U.LastLogin,
	U.LastActivity,
	U.LastAction,
	U.UserAccountStatus,
	U.IsAnonymous,
	S.TotalPosts,
	P.*
FROM
	tblForumstatistics_User S,
	tblForumUsers U,
	tblForumUserProfile P
WHERE
	S.UserID = U.UserID AND
	U.UserID = P.UserID AND
	S.TotalPosts > 0 AND
	P.EnableDisplayInMemberList = 1 AND
	U.UserID > 0
ORDER BY
	S.TotalPosts DESC

-- SELECT top 50 Moderators
SELECT TOP 50
	U.UserID,
	U.UserName,
	U.PasswordFormat,
	U.Email,
	U.DateCreated,
	U.LastLogin,
	U.LastActivity,
	U.LastAction,
	U.UserAccountStatus,
	P.*,
	M.PostsModerated
FROM
	tblForumUsers U,
	tblForumUserProfile P,
	tblForumModerators M
WHERE
	M.UserID = U.UserID AND
	U.UserID = P.UserID AND
	M.PostsModerated > 0
ORDER BY
	PostsModerated DESC

-- SELECT Moderator actions
SELECT
	Description,
	TotalActions
FROM
	tblForumModerationAction
ORDER BY 
	TotalActions DESC 



