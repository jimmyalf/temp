create FUNCTION [sfBaseTruncateString] (@string NVARCHAR(255),@length INT)
	RETURNS NVARCHAR (255)
AS
BEGIN
	--DECLARE @length INT
	--SET @length = 50
	DECLARE @retString NVARCHAR(255)
    IF CHARINDEX(' ', @string, 0) = 0 
		BEGIN SET @retString = LEFT(@string,@length) END
	ELSE IF (LEN(@string)<= @length) 
		BEGIN SET @retString = @string END
	ELSE SET @retString =  LEFT (@string,  @length - CHARINDEX(' ', REVERSE(LEFT(@string,@length)), 0)  )
	RETURN @retString 
END
