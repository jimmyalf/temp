CREATE PROCEDURE spBaseAddFile
					@name NTEXT,
					@directory BIT,
					@contentInfo NVARCHAR (256),
					@keyWords NVARCHAR (256),
					@description NVARCHAR (256),
					@createdBy NVARCHAR (100),
					@status INT OUTPUT,
					@id INT OUTPUT
	AS
	BEGIN	
		INSERT INTO tblBaseFile
			(cName, cDirectory, cContentInfo, cKeyWords, cDescription, 
			 cCreatedBy, cCreatedDate, cChangedBy, cChangedDate)
		VALUES
			(@name, @directory, @contentInfo,@keyWords, @description,
			 @createdBy, GETDATE (), @createdBy, GETDATE ())		
		
		SET @id =@@IDENTITY
		
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