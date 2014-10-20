CREATE procedure spForumIsUserTrackingPost
(
	@ThreadID int,
	@UserName nvarchar(50)
)
AS
DECLARE @TrackingThread bit

IF EXISTS(SELECT ThreadID FROM ThreadTrackings (nolock) WHERE ThreadID = @ThreadID AND UserName=@UserName)
	SELECT IsUserTrackingPost = 1
ELSE
	SELECT IsUserTrackingPost = 0


