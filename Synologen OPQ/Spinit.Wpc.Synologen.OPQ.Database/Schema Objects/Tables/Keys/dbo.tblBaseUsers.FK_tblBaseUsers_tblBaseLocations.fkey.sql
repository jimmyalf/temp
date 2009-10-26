ALTER TABLE [dbo].[tblBaseUsers]
    ADD CONSTRAINT [FK_tblBaseUsers_tblBaseLocations] FOREIGN KEY ([cDefaultLocation]) REFERENCES [dbo].[tblBaseLocations] ([cId]) ON DELETE NO ACTION ON UPDATE NO ACTION;



