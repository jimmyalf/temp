-- =============================================
-- Create test data
-- =============================================

SET IDENTITY_INSERT dbo.SynologenOpqNodes ON

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	1, NULL, 'Test-Root1', 1, 1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	2, NULL, 'Test-Root2', 1, 1, 'Admin', GETDATE ()) 
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	3, 1, 'Test-Root1-Child1', 1, 1, 'Admin', GETDATE ()) 
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	4, 1, 'Test-Root1-Child2', 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	5, 3, 'Test-Root1-Child1-Child1', 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	6, 3, 'Test-Root1-Child1-Child2', 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	7, 5, 'Test-Root1-Child1-Child1-Child1', 1, 1, 'Admin', GETDATE ()) 

SET IDENTITY_INSERT dbo.SynologenOpqNodes OFF

SET IDENTITY_INSERT dbo.SynologenOpqFileCategories ON

INSERT INTO dbo.SynologenOpqFileCategories (
	Id, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	1, 'Test Category', 1, 1, 'Admin', GETDATE ()) 

SET IDENTITY_INSERT dbo.SynologenOpqFileCategories OFF

