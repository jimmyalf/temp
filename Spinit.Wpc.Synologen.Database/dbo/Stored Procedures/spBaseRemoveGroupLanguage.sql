create PROCEDURE spBaseRemoveGroupLanguage
					 @grpId INT,
					 @lngId INT,
					 @locId INT,
					 @status INT OUTPUT
AS
BEGIN
	DECLARE	@dummy INT

	DECLARE chk_grp CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseGroupsLanguages
		WHERE	cGroupId = @grpId
			AND	cLanguageId = @lngId
			AND cLocationId = @locId
		
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

	DELETE FROM	tblBaseGroupsLanguages
	WHERE		cGroupId = @grpId 
		AND		cLanguageId = @lngId
		AND		cLocationId = @locId
	
	SET @status = @@ERROR
END
