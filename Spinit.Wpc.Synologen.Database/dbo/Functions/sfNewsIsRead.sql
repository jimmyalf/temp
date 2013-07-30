CREATE FUNCTION sfNewsIsRead
( @userId INT,
  @newsId INT)
RETURNS  BIT 
AS
BEGIN
DECLARE @ret bit
set @ret = 0

	IF ((@userId IS NOT NULL) AND (@userId > 0)
		AND (@newsId IS NOT NULL) AND (@newsId > 0))
	BEGIN
		DECLARE @tmp INT
		SELECT	@tmp = count(cId)
		FROM	tblNewsRead
			LEFT JOIN tblBaseUsers ON tblNewsRead.cReadBy=tblBaseUsers.cUserName
		WHERE tblBaseUsers.cId = @userId
		AND tblNewsRead.cNewsId = @newsId

		if @tmp > 0
		BEGIN
			SET @ret = 1
		END
		
	END
	return @ret
END
