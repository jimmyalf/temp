-- =============================================
-- Create test data
-- =============================================
SET IDENTITY_INSERT dbo.tblBaseUsers ON
	
INSERT INTO tblBaseUsers
	(cId,cUserName,cPassword,cFirstName,cLastName,cEmail,cDefaultLocation,cActive,cCreatedBy,cCreatedDate,cChangedBy,cChangedDate)
	 VALUES 
	 (1,'admin','admin',null,null,null,null,1,null,null,null,null) 

SET IDENTITY_INSERT dbo.tblBaseUsers OFF

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

SET IDENTITY_INSERT dbo.tblSynologenShopCategory ON

INSERT INTO [dbo].[tblSynologenShopCategory]
           (cId,cName)
     VALUES (1, 'Synolog')

SET IDENTITY_INSERT dbo.tblSynologenShopCategory OFF

SET IDENTITY_INSERT dbo.[tblSynologenShop] ON

INSERT INTO [dbo].[tblSynologenShop]
           (cId,cCategoryId,cShopName)
     VALUES
			(1,1,'Synologenen Butiken AB')

SET IDENTITY_INSERT dbo.[tblSynologenShop] OFF
