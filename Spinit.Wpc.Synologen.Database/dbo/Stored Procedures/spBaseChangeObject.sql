create PROCEDURE spBaseChangeObject
					@id INT,
					@name NVARCHAR (50),
					@description NVARCHAR (512),
					@cmpId INT,
					@status INT OUTPUT
AS
BEGIN		
	DECLARE @dummy INT,
			@objName NVARCHAR (50),
			@objDescription NVARCHAR (512),
			@objCmpId INT
			
	DECLARE get_obj CURSOR LOCAL FOR
		SELECT	cName,
				cDescription,
				cCmpId
		FROM	tblBaseObjects
		WHERE	cId = @id
		
	OPEN get_obj
	FETCH NEXT FROM get_obj INTO	@objName,
									@objDescription,
									@objCmpId
									
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_obj
			DEALLOCATE get_obj
			
			SET @status = -1
			RETURN
		END
		
	CLOSE get_obj
	DEALLOCATE get_obj
	
	IF (@cmpId IS NOT NULL)
		BEGIN
			IF (@cmpId = -1)
				BEGIN
					SET @objCmpId = NULL
				END
			ELSE
				BEGIN
					SET @objCmpId = @cmpId
				END
		END
	
	IF (@name IS NOT NULL)
		BEGIN
			DECLARE chk_exst CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseObjects
				WHERE	cName = @name
					AND	cCmpId = @objCmpId
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
					SET @objName = NULL
				END
			ELSE
				BEGIN
					SET @objName = @name
				END
		END

	IF (@description IS NOT NULL)
		BEGIN
			IF (LEN (@description) = 0)
				BEGIN
					SET @objDescription = NULL
				END
			ELSE
				BEGIN
					SET @objDescription = @description
				END
		END
		
	UPDATE	tblBaseObjects
	SET		cName = @objName,
			cDescription = @objDescription,
			cCmpId = @objCmpId
	WHERE	cId = @id

	SET @status = @@ERROR
END
