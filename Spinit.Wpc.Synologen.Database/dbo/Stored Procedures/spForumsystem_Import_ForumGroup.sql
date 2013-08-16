create PROCEDURE spForumsystem_Import_ForumGroup
(
	@ForumGroupID int,
	@Name nvarchar(256),
	@SortOrder int
)
AS
BEGIN

	IF EXISTS(SELECT * FROM tblForumForumGroups WHERE ForumGroupID = @ForumGroupID)
		UPDATE
			tblForumForumGroups
		SET 
			Name = @Name,
			SortOrder = @SortOrder
		WHERE
			ForumGroupID = @ForumGroupID
	ELSE
	BEGIN
		SET IDENTITY_INSERT tblForumForumGroups ON
		INSERT INTO
			tblForumForumGroups
			(
				ForumGroupID,
				Name,
				SortOrder
			)
		VALUES
			(
				@ForumGroupID,
				@Name,
				@SortOrder
			)
		SET IDENTITY_INSERT tblForumForumGroups OFF
	END
END


