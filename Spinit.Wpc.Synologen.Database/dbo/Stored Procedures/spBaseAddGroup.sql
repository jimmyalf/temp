CREATE PROCEDURE spBaseAddGroup 
					@name NVARCHAR (50),
					@description NVARCHAR (512),
					@grpTpeId INT,
					@userId NVARCHAR (100),
					@status INT OUTPUT,
					@id INT OUTPUT
							
AS
BEGIN
	DECLARE @dummy INT
	
	DECLARE chk_exst CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseGroups
		WHERE	cName = @name
		
	OPEN chk_exst
	FETCH NEXT FROM chk_exst INTO @dummy 
	
	
	IF (@@FETCH_STATUS <> -1)
		BEGIN
			CLOSE chk_exst
			DEALLOCATE chk_exst
			
			SET @status = -1
			RETURN
		END
		
	CLOSE chk_exst
	DEALLOCATE chk_exst

	INSERT INTO tblBaseGroups
		(cName, cDescription, cGrpTpeId, cCreatedBy, cCreatedDate)
	VALUES
		(@name, @description, @grpTpeId, @userId, GETDATE ())
	
	SET @id = @@IDENTITY

	IF (@@ERROR = 0)
		BEGIN
			SET @status = 0
		END
	ELSE
		BEGIN
			SET @status = @@ERROR
			SET @id = 0
		END
END
