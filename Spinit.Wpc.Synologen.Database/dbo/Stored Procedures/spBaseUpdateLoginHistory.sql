CREATE PROCEDURE spBaseUpdateLoginHistory
					@userName NVARCHAR (100),
					@ip VARCHAR (50),
					@userAgent NVARCHAR (512),
					@session NVARCHAR (256),
					@status INT OUTPUT
AS
BEGIN

	DECLARE @UserId INT
		
	DECLARE get_usr CURSOR LOCAL FOR
		SELECT	cId
		FROM	tblBaseUsers
		WHERE	cUserName = @userName
		
	OPEN get_usr
	FETCH NEXT FROM get_usr INTO @UserId
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_usr
			DEALLOCATE get_usr
					
			SET @status = -1
			RETURN
		END

	CLOSE get_usr
	DEALLOCATE get_usr
			
	INSERT INTO tblBaseLoginHistory
		(cSuccess, cLoginTime, cName, cUsrId, cIpNumber, cUserAgent, cSession)
	VALUES
		(1, GETDATE (), @userName, @UserId, @ip, @userAgent, @session)
		
	SET @status = @@ERROR
END
