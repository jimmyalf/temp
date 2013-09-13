CREATE PROCEDURE spContTreeChgPagePublish
					@id INT,
					@changedBy NVARCHAR (100),
					@publishDate SMALLDATETIME,
					@unPublishDate SMALLDATETIME,
					@status INT OUTPUT
	AS
		UPDATE	tblContTree
		SET		cChangedBy = @changedBy,
				cChangedDate = GETDATE (),
				--cPublishDate = @publishDate,
				cPublishedDate = @publishDate,
				--cUnPublishDate = @unPublishDate,
				cUnPublishedDate = @unPublishDate
		WHERE	cId = @id
		
		SELECT @status = @@ERROR
