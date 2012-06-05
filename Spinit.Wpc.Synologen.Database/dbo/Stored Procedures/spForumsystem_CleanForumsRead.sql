create   procedure spForumsystem_CleanForumsRead
(
	@ForumID int
)
AS
BEGIN
	DELETE
		tblForumForumsRead
	WHERE
		MarkReadAfter = 0 AND
		ForumID = @ForumID
END



