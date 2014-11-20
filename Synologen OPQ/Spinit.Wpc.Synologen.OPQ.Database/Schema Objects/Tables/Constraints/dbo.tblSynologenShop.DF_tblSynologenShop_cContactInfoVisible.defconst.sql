ALTER TABLE [dbo].[tblSynologenShop]
    ADD CONSTRAINT [DF_tblSynologenShop_cContactInfoVisible] DEFAULT (1) FOR [cActive];

