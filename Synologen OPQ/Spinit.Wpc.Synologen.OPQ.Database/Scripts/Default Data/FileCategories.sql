-- =============================================
-- Create default document-types
-- =============================================

SET IDENTITY_INSERT dbo.SynologenOpqFileCategories ON

INSERT INTO dbo.SynologenOpqFileCategories (
	[Id], [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES ( 
	1, 'Centrala dokument', 1, 1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqFileCategories (
	[Id], [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES ( 
	2, 'Egna dokument', 1, 1, 'Admin', GETDATE ())

INSERT INTO dbo.SynologenOpqFileCategories (
	[Id], [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES ( 
	3, 'Ifyllda dokument', 1, 1, 'Admin', GETDATE ())

SET IDENTITY_INSERT dbo.SynologenOpqFileCategories OFF
