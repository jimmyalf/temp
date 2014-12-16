CREATE TABLE [dbo].[tblBaseUsersGroups] (
    [cUserId]  INT NOT NULL,
    [cGroupId] INT NOT NULL,
    CONSTRAINT [PK_tblBaseUsersGroups] PRIMARY KEY CLUSTERED ([cUserId] ASC, [cGroupId] ASC),
    CONSTRAINT [FK_tblBaseUsersGroups_tblBaseGroups] FOREIGN KEY ([cGroupId]) REFERENCES [dbo].[tblBaseGroups] ([cId]),
    CONSTRAINT [FK_tblBaseUsersGroups_tblBaseUsers] FOREIGN KEY ([cUserId]) REFERENCES [dbo].[tblBaseUsers] ([cId])
);


GO
CREATE TRIGGER trForumDeleteUsersInRole
      ON    tblBaseUsersGroups
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
CREATE TRIGGER trForumInsertUsersInRole
      ON    tblBaseUsersGroups
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
