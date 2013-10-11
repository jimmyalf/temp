create PROCEDURE spQuickFormGetQuickForm
					@type INT,
					@id INT,
					@status INT OUTPUT
	AS
		IF (@type = 0)
		BEGIN
			SELECT * 
			FROM tblQuickForm
		END
		
		IF (@type = 1)
		BEGIN
			SELECT * 
			FROM tblQuickForm
			WHERE cId = @id
		END
		
		IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
