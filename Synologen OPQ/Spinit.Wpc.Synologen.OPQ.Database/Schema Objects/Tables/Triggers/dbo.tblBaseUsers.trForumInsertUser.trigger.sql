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