create procedure spForumRemoveModeratedForumForUser
(
	@UserName	nvarchar(50),
	@ForumID	int
)
 AS
	-- remove a row from the Moderators table
	DELETE FROM Moderators
	WHERE UserName = @UserName and ForumID = @ForumID



