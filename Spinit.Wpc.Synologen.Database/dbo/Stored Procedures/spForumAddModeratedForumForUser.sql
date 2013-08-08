create procedure spForumAddModeratedForumForUser
(
	@UserName	nvarchar(50),
	@ForumID	int,
	@EmailNotification	bit
)
 AS
	-- add a row to the Moderators table
	-- if the user wants to add All Forums, go ahead and delete all of the other forums
	IF @ForumID = 0
		DELETE FROM Moderators WHERE Username = @UserName
	-- now insert the new row into the table
	INSERT INTO Moderators (Username, ForumID, EmailNotification)
	VALUES (@UserName, @ForumID, @EmailNotification)


