CREATE   PROCEDURE spForumGetTotalPostCount
 AS
	SELECT TOP 1 
		TotalPosts 
	FROM 
		tblForumStatistics

