CREATE   procedure spForumsystem_UpdateMostActiveUsers
AS
BEGIN
	SET NOCOUNT ON

	DELETE tblForumstatistics_User
	
	INSERT INTO tblForumstatistics_User
	SELECT TOP 100
		U.UserID,
		TotalPosts = ISNULL( (SELECT count(PostID) FROM tblForumPosts WHERE UserID = P.UserID AND PostDate > DateAdd(day, -1, GetDate())), 0)
	FROM
		tblForumUserProfile P,
		tblForumUsers U
	WHERE
		U.UserID = P.UserID AND
		U.IsAnonymous = 0 AND
		U.UserAccountStatus = 1
	ORDER BY
		TotalPosts DESC

	SET NOCOUNT OFF
END


