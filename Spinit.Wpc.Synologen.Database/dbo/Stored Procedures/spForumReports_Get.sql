create proc spForumReports_Get
(
	@ReportID	int = 0
)
as
	select
		*
	FROM
		tblForumReports
	WHERE
		ReportID = @ReportID or
		( 
			@ReportID = 0 AND
			1=1
		)


