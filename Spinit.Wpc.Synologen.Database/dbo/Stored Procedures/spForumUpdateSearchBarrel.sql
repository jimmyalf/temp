CREATE   procedure spForumUpdateSearchBarrel (
	@WordHash int,
	@Weight float,
	@PostID int,
	@ThreadID int,
	@ForumID int
)
AS
BEGIN
	IF EXISTS (SELECT WordHash FROM tblForumSearchBarrel WHERE PostID = @PostID AND WordHash = @WordHash)
		UPDATE 
			tblForumSearchBarrel 
		SET
			Weight = @Weight
		WHERE
			WordHash = @WordHash AND
			PostID = @PostID
	ELSE
		INSERT INTO
			tblForumSearchBarrel
			(WordHash, PostID, ThreadID, ForumID, Weight)
		VALUES
			(@WordHash, @PostID, @ThreadID, @ForumID, @Weight)
END


