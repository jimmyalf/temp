CREATE procedure spForumExceptions_Log
(
	@SiteID int,
	@ExceptionHash varchar(128),
	@Category int,
	@Exception nvarchar(2000),
	@ExceptionMessage nvarchar(500),
	@UserAgent nvarchar(64),
	@IPAddress varchar(15),
	@HttpReferrer nvarchar (256),
	@HttpVerb nvarchar(24),
	@PathAndQuery nvarchar(512)
)
AS
BEGIN

IF EXISTS (SELECT ExceptionID FROM tblForumExceptions WHERE ExceptionHash = @ExceptionHash)

	UPDATE
		tblForumExceptions
	SET
		DateLastOccurred = GetDate(),
		Frequency = Frequency + 1
	WHERE
		ExceptionHash = @ExceptionHash
ELSE
	INSERT INTO 
		tblForumExceptions
	(
		ExceptionHash,
		SiteID,
		Category,
		Exception,
		ExceptionMessage,
		UserAgent,
		IPAddress,
		HttpReferrer,
		HttpVerb,
		PathAndQuery,
		DateCreated,
		DateLastOccurred,
		Frequency
	)
	VALUES
	(
		@ExceptionHash,
		@SiteID,
		@Category,
		@Exception,
		@ExceptionMessage,
		@UserAgent,
		@IPAddress,
		@HttpReferrer,
		@HttpVerb,
		@PathAndQuery,
		GetDate(),
		GetDate(),
		1
	)

END


