create proc spForumStyle_CreateUpdateDelete
(
	  @StyleID				int out
	, @DeleteStyle			bit = 0
	, @StyleName			varchar(30)
	, @StyleSheetTemplate	varchar(30)
	, @BodyBackgroundColor	int
	, @BodyTextColor		int
	, @LinkVisited			int
	, @LinkHover			int
	, @LinkActive			int
	, @RowColorPrimary		int
	, @RowColorSecondary	int
	, @RowColorTertiary		int
	, @RowClassPrimary		varchar(30)
	, @RowClassSecondary	varchar(30)
	, @RowClassTertiary		varchar(30)
	, @HeaderColorPrimary	int
	, @HeaderColorSecondary	int
	, @HeaderColorTertiary	int
	, @HeaderStylePrimary	varchar(30)
	, @HeaderStyleSecondary	varchar(30)
	, @HeaderStyleTertiary	varchar(30)
	, @CellColorPrimary		int
	, @CellColorSecondary	int
	, @CellColorTertiary	int
	, @CellClassPrimary		varchar(30)
	, @CellClassSecondary	varchar(30)
	, @CellClassTertiary	varchar(30)
	, @FontFacePrimary		varchar(30)	
	, @FontFaceSecondary	varchar(30)
	, @FontFaceTertiary		varchar(30)
	, @FontSizePrimary		smallint
	, @FontSizeSecondary	smallint
	, @FontSizeTertiary		smallint
	, @FontColorPrimary		int
	, @FontColorSecondary	int
	, @FontColorTertiary	int
	, @SpanClassPrimary		varchar(30)
	, @SpanClassSecondary	varchar(30)
	, @SpanClassTertiary	varchar(30)
)
AS

IF( @DeleteStyle = 1 ) 
BEGIN
	
	DELETE tblForumStyles
	WHERE
		StyleID	= @StyleID

	RETURN
END

IF( @StyleID > 0 )
BEGIN

	UPDATE tblForumStyles SET
		  StyleName 			= @StyleName
		, StyleSheetTemplate 	= @StyleSheetTemplate
		, BodyBackgroundColor 	= @BodyBackgroundColor
		, BodyTextColor 		= @BodyTextColor
		, LinkVisited 			= @LinkVisited
		, LinkHover 			= @LinkHover
		, LinkActive 			= @LinkActive
		, RowColorPrimary 		= @RowColorPrimary
		, RowColorSecondary 	= @RowColorSecondary
		, RowColorTertiary 		= @RowColorTertiary
		, RowClassPrimary 		= @RowClassPrimary
		, RowClassSecondary 	= @RowClassSecondary
		, RowClassTertiary 		= @RowClassTertiary
		, HeaderColorPrimary 	= @HeaderColorPrimary
		, HeaderColorSecondary	= @HeaderColorSecondary
		, HeaderColorTertiary 	= @HeaderColorTertiary
		, HeaderStylePrimary 	= @HeaderStylePrimary
		, HeaderStyleSecondary 	= @HeaderStyleSecondary
		, HeaderStyleTertiary 	= @HeaderStyleTertiary
		, CellColorPrimary 		= @CellColorPrimary
		, CellColorSecondary	= @CellColorSecondary
		, CellColorTertiary 	= @CellColorTertiary
		, CellClassPrimary 		= @CellClassPrimary
		, CellClassSecondary 	= @CellClassSecondary
		, CellClassTertiary 	= @CellClassTertiary
		, FontFacePrimary 		= @FontFacePrimary
		, FontFaceSecondary 	= @FontFaceSecondary
		, FontFaceTertiary 		= @FontFaceTertiary
		, FontSizePrimary 		= @FontSizePrimary
		, FontSizeSecondary 	= @FontSizeSecondary
		, FontSizeTertiary 		= @FontSizeTertiary
		, FontColorPrimary 		= @FontColorPrimary
		, FontColorSecondary 	= @FontColorSecondary
		, FontColorTertiary 	= @FontColorTertiary
		, SpanClassPrimary 		= @SpanClassPrimary
		, SpanClassSecondary 	= @SpanClassSecondary
		, SpanClassTertiary 	= @SpanClassTertiary
	WHERE
		StyleID = @StyleID


END
ELSE
BEGIN

	INSERT INTO tblForumStyles (
		  StyleName				, StyleSheetTemplate		, BodyBackgroundColor		, BodyTextColor
		, LinkVisited			, LinkHover					, LinkActive				, RowColorPrimary
		, RowColorSecondary		, RowColorTertiary			, RowClassPrimary			, RowClassSecondary
		, RowClassTertiary		, HeaderColorPrimary		, HeaderColorSecondary		, HeaderColorTertiary
		, HeaderStylePrimary	, HeaderStyleSecondary		, HeaderStyleTertiary		, CellColorPrimary
		, CellColorSecondary	, CellColorTertiary			, CellClassPrimary			, CellClassSecondary
		, CellClassTertiary		, FontFacePrimary			, FontFaceSecondary			, FontFaceTertiary
		, FontSizePrimary		, FontSizeSecondary			, FontSizeTertiary			, FontColorPrimary
		, FontColorSecondary	, FontColorTertiary			, SpanClassPrimary			, SpanClassSecondary
		, SpanClassTertiary
	) VALUES (
		  @StyleName			, @StyleSheetTemplate		, @BodyBackgroundColor		, @BodyTextColor		, @LinkVisited			, @LinkHover				, @LinkActive				, @RowColorPrimary
		, @RowColorSecondary	, @RowColorTertiary			, @RowClassPrimary			, @RowClassSecondary
		, @RowClassTertiary		, @HeaderColorPrimary		, @HeaderColorSecondary		, @HeaderColorTertiary
		, @HeaderStylePrimary	, @HeaderStyleSecondary		, @HeaderStyleTertiary		, @CellColorPrimary
		, @CellColorSecondary	, @CellColorTertiary		, @CellClassPrimary			, @CellClassSecondary
		, @CellClassTertiary	, @FontFacePrimary			, @FontFaceSecondary		, @FontFaceTertiary
		, @FontSizePrimary		, @FontSizeSecondary		, @FontSizeTertiary			, @FontColorPrimary
		, @FontColorSecondary	, @FontColorTertiary		, @SpanClassPrimary			, @SpanClassSecondary
		, @SpanClassTertiary
	)

	SELECT @StyleID = @@IDENTITY
END
RETURN


