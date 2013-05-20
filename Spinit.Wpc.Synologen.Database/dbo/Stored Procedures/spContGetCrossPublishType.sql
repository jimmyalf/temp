CREATE PROCEDURE spContGetCrossPublishType
@type INT, @id INT, @status INT OUTPUT
AS
BEGIN
			IF (@type = 0)
				BEGIN
					SELECT	cId,
							cName,
							cDescription
					FROM	tblContCrossPublishType
				END
				
			IF (@type = 1)
				BEGIN
					SELECT	cId,
							cName,
							cDescription
					FROM	tblContCrossPublishType
					WHERE	cId = @id
				END
				
			SELECT @status = @@ERROR		
		END
