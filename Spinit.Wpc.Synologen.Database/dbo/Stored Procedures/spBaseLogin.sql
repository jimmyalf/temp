CREATE PROCEDURE spBaseLogin
					@userName NVARCHAR (100),
					@password NVARCHAR (100),
					@ip VARCHAR (50),
					@userAgent NVARCHAR (512),
					@session NVARCHAR (256),
					@id INT OUTPUT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE	@usrPwd NVARCHAR (50),
			@active BIT
			
	
	DECLARE get_usr CURSOR LOCAL FOR
		SELECT	cId,
				cPassword,
				cActive
		FROM	tblBaseUsers
		WHERE	cUserName = @userName
		
	OPEN get_usr
	FETCH NEXT FROM get_usr INTO	@id,
									@usrPwd,
									@active
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_usr
			DEALLOCATE get_usr
			
			INSERT INTO tblBaseLoginHistory
				(cSuccess, cLoginTime, cName, cPassword, cIpNumber, cUserAgent,
				 cSession)
			VALUES
				(0, GETDATE (), @userName, @password, @ip, @userAgent,
				 @session)
		
			SET @status = -1
			RETURN
		END

	CLOSE get_usr
	DEALLOCATE get_usr
	
	IF (((@usrPwd IS NULL) AND (@password IS NOT NULL))
		OR ((@usrPwd IS NOT NULL) AND (@password IS NULL))
		OR ((@usrPwd IS NOT NULL) AND (@password IS NOT NULL)
			AND (@usrPwd != @password)))
		BEGIN
			INSERT INTO tblBaseLoginHistory
				(cSuccess, cLoginTime, cName, cPassword, cIpNumber, cUserAgent,
				 cSession)
			VALUES
				(0, GETDATE (), @userName, @password, @ip, @userAgent,
				 @session)

			SET @status = -1
			RETURN
		END
		
	IF (@active = 0)
		BEGIN	
			INSERT INTO tblBaseLoginHistory
				(cSuccess, cLoginTime, cUsrId, cIpNumber, cUserAgent, cSession)
			VALUES
				(0, GETDATE (), @id, @ip, @userAgent, @session)
		
			SET @status = -1
			RETURN
		END
		
	INSERT INTO tblBaseLoginHistory
		(cSuccess, cLoginTime, cName, cUsrId, cIpNumber, cUserAgent, cSession)
	VALUES
		(1, GETDATE (), @userName, @id, @ip, @userAgent, @session)
		
	SET @status = @@ERROR
END
