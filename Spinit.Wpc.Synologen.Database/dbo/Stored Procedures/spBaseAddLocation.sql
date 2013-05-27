CREATE PROCEDURE spBaseAddLocation 
					@name NVARCHAR(256),
					@description NVARCHAR(256),
					@allowCrossPublishing BIT,
					@infoName NVARCHAR(256),
					@infoAdress NVARCHAR(256),
					@infoVisitAdress NVARCHAR(256),
					@infoZipCode NVARCHAR(256),
					@infoCity NVARCHAR(256),
					@infoPhone NVARCHAR(256),
					@infoFax NVARCHAR(256),
					@infoEmail NVARCHAR(256),
					@infoCopyRightInfo NVARCHAR(256),
					@infoWebmaster NVARCHAR(256),
					@alias1 NVARCHAR(256),
					@alias2 NVARCHAR(256),
					@alias3 NVARCHAR(256),
					@publishPath NVARCHAR(256),
					@relativePath NVARCHAR(256),
					@sitePath NVARCHAR(255),
					@publishActive BIT,
					@ftpPublishActive BIT,
					@ftpPassive BIT,
					@ftpUserName NVARCHAR(256),
					@ftpPassword NVARCHAR(256),
					@ftpSite NVARCHAR(256),
					@extranet BIT,
					@docType INT,
					@docSubType INT,
					@frontType INT,
					@status INT OUTPUT,
					@id INT OUTPUT
							
AS
BEGIN
	INSERT INTO tblBaseLocations
		(cName,	cDescription, cAllowCrossPublishing, cInfoName, cInfoAdress,
		 cInfoVisitAdress, cInfoZipCode, cInfoCity, cInfoPhone, cInfoFax,
		 cInfoEmail, cInfoCopyRightInfo, cInfoWebmaster, cAlias1, cAlias2,
		 cAlias3, cPublishPath, cRelativePath, cSitePath, cPublishActive, cFtpPublishActive,
		 cFtpPassive, cFtpUserName, cFtpPassword, cFtpSite, cExtranet,
		 cDocType, cDocSubType, cFrontType)				
	VALUES
		(@name, @description, @allowCrossPublishing, @infoName, @infoAdress,
		 @infoVisitAdress, @infoZipCode, @infoCity,	@infoPhone, @infoFax,
		 @infoEmail, @infoCopyRightInfo, @infoWebmaster, @alias1, @alias2,
		 @alias3, @publishPath, @relativePath, @sitePath, @publishActive, @ftpPublishActive,
		 @ftpPassive, @ftpUserName, @ftpPassword, @ftpSite, @extranet,
		 @docType, @docSubType, @frontType)
	
	SELECT @id = @@IDENTITY
	
	IF (@@ERROR = 0)
		BEGIN
			SET @status = 0
		END
	ELSE
		BEGIN
			SET @status = @@ERROR
			SET @id = 0
		END
END
