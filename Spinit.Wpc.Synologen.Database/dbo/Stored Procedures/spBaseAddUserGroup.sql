CREATE PROCEDURE spBaseAddUserGroup
					@usrId int,
					@grpId int,
					@status int OUTPUT
AS
BEGIN
	DECLARE @dummy INT
	
	DECLARE chk_usr CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseUsers
		WHERE	cId = @usrId
		
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
	
	DECLARE chk_grp CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseGroups
		WHERE	cId = @grpId
		
	OPEN chk_grp
	FETCH NEXT FROM chk_grp INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_grp
			DEALLOCATE chk_grp
			
			SET @status = -2
			RETURN
		END
		
	CLOSE chk_grp
	DEALLOCATE chk_grp

	INSERT INTO tblBaseUsersGroups
 		(cUserId, cGroupId) 
	VALUES 
		(@usrId, @grpId)
			 
	SET @status = 0
END
