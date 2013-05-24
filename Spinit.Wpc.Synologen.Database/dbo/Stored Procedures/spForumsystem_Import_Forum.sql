CREATE PROCEDURE spForumsystem_Import_Forum
	@ForumID [int],
	@ForumGroupID [int] = 0,
	@ParentID [int] = 0,
	@DateCreated [datetime],
	@IsActive [bit] = 0,
	@IsModerated [bit] = 1,
	@SortOrder [int] = 0,
	@Name [nvarchar](256) = '',
	@Description [nvarchar](3000) = ''
AS
BEGIN

IF EXISTS(SELECT * FROM tblForumForums WHERE ForumID = @ForumID)
	-- Update the forum information
	UPDATE 
		tblForumForums 
	SET
		Name = @Name,
		Description = @Description,
		ParentID = @ParentID,
		DateCreated = @DateCreated,
		ForumGroupID = @ForumGroupID,
		IsModerated = @IsModerated,
		IsActive = @IsActive,
		SortOrder = @SortOrder
	WHERE 
		ForumID = @ForumID
ELSE
BEGIN
	SET IDENTITY_INSERT tblForumForums ON
	INSERT INTO 
		tblForumForums (
			ForumID,
			ForumGroupID, 
			ParentID, 
			DateCreated,
			IsActive,
			IsModerated,
			SortOrder, 
			Name, 
			Description
			)
		VALUES (
			@ForumID,
			@ForumGroupID,
			@ParentID,
			@DateCreated,
			@IsActive,
			@IsModerated,
			@SortOrder,
			@Name,
			@Description
			)
	SET IDENTITY_INSERT tblForumForums OFF

	INSERT INTO 
		tblForumForumPermissions 
		(ForumID, RoleID, [View], [Read], Post, Reply, Edit, [Delete], Sticky, Announce, CreatePoll, Vote, Moderate, Attachment)
	VALUES
		(@ForumID, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0)

END

END


