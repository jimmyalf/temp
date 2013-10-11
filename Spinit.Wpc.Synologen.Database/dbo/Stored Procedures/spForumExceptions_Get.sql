CREATE  procedure spForumExceptions_Get
(
	@SiteID int,
	@ExceptionType int = 0,
	@MinFrequency int = 10
)
AS
BEGIN

IF @ExceptionType < 0
	SELECT TOP 100
		E.*
	FROM
		tblForumExceptions E
	WHERE
		E.SiteID = @SiteID AND
		Frequency >= @MinFrequency
	ORDER BY
		Frequency DESC
ELSE
	SELECT TOP 100
		E.*
	FROM
		tblForumExceptions E
	WHERE
		E.SiteID = @SiteID AND
		E.Category = @ExceptionType AND
		Frequency >= @MinFrequency
	ORDER BY
		Frequency DESC
END




