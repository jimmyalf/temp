create proc spForumService_CreateUpdateDelete
(
	  @ServiceID				int out
	, @DeleteService			bit = 0
	, @ServiceName				nvarchar(60)
	, @ServiceTypeCode			int
	, @ServiceAssemblyPath		nvarchar(512)
	, @ServiceFullClassName		nvarchar(512)
	, @ServiceWorkingDirectory	nvarchar(512)
)
AS

-- are we deleting the service
IF ( @DeleteService > 0 )
BEGIN

	DELETE tblForumServices
	WHERE
		ServiceID	= @ServiceID

	RETURN
END

IF( @ServiceID > 0 )
BEGIN

	UPDATE tblForumServices SET
		  ServiceName				= @ServiceName
		, ServiceTypeCode			= @ServiceTypeCode
		, ServiceAssemblyPath		= @ServiceAssemblyPath
		, ServiceFullClassName		= @ServiceFullClassName
		, ServiceWorkingDirectory	= @ServiceWorkingDirectory
	WHERE
		ServiceID	= @ServiceID

END
ELSE
BEGIN

	INSERT INTO tblForumServices (
		ServiceName, ServiceTypeCode, ServiceAssemblyPath, ServiceFullClassName, ServiceWorkingDirectory
	) VALUES (
		@ServiceName, @ServiceTypeCode, @ServiceAssemblyPath, @ServiceFullClassName, @ServiceWorkingDirectory
	)

	SET @ServiceID = @@identity

END

RETURN


