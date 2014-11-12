CREATE PROCEDURE spBaseRemoveGroup
					@id INT,
					@status INT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION DELETE_GROUP
	
	DELETE FROM	tblBaseUsersGroups
	WHERE		cGroupId = @id
	
	IF (@@ERROR != 0)
		BEGIN
			SET @status = @@ERROR
			ROLLBACK TRANSACTION DELETE_GROUP
			RETURN
		END
		
	DELETE FROM tblBaseGroupsObjects
	WHERE		cGroupId = @id

	IF (@@ERROR != 0)
		BEGIN
			SET @status = @@ERROR
			ROLLBACK TRANSACTION DELETE_GROUP
			RETURN
		END
		
	DELETE FROM tblBaseGroupsLocations
	WHERE		cGroupId = @id
	
	IF (@@ERROR != 0)
		BEGIN
			SET @status = @@ERROR
			ROLLBACK TRANSACTION DELETE_GROUP
			RETURN
		END

	DELETE FROM tblBaseGroupsLanguages
	WHERE		cGroupId = @id
	
	IF (@@ERROR != 0)
		BEGIN
			SET @status = @@ERROR
			ROLLBACK TRANSACTION DELETE_GROUP
			RETURN
		END

	DELETE FROM	tblBaseGroups
	WHERE		cId = @id
		
	IF (@@ERROR != 0)
		BEGIN
			SET @status = @@ERROR
			ROLLBACK TRANSACTION DELETE_GROUP
			RETURN
		END

	COMMIT TRANSACTION DELETE_GROUP
	SET @status = @@ERROR
END
