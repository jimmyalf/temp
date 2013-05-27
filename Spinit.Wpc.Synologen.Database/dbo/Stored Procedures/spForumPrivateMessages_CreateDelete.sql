CREATE  procedure spForumPrivateMessages_CreateDelete
(
	@UserID int,
	@ThreadID int,
	@Action int
)
AS
BEGIN

IF @Action = 0
BEGIN
	-- Does the user already have the ability to see this thread?
	IF EXISTS (SELECT UserID FROM tblForumPrivateMessages WHERE UserID = @UserID and ThreadID = @ThreadID)
		return

	INSERT INTO
		tblForumPrivateMessages
	VALUES
		(
			@UserID,
			@ThreadID
		)

	RETURN
END

IF @Action = 2
BEGIN
	DELETE
		tblForumPrivateMessages
	WHERE
		UserID = @UserID AND
		ThreadID = @ThreadID
	RETURN
END

END


