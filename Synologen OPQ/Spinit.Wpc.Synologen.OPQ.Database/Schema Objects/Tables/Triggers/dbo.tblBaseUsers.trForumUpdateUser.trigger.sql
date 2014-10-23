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