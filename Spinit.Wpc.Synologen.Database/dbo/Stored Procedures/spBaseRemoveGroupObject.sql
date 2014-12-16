create PROCEDURE spBaseRemoveGroupObject
					 @grpId int,
					 @objId int,
					 @status int OUTPUT
AS
BEGIN
	DECLARE	@dummy INT

	DECLARE chk_grp CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseGroupsObjects
		WHERE	cGroupId = @grpId
			AND	cObjectId = @objId
		
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

	DELETE FROM	tblBaseGroupsObjects
	WHERE		cGroupId = @grpId 
		AND		cObjectId = @objId
	
	SET @status = @@ERROR
END
