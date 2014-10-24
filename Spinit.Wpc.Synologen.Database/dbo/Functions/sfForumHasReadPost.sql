
CREATE   function sfForumHasReadPost (
	@UserID int, 
	@ThreadID int, 
	@ForumID int
)
RETURNS bit
AS
BEGIN
DECLARE @HasRead bit
DECLARE @ReadAfter int

SET @HasRead = 0

	-- Do we have topics marked as read?
	SELECT 
		@ReadAfter = MarkReadAfter 
	FROM 
		tblForumForumsRead 
	WHERE 
		UserID = @UserID AND 
		ForumID = @ForumID

	IF @ReadAfter IS NOT NULL
	BEGIN
		IF @ReadAfter > @ThreadID
			RETURN 1
	END
	
	IF EXISTS (SELECT ThreadID FROM tblForumThreadsRead WHERE UserID = @UserID AND ThreadID = @ThreadID)
	  SET @HasRead = 1

RETURN @HasRead
END

