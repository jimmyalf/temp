create proc spForumSmilies_Get
(
	@SmileyID	int = 0
)
as
	select
		*
	from
		tblForumSmilies
	WHERE
		SmileyID	= @SmileyID OR
		(
			@SmileyID = 0 AND
			1=1
		)



