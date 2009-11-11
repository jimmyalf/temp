-- =============================================
-- Create test data
-- =============================================
SET IDENTITY_INSERT dbo.tblBaseUsers ON
	
INSERT INTO tblBaseUsers
	(cId,cUserName,cPassword,cFirstName,cLastName,cEmail,cDefaultLocation,cActive,cCreatedBy,cCreatedDate,cChangedBy,cChangedDate)
	 VALUES 
	 (1,'admin','admin',null,null,null,null,1,null,null,null,null) 

SET IDENTITY_INSERT dbo.tblBaseUsers ON


