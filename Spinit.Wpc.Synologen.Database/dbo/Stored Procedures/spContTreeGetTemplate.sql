CREATE PROCEDURE spContTreeGetTemplate
					@id INT,
					@status INT OUTPUT
	AS		
		SELECT	cTemplate,
				cStylesheet
		FROM	tblContTree
		WHERE	cId = @id
					
		SELECT @status = @@ERROR
