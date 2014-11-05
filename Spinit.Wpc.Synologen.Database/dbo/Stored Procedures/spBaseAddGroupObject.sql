create PROCEDURE spBaseAddGroupObject
					@grpId INT,
					@objId INT,
					@objTpeId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
	
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
			
			SET @status = -1
			RETURN
		END
		
	CLOSE chk_grp
	DEALLOCATE chk_grp
	
	DECLARE chk_obj CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseObjects
		WHERE	cId = @objId
		
	OPEN chk_obj
	FETCH NEXT FROM chk_obj INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_obj
			DEALLOCATE chk_obj
			
			SET @status = -2
			RETURN
		END
		
	CLOSE chk_obj
	DEALLOCATE chk_obj

	DECLARE chk_exs CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseGroupsObjects
		WHERE	cGroupId = @grpId
			AND	cObjectId = @objId
		
	OPEN chk_exs
	FETCH NEXT FROM chk_exs INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			INSERT INTO tblBaseGroupsObjects
 				(cGroupId, cObjectId, cObjTpeId) 
			VALUES 
				(@grpId, @objId, @objTpeId)
		END
	ELSE
		BEGIN
			UPDATE	tblBaseGroupsObjects
			SET		cObjTpeId = @objTpeId
			WHERE	cGroupId = @grpId
				AND	cObjectId = @objId
		END
		
	CLOSE chk_exs
	DEALLOCATE chk_exs

			 
	SET @status = 0
END
