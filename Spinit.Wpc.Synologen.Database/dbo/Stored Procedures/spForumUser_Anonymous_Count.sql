CREATE  procedure spForumUser_Anonymous_Count
(
	@TimeWindow int,
	@AnonymousUserCount int out
)
AS
BEGIN
DECLARE @StatDate datetime

	-- Clean up the anonymous users table
	DELETE tblForumAnonymousUsers WHERE LastLogin < DateAdd(minute, -@TimeWindow, GetDate())	

	-- Get a count of anonymous users
	SET @AnonymousUserCount = (SELECT count(UserID) FROM tblForumAnonymousUsers)

	-- Do we need to update our forum statistics?
	SET @StatDate = (SELECT MAX(DateCreated) FROM tblForumstatistics_Site)
	IF (SELECT TotalAnonymousUsers FROM tblForumstatistics_Site WHERE DateCreated = @StatDate) < @AnonymousUserCount
		UPDATE
			tblForumstatistics_Site
		SET 
			TotalAnonymousUsers = @AnonymousUserCount
		WHERE
			DateCreated = @StatDate

END



