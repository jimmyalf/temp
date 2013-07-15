create procedure spForumsystem_DuplicatePost
(
@UserID int,
@Body ntext,
@IntervalInMinutes int = 0,
@IsDuplicate bit out
)
AS

	IF @IntervalInMinutes > 0
		-- Check for duplicates
		IF EXISTS (SELECT TOP 1 PostID FROM tblForumPosts (nolock) WHERE UserID = @UserID AND Body LIKE @Body AND PostDate > DateAdd(minute, -@IntervalInMinutes, GetDate()) )
			SET @IsDuplicate = 1
		ELSE
			SET @IsDuplicate = 0
        ELSE
		-- Check for duplicates
		IF EXISTS (SELECT TOP 1 PostID FROM tblForumPosts (nolock) WHERE UserID = @UserID AND Body LIKE @Body)
			SET @IsDuplicate = 1
		ELSE
			SET @IsDuplicate = 0


