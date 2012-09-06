namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence
{
	public static class Sql
	{
		public static string DropSchema
		{
			get
			{
				return @"declare @n char(1)
				set @n = char(10)

				declare @stmt nvarchar(max)

				select @stmt = isnull( @stmt + @n, '' ) +
					'drop procedure [' + name + ']'
				from sys.procedures

				-- check constraints
				select @stmt = isnull( @stmt + @n, '' ) +
					'alter table [' + object_name( parent_object_id ) + '] drop constraint [' + name + ']'
				from sys.check_constraints

				-- functions
				select @stmt = isnull( @stmt + @n, '' ) +
					'drop function [' + name + ']'
				from sys.objects
				where type in ( 'FN', 'IF', 'TF' )

				-- views
				select @stmt = isnull( @stmt + @n, '' ) +
					'drop view [' + name + ']'
				from sys.views

				-- foreign keys
				select @stmt = isnull( @stmt + @n, '' ) +
					'alter table [' + object_name( parent_object_id ) + '] drop constraint [' + name + ']'
				from sys.foreign_keys

				-- tables
				select @stmt = isnull( @stmt + @n, '' ) +
					'drop table [' + name + ']'
				from sys.tables

				-- user defined types
				select @stmt = isnull( @stmt + @n, '' ) +
					'drop type [' + name + ']'
				from sys.types
				where is_user_defined = 1

				exec sp_executesql @stmt";
			}
		}

		public static string CreateBaseFileTable
		{
			get { 
				return 
					@"/****** Object:  Table [dbo].[tblBaseFile]    Script Date: 2012-09-05 10:23:02 ******/
					SET ANSI_NULLS ON
					GO

					SET QUOTED_IDENTIFIER ON
					GO

					CREATE TABLE [dbo].[tblBaseFile](
						[cId] [int] IDENTITY(1,1) NOT NULL,
						[cName] [ntext] NOT NULL,
						[cDirectory] [bit] NULL,
						[cContentInfo] [nvarchar](256) NULL,
						[cKeyWords] [nvarchar](256) NULL,
						[cDescription] [nvarchar](256) NULL,
						[cCreatedBy] [nvarchar](100) NOT NULL,
						[cCreatedDate] [smalldatetime] NOT NULL,
						[cChangedBy] [nvarchar](100) NULL,
						[cChangedDate] [smalldatetime] NULL,
						[cIconType] [int] NULL,
						CONSTRAINT [PK_tblBaseFile] PRIMARY KEY CLUSTERED 
					(
						[cId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

					GO"; 
			}
		}

		public static string CreateBaseLocationLanguageTable
		{
			get { return @"/****** Object:  Table [dbo].[tblBaseLocationsLanguages]    Script Date: 2012-09-05 10:43:57 ******/
					SET ANSI_NULLS ON
					GO

					SET QUOTED_IDENTIFIER ON
					GO

					CREATE TABLE [dbo].[tblBaseLocationsLanguages](
						[cLocationId] [int] NOT NULL,
						[cLanguageId] [int] NOT NULL,
						[cIsDefault] [bit] NOT NULL,
						CONSTRAINT [PK_tblBaseLocationsLanguages] PRIMARY KEY CLUSTERED 
					(
						[cLocationId] ASC,
						[cLanguageId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY]

					GO"; }
		}

		public static string CreateContentPageTable
		{
			get { 
				return 
					@"/****** Object:  Table [dbo].[tblContPage]    Script Date: 2012-09-05 10:27:47 ******/
					SET ANSI_NULLS ON
					GO

					SET QUOTED_IDENTIFIER ON
					GO

					CREATE TABLE [dbo].[tblContPage](
						[cId] [int] IDENTITY(1,1) NOT NULL,
						[cPgeTpeId] [int] NOT NULL,
						[cName] [nvarchar](255) NOT NULL,
						[cSize] [bigint] NOT NULL,
						[cContent] [ntext] NOT NULL,
						[cCreatedBy] [nvarchar](100) NOT NULL,
						[cCreatedDate] [smalldatetime] NOT NULL,
						[cChangedBy] [nvarchar](100) NULL,
						[cChangedDate] [smalldatetime] NULL,
						CONSTRAINT [PK_tblContPage] PRIMARY KEY CLUSTERED 
					(
						[cId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

					GO"; 
			}
		}

		public static string CreateContentTreeTable
		{
			get
			{
				return 
					@"/****** Object:  Table [dbo].[tblContTree]    Script Date: 2012-09-05 10:30:34 ******/
					SET ANSI_NULLS ON
					GO

					SET QUOTED_IDENTIFIER ON
					GO

					CREATE TABLE [dbo].[tblContTree](
						[cId] [int] IDENTITY(1,1) NOT NULL,
						[cPgeId] [int] NULL,
						[cLocId] [int] NOT NULL,
						[cLngId] [int] NULL,
						[cTreTpeId] [int] NOT NULL,
						[cParent] [int] NULL,
						[cName] [nvarchar](255) NOT NULL,
						[cFileName] [ntext] NOT NULL,
						[cDescription] [ntext] NULL,
						[cNote] [nvarchar](256) NULL,
						[cKeywords] [ntext] NULL,
						[cLink] [nvarchar](256) NULL,
						[cTarget] [nvarchar](256) NULL,
						[cHeader] [ntext] NULL,
						[cHideInMenu] [bit] NULL,
						[cExcludeFromSearch] [bit] NULL,
						[cTemplate] [int] NULL,
						[cStylesheet] [int] NULL,
						[cOrder] [int] NOT NULL,
						[cCreatedBy] [nvarchar](100) NOT NULL,
						[cCreatedDate] [smalldatetime] NOT NULL,
						[cChangedBy] [nvarchar](100) NULL,
						[cChangedDate] [smalldatetime] NULL,
						[cApprovedBy] [nvarchar](100) NULL,
						[cApprovedDate] [smalldatetime] NULL,
						[cPublishDate] [smalldatetime] NULL,
						[cPublishedDate] [smalldatetime] NULL,
						[cUnPublishDate] [smalldatetime] NULL,
						[cUnpublishedDate] [smalldatetime] NULL,
						[cLockedBy] [nvarchar](100) NULL,
						[cLockedDate] [smalldatetime] NULL,
						[cCrsTpeId] [int] NOT NULL,
						[cLockFileName] [bit] NOT NULL,
						[cNeedsAuthentication] [bit] NOT NULL,
						[cCssClass] [nvarchar](255) NULL,
						[cPublish] [bit] NOT NULL,
						CONSTRAINT [PK_tblContTree] PRIMARY KEY CLUSTERED 
					(
						[cId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

					GO";
			}
		}

		public static string CreateNewsTable
		{
			get
			{
				return 
					@"/****** Object:  Table [dbo].[tblNews]    Script Date: 2012-09-05 10:33:37 ******/
					SET ANSI_NULLS ON
					GO

					SET QUOTED_IDENTIFIER ON
					GO

					CREATE TABLE [dbo].[tblNews](
						[cId] [int] IDENTITY(1,1) NOT NULL,
						[cNewsType] [smallint] NOT NULL,
						[cHeading] [ntext] NULL,
						[cSummary] [ntext] NULL,
						[cBody] [ntext] NULL,
						[cFormatedBody] [ntext] NULL,
						[cExternalLink] [nvarchar](255) NULL,
						[cStartDate] [datetime] NULL,
						[cEndDate] [datetime] NULL,
						[cCreatedBy] [nvarchar](100) NULL,
						[cCreatedDate] [datetime] NULL,
						[cEditedBy] [nvarchar](100) NULL,
						[cEditedDate] [datetime] NULL,
						[cApprovedBy] [nvarchar](100) NULL,
						[cApprovedDate] [datetime] NULL,
						[cLockedBy] [nvarchar](100) NULL,
						[cLockedDate] [datetime] NULL,
						[cSpotImage] [int] NULL,
						[cSpotHeight] [int] NULL,
						[cSpotWidth] [int] NULL,
						[cSpotAlign] [int] NULL,
						CONSTRAINT [PK_tblNews] PRIMARY KEY CLUSTERED 
					(
						[cId] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

					GO";
			}
		}

		public static string CreateGetPageUrlFunction
		{
			get { 
				return 
					@"/****** Object:  UserDefinedFunction [dbo].[sfContGetFileUrlDownString]    Script Date: 2012-09-05 12:18:15 ******/
						SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE FUNCTION [dbo].[sfContGetFileUrlDownString] (@treId INT)
						RETURNS NVARCHAR (4000)
						AS
							BEGIN
								DECLARE	@retString NVARCHAR (4000),
										@tmpString NVARCHAR (4000),
										@first INT,
										@id INT,
										@locId INT,
										@lngId INT,
										@treTpeId INT,
										@parTreTpeId INT,
										@noOfLngs INT,
										@isDefault BIT,
										@link NVARCHAR (256)
								
								SET @first = 1
								SET @retString = NULL
		
								SET @id = @treId
		
								DECLARE get_tree CURSOR LOCAL FOR
									SELECT	tre.cLocId,
											tre.cLngId,
											tre.cTreTpeId,
											par.cTreTpeId AS ParTreTpeId,
											tre.cLink
									FROM	tblContTree tre
										INNER JOIN tblContTree par
											ON tre.cParent = par.cId
									WHERE	tre.cId = @id
			
								OPEN get_tree
								FETCH NEXT FROM get_tree INTO	@locId,
																@lngId,
																@treTpeId,
																@parTreTpeId,
																@link
										
								IF (@@FETCH_STATUS = -1)
									BEGIN
										CLOSE get_tree
										DEALLOCATE get_tree
				
										DECLARE get_tree_loc CURSOR LOCAL FOR
											SELECT	tre.cLocId,
													tre.cLngId,
													tre.cTreTpeId,
													0 AS ParTreTpeId,
													tre.cLink
											FROM	tblContTree tre
											WHERE	tre.cId = @treId
												AND tre.cTreTpeId = 1
					
										OPEN get_tree_loc
										FETCH NEXT FROM get_tree_loc INTO	@locId,
																			@lngId,
																			@treTpeId,
																			@parTreTpeId,
																			@link
					
				
										IF (@@FETCH_STATUS = -1)
											BEGIN
												CLOSE get_tree_loc
												DEALLOCATE get_tree_loc
				
												RETURN (NULL)
											END
					
										CLOSE get_tree_loc
										DEALLOCATE get_tree_loc
									END
								ELSE
									BEGIN
										CLOSE get_tree
										DEALLOCATE get_tree
									END
		
								IF (@treTpeId = 1)
									BEGIN
										DECLARE get_default CURSOR LOCAL FOR 
											SELECT	tre.cId,
													tre.cLocId,
													tre.cLngId,
													tre.cTreTpeId,
													par.cTreTpeId AS ParTreTpeId,
													tre.cLink
											FROM	tblContTree tre
												INNER JOIN tblContTree par
													ON tre.cParent = par.cId
											WHERE	tre.cLocId = @locId
												AND	tre.cTreTpeId = 5
												AND par.cTreTpeId = 2
												AND par.cLngId = (SELECT	cLanguageId
																  FROM		tblBaseLocationsLanguages
																  WHERE		cLocationId = @locId
																	AND		cIsDefault = 1)
											
										OPEN get_default
										FETCH NEXT FROM get_default INTO	@id,
																			@locId,
																			@lngId,
																			@treTpeId,
																			@parTreTpeId,
																			@link
													
										IF (@@FETCH_STATUS = -1)
											BEGIN
												CLOSE get_default
												DEALLOCATE get_default
						
												RETURN (NULL)
											END
					
										CLOSE get_default
										DEALLOCATE get_default
									END
			
								IF (@treTpeId = 2) OR (@treTpeId = 3)
									BEGIN
										DECLARE get_default CURSOR LOCAL FOR 
											SELECT	tre.cId,
													tre.cLocId,
													tre.cLngId,
													tre.cTreTpeId,
													par.cTreTpeId AS ParTreTpeId,
													tre.cLink
											FROM	tblContTree tre
												INNER JOIN tblContTree par
													ON tre.cParent = par.cId
											WHERE	tre.cParent = @id
												AND	tre.cTreTpeId = 5
				
										OPEN get_default
										FETCH NEXT FROM get_default INTO	@id,
																			@locId,
																			@lngId,
																			@treTpeId,
																			@parTreTpeId,
																			@link
													
										IF (@@FETCH_STATUS = -1)
											BEGIN
												CLOSE get_default
												DEALLOCATE get_default
						
												RETURN (NULL)
											END
					
										CLOSE get_default
										DEALLOCATE get_default
									END
			
								IF (@treTpeId = 9) OR (@treTpeId = 10) OR (@treTpeId = 17)
									BEGIN
										RETURN (NULL)
									END
			
								IF (@treTpeId = 12)
									BEGIN
										RETURN (@link)
									END
		
								DECLARE get_no_lngs CURSOR LOCAL FOR
									SELECT	cIsDefault,
											'Lngs' =
												(SELECT	COUNT (cLanguageId)
												 FROM	tblBaseLocationsLanguages
												 WHERE	cLocationId = @locId)
									FROM	tblBaseLocationsLanguages
									WHERE	cLocationId = @locId
										AND	cLanguageId = @lngId
												
								OPEN get_no_lngs
								FETCH NEXT FROM get_no_lngs INTO	@isDefault,
																	@noOfLngs
											
								IF (@@FETCH_STATUS = -1)
									BEGIN
										CLOSE get_no_lngs
										DEALLOCATE get_no_lngs
				
										RETURN (NULL)
									END
			
								CLOSE get_no_lngs
								DEALLOCATE get_no_lngs
		
								IF (@treTpeId = 5) AND (@parTreTpeId = 2)
									BEGIN
										IF (@isDefault = 1)
											BEGIN
												RETURN ('/index.aspx')
											END
									END
		
								DECLARE get_string CURSOR LOCAL FOR
									SELECT	cFileName,
											cTreTpeId
									FROM	sfContGetTreUp (@id)
			
								OPEN get_string
								FETCH NEXT FROM get_string INTO	@tmpString,
																@treTpeId
		
								WHILE (@@FETCH_STATUS <> -1)
									BEGIN
										IF (@treTpeId = 9) OR (@treTpeId = 10)
											BEGIN
												RETURN (NULL)
											END

										IF (@treTpeId = 2) AND (@noOfLngs = 1)
											BEGIN
												BREAK
											END
			
										IF (@first = 1)
											BEGIN
												SET @retString = @tmpString + '.aspx'
												SET @first = 0
											END
										ELSE
											BEGIN
												SET @retString =  @tmpString + '/' + @retString
											END
				
										FETCH NEXT FROM get_string INTO	@tmpString,
																		@treTpeId
									END
			
								CLOSE get_string
								DEALLOCATE get_string
				
								RETURN ('/' + @retString)	END

						GO"; 
			}
		}

		public static string CreateDefaultBaseLocationLanguageData
		{
			get { return @"INSERT INTO tblBaseLocationsLanguages(cLocationId, cLanguageId ,cIsDefault) VALUES(1,1,1)"; }
		}
	}
}

