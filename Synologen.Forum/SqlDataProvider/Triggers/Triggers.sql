IF EXISTS  (SELECT name 
                  FROM sysobjects 
                  WHERE name = 'trForumInsertUser' 
                        AND type = 'TR')
   DROP TRIGGER trForumInsertUser
GO
CREATE TRIGGER trForumInsertUser
      ON    tblBaseUsers
      AFTER INSERT
      AS
	BEGIN
		DECLARE @id INT
		DECLARE get_changed CURSOR LOCAL FOR
	        SELECT cId
            	FROM INSERTED
		OPEN get_changed            
		FETCH NEXT FROM get_changed INTO @id
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO tblForumUsers (UserName, Password, PasswordFormat, 
			Salt, Email, DateCreated, LastLogin, LastActivity, LastAction, 
			UserAccountStatus, IsAnonymous, ForceLogin)
			SELECT cUserName, cPassword, 0, '', cEmail, cCreatedDate,
			cCreatedDate, cCreatedDate, '', 1, 0 , 0 FROM tblBaseUsers WHERE cId = @id
			FETCH NEXT FROM get_changed INTO @id
		END
		CLOSE get_changed
		DEALLOCATE get_changed
	END
GO


IF EXISTS  (SELECT name 
                  FROM sysobjects 
                  WHERE name = 'trForumUpdateUser' 
                        AND type = 'TR')
   DROP TRIGGER trForumUpdateUser
GO
CREATE TRIGGER trForumUpdateUser
      ON    tblBaseUsers
      AFTER UPDATE
      AS
	BEGIN
		DECLARE @userName NVARCHAR(100)
		DECLARE @oldUserName NVARCHAR(100)
		DECLARE @password NVARCHAR(100)
		DECLARE @id INT
		DECLARE get_changed CURSOR LOCAL FOR
	        SELECT cId, cUserName
            	FROM DELETED
		OPEN get_changed            
		FETCH NEXT FROM get_changed INTO @id, @oldUserName
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			SELECT @userName = cUserName, @password = cPassword
			FROM tblBaseUsers
			WHERE cId = @id
			
			SELECT UserID FROM tblForumUsers
			WHERE UserName = @oldUserName
			
			IF (@@ROWCOUNT > 0)
			BEGIN		
				UPDATE tblForumUsers 
				SET UserName = @userName,
				Password = @passWord
				WHERE UserName = @oldUserName
			END
			ELSE
			BEGIN
				INSERT INTO tblForumUsers (UserName, Password, PasswordFormat, 
				Salt, Email, DateCreated, LastLogin, LastActivity, LastAction, 
				UserAccountStatus, IsAnonymous, ForceLogin)
				SELECT cUserName, cPassword, 0, '', cEmail, cCreatedDate,
				cCreatedDate, cCreatedDate, '', 1, 0 , 0 FROM tblBaseUsers WHERE cId = @id			
			END
			
			FETCH NEXT FROM get_changed INTO @id, @oldUserName
		END
		CLOSE get_changed
		DEALLOCATE get_changed
	END
GO

/****** Object:  Trigger [dbo].[trForumDeleteUsersInRole]    Script Date: 09/14/2011 10:04:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create TRIGGER [dbo].[trForumDeleteUsersInRole]
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

/****** Object:  Trigger [dbo].[trForumInsertUsersInRole]    Script Date: 09/14/2011 10:05:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create TRIGGER [dbo].[trForumInsertUsersInRole]
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