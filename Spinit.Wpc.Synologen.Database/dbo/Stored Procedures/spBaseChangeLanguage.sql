create PROCEDURE spBaseChangeLanguage
					@id INT,
					@name NVARCHAR(256),
					@description NVARCHAR (256),
					@resource NVARCHAR (50),
					@status INT OUTPUT
AS
BEGIN		
	UPDATE	tblBaseLanguages		
	SET		cName = @name,
			cDescription = @description,
			cResource = @resource
	WHERE	cId = @id

	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
END
