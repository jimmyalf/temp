CREATE PROCEDURE spBaseChangeGroup
					@id INT,
					@name NVARCHAR (50),
					@description NVARCHAR (512),
					@grpTpeId INT,
					@userId NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN		
	DECLARE @dummy INT,
			@grpName NVARCHAR (50),
			@grpDescription NVARCHAR (512),
			@grpGrpTpeId INT
			
	DECLARE get_grp CURSOR LOCAL FOR
		SELECT	cName,
				cDescription,
				cGrpTpeId
		FROM	tblBaseGroups
		WHERE	cId = @id
		
	OPEN get_grp
	FETCH NEXT FROM get_grp INTO	@grpName,
									@grpDescription,
									@grpGrpTpeId
									
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_grp
			DEALLOCATE get_grp
			
			SET @status = -1
			RETURN
		END
		
	CLOSE get_grp
	DEALLOCATE get_grp
	
	IF (@name IS NOT NULL)
		BEGIN
			DECLARE chk_exst CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseGroups
				WHERE	cName = @name
					AND cId <> @id
				
			OPEN chk_exst
			FETCH NEXT FROM chk_exst INTO @dummy 
			
			
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					CLOSE chk_exst
					DEALLOCATE chk_exst
					
					SET @status = -2
					RETURN
				END
				
			CLOSE chk_exst
			DEALLOCATE chk_exst
		END
		
	IF (@name IS NOT NULL)
		BEGIN
			IF (LEN (@name) = 0)
				BEGIN
					SET @grpName = NULL
				END
			ELSE
				BEGIN
					SET @grpName = @name
				END
		END

	IF (@description IS NOT NULL)
		BEGIN
			IF (LEN (@description) = 0)
				BEGIN
					SET @grpDescription = NULL
				END
			ELSE
				BEGIN
					SET @grpDescription = @description
				END
		END
		
	IF (@grpTpeId IS NOT NULL)
		BEGIN
			IF (@grpTpeId = -1)
				BEGIN
					SET @grpGrpTpeId = NULL
				END
			ELSE
				BEGIN
					SET @grpGrpTpeId = @grpTpeId
				END
		END

	UPDATE	tblBaseGroups
	SET		cName = @grpName,
			cDescription = @grpDescription,
			cGrpTpeId = @grpGrpTpeId,
			cChangedBy = @userId,
			cChangedDate = GETDATE ()
	WHERE	cId = @id

	SET @status = @@ERROR
END
