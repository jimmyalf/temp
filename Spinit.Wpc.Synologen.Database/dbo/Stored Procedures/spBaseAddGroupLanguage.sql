create PROCEDURE spBaseAddGroupLanguage
					@grpId INT,
					@lngId INT,
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
	
	DECLARE chk_lng CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseLanguages
		WHERE	cId = @lngId
		
	OPEN chk_lng
	FETCH NEXT FROM chk_lng INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_lng
			DEALLOCATE chk_lng
			
			SET @status = -2
			RETURN
		END
		
	CLOSE chk_lng
	DEALLOCATE chk_lng

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
			
			SET @status = -3
			RETURN
		END
		
	CLOSE chk_loc
	DEALLOCATE chk_loc

	INSERT INTO tblBaseGroupsLanguages
 		(cGroupId, cLanguageId, cLocationId) 
	VALUES 
		(@grpId, @lngId, @locId)
			 
	SET @status = 0
END
