ALTER TABLE [dbo].[tblBaseLocations]
    ADD CONSTRAINT [DF_tblBaseLocations_cExtranet] DEFAULT (0) FOR [cExtranet];

