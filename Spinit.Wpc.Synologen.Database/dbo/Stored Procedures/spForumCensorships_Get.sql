CREATE proc spForumCensorships_Get
(
	@Word	nvarchar(40) = ''
)
as
	select
		*
	from
		tblForumCensorship
	WHERE
		Word	= @Word or 
		(
			@Word = '' AND
			1=1
		)


