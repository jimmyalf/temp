CREATE    PROCEDURE spForumGetForumTrackingEmailsForThread
(
	@ForumID	int,
	@SubscriptionType	int
)
AS
	-- now, get all of the emails of the users who are tracking this thread
	SELECT
		Email
	FROM 
		Users U (nolock),
		ForumTrackings F
	WHERE
		U.Username = F.Username AND
		F.ForumID = @ForumID AND
		F.subType = @SubscriptionType


