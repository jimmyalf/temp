create PROCEDURE spContGetSitePaths
					@status INT OUTPUT
	AS
		BEGIN
			SELECT DISTINCT cLocId, dbo.sfContGetFileNameDownString(cId) AS FilePath 
			FROM tblContTree
			WHERE dbo.sfContGetFileNameDownString(cId) IS NOT NULL AND cPublish = 1
			SELECT @status = @@ERROR
		END
