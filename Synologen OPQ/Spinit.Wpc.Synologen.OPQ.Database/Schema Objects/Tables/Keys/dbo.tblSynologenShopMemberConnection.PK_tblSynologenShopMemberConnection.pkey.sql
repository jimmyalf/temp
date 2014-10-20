ALTER TABLE [dbo].[tblSynologenShopMemberConnection]
    ADD CONSTRAINT [PK_tblSynologenShopMemberConnection] PRIMARY KEY CLUSTERED ([cSynologenShopId] ASC, [cMemberId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

