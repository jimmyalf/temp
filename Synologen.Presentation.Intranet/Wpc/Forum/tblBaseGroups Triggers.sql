/****** Object:  Trigger [dbo].[trForumInsertUserRole]    Script Date: 05/05/2009 10:01:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[trForumInsertUserRole]
      ON    [dbo].[tblBaseGroups]
      AFTER INSERT
      AS
	BEGIN
		DECLARE @id INT
		DECLARE @temp INT
		DECLARE @groupName NVARCHAR(100)
		
		DECLARE get_changed CURSOR LOCAL FOR
	        SELECT cId FROM INSERTED
	        
		OPEN get_changed            
		FETCH NEXT FROM get_changed INTO @id
		
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			SET @temp = 0
			SELECT @groupName = cName FROM tblBaseGroups WHERE cId = @id
			SELECT @temp = RoleID FROM tblForumRoles WHERE Name = @groupName
			IF @temp = 0 BEGIN				
				INSERT INTO tblForumRoles (Name, Description)
				SELECT cName, '' FROM tblBaseGroups WHERE cId = @id		
			END
			FETCH NEXT FROM get_changed INTO @id
		END
		CLOSE get_changed
		DEALLOCATE get_changed
	END


GO

/****** Object:  Trigger [dbo].[trForumUpdateUserRole]    Script Date: 05/05/2009 10:01:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trForumUpdateUserRole]
      ON    [dbo].[tblBaseGroups]
      AFTER UPDATE
      AS
	BEGIN
		DECLARE @groupName NVARCHAR(50)
		DECLARE @oldGroupName NVARCHAR(50)
		DECLARE @id INT
		
		DECLARE get_changed CURSOR LOCAL FOR
	        SELECT cId, cName FROM DELETED
	        
		OPEN get_changed            
		FETCH NEXT FROM get_changed INTO @id, @oldGroupName
		
		WHILE (@@FETCH_STATUS <> -1) BEGIN
			SELECT @groupName = cName
			FROM tblBaseGroups
			WHERE cId = @id
			
			SELECT RoleID FROM tblForumRoles
			WHERE Name = @oldGroupName
			
			IF (@@ROWCOUNT > 0) BEGIN		
				UPDATE tblForumRoles 
					SET Name = @groupName
				WHERE Name = @oldGroupName
			END
			ELSE BEGIN
				INSERT INTO tblForumRoles (Name, Description)
				SELECT cName, '' FROM tblBaseGroups WHERE cId = @id			
			END
			
			FETCH NEXT FROM get_changed INTO @id, @oldGroupName
		END
		CLOSE get_changed
		DEALLOCATE get_changed
	END
GO


