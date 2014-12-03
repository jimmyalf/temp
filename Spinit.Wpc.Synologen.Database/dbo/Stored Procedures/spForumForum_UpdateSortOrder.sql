CREATE procedure spForumForum_UpdateSortOrder
        ( 
                 @ForumID int, 
                 @MoveUp bit 
        ) 
        AS 
        BEGIN 
        DECLARE @currentSortValue int 
        DECLARE @replaceSortValue int 

        -- Get the current sort order 
        SELECT @currentSortValue = SortOrder FROM tblForumForums WHERE ForumID = @ForumID 

        -- Move the item up or down? 
        IF (@MoveUp = 1) 
          BEGIN 
                IF (@currentSortValue != 1) 
                  BEGIN 
                        SET @replaceSortValue = @currentSortValue - 1 

                        UPDATE tblForumForums SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue 
                        UPDATE tblForumForums SET SortOrder = @replaceSortValue WHERE ForumID = @ForumID 
                  END 
          END 

        ELSE 
          BEGIN 
                IF (@currentSortValue < (SELECT MAX(ForumID) FROM tblForumForums)) 
                BEGIN 
                  SET @replaceSortValue = @currentSortValue + 1 

                  UPDATE tblForumForums SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue 
                  UPDATE tblForumForums SET SortOrder = @replaceSortValue WHERE ForumID = @ForumID 
                END 
          END 
        END 

