CREATE proc spForumCensorship_CreateUpdateDelete
(
	  @Word			nvarchar(40)
	, @DeleteWord 	bit = 0
	, @Replacement	nvarchar(40)
)
as
SET NOCOUNT ON

if( @DeleteWord > 0 )
BEGIN
	DELETE FROM
		tblForumCensorship
	WHERE
		Word = @Word
	RETURN
END
ELSE
BEGIN
	UPDATE tblForumCensorship SET
		Replacement	= @Replacement
	WHERE
		Word	= @Word

	IF( @@rowcount = 0 )
	BEGIN
	INSERT INTO tblForumCensorship (
		Word, Replacement
	) VALUES (
		@Word, @Replacement
	)
	END
END


