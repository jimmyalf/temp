CREATE procedure spForumGetSubscriptionType
(
	@UserID int,
	@ForumID int,
	@SubscriptionType int OUTPUT
)
 AS
select SubscriptionType from tblForumTrackedForums (nolock) where UserID=@UserID AND ForumID=@ForumID



