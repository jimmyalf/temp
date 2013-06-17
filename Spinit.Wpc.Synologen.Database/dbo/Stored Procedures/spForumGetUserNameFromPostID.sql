create procedure spForumGetUserNameFromPostID
(
	@PostID	int
)
 AS
	-- returns who posted a particular post
	SELECT UserName
	FROM Posts (nolock)
	WHERE PostID = @PostID




