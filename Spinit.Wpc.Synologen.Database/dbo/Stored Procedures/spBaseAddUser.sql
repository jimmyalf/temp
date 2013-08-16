CREATE PROCEDURE spBaseAddUser 
					@userName NVARCHAR (100),
					@password NVARCHAR (100),
					@firstName NVARCHAR (100),
					@lastName NVARCHAR (100),
					@email NVARCHAR (512),
					@defaultLocation INT,
					@userId NVARCHAR (100),
					@status INT OUTPUT,
					@id INT OUTPUT			
AS	
BEGIN
	DECLARE @dummy INT
	
	DECLARE chk_exst CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseUsers
		WHERE	cUserName = @userName
		
	OPEN chk_exst
	FETCH NEXT FROM chk_exst INTO @dummy 
	
	
	IF (@@FETCH_STATUS <> -1)
		BEGIN
			CLOSE chk_exst
			DEALLOCATE chk_exst
			
			SET @status = -1
			RETURN
		END
		
	CLOSE chk_exst
	DEALLOCATE chk_exst
	
	INSERT INTO tblBaseUsers
		(cUserName, cPassword, cFirstName, cLastName, cEmail, cDefaultLocation, cActive,
		 cCreatedBy, cCreatedDate)
	VALUES
		(@username, @password, @firstName, @lastName, @email, @defaultLocation, 1,
		 @userId, GETDATE ())

	--To override new forum triggered ID	
	SELECT @id = SCOPE_IDENTITY()  

	IF (@@ERROR = 0)
		BEGIN
			SET @status = 0
		END
	ELSE
		BEGIN
			SET @status = @@ERROR
			SET @id = 0
		END




END
