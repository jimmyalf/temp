create PROCEDURE spBaseAddUserToGroup
					@usrId INT,
					@grpId INT,
					@status INT OUTPUT
AS
BEGIN
	SELECT	1
	FROM	tblBaseUsers
	WHERE	cId = @usrId
			
	IF @@ROWCOUNT = 0
	BEGIN
		SET @status = -3
		RETURN
	END

	SELECT	1
	FROM	tblBaseGroups
	WHERE	cId = @grpId
			
	IF @@ROWCOUNT = 0
	BEGIN
		SET @status = -3
		RETURN
	END

	INSERT INTO tblBaseUsersGroups
 		(cUserId, cGroupId) 
	VALUES 
		(@usrId, @grpId)
			 
	SET @status = @@ERROR
END
