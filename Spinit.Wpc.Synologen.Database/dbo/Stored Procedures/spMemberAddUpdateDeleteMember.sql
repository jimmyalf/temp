/*
	Type 0 - Add Member
	Type 1 - Update Member
	Type 2 - Delete Member
*/

CREATE PROCEDURE spMemberAddUpdateDeleteMember
					@type INT,
					@languageid INT = 0,
					@orgname NVARCHAR(255) = '',
					@description NVARCHAR(255) = '',
					@contactFirst NVARCHAR(255) = '',
					@contactLast NVARCHAR(255) = '',
					@address NVARCHAR(255) = '',
					@zipcode NVARCHAR(50) = '',
					@city NVARCHAR(50) = '',
					@phone NVARCHAR(50) = '',
					@fax NVARCHAR(50) = '',
					@mobile NVARCHAR(50) = '',
					@email NVARCHAR(50) = '',
					@www NVARCHAR(255) = '',
					@voip NVARCHAR(50) = '',
					@skype NVARCHAR(50) = '',
					@cordless NVARCHAR(50) = '',
					@body NTEXT = '',
					@active INT = 1,
					@other1 NVARCHAR(100) = '',
					@other2 NVARCHAR(100) = '',
					@other3 NVARCHAR(255) = '',
					@profilePictureId INT = 0,
					@defaultDirectoryId INT = 0,
					@createdBy NVARCHAR(100) = '',
					@editedBy NVARCHAR(100) = '',
					@status INT OUTPUT,
					@id INT OUTPUT
	AS
		DECLARE @memberContentId INT
		BEGIN TRANSACTION ADD_UPDATE_DELETE_MEMBER
		IF (@type = 0)
		BEGIN
			INSERT INTO tblMembers
				(cOrgName, cActive)
			VALUES
				(@orgname, @active)				
			SELECT @id = @@IDENTITY
			INSERT INTO tblMembersContent
				(cMemberId, cDescription, cContactFirst, cContactLast, cAddress, cZipcode, cCity, cPhone, 
				cFax, cMobile, cEmail, cWww, cVoip, cSkype, cCordless, cBody, cOther1, cOther2, cOther3,
				cProfilePictureId, cDefaultDirectoryId, cCreatedBy,cCreatedDate, cEditedBy, cEditedDate)
			VALUES
				(@id, @description, @contactFirst, @contactLast, @address, @zipcode, @city, @phone,
				@fax, @mobile, @email, @www, @voip, @skype, @cordless, @body, @other1, @other2, @other3,
				@profilePictureId, @defaultDirectoryId, @createdBy, GETDATE(), @createdBy, GETDATE())
			SELECT @memberContentId = @@IDENTITY
			INSERT INTO tblMemberLanguageConnection
				(cLanguageId, cMembersContentId)
			VALUES
				(@languageid, @memberContentId)
		END			 
		IF (@type = 1)
		BEGIN
			UPDATE tblMembers
			SET cOrgName = @orgname,
			cActive = @active
			WHERE cId = @id
			
			SELECT @memberContentId = cMembersContentId
			FROM tblMemberLanguageConnection
			INNER JOIN tblMembersContent ON 
				tblMemberLanguageConnection.cMembersContentId =
				tblMembersContent.cId 
			WHERE cMemberId = @id AND cLanguageId = @languageid
			
			IF (@@ROWCOUNT > 0)
			BEGIN
				UPDATE tblMembersContent
				SET cDescription = @description,
				cContactFirst = @contactFirst,
				cContactLast = @contactLast,
				cAddress = @address,
				cZipcode = @zipcode,			
				cCity = @city,
				cPhone = @phone,
				cFax = @fax,
				cMobile = @mobile,
				cEmail = @email,
				cWww = @www,
				cVoip = @voip,
				cSkype = @skype,
				cCordless = @cordless,
				cBody = @body,
				cOther1 = @other1,
				cOther2 = @other2,
				cOther3 = @other3,
				cProfilePictureId = @profilePictureId,
				cDefaultDirectoryId	= @defaultDirectoryId,
				cEditedBy = @editedBy,
				cEditedDate = GETDATE()			
				WHERE cId = @memberContentId
			END
			ELSE
			BEGIN
				INSERT INTO tblMembersContent
					(cMemberId, cDescription, cContactFirst, cContactLast, cAddress, cZipcode, cCity, cPhone, 
					cFax, cMobile, cEmail, cWww, cVoip, cSkype, cCordless, cBody, cOther1, cOther2, cOther3,
					cProfilePictureId, cDefaultDirectoryId)
				VALUES
					(@id, @description, @contactFirst, @contactLast, @address, @zipcode, @city, @phone,
					@fax, @mobile, @email, @www, @voip, @skype, @cordless, @body, @other1, @other2, @other3,
					@profilePictureId, @defaultDirectoryId)
				SELECT @memberContentId = @@IDENTITY
				INSERT INTO tblMemberLanguageConnection
					(cLanguageId, cMembersContentId)
				VALUES
					(@languageid, @memberContentId)						
			END
			
		END
		IF (@type = 2)
		BEGIN
			SELECT @memberContentId = cMembersContentId
			FROM tblMemberLanguageConnection
			INNER JOIN tblMembersContent ON 
				tblMemberLanguageConnection.cMembersContentId =
				tblMembersContent.cId 
			WHERE cMemberId = @id AND cLanguageId = @languageid
			
			IF (@@ROWCOUNT > 0)
			BEGIN
				DELETE FROM tblMemberLanguageConnection
				WHERE cMembersContentId = @memberContentId
				
				DELETE FROM tblMembersContent
				WHERE cId = @memberContentId								
			END
			
			SELECT * FROM tblMembersContent
			WHERE cMemberId = @id
			IF (@@ROWCOUNT = 0)
			BEGIN
				DELETE FROM tblMemberClassifiedAds
				WHERE cMemberId = @id
			
				DELETE FROM tblMemberUserConnection
				WHERE cMemberId = @id
				
				DELETE FROM tblMemberLocationConnection
				WHERE cMemberId = @id
				
				DELETE FROM tblMemberCategoryConnection
				WHERE cMemberId = @id
				
				DELETE FROM tblMembers
				WHERE cId = @id
			END															
		END 

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_MEMBER
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_MEMBER
			END
