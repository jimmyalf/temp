namespace Synologen.Maintenance.UpgradeWpc2012.Test.Persistence
{
	public class Sql : Spinit.Wpc.Maintenance.FileAndContentMigration.Test.Sql
	{
		public static string CreateOPQDocumentTable
		{
			get
			{
				return @"/****** Object:  Table [dbo].[SynologenOpqDocuments]    Script Date: 2012-09-07 14:10:21 ******/
						SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE TABLE [dbo].[SynologenOpqDocuments](
							[Id] [int] IDENTITY(1,1) NOT NULL,
							[NdeId] [int] NOT NULL,
							[ShpId] [int] NULL,
							[CncId] [int] NULL,
							[ShopGroupId] [int] NULL,
							[DocTpeId] [int] NOT NULL,
							[DocumentContent] [ntext] NOT NULL,
							[IsActive] [bit] NOT NULL,
							[CreatedById] [int] NOT NULL,
							[CreatedByName] [nvarchar](100) NOT NULL,
							[CreatedDate] [datetime] NOT NULL,
							[ChangedById] [int] NULL,
							[ChangedByName] [nvarchar](100) NULL,
							[ChangedDate] [datetime] NULL,
							[ApprovedById] [int] NULL,
							[ApprovedByName] [nvarchar](100) NULL,
							[ApprovedDate] [datetime] NULL,
							[LockedById] [int] NULL,
							[LockedByName] [nvarchar](100) NULL,
							[LockedDate] [datetime] NULL,
						 CONSTRAINT [SynologenOpqDocuments_PK] PRIMARY KEY CLUSTERED 
						(
							[Id] ASC
						)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
						) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

						GO";
			}
		}

		public static string CreateOPQDocumentHistoryTable
		{
			get { 
				return @"/****** Object:  Table [dbo].[SynologenOpqDocumentHistories]    Script Date: 2012-09-07 14:36:51 ******/
						SET ANSI_NULLS ON
						GO

						SET QUOTED_IDENTIFIER ON
						GO

						CREATE TABLE [dbo].[SynologenOpqDocumentHistories](
							[Id] [int] NOT NULL,
							[HistoryDate] [datetime] NOT NULL,
							[HistoryId] [int] NOT NULL,
							[HistoryName] [nvarchar](100) NOT NULL,
							[NdeId] [int] NOT NULL,
							[ShpId] [int] NULL,
							[CncId] [int] NULL,
							[ShopGroupId] [int] NULL,
							[DocTpeId] [int] NOT NULL,
							[DocumentContent] [ntext] NOT NULL,
							[IsActive] [bit] NOT NULL,
							[CreatedById] [int] NOT NULL,
							[CreatedByName] [nvarchar](100) NOT NULL,
							[CreatedDate] [datetime] NOT NULL,
							[ChangedById] [int] NULL,
							[ChangedByName] [nvarchar](100) NULL,
							[ChangedDate] [datetime] NULL,
							[ApprovedById] [int] NULL,
							[ApprovedByName] [nvarchar](100) NULL,
							[ApprovedDate] [datetime] NULL,
							[LockedById] [int] NULL,
							[LockedByName] [nvarchar](100) NULL,
							[LockedDate] [datetime] NULL,
						 CONSTRAINT [SynologenOpqDocumentHistories_PK] PRIMARY KEY CLUSTERED 
						(
							[Id] ASC,
							[HistoryDate] ASC
						)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
						) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

						GO";
			}
		}
	}
}

