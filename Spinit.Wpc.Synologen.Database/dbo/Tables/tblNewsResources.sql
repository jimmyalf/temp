CREATE TABLE [dbo].[tblNewsResources] (
    [cId]         INT            NOT NULL,
    [cLanguageId] INT            NOT NULL,
    [cResource]   NVARCHAR (255) NOT NULL
);


GO
create TRIGGER tblNewsResources_AspNet_SqlCacheNotification_Trigger ON tblNewsResources
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'tblNewsResources'
                       END
