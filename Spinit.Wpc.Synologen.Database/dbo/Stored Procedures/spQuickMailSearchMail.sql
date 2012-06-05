create PROCEDURE spQuickMailSearchMail
					@type INT,
					@id INT,
					@cmpId INT,
					@locId INT,
					@lngId INT,
					@mlTpeId INT,
					@name NVARCHAR (512),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
			ORDER BY cName ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
				INNER JOIN tblQuickMailComponentConnection
					ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
			WHERE	tblQuickMailComponentConnection.cCmpId = @cmpId
			ORDER BY cName ASC
		END
							
	IF @type = 2
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
				INNER JOIN tblQuickMailLocationConnection
					ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLanguageConnection
					ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
			WHERE	tblQuickMailLocationConnection.cLocId = @locId
				AND	tblQuickMailLanguageConnection.cLngId = @lngId
			ORDER BY cName ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
				INNER JOIN tblQuickMailComponentConnection
					ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLocationConnection
					ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLanguageConnection
					ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
			WHERE	tblQuickMailComponentConnection.cCmpId = @cmpId
				AND	tblQuickMailLocationConnection.cLocId = @locId
				AND	tblQuickMailLanguageConnection.cLngId = @lngId
			ORDER BY cName ASC
		END

	IF @type = 4
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
				INNER JOIN tblQuickMailComponentConnection
					ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLocationConnection
					ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLanguageConnection
					ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
			WHERE	tblQuickMailComponentConnection.cCmpId = @cmpId
				AND	tblQuickMailLocationConnection.cLocId = @locId
				AND	tblQuickMailLanguageConnection.cLngId = @lngId
				AND tblQuickMailMail.cMlTpeId = @mlTpeId
			ORDER BY cName ASC
		END

	IF @type = 5
		BEGIN
			IF (@cmpId > 0)
			BEGIN
				SELECT	cId,
						cMlTpeId,
						cPriority,
						cName,
						cFileName,
						cFrom,
						cFriendlyFrom,
						cSubject,
						cUseHtml,
						cHtmlBody,
						cUsePlain,
						cPlainBody,
						cCharacterEncoding,
						cBodyPartEncoding,
						cEmbedPictures,
						cUseAltLink,
						cActive,
						cApprovedBy,
						cApprovedDate,
						cLockedBy,
						cLockedDate,
						cCreatedBy,
						cCreatedDate,
						cChangedBy,
						cChangedDate,
						cSendOutDate,
						cSent
				FROM	tblQuickMailMail
					INNER JOIN tblQuickMailComponentConnection
						ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
					INNER JOIN tblQuickMailLocationConnection
						ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
					INNER JOIN tblQuickMailLanguageConnection
						ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
				WHERE	tblQuickMailComponentConnection.cCmpId = @cmpId
					AND	tblQuickMailLocationConnection.cLocId = @locId
					AND	tblQuickMailLanguageConnection.cLngId = @lngId
					AND tblQuickMailMail.cName LIKE '%' +  @name + '%'
				ORDER BY cName ASC			
			END
			ELSE
			BEGIN
				SELECT	cId,
						cMlTpeId,
						cPriority,
						cName,
						cFileName,
						cFrom,
						cFriendlyFrom,
						cSubject,
						cUseHtml,
						cHtmlBody,
						cUsePlain,
						cPlainBody,
						cCharacterEncoding,
						cBodyPartEncoding,
						cEmbedPictures,
						cUseAltLink,
						cActive,
						cApprovedBy,
						cApprovedDate,
						cLockedBy,
						cLockedDate,
						cCreatedBy,
						cCreatedDate,
						cChangedBy,
						cChangedDate,
						cSendOutDate,
						cSent
				FROM	tblQuickMailMail
					INNER JOIN tblQuickMailComponentConnection
						ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
					INNER JOIN tblQuickMailLocationConnection
						ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
					INNER JOIN tblQuickMailLanguageConnection
						ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
				WHERE	tblQuickMailLocationConnection.cLocId = @locId
					AND	tblQuickMailLanguageConnection.cLngId = @lngId
					AND tblQuickMailMail.cName LIKE '%' +  @name + '%'
				ORDER BY cName ASC						
			END
		END

	IF @type = 6
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
				INNER JOIN tblQuickMailComponentConnection
					ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLocationConnection
					ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLanguageConnection
					ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
			WHERE	tblQuickMailComponentConnection.cCmpId = @cmpId
				AND	tblQuickMailLocationConnection.cLocId = @locId
				AND	tblQuickMailLanguageConnection.cLngId = @lngId
				AND tblQuickMailMail.cName = @name
			ORDER BY cName ASC
		END

	IF @type = 7
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
			WHERE	cId = @id
			ORDER BY cName ASC
		END
		
	IF @type = 8
		BEGIN
			SELECT	cId,
					cMlTpeId,
					cPriority,
					cName,
					cFileName,
					cFrom,
					cFriendlyFrom,
					cSubject,
					cUseHtml,
					cHtmlBody,
					cUsePlain,
					cPlainBody,
					cCharacterEncoding,
					cBodyPartEncoding,
					cEmbedPictures,
					cUseAltLink,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate,
					cSendOutDate,
					cSent
			FROM	tblQuickMailMail
				INNER JOIN tblQuickMailComponentConnection
					ON tblQuickMailComponentConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLocationConnection
					ON tblQuickMailLocationConnection.cMlId = tblQuickMailMail.cId
				INNER JOIN tblQuickMailLanguageConnection
					ON tblQuickMailLanguageConnection.cMlId = tblQuickMailMail.cId
			WHERE	tblQuickMailComponentConnection.cCmpId = @cmpId
				AND	tblQuickMailLocationConnection.cLocId = @locId
				AND	tblQuickMailLanguageConnection.cLngId = @lngId
				AND tblQuickMailMail.cMlTpeId = @mlTpeId
				AND tblQuickMailMail.cSent <> 1 
			ORDER BY cName ASC
		END
		

	SET @status = @@ERROR
			
END
