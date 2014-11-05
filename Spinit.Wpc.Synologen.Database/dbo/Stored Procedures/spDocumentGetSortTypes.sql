CREATE PROCEDURE spDocumentGetSortTypes
					@type INT,
					@sortTypeId INT=-1,
					@status INT OUTPUT
	AS
		IF (@type = 1)/*All */
		BEGIN
			SELECT * FROM tblDocumentSortType	
		END		
		IF (@type = 0)/*Specific*/
		BEGIN
			SELECT * FROM tblDocumentSortType 
			WHERE cId = @sortTypeId		
		END
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
