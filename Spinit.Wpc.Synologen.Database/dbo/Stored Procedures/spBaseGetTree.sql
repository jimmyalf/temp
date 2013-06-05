create procedure spBaseGetTree
@id INT,
					@status INT OUTPUT
					
	AS

			BEGIN
				SELECT	*
				FROM	tblContTree
				WHERE	cId = @id
			END

	
	

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
