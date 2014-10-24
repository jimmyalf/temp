create PROCEDURE spCourseAddUpdateDeleteCourse
					@action INT,
					@id INT OUTPUT,
					@courseMainId INT=0,
					@cityId INT=0,
					@heading NVARCHAR(255)= '',
					@summary NTEXT= '',
					@body NTEXT= '',
					@formatedBody NTEXT= '',
					@contactName NVARCHAR (50)= '',
					@contactEmail NVARCHAR (50)= '',
					@contactPhone NVARCHAR (15)= '',
					@contactMobile NVARCHAR (15)= '',
					@lastApplicationDate smalldatetime= '',
					@publishStartDate smalldatetime= '',
					@publishEndDate smalldatetime= '',
					@courseStartDate smalldatetime= '',
					@courseEndDate smalldatetime= '',
					@minParticipants INT= -1,
					@maxParticipants INT= -1,
					@confirmSubject NVARCHAR(255)= '',
					@confirmBody NTEXT= '',
					@reminderSubject NVARCHAR(255)= '',
					@reminderBody NTEXT= '',	
					@daysBeforeReminder INT= -1,	
					@reminderSent BIT= 0,	
					@adminEmail NVARCHAR (50)= '',
					@contactAutoEmail BIT = 0,
					@adminAutoEmail BIT = 0,
					@createdBy NVARCHAR (255)= '',
					@createdDate smalldatetime= '',
					@editedBy NVARCHAR (255)= '',
					@editedDate smalldatetime= '',
					@approvedBy NVARCHAR (255)= '',
					@approvedDate smalldatetime= '',															
					@lockedBy NVARCHAR (255)= '',
					@lockedDate smalldatetime= '',
					@submissed INT= -1,
					@visibleTo INT= -1,
					@languageId INT=-1,
					@locationId INT=-1,
					@status INT OUTPUT
AS
		BEGIN TRANSACTION COURSE_ADD_UPDATE_DELETE
		IF (@action = 0) -- Create
		BEGIN
			INSERT INTO tblCourse
				(cCourseMainId,cCityId,cHeading,cSummary,cBody,cFormatedBody,cContactName,cContactEmail,cContactPhone,
				cContactMobile,cLastApplicationDate,cPublishStartDate,cPublishEndDate,cCourseStartDate,
				cCourseEndDate,cMinParticipants,cMaxParticipants,cConfirmSubject,cConfirmBody,
				cReminderSubject,cReminderBody,cDaysBeforeReminder,cReminderSent,cAdminEmail,
				cContactAutoEmail,cAdminAutoEmail,cCreatedBy,cCreatedDate,cEditedBy,cEditedDate,
				cApprovedBy,cApprovedDate,cLockedBy,cLockedDate,cSubmissed,cVisibleTo)
			VALUES
				(@courseMainId,@cityId,@heading,@summary,@body,@formatedBody,@contactName,@contactEmail,@contactPhone,
				@contactMobile,@lastApplicationDate,@publishStartDate,@publishEndDate,@courseStartDate,
				@courseEndDate,@minParticipants,@maxParticipants,@confirmSubject,@confirmBody,
				@reminderSubject,@reminderBody,@daysBeforeReminder,@reminderSent,@adminEmail,
				@contactAutoEmail,@adminAutoEmail,@createdBy,GETDATE(),@editedBy,@editedDate,
				@approvedBy,@approvedDate,@lockedBy,@lockedDate,@submissed,@visibleTo)			
			SELECT @id = @@IDENTITY
		END			 
		IF (@action = 1) -- Update
		BEGIN 
			UPDATE tblCourse
			SET
				cCourseMainId = @courseMainId,
				cCityId = @cityId,
				cHeading = @heading,
				cSummary = @summary,
				cBody = @body,
				cFormatedBody = @formatedBody,
				cContactName = @contactName,
				cContactEmail = @contactEmail,
				cContactPhone = @contactPhone,
				cContactMobile = @contactMobile,
				cLastApplicationDate = @lastApplicationDate,
				cPublishStartDate = @publishStartDate,
				cPublishEndDate = @publishEndDate,
				cCourseStartDate = @courseStartDate,
				cCourseEndDate = @courseEndDate,
				cMinParticipants = @minParticipants,
				cMaxParticipants = @maxParticipants,
				cConfirmSubject = @confirmSubject,
				cConfirmBody = @confirmBody,
				cReminderSubject = @reminderSubject,
				cReminderBody = @reminderBody,
				cDaysBeforeReminder = @daysBeforeReminder,
				cReminderSent = @reminderSent,
				cAdminEmail = @adminEmail,
				cContactAutoEmail = @contactAutoEmail,
				cAdminAutoEmail = @adminAutoEmail,
				cCreatedBy = @createdBy,
				cCreatedDate = @createdDate,
				cEditedBy = @editedBy,
				cEditedDate = GETDATE(),
				cApprovedBy = @approvedBy,
				cApprovedDate = @approvedDate,
				cLockedBy = @lockedBy,
				cLockedDate = @lockedDate,
				cSubmissed = @submissed,
				cVisibleTo = @visibleTo
			WHERE cId = @id
		END
		IF (@action = 2) -- Delete
		BEGIN
			DELETE FROM tblCourseLocationConnection
			WHERE cCourseId = @id AND cLocationId = @locationId
			


			SELECT * FROM tblCourseLocationConnection
			WHERE cCourseId = @id
			IF (@@ROWCOUNT = 0)
			BEGIN

				DELETE FROM tblCourseLanguageConnection
				WHERE cCourseId = @id AND cLanguageId = @languageId

				SELECT * FROM tblCourseLanguageConnection
				WHERE cCourseId = @id
				IF (@@ROWCOUNT=0)
				BEGIN
					DELETE FROM tblCourse
					WHERE cId = @id
				END
			END
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @id = 0
			ROLLBACK TRANSACTION COURSE_ADD_UPDATE_DELETE
			RETURN
		END
									
		IF (@@ERROR = 0)
		BEGIN
			COMMIT TRANSACTION COURSE_ADD_UPDATE_DELETE
		END
