CREATE  procedure spForumUpdateForumLastCrawlDate (
	@ForumID int,
	@LastCrawled datetime,
	@DeepCrawl bit
)
AS
BEGIN
	IF EXISTS (SELECT ForumID FROM tblForumSearchLastCrawled WHERE ForumID = @ForumID)

		-- Are we doing a deep crawl?
		IF @DeepCrawl = 1
			UPDATE 
				tblForumSearchLastCrawled
			SET 
				LastDeepCrawl = @LastCrawled
			WHERE
				ForumID = @ForumID
		ELSE
			UPDATE 
				tblForumSearchLastCrawled
			SET 
				LastShallowCrawl = @LastCrawled
			WHERE
				ForumID = @ForumID
			
	ELSE
		INSERT INTO
			tblForumSearchLastCrawled
		(
			ForumID,
			LastDeepCrawl,
			LastShallowCrawl
		)
		VALUES
		(
			@ForumID,
			@LastCrawled,
			@LastCrawled
		)

END

 
