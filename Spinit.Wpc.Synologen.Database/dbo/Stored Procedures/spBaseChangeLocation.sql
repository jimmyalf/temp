CREATE PROCEDURE spBaseChangeLocation
					@id INT,
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
					@status INT OUTPUT
AS
BEGIN		
	UPDATE	tblBaseLocations			
	SET		cName = @name,
			cDescription = @description,
			cAllowCrossPublishing = @allowCrossPublishing,
			cInfoName = @infoName,
			cInfoAdress = @infoAdress,
			cInfoVisitAdress = @infoVisitAdress,
			cInfoZipCode = @infoZipCode,
			cInfoCity = @infoCity,
			cInfoPhone = @infoPhone,
			cInfoFax = @infoFax,
			cInfoEmail = @infoEmail,
			cInfoCopyRightInfo = @infoCopyRightInfo,
			cInfoWebmaster = @infoWebmaster,
			cAlias1 = @alias1,
			cAlias2 = @alias2,
			cAlias3 = @alias3,
			cPublishPath = @publishPath,
			cRelativePath = @relativePath,
			cSitePath = @sitePath,
			cPublishActive = @publishActive,
			cFtpPublishActive = @ftpPublishActive,
			cFtpPassive = @ftpPassive,
			cFtpUserName = @ftpUserName,
			cFtpPassword = @ftpPassword,
			cFtpSite = @ftpSite,
			cExtranet = @extranet,
			cDocType = @docType,
			cDocSubType = @docSubType,
			cFrontType = @frontType
	WHERE	cId = @id

	IF (@@ERROR = 0)
		BEGIN
			SELECT @status = 0
		END
	ELSE
		BEGIN
			SELECT @status = @@ERROR
		END
END
