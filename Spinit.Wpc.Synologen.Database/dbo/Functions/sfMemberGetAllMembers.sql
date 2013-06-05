/*
	Type 0 - All Members, Find defaultlanguage or other
	Type 1 - All Members only in correct language

*/


CREATE FUNCTION sfMemberGetAllMembers (@type INT, 
@locationid INT, 
@languageid INT)
RETURNS @memberList table
(
	cId int not null,
	cMemberId int not null,
	cDescription nvarchar(255),
	cAddress nvarchar(255),
	cZipcode nvarchar(50),
	cCity nvarchar(50),
	cPhone nvarchar(50),
	cFax nvarchar(50),
	cMobile nvarchar(50),
	cEmail nvarchar(50),
	cWww nvarchar(255),
	cVoip nvarchar(50),
	cSkype nvarchar(50),
	cCordless nvarchar(50),
	cBody ntext,
	cOther1 nvarchar(255),
	cOther2 nvarchar(255),
	cOther3 nvarchar(255),
	cContactFirst nvarchar(255),
	cContactLast nvarchar(255),
	cProfilePictureId int,
	cDefaultDirectoryId int,
	cOrgName nvarchar(255) COLLATE FINNISH_SWEDISH_CI_AS,
	cActive int,
	cLanguageId int,
	cCreatedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cCreatedDate datetime NULL ,
	cEditedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cEditedDate datetime NULL ,
	cApprovedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cApprovedDate datetime NULL ,
	cLockedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
	cLockedDate datetime NULL  	
)	
AS
BEGIN
	DECLARE @defaultLanguageId INT
	DECLARE @inserted INT
	IF (@locationid > 0)
	BEGIN
		SELECT @defaultLanguageId = cLanguageId
		FROM tblBaseLocationsLanguages
		WHERE cIsDefault = 1 AND cLocationId = @locationid
	END

	DECLARE @id INT
	DECLARE getAll CURSOR LOCAL FOR
	SELECT cMemberId FROM tblMemberLocationConnection
	WHERE cLocationId = @locationid
	OPEN getAll
	FETCH NEXT FROM getAll INTO @id
	IF (@type = 0)
	BEGIN
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO @memberList
				SELECT	mc.cId, mc.cMemberId, mc.cDescription,
				mc.cAddress, mc.cZipcode, mc.cCity, mc.cPhone,
				mc.cFax, mc.cMobile, mc.cEmail, mc.cWww, mc.cVoip,
				mc.cSkype, mc.cCordless, mc.cBody,
				mc.cOther1, mc.cOther2, mc.cOther3, mc.cContactFirst,
				mc.cContactLast, mc.cProfilePictureId, mc.cDefaultDirectoryId, 
				m.cOrgName, m.cActive, mlc.cLanguageId,
				mc.cCreatedBy, mc.cCreatedDate, mc.cEditedBy, mc.cEditedDate,
				mc.cApprovedBy, mc.cApprovedDate, mc.cLockedBy, mc.cLockedDate
				FROM	tblMembersContent mc
				INNER JOIN tblMembers m ON m.cId = mc.cMemberId
				INNER JOIN tblMemberLanguageConnection mlc 
					ON mlc.cMembersContentId = mc.cId
				WHERE m.cId = @id AND cLanguageId = @languageid
				SET @inserted = @@ROWCOUNT
			IF (@inserted = 0)
			BEGIN 
				INSERT INTO @memberList
					SELECT	mc.cId, mc.cMemberId, mc.cDescription,
					mc.cAddress, mc.cZipcode, mc.cCity, mc.cPhone,
					mc.cFax, mc.cMobile, mc.cEmail, mc.cWww, 
					mc.cVoip, mc.cSkype, mc.cCordless, mc.cBody,
					mc.cOther1, mc.cOther2, mc.cOther3, mc.cContactFirst,
					mc.cContactLast, mc.cProfilePictureId, mc.cDefaultDirectoryId, 
					m.cOrgName, m.cActive, mlc.cLanguageId,
					mc.cCreatedBy, mc.cCreatedDate, mc.cEditedBy, mc.cEditedDate,
					mc.cApprovedBy, mc.cApprovedDate, mc.cLockedBy, mc.cLockedDate
					FROM	tblMembersContent mc
					INNER JOIN tblMembers m ON m.cId = mc.cMemberId
					INNER JOIN tblMemberLanguageConnection mlc 
						ON mlc.cMembersContentId = mc.cId
					WHERE m.cId = @id AND cLanguageId = @defaultLanguageId
				SET @inserted = @@ROWCOUNT
			END	
			IF (@inserted = 0)
			BEGIN 												
				DECLARE @langId INT
				SELECT TOP 1 @langId = cLanguageId
				FROM	tblMembersContent mc
				INNER JOIN tblMembers m ON m.cId = mc.cMemberId
				INNER JOIN tblMemberLanguageConnection mlc 
					ON mlc.cMembersContentId = mc.cId
				WHERE m.cId = @id

				INSERT INTO @memberList
					SELECT	mc.cId, mc.cMemberId, mc.cDescription,
					mc.cAddress, mc.cZipcode, mc.cCity, mc.cPhone,
					mc.cFax, mc.cMobile, mc.cEmail, mc.cWww,
					mc.cVoip, mc.cSkype, mc.cCordless, mc.cBody,
					mc.cOther1, mc.cOther2, mc.cOther3, mc.cContactFirst,
					mc.cContactLast, mc.cProfilePictureId, mc.cDefaultDirectoryId,
					m.cOrgName, m.cActive, mlc.cLanguageId,
					mc.cCreatedBy, mc.cCreatedDate, mc.cEditedBy, mc.cEditedDate,
					mc.cApprovedBy, mc.cApprovedDate, mc.cLockedBy, mc.cLockedDate
					FROM	tblMembersContent mc
					INNER JOIN tblMembers m ON m.cId = mc.cMemberId
					INNER JOIN tblMemberLanguageConnection mlc 
						ON mlc.cMembersContentId = mc.cId
					WHERE m.cId = @id AND cLanguageId = @langId
			END
			FETCH NEXT FROM getAll INTO @id
		END
	END
	IF (@type = 1)
	BEGIN
		WHILE (@@FETCH_STATUS <> -1)
		BEGIN
			INSERT INTO @memberList
				SELECT	mc.cId, mc.cMemberId, mc.cDescription,
				mc.cAddress, mc.cZipcode, mc.cCity, mc.cPhone,
				mc.cFax, mc.cMobile, mc.cEmail, mc.cWww,
				mc.cVoip, mc.cSkype, mc.cCordless, mc.cBody,
				mc.cOther1, mc.cOther2, mc.cOther3, mc.cContactFirst,
				mc.cContactLast, mc.cProfilePictureId, mc.cDefaultDirectoryId,
				m.cOrgName, m.cActive, mlc.cLanguageId,
				mc.cCreatedBy, mc.cCreatedDate, mc.cEditedBy, mc.cEditedDate,
				mc.cApprovedBy, mc.cApprovedDate, mc.cLockedBy, mc.cLockedDate
				FROM	tblMembersContent mc
				INNER JOIN tblMembers m ON m.cId = mc.cMemberId
				INNER JOIN tblMemberLanguageConnection mlc 
					ON mlc.cMembersContentId = mc.cId
				WHERE m.cId = @id AND cLanguageId = @languageid
			FETCH NEXT FROM getAll INTO @id
		END	
	END
	CLOSE getAll
	DEALLOCATE getAll

	RETURN
END
