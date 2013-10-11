CREATE  procedure spForumForum_Permission_CreateUpdateDelete
(
	@ForumID 	int,
	@RoleID		int,
	@Action		int,
	@View		tinyint = 0,
	@Read		tinyint = 0,
	@Post		tinyint = 0,
	@Reply		tinyint = 0,	
	@Edit		tinyint = 0,
	@Delete		tinyint = 0,
	@Sticky		tinyint = 0,
	@Announce	tinyint = 0,
	@CreatePoll	tinyint = 0,
	@Vote		tinyint = 0,
	@Moderate       tinyint = 0,
	@Attachment     tinyint = 0
)
AS
BEGIN

-- Create
IF @Action = 0
BEGIN

	-- Check if an entry already exists
	IF EXISTS (SELECT ForumID FROM tblForumForumPermissions WHERE ForumID = @ForumID AND RoleID = @RoleID)
		exec spForumForum_Permission_CreateUpdateDelete @ForumID, @RoleID, 1, @View, @Read, @Post, @Reply, @Edit, @Delete, @Sticky, @Announce, @CreatePoll, @Vote, @Moderate
	ELSE
		INSERT INTO 
			tblForumForumPermissions 
		VALUES	(
				@ForumID,
				@RoleID,
				@View,
				@Read,
				@Post,
				@Reply,
				@Edit,
				@Delete,
				@Sticky,
				@Announce,
				@CreatePoll,
				@Vote,
				@Moderate,
				@Attachment
			)
END
-- UPDATE
ELSE IF @Action = 1
BEGIN

	IF NOT EXISTS (SELECT ForumID FROM tblForumForumPermissions WHERE ForumID = @ForumID AND RoleID = @RoleID)
		exec spForumForum_Permission_CreateUpdateDelete @ForumID, @RoleID, 0, @View, @Read, @Post, @Reply, @Edit, @Delete, @Sticky, @Announce, @CreatePoll, @Vote, @Moderate
	ELSE
		UPDATE
			tblForumForumPermissions
		SET
			[View] = 	@View,
			[Read] =	@Read,
			Post =		@Post,
			Reply =		@Reply,
			Edit =		@Edit,
			[Delete] =	@Delete,
			Sticky = 	@Sticky,
			Announce = 	@Announce,
			CreatePoll = 	@CreatePoll,
			Vote =		@Vote,
			Moderate =      @Moderate,
			Attachment =    @Attachment
		WHERE
			ForumID = @ForumID AND
			RoleID = @RoleID

END
ELSE IF @Action = 2
BEGIN
	DELETE tblForumForumPermissions WHERE ForumID = @ForumID AND RoleID = @RoleID
END

END


