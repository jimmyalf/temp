CREATE procedure spForumIsUserTrackingForum
(
	@ForumID int,
	@UserID int
)
AS
DECLARE @TrackingForum bit

IF EXISTS(SELECT ForumID FROM tblForumTrackedForums (nolock) WHERE ForumID = @ForumID AND UserID=@UserID)
	SELECT IsUserTrackingPost = 1
ELSE
	SELECT IsUserTrackingPost = 0


