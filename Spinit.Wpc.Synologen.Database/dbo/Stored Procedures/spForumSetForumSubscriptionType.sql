CREATE procedure spForumSetForumSubscriptionType
(
	@UserID int,
	@ForumID int,
	@subType int
)
 AS
if (@subType=0)
	DELETE from tblForumTrackedForums where UserID=@UserID and ForumID=@ForumID
ELSE
IF Exists (select SubscriptionType from tblForumTrackedForums (nolock) where UserID=@UserID AND ForumID=@ForumID)
	UPDATE tblForumTrackedForums Set SubscriptionType=@subType where UserID=@UserID and ForumID=@ForumID
ELSE
	INSERT INTO tblForumTrackedForums (UserID, ForumID, SubscriptionType) values (@UserID, @ForumID, @subType)



