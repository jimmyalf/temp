create PROCEDURE spNewsRemNewsPage
					@newsId INT,
					@pageId INT,
					@status INT OUTPUT
	AS
		DELETE FROM	tblNewsPageConnection
		WHERE		cNewsId = @newsId
			AND		cPageId = @pageId
			
		SELECT @status = @@ERROR
