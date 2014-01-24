CREATE procedure spForumUpdateForumGroupSortOrder
(
     @ForumGroupID int,
     @MoveUp bit
)
AS
BEGIN
DECLARE @currentSortValue int
DECLARE @replaceSortValue int

-- Get the current sort order
SELECT @currentSortValue = SortOrder FROM tblForumForumGroups WHERE ForumGroupID = @ForumGroupID

-- Move the item up or down?
IF (@MoveUp = 1)
  BEGIN
    IF (@currentSortValue != 1)
      BEGIN
        SET @replaceSortValue = @currentSortValue - 1

        UPDATE tblForumForumGroups SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue
        UPDATE tblForumForumGroups SET SortOrder = @replaceSortValue WHERE ForumGroupID = @ForumGroupID
      END
  END

ELSE
  BEGIN
    IF (@currentSortValue < (SELECT MAX(ForumGroupID) FROM tblForumForumGroups))
    BEGIN
      SET @replaceSortValue = @currentSortValue + 1

      UPDATE tblForumForumGroups SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue
      UPDATE tblForumForumGroups SET SortOrder = @replaceSortValue WHERE ForumGroupID = @ForumGroupID
    END
  END
END




