create PROCEDURE spBaseRemoveUserFromGroup
					 @usrId int,
					 @grpId int,
					 @status int OUTPUT
AS
BEGIN
	DELETE FROM	tblBaseUsersGroups
	WHERE		cUserId = @usrId 
		AND		cGroupId = @grpId
	
	SET @status = @@ERROR
END
