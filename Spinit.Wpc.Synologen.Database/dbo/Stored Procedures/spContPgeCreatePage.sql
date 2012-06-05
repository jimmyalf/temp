CREATE PROCEDURE spContPgeCreatePage
					@pgeTpeId INT,
					@name NVARCHAR (255),
					@size BIGINT,
					@content NTEXT,
					@createdBy NVARCHAR (100),
					@status INT OUTPUT,
					@id INT OUTPUT
	AS								
		INSERT INTO tblContPage
			(cPgeTpeId, cName, cSize, cContent,
			 cCreatedBy, cCreatedDate)
		VALUES
			(@pgeTpeId, @name, @size, @content,
			 @createdBy, GETDATE ())
			 
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
				SELECT @id = @@IDENTITY
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
				SELECT @id = 0
			END
