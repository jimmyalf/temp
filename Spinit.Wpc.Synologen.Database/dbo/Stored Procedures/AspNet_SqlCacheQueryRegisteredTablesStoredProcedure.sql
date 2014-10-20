create PROCEDURE AspNet_SqlCacheQueryRegisteredTablesStoredProcedure 
         AS
         SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification
