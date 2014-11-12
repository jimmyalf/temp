CREATE  procedure spForumSearch_Add (
	@WordHash int,
	@Word nvarchar(64),
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
			(WordHash, Word, PostID, ThreadID, ForumID, Weight)
		VALUES
			(@WordHash, @Word, @PostID, @ThreadID, @ForumID, @Weight)

	IF EXISTS (SELECT PostID From tblForumPosts WHERE PostID = @PostID AND IsIndexed = 0)
		UPDATE tblForumPosts SET IsIndexed = 1 WHERE PostID = @PostID

END


