-- =============================================
-- Clean the database from all test-data
-- =============================================

------------------------------------------------
-- Cleans the Synologen OPQ tables
------------------------------------------------

DELETE FROM dbo.SynologenOpqDocumentHistories
GO

DELETE FROM dbo.SynologenOpqDocuments
GO

DELETE FROM dbo.SynologenOpqFiles
GO

DELETE FROM dbo.SynologenOpqFileCategories
GO

DELETE FROM dbo.SynologenOpqNodeSupplierConnections
GO

DELETE FROM dbo.SynologenOpqNodes
GO

------------------------------------------------
-- Cleans the Base tables
------------------------------------------------

DELETE FROM dbo.tblBaseFile
GO

DELETE FROM dbo.tblBaseUsers
GO

DELETE FROM dbo.tblBaseLocations
GO