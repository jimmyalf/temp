CREATE    PROCEDURE spForumGetPostRead
(
	@PostID int,
	@UserName nvarchar (50)
)
 AS
BEGIN
	DECLARE @HasRead bit
	SET @HasRead = 0

	IF EXISTS 
	(
		SELECT
			HasRead
		FROM
			PostsRead
		WHERE
			PostID = @PostID AND
			Username = @UserName
	)
		SET @HasRead = 1

	SELECT HasRead = @HasRead
END


