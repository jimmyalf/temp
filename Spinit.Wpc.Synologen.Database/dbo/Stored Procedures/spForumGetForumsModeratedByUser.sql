create procedure spForumGetForumsModeratedByUser
(
	@UserName	nvarchar(50)
)
 AS
	-- determine if this user can moderate ALL forums
	IF EXISTS(SELECT NULL FROM Moderators (nolock) WHERE ForumID = 0 AND Username = @UserName)
		SELECT ForumID, ForumName = 'All Forums', EmailNotification, DateCreated FROM Moderators (nolock)
		WHERE ForumID = 0 AND Username = @UserName
	ELSE
		-- get all of the forums moderated by this particular user
		SELECT
			M.ForumID,
			EmailNotification,
			ForumName = F.Name,
			M.DateCreated
		FROM Moderators M (nolock)
			INNER JOIN Forums F (nolock) ON
				F.ForumID = M.ForumID
		WHERE Username = @UserName



