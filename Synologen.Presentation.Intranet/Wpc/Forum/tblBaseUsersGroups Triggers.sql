/****** Object:  Trigger [dbo].[trForumDeleteUsersInRole]    Script Date: 05/05/2009 10:00:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trForumDeleteUsersInRole]
      ON    [dbo].[tblBaseUsersGroups]
      AFTER DELETE
      AS
	BEGIN
		DECLARE @groupId INT
		DECLARE @forumRoleId INT
		DECLARE @groupName NVARCHAR(50)
		
		DECLARE @userId INT
		DECLARE @forumUserId INT
		DECLARE @userName NVARCHAR(50)
		
		DECLARE get_deleted CURSOR LOCAL FOR
	        SELECT cUserId, cGroupId FROM DELETED
	        
		OPEN get_deleted            
		FETCH NEXT FROM get_deleted INTO @userId, @groupId
		
		WHILE (@@FETCH_STATUS <> -1) BEGIN
			SELECT @userName = cUserName FROM tblBaseUsers WHERE cId = @userId
			SELECT @groupName = cName FROM tblBaseGroups WHERE cId = @groupId	
			SELECT @forumRoleId = RoleID FROM tblForumRoles WHERE Name = @groupName
			SELECT @forumUserId = UserID FROM tblForumUsers WHERE UserName = @userName
			
			IF (@forumRoleId > 0 AND @forumUserId > 0 ) BEGIN
				DELETE FROM tblForumUsersInRoles
				WHERE UserID = @forumUserId AND RoleID = @forumRoleId
			END
			
			FETCH NEXT FROM get_deleted INTO @userId, @groupId
		END
		CLOSE get_deleted
		DEALLOCATE get_deleted
	END

GO

/****** Object:  Trigger [dbo].[trForumInsertUsersInRole]    Script Date: 05/05/2009 10:00:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trForumInsertUsersInRole]
      ON    [dbo].[tblBaseUsersGroups]
      AFTER INSERT
      AS
	BEGIN
		DECLARE @groupId INT
		DECLARE @forumRoleId INT
		DECLARE @groupName NVARCHAR(50)
		
		DECLARE @userId INT
		DECLARE @forumUserId INT
		DECLARE @userName NVARCHAR(50)
		
		DECLARE get_inserted CURSOR LOCAL FOR
	        SELECT cUserId, cGroupId FROM INSERTED
	        
		OPEN get_inserted            
		FETCH NEXT FROM get_inserted INTO @userId, @groupId
		
		WHILE (@@FETCH_STATUS <> -1) BEGIN
			SELECT @userName = cUserName FROM tblBaseUsers WHERE cId = @userId
			SELECT @groupName = cName FROM tblBaseGroups WHERE cId = @groupId	
			SELECT @forumRoleId = RoleID FROM tblForumRoles WHERE Name = @groupName
			SELECT @forumUserId = UserID FROM tblForumUsers WHERE UserName = @userName
			
			IF (@forumRoleId > 0 AND @forumUserId > 0 ) BEGIN
				INSERT INTO tblForumUsersInRoles(UserID, RoleID)
				VALUES(@forumUserId, @forumRoleId)
			END
			
			FETCH NEXT FROM get_inserted INTO @userId, @groupId
		END
		CLOSE get_inserted
		DEALLOCATE get_inserted
	END

GO


