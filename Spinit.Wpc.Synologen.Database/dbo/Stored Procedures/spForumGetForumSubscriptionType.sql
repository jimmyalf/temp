CREATE  procedure spForumGetForumSubscriptionType
(
	@UserID int,
	@ForumID int,
	@SubType int OUTPUT
)
AS
SELECT SubscriptionType FROM tblForumTrackedForums WHERE ForumID=@ForumID AND UserID=@UserID


