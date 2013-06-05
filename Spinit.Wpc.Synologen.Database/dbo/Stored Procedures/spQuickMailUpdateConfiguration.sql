CREATE PROCEDURE spQuickMailUpdateConfiguration
					@action INT,
					@id INT OUTPUT,
					@name NVARCHAR (100),
					@value NVARCHAR (1024),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailConfiguration
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			UPDATE	tblQuickMailConfiguration
			SET		cName = @name,
					cValue = @value
			WHERE	cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
