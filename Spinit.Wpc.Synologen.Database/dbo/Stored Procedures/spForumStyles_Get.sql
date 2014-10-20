create proc spForumStyles_Get
(
	@StyleID	int = 0
)
as
	select
		*
	from
		tblForumStyles
	WHERE
		StyleID = @StyleID OR
		(
			@StyleID = 0 AND
			1=1
		)



