create PROCEDURE spQuickMailAddUpdateDeleteMail
					@action INT,
					@id INT OUTPUT,
					@mlTpeId INT,
					@priority INT,
					@name NVARCHAR (512),
					@fileName NVARCHAR (512),
					@from NVARCHAR (512),
					@friendlyFrom NVARCHAR (512),
					@subject NVARCHAR (512),
					@useHtml BIT,
					@htmlBody NTEXT,
					@usePlain BIT,
					@plainBody NTEXT,
					@characterEncoding NVARCHAR (20),
					@bodyPartEncoding NVARCHAR (20),
					@embedPictures BIT,
					@useAltLink BIT,
					@active BIT,
					@approvedBy NVARCHAR (100),
					@approvedDate DATETIME,
					@lockedBy NVARCHAR (100),
					@lockedDate DATETIME,					
					@userName NVARCHAR (100),
					@sendOutDate DATETIME,
					@sent BIT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblQuickMailMailType
			WHERE	cId = @mlTpeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -10
					RETURN
				END		
						
			INSERT INTO tblQuickMailMail
				(cMlTpeId, cPriority, cName, cFileName, cFrom, cFriendlyFrom,
				 cSubject, cUseHtml, cHtmlBody, cUsePlain, cPlainBody,
				 cCharacterEncoding, cBodyPartEncoding, cEmbedPictures, cUseAltLink,
				 cActive, cApprovedBy, cApprovedDate, cLockedBy,
				 cLockedDate, cCreatedBy, cCreatedDate, cSendOutDate, cSent)
			VALUES
				(@mlTpeId, @priority, @name, @fileName, @from, @friendlyFrom,
				 @subject, @useHtml, @htmlBody, @usePlain, @plainBody,
				 @characterEncoding, @bodyPartEncoding, @embedPictures, @useAltLink,
				 @active, @approvedBy, @approvedDate, @lockedBy,
				 @lockedDate, @userName, GETDATE (), @sendOutDate, @sent)
				
			SET @id = @@IDENTITY
			
			IF (@@ERROR = 0) AND (@fileName IS NOT NULL)
				BEGIN
					UPDATE	tblQuickMailMail
					SET		cFileName = @fileName + '_' + CAST (@id AS NVARCHAR (10)) + '.html'
					WHERE	cId = @id
				END
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailMail
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			SELECT	@dummy = 1
			FROM	tblQuickMailMailType
			WHERE	cId = @mlTpeId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -10
					RETURN
				END		

			UPDATE	tblQuickMailMail
			SET		cMlTpeId = @mlTpeId,
					cPriority = @priority,
					cName = @name,
					cFileName = @fileName,
					cFrom = @from,
					cFriendlyFrom = @friendlyFrom,
					cSubject = @subject,
					cUseHtml = @useHtml,
					cHtmlBody = @htmlBody,
					cUsePlain = @usePlain,
					cPlainBody = @plainBody,
					cCharacterEncoding = @characterEncoding,
					cBodyPartEncoding = @bodyPartEncoding,
					cEmbedPictures = @embedPictures,
					cUseAltLink = @useAltLink,
					cActive = @active,
					cApprovedBy = @approvedBy,
					cApprovedDate = @approvedDate,
					cLockedBy = @lockedBy,
					cLockedDate = @lockedDate,
					cChangedBy = @userName,
					cChangedDate = GETDATE (),
					cSendOutDate = @sendOutDate,
					cSent = @sent
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblQuickMailMail
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblQuickMailComponentConnection
			WHERE		cMlId = @id
			
			IF @@ERROR <> 0
				BEGIN
					SET @status = @@ERROR
					RETURN
				END
				
			DELETE FROM tblQuickMailLocationConnection
			WHERE		cMlId = @id
			
			IF @@ERROR <> 0
				BEGIN
					SET @status = @@ERROR
					RETURN
				END
				
			DELETE FROM tblQuickMailLanguageConnection
			WHERE		cMlId = @id
			
			IF @@ERROR <> 0
				BEGIN
					SET @status = @@ERROR
					RETURN
				END
				
			DELETE FROM tblQuickMailFileConnection
			WHERE		cMlId = @id
			
			IF @@ERROR <> 0
				BEGIN
					SET @status = @@ERROR
					RETURN
				END
				
			DELETE FROM tblQuickMailPageConnection
			WHERE		cMlId = @id
			
			IF @@ERROR <> 0
				BEGIN
					SET @status = @@ERROR
					RETURN
				END
				
			DELETE FROM tblQuickMailMail
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
