CREATE proc spForumSmiley_CreateUpdateDelete
(
	  @SmileyID		int out
	, @DeleteSmiley	bit = 0
	, @SmileyCode	nvarchar(20)
	, @SmileyUrl	nvarchar(512)
	, @SmileyText	nvarchar(512)
	, @BracketSafe	bit = 0
)
as

IF( @DeleteSmiley > 0 ) 
BEGIN

	DELETE tblForumSmilies
	WHERE
		SmileyID = @SmileyID

	RETURN
END

IF( @SmileyID > 0 ) 
BEGIN
	UPDATE tblForumSmilies SET
		  SmileyCode	= @SmileyCode
		, SmileyUrl		= @SmileyUrl
		, SmileyText	= @SmileyText
		, BracketSafe	= @BracketSafe
	WHERE
		SmileyID	= @SmileyID
END
ELSE
BEGIN

	INSERT INTO tblForumSmilies (
		SmileyCode, SmileyUrl, SmileyText, BracketSafe
	) VALUES (
		@SmileyCode, @SmileyUrl, @SmileyText, @BracketSafe
	)

	SET @SmileyID = @@identity
END
RETURN



