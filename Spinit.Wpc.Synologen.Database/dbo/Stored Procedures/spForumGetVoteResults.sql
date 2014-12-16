CREATE  procedure spForumGetVoteResults (
	@PostID int
)
AS
BEGIN
  SELECT
	Vote,
	VoteCount
  FROM
	Vote
  WHERE
	PostID = @PostID
END



