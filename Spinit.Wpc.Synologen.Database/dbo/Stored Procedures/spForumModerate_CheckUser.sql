CREATE procedure spForumModerate_CheckUser 
(
	@UserID		int,
	@ForumID	int
)
AS


IF EXISTS(SELECT ForumID FROM tblForumModerators WHERE UserID = @UserID AND ForumID = 0)
  SELECT 1
ELSE
  IF EXISTS (SELECT ForumID FROM tblForumModerators WHERE UserID = @UserID AND ForumID = @ForumID)
    SELECT 1
  ELSE
    SELECT 0

 
