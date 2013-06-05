create procedure spForumPostAttachment_Add 
(
	@PostID int,
	@UserID int,
	@ForumID int,
	@Filename nvarchar(256),
	@Content image,
	@ContentType nvarchar(50),
	@ContentSize int
)
AS
BEGIN

	IF EXISTS (SELECT PostID FROM tblForumPostAttachments WHERE PostID = @PostID)
		RETURN

	INSERT INTO 
		tblForumPostAttachments
	(
		PostID,
		ForumID,
		UserID,
		[FileName],
		Content,
		ContentType,
		ContentSize
	)
	VALUES
	(
		@PostID,
		@ForumID,
		@UserID,
		@Filename,
		@Content,
		@ContentType,
		@ContentSize
	)

END




