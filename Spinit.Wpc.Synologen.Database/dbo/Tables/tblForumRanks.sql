CREATE TABLE [dbo].[tblForumRanks] (
    [RankID]          INT            IDENTITY (1, 1) NOT NULL,
    [RankName]        NVARCHAR (30)  NULL,
    [PostingCountMin] INT            NULL,
    [PostingCountMax] INT            NULL,
    [RankIconUrl]     NVARCHAR (256) NULL,
    CONSTRAINT [PK_RANK_ID] PRIMARY KEY CLUSTERED ([RankID] ASC),
    CONSTRAINT [UK_RANK_NAME] UNIQUE NONCLUSTERED ([RankName] ASC)
);

