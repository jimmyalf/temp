create proc spForumReport_CreateUpdateDelete
(
	  @ReportID			int out
	, @DeleteReport		bit		= 0
	, @ReportName		varchar(20)
	, @Active			bit
	, @ReportCommand	varchar(6500)
	, @ReportScript		ntext
)	
AS

IF( @DeleteReport > 0 )
BEGIN
	DELETE tblForumReports
	WHERE
		ReportID	= @ReportID
	RETURN
END

IF( @ReportID > 0 )
BEGIN
	UPDATE tblForumReports SET
		  ReportName	= @ReportName
		, Active		= @Active
		, ReportCommand	= @ReportCommand
		, ReportScript	= @ReportScript
	WHERE
		ReportID	= @ReportID
END
ELSE
BEGIN	
	INSERT INTO tblForumReports ( 
		ReportName, Active, ReportCommand, ReportScript 
	) VALUES (
		@ReportName, @Active, @ReportCommand, @ReportScript
	)	

	SET @ReportID = @@identity
END
RETURN


