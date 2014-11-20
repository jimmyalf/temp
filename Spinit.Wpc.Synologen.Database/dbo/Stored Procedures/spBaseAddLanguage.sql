create PROCEDURE spBaseAddLanguage
					@name NVARCHAR (256),
					@description NVARCHAR (256),
					@resource NVARCHAR (50),
					@status INT OUTPUT,
					@id INT OUTPUT
							
AS
BEGIN
	INSERT INTO tblBaseLanguages
		(cName,	cDescription, cResource)				
	VALUES
		(@name, @description, @resource)
	
	SELECT @id = @@IDENTITY
	
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
