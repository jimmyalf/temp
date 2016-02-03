CREATE TABLE [dbo].[tblBaseUsers] (
    [cId]              INT            IDENTITY (1, 1) NOT NULL,
    [cUserName]        NVARCHAR (100) NOT NULL,
    [cPassword]        NVARCHAR (100) NOT NULL,
    [cFirstName]       NVARCHAR (100) NULL,
    [cLastName]        NVARCHAR (100) NULL,
    [cEmail]           NVARCHAR (512) NULL,
    [cDefaultLocation] INT            NULL,
    [cActive]          BIT            NOT NULL,
    [cCreatedBy]       NVARCHAR (100) NULL,
    [cCreatedDate]     DATETIME       NULL,
    [cChangedBy]       NVARCHAR (100) NULL,
    [cChangedDate]     DATETIME       NULL,
    CONSTRAINT [PK_tblBaseUsers] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblBaseUsers_tblBaseLocations] FOREIGN KEY ([cDefaultLocation]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [IX_tblBaseUsers] UNIQUE NONCLUSTERED ([cUserName] ASC)
);


GO
CREATE TRIGGER trForumInsertUser
      ON    dbo.tblBaseUsers
      AFTER INSERT
      AS
	BEGIN
		DECLARE @id INT
		DECLARE @temp INT
		DECLARE @userName NVARCHAR(100)
		DECLARE get_changed CURSOR LOCAL FOR
	        SELECT cId
            	FROM INSERTED
		OPEN get_changed            
		FETCH NEXT FROM get_changed INTO @id
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			SET @temp = 0
			SELECT @userName=cUserName FROM tblBaseUsers WHERE cId = @id
			SELECT @temp= UserId FROM tblForumUsers WHERE UserName = @userName 
			IF @temp = 0
			BEGIN
				INSERT INTO tblForumUsers (UserName, Password, PasswordFormat, 
				Salt, Email, DateCreated, LastLogin, LastActivity, LastAction, 
				UserAccountStatus, IsAnonymous, ForceLogin)
				SELECT cUserName, cPassword, 0, '', cEmail, cCreatedDate,
				cCreatedDate, cCreatedDate, '', 1, 0 , 0 FROM tblBaseUsers WHERE cId = @id
			END
			FETCH NEXT FROM get_changed INTO @id
		END
		CLOSE get_changed
		DEALLOCATE get_changed
	END

GO
create TRIGGER trForumUpdateUser
      ON    dbo.tblBaseUsers
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
