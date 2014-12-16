create procedure spForumPostAttachment
(
	@PostID int
)
AS
BEGIN

	SELECT
		*
	FROM
		tblForumPostAttachments
	WHERE
		PostID = @PostID

END


