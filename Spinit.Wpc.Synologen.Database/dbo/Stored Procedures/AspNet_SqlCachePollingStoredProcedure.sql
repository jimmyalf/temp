create PROCEDURE AspNet_SqlCachePollingStoredProcedure AS
         SELECT tableName, changeId FROM dbo.AspNet_SqlCacheTablesForChangeNotification
         RETURN 0

GO
GRANT EXECUTE
    ON OBJECT::[dbo].[AspNet_SqlCachePollingStoredProcedure] TO [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]
    AS [dbo];

