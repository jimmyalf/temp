create PROCEDURE spBaseAddGroupLocation
					@grpId INT,
					@locId INT,
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
	
	DECLARE chk_loc CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseLocations
		WHERE	cId = @locId
		
	OPEN chk_loc
	FETCH NEXT FROM chk_loc INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_loc
			DEALLOCATE chk_loc
			
			SET @status = -2
			RETURN
		END
		
	CLOSE chk_loc
	DEALLOCATE chk_loc

	INSERT INTO tblBaseGroupsLocations
 		(cGroupId, cLocationId) 
	VALUES 
		(@grpId, @locId)
			 
	SET @status = 0
END
