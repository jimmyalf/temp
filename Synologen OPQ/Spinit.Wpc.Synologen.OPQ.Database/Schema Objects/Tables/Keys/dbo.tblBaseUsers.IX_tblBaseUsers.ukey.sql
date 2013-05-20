ALTER TABLE [dbo].[tblBaseUsers]
    ADD CONSTRAINT [IX_tblBaseUsers] UNIQUE NONCLUSTERED ([cUserName] ASC);

