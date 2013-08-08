create procedure spForumThread_Rate_Get
(
	@ThreadID int
)
AS
BEGIN
	SELECT
		*
	FROM
		tblForumUsers U,
		tblForumUserProfile UP,
		tblForumPostRating R
	WHERE
		R.UserID = U.UserID AND
		R.ThreadID = @ThreadID AND
		U.UserID = UP.UserID 
END



