create FUNCTION [sfContGetLinkPageId] (@treeId INT)
	RETURNS INT
AS
BEGIN
	DECLARE @id INT,
			@link NVARCHAR (256),
			@inx INT
	
	SET @id = 0
	
	DECLARE get_link CURSOR LOCAL FOR
		SELECT	cLink
		FROM	tblContTree
		WHERE	cId = @treeId
		
	OPEN get_link
	FETCH NEXT FROM get_link INTO @link
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_link
			DEALLOCATE get_link
			
			RETURN @id
		END
		
	CLOSE get_link
	DEALLOCATE get_link
	
	SET @inx = CHARINDEX ('#internal=', @link)
	
	IF (@inx > 0)
		BEGIN
			SET @id = CAST (SUBSTRING (@link, @inx + 10, LEN (@link) - @inx - 9) AS INT)
		END
			
	RETURN @id
END
