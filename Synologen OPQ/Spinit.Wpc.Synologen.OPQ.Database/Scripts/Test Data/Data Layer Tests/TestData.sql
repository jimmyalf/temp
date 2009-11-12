-- =============================================
-- Create test data
-- =============================================

------------------------------------------------
-- Inserts into Base Location
------------------------------------------------

SET IDENTITY_INSERT dbo.tblBaseLocations ON 

INSERT INTO dbo.tblBaseLocations (
	cId, cName, cDescription, cAllowCrossPublishing, cInfoName, cInfoAdress, cInfoVisitAdress,
	cInfoZipCode, cInfoCity, cInfoPhone, cInfoFax, cInfoEmail, cInfoCopyRightInfo, cInfoWebMaster,
	cAlias1, cAlias2, cAlias3, cPublishPath, cRootPath, cPublishActive, cFtpPublishActive,
	cFtpPassive, cFtpUserName, cFtpPassword, cFtpSite, cExtranet, cDocType, cDocSubType) 
VALUES ( 
	1, 'www.test.se', NULL, 1, 'Test', NULL, NULL,
	NULL, NULL, NULL, NULL, NULL, NULL, NULL,
	'http://www.test.se', NULL, NULL, 'c:\Test', NULL, 1, 0,
	1, NULL, NULL, NULL, 0, 2, 1) 

SET IDENTITY_INSERT dbo.tblBaseLocations OFF

------------------------------------------------
-- Inserts into Base User
------------------------------------------------

SET IDENTITY_INSERT dbo.tblBaseUsers ON

INSERT INTO dbo.tblBaseUsers (
	cId, cUserName, cPassword, cFirstName, cLastName, cEmail, cDefaultLocation, cActive,
	cCreatedBy, cCreatedDate) 
VALUES ( 
	1, 'Admin', 'g@llum', 'Admin', 'Spinit', 'info@spinit.se', 1, 1,
	'Admin', GETDATE ()) 

SET IDENTITY_INSERT dbo.tblBaseUsers OFF

------------------------------------------------
-- Inserts into Base File
------------------------------------------------

SET IDENTITY_INSERT dbo.tblBaseFile ON

INSERT INTO dbo.tblBaseFile (
	cId, cName, cDirectory, cContentInfo, cKeyWords, cDescription, cCreatedBy, cCreatedDate, cIconType) 
VALUES ( 
	1, 'Test_1', 1, NULL, NULL, NULL, 'Admin', GETDATE (), NULL) 
	
INSERT INTO dbo.tblBaseFile (
	cId, cName, cDirectory, cContentInfo, cKeyWords, cDescription, cCreatedBy, cCreatedDate, cIconType) 
VALUES ( 
	2, 'Test_2', 1, NULL, NULL, NULL, 'Admin', GETDATE (), NULL) 

SET IDENTITY_INSERT dbo.tblBaseFile OFF

------------------------------------------------
-- Inserts into nodes
------------------------------------------------

SET IDENTITY_INSERT dbo.SynologenOpqNodes ON

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate) 
VALUES (
	1, NULL, 'Test-Root1', 1, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	2, NULL, 'Test-Root2', 1, 0, 1, 'Admin', GETDATE ()) 
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate) 
VALUES (
	3, 1, 'Test-Root1-Child1', 1, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ()) 
	
INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate) 
VALUES (
	4, 1, 'Test-Root1-Child2', 0, 1, 1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate) 
VALUES (
	5, 3, 'Test-Root1-Child1-Child1', 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate) 
VALUES (
	6, 3, 'Test-Root1-Child1-Child2', 1, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqNodes (
	Id, Parent, [Name], IsMenu, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate) 
VALUES (
	7, 5, 'Test-Root1-Child1-Child1-Child1', 1, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ()) 

SET IDENTITY_INSERT dbo.SynologenOpqNodes OFF

------------------------------------------------
-- Inserts into nodes
------------------------------------------------

INSERT INTO dbo.SynologenOpqNodeSupplierConnections (
	NdeId, SupId)
VALUES ( 
	1, 1) 

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
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate)
VALUES ( 
	1, 0, 1, 1, 1, NULL, NULL, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ())
	
INSERT INTO dbo.SynologenOpqFiles (
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate)
VALUES ( 
	2, 0, 1, 1, 1, NULL, NULL, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqFiles (
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate,
	ApprovedById, ApprovedByName, ApprovedDate)
VALUES ( 
	3, 0, 1, 1, 1, NULL, NULL, 1, 1, 'Admin', GETDATE (),
	1, 'Admin', GETDATE ()) 

INSERT INTO dbo.SynologenOpqFiles (
	Id, [Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate)
VALUES ( 
	4, 0, 1, 1, 1, NULL, NULL, 0, 1, 'Admin', GETDATE ()) 

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

