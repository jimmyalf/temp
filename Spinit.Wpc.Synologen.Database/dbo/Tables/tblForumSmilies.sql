CREATE TABLE [dbo].[tblForumSmilies] (
    [SmileyID]    INT            IDENTITY (1, 1) NOT NULL,
    [SmileyCode]  NVARCHAR (10)  NOT NULL,
    [SmileyUrl]   NVARCHAR (256) NOT NULL,
    [SmileyText]  NVARCHAR (256) NULL,
    [BracketSafe] BIT            CONSTRAINT [DF__forums_Sm__Brack__173876EA] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_SMILIES_ID] PRIMARY KEY CLUSTERED ([SmileyID] ASC)
);

