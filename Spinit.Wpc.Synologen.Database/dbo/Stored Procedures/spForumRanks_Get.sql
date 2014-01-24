create procedure spForumRanks_Get
(
	@UserID					int = 0
)
AS
	SELECT r.*
	FROM
		tblForumRanks r


