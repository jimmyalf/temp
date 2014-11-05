CREATE       PROCEDURE spForumGetModeratedForums
(
	@UserName nvarchar(50)
)
 AS
	-- returns a list of posts awaiting moderation
	-- the posts returned are those that this user can work on
	-- if Moderators.ForumID = 0 for this user, then they can moderate ALL forums
	IF EXISTS(SELECT NULL FROM Moderators (nolock) WHERE UserName=@UserName AND ForumID=0)
		-- return ALL posts awaiting moderation
		SELECT
			ForumID,
			ForumGroupID,
			Name,
			Description,
			DateCreated,
			Moderated,
			DaysToView,
			Active,
			SortOrder
		FROM 
			Forums
		WHERE 	
			Active = 1
		ORDER BY 
			DateCreated
	ELSE
		-- return only those posts in the forum this user can moderate
		SELECT
			ForumID,
			ForumGroupID,
			Name,
			Description,
			DateCreated,
			Moderated,
			DaysToView,
			Active,
			SortOrder

		FROM 
			Forums
		WHERE 
			Active = 1 AND 
			ForumID IN (SELECT ForumID FROM Moderators (nolock) WHERE UserName=@UserName)
		ORDER BY 
			DateCreated



