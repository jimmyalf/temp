CREATE PROCEDURE spNewsAddUpdateDeleteNews
					@action INT,
					@id INT OUTPUT,
					@newsType SMALLINT = 1,
					@heading NTEXT = '',
					@summary NTEXT = '',
					@body NTEXT = '',
					@formatedBody NTEXT = '',
					@externalLink NVARCHAR (255) = '',
					@spotImage INT  = -1,
					@spotHeight INT = 0,
					@spotWidth INT = 0,
					@spotAlign INT = 0,
					@startDate DATETIME = '',
					@endDate DATETIME = '',
					@createdDate DATETIME = '',
					@createdBy NVARCHAR(100) = '',
					@editedBy NVARCHAR(100) = '',
					@approvedBy NVARCHAR(100) = '',
					@approvedDate DATETIME = '',
					@lockedBy NVARCHAR(100) = '',
					@lockedDate DATETIME = '',
					@status INT OUTPUT	
AS
		BEGIN TRANSACTION ADD_UPDATE_DELETE_NEWS
		IF (@action = 0) -- Create
		BEGIN
			INSERT INTO tblNews
				(cNewsType, cHeading, cSummary, cBody, cFormatedBody, cExternalLink, cSpotImage,
				cSpotHeight, cSpotWidth, cSpotAlign, cStartDate,
				cEndDate, cCreatedBy, cCreatedDate, cEditedBy, cEditedDate, cApprovedBy, cApprovedDate,
				cLockedBy, cLockedDate)
			VALUES
				(@newsType, @heading, @summary, @body, @formatedBody, @externalLink, @spotImage,
				@spotHeight, @spotWidth, @spotAlign, @startDate,
				@endDate, @createdBy, getDate(), null, null, @approvedBy, @approvedDate,
				@lockedBy, @lockedDate)				
			SELECT @id = @@IDENTITY
		END			 
		IF (@action = 1) -- Update
		BEGIN
			UPDATE tblNews
			SET cNewsType = @newsType,
			cHeading = @heading,
			cSummary = @summary,
			cBody = @body,
			cFormatedBody = @formatedBody,
			cExternalLink = @externalLink,
			cSpotImage = @spotImage,
			cSpotHeight = @spotHeight,
			cSpotWidth = @spotWidth,
			cSpotAlign = @spotAlign,
			cStartDate = @startDate,
			cEndDate = @endDate,
			cCreatedBy = @createdBy,
			cCreatedDate = @createdDate,
			cEditedBy = @editedBy,
			cEditedDate = getDate(),
			cApprovedBy = @approvedBy,
			cApprovedDate = @approvedDate,
			cLockedBy = @lockedBy,
			cLockedDate = @lockedDate
			
			WHERE cId = @id
		END
		IF (@action = 2) -- Delete
		BEGIN
			DELETE FROM tblNewsLocationConnection
			WHERE cNewsId = @id
			
			DELETE FROM tblNewsCategoryConnection
			WHERE cNewsId = @id
			
			DELETE FROM tblNewsLanguageConnection
			WHERE cNewsId = @id

			DELETE FROM tblNewsGroupConnection
			WHERE cNewsId = @id

			DELETE FROM tblNewsFileConnection
			WHERE cNewsId = @id

			DELETE FROM tblNewsPageConnection
			WHERE cNewsId = @id

			DELETE FROM tblNewsRead
			WHERE cNewsId = @id

			DELETE FROM tblNews
			WHERE cId = @id
		END

		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
			BEGIN
				SELECT @id = 0
				ROLLBACK TRANSACTION ADD_UPDATE_DELETE_NEWS
				RETURN
			END
									
		IF (@@ERROR = 0)
			BEGIN
				COMMIT TRANSACTION ADD_UPDATE_DELETE_NEWS
			END
