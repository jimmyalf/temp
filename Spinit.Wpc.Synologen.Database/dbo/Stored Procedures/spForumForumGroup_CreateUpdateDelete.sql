CREATE       PROCEDURE spForumForumGroup_CreateUpdateDelete
(
	@ForumGroupID	int out,
	@Name		nvarchar(256),
	@Action 	int
)
AS

-- CREATE
IF @Action = 0
BEGIN
	DECLARE @SortOrder int

	SET @SortOrder = ISNULL((SELECT MAX(SortOrder) + 1 FROM tblForumForumGroups), 0)

	-- Create a new forum group
	INSERT INTO 
		tblForumForumGroups 
		(
			Name,
			SortOrder
		)
	VALUES 
		(
			@Name,
			@SortOrder
		)
	
	SET @ForumGroupID = @@IDENTITY
END


-- UPDATE
ELSE IF @Action = 1
BEGIN

	IF EXISTS(SELECT ForumGroupID FROM tblForumForumGroups WHERE ForumGroupID = @ForumGroupID)
	BEGIN
		UPDATE
			tblForumForumGroups
		SET
			Name = @Name
		WHERE
			ForumGroupID = @ForumGroupID
	END

END

-- DELETE
ELSE IF @Action = 2
BEGIN
	DELETE tblForumForumGroups WHERE ForumGroupID = @ForumGroupID
END

