CREATE   PROCEDURE spForumUpdateForumGroup
(
	@ForumGroupName		nvarchar(256),
	@ForumGroupID	int
)
 AS
	IF @ForumGroupName IS NULL
		DELETE
			ForumGroups
		WHERE
			ForumGroupID = @ForumGroupID
	ELSE
		-- insert a new forum
		UPDATE 
			ForumGroups 
		SET 
			Name = @ForumGroupName
		WHERE 
			ForumGroupID = @ForumGroupID		





