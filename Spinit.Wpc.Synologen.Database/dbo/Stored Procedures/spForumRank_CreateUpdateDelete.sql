CREATE procedure spForumRank_CreateUpdateDelete
(
	@RankID				int out,
	@DeleteRank			bit	= 0,
	@RankName			nvarchar(60),
	@PostingCountMin	int,
	@PostingCountMax	int,	
	@RankIconUrl		nvarchar(512)
)
AS

-- are we deleting the rank
IF( @DeleteRank = 1 )
BEGIN

	DELETE tblForumRanks
	WHERE
		RankID	= @RankID

	RETURN
END

-- are we updating the rank
IF( @RankID >  0 )
BEGIN

	UPDATE tblForumRanks SET
		  RankName			= @RankName
		, PostingCountMin	= @PostingCountMin
		, PostingCountMax	= @PostingCountMax
		, RankIconUrl		= @RankIconUrl
	WHERE
		  RankID	= @RankID
		

END
ELSE
BEGIN
	INSERT INTO tblForumRanks (
		RankName, PostingCountMin, PostingCountMax, RankIconUrl
	)
	VALUES( 
		@RankName, @PostingCountMin, @PostingCountMax, @RankIconUrl 
	)

	SET @RankID = @@identity
END

RETURN


