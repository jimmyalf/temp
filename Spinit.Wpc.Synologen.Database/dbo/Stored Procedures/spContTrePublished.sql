
CREATE PROCEDURE spContTrePublished
					@id INT,
					@publishedDate SMALLDATETIME,
					@unPublishedDate SMALLDATETIME,
					@status INT OUTPUT
	AS
		UPDATE	tblContTree
		SET		cPublishedDate = @publishedDate,
				cUnPublishedDate = @unPublishedDate
		WHERE	cId = @id
		
		SELECT @status = @@ERROR
