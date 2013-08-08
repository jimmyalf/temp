CREATE      procedure spForumsystem_ResetForumStatistics
(
    @ForumID int = 0
)
AS
DECLARE @AutoDelete bit
DECLARE @AutoDeleteThreshold int
DECLARE @ForumCount int
DECLARE @ThreadID int
DECLARE @PostID int
SET @ForumCount = 1

IF @ForumID = 0
    -- Reset the statistics on all of the forums.
    WHILE @ForumCount < (SELECT Max(ForumID) FROM tblForumForums)
    BEGIN
        IF EXISTS(SELECT ForumID FROM tblForumForums WHERE ForumID = @ForumCount)
            EXEC spForumsystem_ResetForumStatistics @ForumCount
            
        SET @ForumCount = @ForumCount + 1
    END
ELSE
    BEGIN
    
	-- Finally, perform any auto-delete
	SELECT 
		@AutoDelete = EnableAutoDelete, 
		@AutoDeleteThreshold = AutoDeleteThreshold 
	FROM 
		tblForumForums
	WHERE
		ForumID = @ForumID

	-- Do we need to cleanup the forum?
	IF @AutoDelete = 1
	BEGIN
		DELETE tblForumThreads WHERE ThreadDate < DateAdd(dd, -@AutoDeleteThreshold, GetDate()) AND LastViewedDate < DateAdd(dd, -@AutoDeleteThreshold, GetDate()) AND StickyDate < DateAdd(dd, -@AutoDeleteThreshold, GetDate()) AND ForumID = @ForumID
		exec sp_recompile 'tblForumThreads'
		exec sp_recompile 'tblForumPosts'
		exec sp_recompile 'tblForumSearchBarrel'
	END

        -- Select the most recent post from the forum.
        SELECT TOP 1
            @ThreadID = ThreadID,
            @PostID = PostID
        FROM 
            tblForumPosts
        WHERE 
            ForumID = @ForumID AND
            IsApproved = 1
        ORDER BY
            PostID DESC
   
        -- If the thread is null, reset the forum statistics.
        IF @ThreadID IS NULL
            UPDATE
                tblForumForums
            SET
                TotalThreads = 0,
                TotalPosts = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID = @ForumID),
                MostRecentPostID = 0,
                MostRecentThreadID = 0,
                MostRecentPostDate = '1/01/1797',
                MostRecentPostAuthorID = 0,
                MostRecentPostSubject = '',
                MostRecentPostAuthor = ''
            WHERE
                ForumID = @ForumID
            
        ELSE
            EXEC spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID

    END

