CREATE  PROCEDURE spForumEmails_Enqueue
(
	@EmailTo	nvarchar(2000),
	@EmailCc	ntext,
	@EmailBcc	nvarchar(2000),
	@EmailFrom	nvarchar(256),
	@EmailSubject	nvarchar(1024),
	@EmailBody	ntext,
	@EmailPriority	int,
	@EmailBodyFormat int
)
AS
BEGIN

	INSERT INTO
		tblForumEmailQueue
		(
			emailTo,
			emailCc,
			emailBcc,
			EmailFrom,
			EmailSubject,
			EmailBody,
			emailPriority,
			emailBodyFormat
		)
	VALUES
		(
			@EmailTo,
			@EmailCc,
			@EmailBcc,
			@EmailFrom,
			@EmailSubject,
			@EmailBody,
			@EmailPriority,
			@EmailBodyFormat
		)		
END




