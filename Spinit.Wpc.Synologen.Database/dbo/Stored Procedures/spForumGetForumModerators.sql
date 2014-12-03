CREATE  PROCEDURE spForumGetForumModerators
(
	@ForumID	int
)
 AS
	-- get a list of forum moderators
	SELECT 
		UserName, EmailNotification, DateCreated
	FROM 
		Moderators (nolock)
	WHERE 
		ForumID = @ForumID OR ForumID = 0




