-- =============================================
-- Create test data
-- =============================================

------------------------------------------------
-- Inserts into nodes
------------------------------------------------

SET IDENTITY_INSERT dbo.SynologenOpqNodes ON

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	1, NULL, 'Test-Root1', 1, 1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	2, NULL, 'Test-Root2', 0, 1, 'Admin', GETDATE ()) 
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	3, 1, 'Test-Root1-Child1', 1, 1, 'Admin', GETDATE ()) 
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	4, 1, 'Test-Root1-Child2', 0, 1, 'Admin', GETDATE ()) 

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

------------------------------------------------
-- Inserts into file-categories
------------------------------------------------

SET IDENTITY_INSERT dbo.SynologenOpqFileCategories ON

INSERT INTO dbo.SynologenOpqFileCategories (
	Id, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	1, 'Test Category 1', 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqFileCategories (
	Id, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	2, 'Test Category 2', 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqFileCategories (
	Id, [Name], IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	3, 'Test Category 3', 0, 1, 'Admin', GETDATE ()) 

SET IDENTITY_INSERT dbo.SynologenOpqFileCategories OFF

------------------------------------------------
-- Inserts into files
------------------------------------------------

SET IDENTITY_INSERT dbo.SynologenOpqFiles ON

INSERT INTO dbo.SynologenOpqFiles (
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate)
VALUES ( 
	1, 0, 1, 1, 1, NULL, NULL, 1, 1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqFiles (
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate)
VALUES ( 
	2, 0, 1, 1, 1, NULL, NULL, 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqFiles (
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate)
VALUES ( 
	3, 0, 1, 1, 1, NULL, NULL, 1, 1, 'Admin', GETDATE ()) 

SET IDENTITY_INSERT dbo.SynologenOpqFiles OFF

------------------------------------------------
-- Inserts into documents
------------------------------------------------

SET IDENTITY_INSERT dbo.SynologenOpqDocuments ON

INSERT INTO dbo.SynologenOpqDocuments (
	Id, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate) 
VALUES ( 
	1, 1, NULL, NULL, 1, 'Test-Content-1', 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE (), NULL, NULL, NULL) 

INSERT INTO dbo.SynologenOpqDocuments (
	Id, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
	ChangedById, ChangedByName, ChangedDate, ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate) 
VALUES ( 
	2, 2, NULL, NULL, 1, 'Test-Content-2', 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE (), 1, 'Admin', GETDATE (), NULL, NULL, NULL) 

INSERT INTO dbo.SynologenOpqDocuments (
	Id, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate) 
VALUES ( 
	3, 3, NULL, NULL, 1, 'Test-Content-3', 1, 1, 'Admin', GETDATE (),
	NULL, NULL, NULL, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqDocuments (
	Id, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate) 
VALUES ( 
	4, 4, NULL, NULL, 1, 'Test-Content-4', 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE (), 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqDocuments (
	Id, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate) 
VALUES ( 
	5, 5, NULL, NULL, 1, 'Test-Content-5', 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE (), NULL, NULL, NULL) 

INSERT INTO dbo.SynologenOpqDocuments (
	Id, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate, LockedById, LockedByName, LockedDate) 
VALUES ( 
	6, 5, NULL, NULL, 1, 'Test-Content-6', 0, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE (), NULL, NULL, NULL) 

SET IDENTITY_INSERT dbo.SynologenOpqDocuments OFF

------------------------------------------------
-- Inserts into document histories
------------------------------------------------

INSERT INTO dbo.SynologenOpqDocumentHistories (
	Id, HistoryDate, HistoryId, HistoryName, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive,
	CreatedById, CreatedByName, CreatedDate, ApprovedById, ApprovedByName, ApprovedDate, 
	LockedById, LockedByName, LockedDate)
VALUES ( 
	1, DATEADD (DAY, -1, GETDATE ()), 1, 'Admin', 1, NULL, NULL, 1, 'Test-Content-History-1-1', 1,
	1, 'Admin', GETDATE (), 1, 'Admin', DATEADD (DAY, -1, GETDATE ()),
	1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqDocumentHistories (
	Id, HistoryDate, HistoryId, HistoryName, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive,
	CreatedById, CreatedByName, CreatedDate, ApprovedById, ApprovedByName, ApprovedDate, 
	LockedById, LockedByName, LockedDate)
VALUES ( 
	1, DATEADD (DAY, -2, GETDATE ()), 1, 'Admin', 1, NULL, NULL, 1, 'Test-Content-History-1-2', 1,
	1, 'Admin', GETDATE (), 1, 'Admin', DATEADD (DAY, -2, GETDATE ()),
	NULL, NULL, NULL)

INSERT INTO dbo.SynologenOpqDocumentHistories (
	Id, HistoryDate, HistoryId, HistoryName, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive,
	CreatedById, CreatedByName, CreatedDate, ApprovedById, ApprovedByName, ApprovedDate, 
	LockedById, LockedByName, LockedDate)
VALUES ( 
	1, DATEADD (DAY, -3, GETDATE ()), 1, 'Admin', 1, NULL, NULL, 1, 'Test-Content-History-1-3', 1,
	1, 'Admin', GETDATE (), 1, 'Admin', DATEADD (DAY, -3, GETDATE ()),
	NULL, NULL, NULL)

INSERT INTO dbo.SynologenOpqDocumentHistories (
	Id, HistoryDate, HistoryId, HistoryName, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive,
	CreatedById, CreatedByName, CreatedDate, ApprovedById, ApprovedByName, ApprovedDate, 
	LockedById, LockedByName, LockedDate)
VALUES ( 
	3, DATEADD (DAY, -1, GETDATE ()), 1, 'Admin', 3, NULL, NULL, 1, 'Test-Content-History-3-1', 1,
	1, 'Admin', GETDATE (), 1, 'Admin', DATEADD (DAY, -1, GETDATE ()),
	1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqDocumentHistories (
	Id, HistoryDate, HistoryId, HistoryName, NdeId, ShpId, CncId, DocTpeId, DocumentContent, IsActive,
	CreatedById, CreatedByName, CreatedDate, ApprovedById, ApprovedByName, ApprovedDate, 
	LockedById, LockedByName, LockedDate)
VALUES ( 
	3, DATEADD (DAY, -2, GETDATE ()), 1, 'Admin', 3, NULL, NULL, 1, 'Test-Content-History-3-2', 1,
	1, 'Admin', GETDATE (), 1, 'Admin', DATEADD (DAY, -2, GETDATE ()),
	NULL, NULL, NULL) 

