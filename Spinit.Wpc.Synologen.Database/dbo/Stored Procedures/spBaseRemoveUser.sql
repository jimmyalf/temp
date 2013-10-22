CREATE PROCEDURE spBaseRemoveUser
				 @id int,
				 @status int OUTPUT
AS
BEGIN
	DECLARE @dummy INT
	DECLARE chk_usr CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseUsers
		WHERE	cId = @id
		
	OPEN chk_usr
	FETCH NEXT FROM chk_usr INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_usr
			DEALLOCATE chk_usr
			
			SET @status = -1
			RETURN
		END
		
	CLOSE chk_usr
	DEALLOCATE chk_usr
	
	SELECT @status = 0

	DELETE FROM	tblBaseLoginHistory
	WHERE		cUsrId = @id
	
	SET @status = @@ERROR
	
	IF (@@Error != 0)
		BEGIN
			RETURN
		END
		
	DELETE FROM	tblBaseUsersGroups
	WHERE		cUserId = @id
	
	SET @status = @@ERROR
	
	IF (@@Error != 0)
		BEGIN
			RETURN
		END

	DELETE FROM	tblBaseUsers
	WHERE		cId = @id

	SET @status = @@ERROR
END
