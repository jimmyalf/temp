CREATE  PROCEDURE spForumGetForumsNotModeratedByUser
(
	@UserName	nvarchar(50)
)
 AS
	-- determine if this user can moderate ALL forums
	IF NOT EXISTS(SELECT NULL FROM Moderators (nolock) WHERE ForumID = 0 AND Username = @UserName)
		-- get all of the forums NOT moderated by this particular user
		SELECT ForumID =  0, ForumName =  'All Forums'
		UNION
		SELECT
			ForumID,
			ForumName = F.Name
		FROM Forums F (nolock) 
		WHERE ForumID NOT IN (SELECT ForumID FROM Moderators (nolock) WHERE Username = @UserName)



