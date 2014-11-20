CREATE PROCEDURE spForumForum_CreateUpdateDelete
(
	@ForumID	int out,
	@DeleteForum	bit = 0,
	@Name		nvarchar(256) = '',
	@Description	nvarchar(3000) = '',
	@ParentID	int = 0,
	@ForumGroupID	int = 0,
	@IsModerated	bit = 1,
	@DisplayPostsOlderThan	int = 0,
	@IsActive 	bit = 0,
	@EnablePostStatistics bit = 1,
	@EnableAutoDelete bit = 0,
	@EnableAnonymousPosting bit = 0,
	@AutoDeleteThreshold int = 90,
	@SortOrder int = 0
)
AS

-- Are we deleting the forum?
IF @DeleteForum = 1
BEGIN
	-- delete the specified forum and all of its posts
	-- first we must remove all the thread tracking rows
	DELETE 
		tblForumTrackedThreads
	WHERE 
		ThreadID IN (SELECT DISTINCT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID)

	-- we must remove all of the moderators for this forum
	DELETE 
		tblForumModerators
	WHERE 
		ForumID = @ForumID

	-- now we must remove all of the posts
	DELETE 
		tblForumPosts
	WHERE 
		ForumID = @ForumID

	-- finally we can delete the actual forum
	DELETE 
		tblForumForums
	WHERE 
		ForumID = @ForumID

	RETURN
END

-- Are we updating a forum
IF @ForumID > 0
BEGIN
	-- if we are making the forum non-moderated, remove all forum moderators for this forum
	IF @IsModerated = 0
		DELETE 
			tblForumModerators
		WHERE 
			ForumID = @ForumID

	-- Update the forum information
	UPDATE 
		tblForumForums 
	SET
		Name = @Name,
		Description = @Description,
		ParentID = @ParentID,
		ForumGroupID = @ForumGroupID,
		IsModerated = @IsModerated,
		IsActive = @IsActive,
		DaysToView = @DisplayPostsOlderThan,
		EnablePostStatistics = @EnablePostStatistics,
		EnableAutoDelete = @EnableAutoDelete,
		EnableAnonymousPosting = @EnableAnonymousPosting,
		AutoDeleteThreshold = @AutoDeleteThreshold,
		SortOrder = @SortOrder
	WHERE 
		ForumID = @ForumID
END
ELSE
BEGIN

	-- Create a new Forum
	INSERT INTO 
		tblForumForums (
			Name, 
			Description, 
			ParentID, 
			ForumGroupID, 
			IsModerated, 
			DaysToView, 
			IsActive,
			EnablePostStatistics,
			EnableAutoDelete,
			AutoDeleteThreshold
			)
		VALUES (
			@Name,
			@Description,
			@ParentID,
			@ForumGroupID,
			@IsModerated,
			@DisplayPostsOlderThan,
			@IsActive,
			@EnablePostStatistics,
			@EnableAutoDelete,
			@AutoDeleteThreshold
			)
	
	SET @ForumID = @@Identity

END


