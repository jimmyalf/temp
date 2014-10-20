-- =============================================
-- Create default document-types
-- =============================================

SET IDENTITY_INSERT dbo.SynologenOpqDocumentTypes ON

INSERT INTO dbo.SynologenOpqDocumentTypes (
	[Id], [Name]) 
VALUES ( 
	1, 'Rutin')
	
INSERT INTO dbo.SynologenOpqDocumentTypes (
	[Id], [Name]) 
VALUES ( 
	2, 'Förbättring')

SET IDENTITY_INSERT dbo.SynologenOpqDocumentTypes OFF
