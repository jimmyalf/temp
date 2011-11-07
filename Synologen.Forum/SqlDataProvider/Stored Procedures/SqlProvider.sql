IF EXISTS	(SELECT NAME
			 FROM sysobjects
			 WHERE name = 'sfForumHasReadPost'
				AND	(type = 'TF' OR type = 'FN'))
	DROP FUNCTION sfForumHasReadPost
GO

CREATE   function sfForumHasReadPost (
	@UserID int, 
	@ThreadID int, 
	@ForumID int
)
RETURNS bit
AS
BEGIN
DECLARE @HasRead bit
DECLARE @ReadAfter int

SET @HasRead = 0

	-- Do we have topics marked as read?
	SELECT 
		@ReadAfter = MarkReadAfter 
	FROM 
		tblForumForumsRead 
	WHERE 
		UserID = @UserID AND 
		ForumID = @ForumID

	IF @ReadAfter IS NOT NULL
	BEGIN
		IF @ReadAfter > @ThreadID
			RETURN 1
	END
	
	IF EXISTS (SELECT ThreadID FROM tblForumThreadsRead WHERE UserID = @UserID AND ThreadID = @ThreadID)
	  SET @HasRead = 1

RETURN @HasRead
END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumAddForumToRole' 
				AND type = 'P')
   DROP PROCEDURE spForumAddForumToRole
GO


CREATE PROCEDURE spForumAddForumToRole
(
   @ForumID int,
   @Rolename nvarchar(256)
)
AS
IF NOT EXISTS (SELECT ForumID FROM PrivateForums WHERE ForumID=@ForumID AND Rolename=@Rolename) AND
    EXISTS (SELECT ForumID FROM Forums WHERE ForumID=@ForumID) AND
    EXISTS (SELECT Rolename FROM UserRoles WHERE Rolename=@Rolename)
    BEGIN
        INSERT INTO
            PrivateForums(ForumID, RoleName)
        VALUES
            (@ForumID, @Rolename)
    END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumAddModeratedForumForUser' 
				AND type = 'P')
   DROP PROCEDURE spForumAddModeratedForumForUser
GO
create procedure spForumAddModeratedForumForUser
(
	@UserName	nvarchar(50),
	@ForumID	int,
	@EmailNotification	bit
)
 AS
	-- add a row to the Moderators table
	-- if the user wants to add All Forums, go ahead and delete all of the other forums
	IF @ForumID = 0
		DELETE FROM Moderators WHERE Username = @UserName
	-- now insert the new row into the table
	INSERT INTO Moderators (Username, ForumID, EmailNotification)
	VALUES (@UserName, @ForumID, @EmailNotification)


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumBlockedIpAddress_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumBlockedIpAddress_CreateUpdateDelete
GO
CREATE PROCEDURE spForumBlockedIpAddress_CreateUpdateDelete
(
	@IpID	int = 0 out,
	@DeleteBlockedIpAddress	bit = 0,
	@Address		nvarchar(50) = '',
	@Reason	nvarchar(512) = ''
)
AS

-- Are we deleting the role?
IF @DeleteBlockedIpAddress = 1
BEGIN

	DELETE 
		tblForumBlockedIpAddresses
	WHERE 
		IpID = @IpID

	RETURN
END

-- Are we updating a forum
IF  @IpID > 0
BEGIN
	-- Update the role
	UPDATE 
		tblForumBlockedIpAddresses
	SET
		Address = @Address,
		Reason = @Reason
	WHERE 
		IpID = @IpID
END
ELSE
BEGIN

	-- Create a new Forum
	INSERT INTO 
		tblForumBlockedIpAddresses (
			Address, 
			Reason
			)
		VALUES (
			@Address,
			@Reason
			)
	
	SET @IpID = @@Identity

END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumBlockedIpAddress_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumBlockedIpAddress_Get
GO
CREATE proc spForumBlockedIpAddress_Get
(
	@IpID int
) AS 
	SELECT
		*
	FROM
		tblForumBlockedIpAddresses
	WHERE
		IpID = @IpID



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumBlockedIpAddresses_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumBlockedIpAddresses_Get
GO
CREATE proc spForumBlockedIpAddresses_Get AS 
	SELECT
		*
	FROM
		tblForumBlockedIpAddresses



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumCensorship_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumCensorship_CreateUpdateDelete
GO
CREATE proc spForumCensorship_CreateUpdateDelete
(
	  @Word			nvarchar(40)
	, @DeleteWord 	bit = 0
	, @Replacement	nvarchar(40)
)
as
SET NOCOUNT ON

if( @DeleteWord > 0 )
BEGIN
	DELETE FROM
		tblForumCensorship
	WHERE
		Word = @Word
	RETURN
END
ELSE
BEGIN
	UPDATE tblForumCensorship SET
		Replacement	= @Replacement
	WHERE
		Word	= @Word

	IF( @@rowcount = 0 )
	BEGIN
	INSERT INTO tblForumCensorship (
		Word, Replacement
	) VALUES (
		@Word, @Replacement
	)
	END
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumCensorships_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumCensorships_Get
GO
CREATE proc spForumCensorships_Get
(
	@Word	nvarchar(40) = ''
)
as
	select
		*
	from
		tblForumCensorship
	WHERE
		Word	= @Word or 
		(
			@Word = '' AND
			1=1
		)


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumDisallowedName_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumDisallowedName_CreateUpdateDelete
GO
CREATE PROCEDURE spForumDisallowedName_CreateUpdateDelete
(
	@Name		nvarchar(64),
	@Replacement 	nvarchar(64),
	@DeleteName	bit = 0
)
AS

SET NOCOUNT ON

if( @DeleteName > 0 )
BEGIN
	DELETE FROM
		tblForumDisallowedNames
	WHERE
		DisallowedName = @Name
END
ELSE 
BEGIN
		UPDATE tblForumDisallowedNames SET
			DisallowedName = @Replacement
		WHERE
			DisallowedName = @Name

	if( @@rowcount = 0 )
	BEGIN
		INSERT INTO tblForumDisallowedNames (
			DisallowedName
		) VALUES (
			@Name
		)
		
	END
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumDisallowedNames_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumDisallowedNames_Get
GO
CREATE PROCEDURE spForumDisallowedNames_Get
AS 

	SELECT 
		DisallowedName 
	FROM 
		tblForumDisallowedNames



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumEmails_Dequeue' 
				AND type = 'P')
   DROP PROCEDURE spForumEmails_Dequeue
GO
CREATE    PROCEDURE spForumEmails_Dequeue
AS
BEGIN

	SELECT * FROM tblForumEmailQueue
	
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumEmails_Enqueue' 
				AND type = 'P')
   DROP PROCEDURE spForumEmails_Enqueue
GO
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




GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumEmails_TrackingForum' 
				AND type = 'P')
   DROP PROCEDURE spForumEmails_TrackingForum
GO
CREATE PROCEDURE spForumEmails_TrackingForum
(
	@PostID    INT
)
AS

DECLARE @ForumID INT
DECLARE @UserID INT
DECLARE @PostLevel INT
DECLARE @ThreadID INT

-- First get the post info
SELECT 
	@ForumID = ForumID, 
	@UserID = UserID,
	@PostLevel = PostLevel,
	@ThreadID = ThreadID
FROM 
	tblForumPosts (nolock) 
WHERE 
	PostID = @PostID

-- Check if its a new thread or not
IF (@PostLevel = 1)
BEGIN
	-- this is a new thread (1 & 2)
	
	-- Check if this is a PM message
	IF (@ForumID = 0)
	BEGIN
		
		-- we have to bind to the PM users for this ThreadID
		SELECT
			U.Email,
			UP.EnableHtmlEmail
		FROM
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID
			JOIN tblForumPrivateMessages PM ON PM.UserID = F.UserID AND PM.ThreadID = @ThreadID
		WHERE
			F.ForumID IN (-1, 0) AND
			F.SubscriptionType <> 0

	END
	ELSE BEGIN

		SELECT
			U.Email, 
			UP.EnableHtmlEmail
		FROM 
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID				
		WHERE
			F.ForumID = @ForumID AND
			F.SubscriptionType <> 0
	END
END
ELSE BEGIN
	-- this is a reply to an existing post (2)

	-- Check if this is a PM message
	IF (@ForumID = 0)
	BEGIN
		
		-- we have to bind to the PM users for this ThreadID
		SELECT
			U.Email,
			UP.EnableHtmlEmail
		FROM
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID
			JOIN tblForumPrivateMessages PM ON PM.UserID = F.UserID AND PM.ThreadID = @ThreadID
		WHERE
			F.ForumID IN (-1, 0) AND
			F.SubscriptionType = 2

	END
	ELSE BEGIN

		SELECT
			U.Email, 
			UP.EnableHtmlEmail
		FROM 
			tblForumTrackedForums F
			JOIN tblForumUsers U (nolock) ON U.UserID = F.UserID
			JOIN tblForumUserProfile UP ON UP.UserID = U.UserID				
		WHERE
			F.ForumID = @ForumID AND
			F.SubscriptionType = 2
	END
END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumEmails_TrackingThread' 
				AND type = 'P')
   DROP PROCEDURE spForumEmails_TrackingThread
GO
CREATE PROCEDURE spForumEmails_TrackingThread
(
	@PostID INT
)
AS

DECLARE @ForumID INT
DECLARE @UserID INT
DECLARE @PostLevel INT
DECLARE @ThreadID INT

-- First get the post info
SELECT 
	@ForumID = ForumID, 
	@UserID = UserID,
	@PostLevel = PostLevel,
	@ThreadID = ThreadID
FROM 
	tblForumPosts (nolock) 
WHERE 
	PostID = @PostID


-- Check if this is a PM message
IF (@ForumID = 0)
BEGIN
	
	-- we have to bind to the PM users for this ThreadID
	SELECT
		U.Email,
		UP.EnableHtmlEmail
	FROM
		tblForumTrackedThreads T
		JOIN tblForumUsers U (nolock) ON U.UserID = T.UserID
		JOIN tblForumUserProfile UP ON UP.UserID = U.UserID
		JOIN tblForumPrivateMessages PM ON PM.UserID = T.UserID AND PM.ThreadID = @ThreadID
	WHERE
		T.ThreadID = @ThreadID

END
ELSE BEGIN

	SELECT
		U.Email, 
		UP.EnableHtmlEmail
	FROM 
		tblForumTrackedThreads T
		JOIN tblForumUsers U (nolock) ON U.UserID = T.UserID
		JOIN tblForumUserProfile UP ON UP.UserID = U.UserID				
	WHERE
		T.ThreadID = @ThreadID
END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumExceptions_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumExceptions_Get
GO
CREATE  procedure spForumExceptions_Get
(
	@SiteID int,
	@ExceptionType int = 0,
	@MinFrequency int = 10
)
AS
BEGIN

IF @ExceptionType < 0
	SELECT TOP 100
		E.*
	FROM
		tblForumExceptions E
	WHERE
		E.SiteID = @SiteID AND
		Frequency >= @MinFrequency
	ORDER BY
		Frequency DESC
ELSE
	SELECT TOP 100
		E.*
	FROM
		tblForumExceptions E
	WHERE
		E.SiteID = @SiteID AND
		E.Category = @ExceptionType AND
		Frequency >= @MinFrequency
	ORDER BY
		Frequency DESC
END




GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumExceptions_Log' 
				AND type = 'P')
   DROP PROCEDURE spForumExceptions_Log
GO
CREATE procedure spForumExceptions_Log
(
	@SiteID int,
	@ExceptionHash varchar(128),
	@Category int,
	@Exception nvarchar(2000),
	@ExceptionMessage nvarchar(500),
	@UserAgent nvarchar(64),
	@IPAddress varchar(15),
	@HttpReferrer nvarchar (256),
	@HttpVerb nvarchar(24),
	@PathAndQuery nvarchar(512)
)
AS
BEGIN

IF EXISTS (SELECT ExceptionID FROM tblForumExceptions WHERE ExceptionHash = @ExceptionHash)

	UPDATE
		tblForumExceptions
	SET
		DateLastOccurred = GetDate(),
		Frequency = Frequency + 1
	WHERE
		ExceptionHash = @ExceptionHash
ELSE
	INSERT INTO 
		tblForumExceptions
	(
		ExceptionHash,
		SiteID,
		Category,
		Exception,
		ExceptionMessage,
		UserAgent,
		IPAddress,
		HttpReferrer,
		HttpVerb,
		PathAndQuery,
		DateCreated,
		DateLastOccurred,
		Frequency
	)
	VALUES
	(
		@ExceptionHash,
		@SiteID,
		@Category,
		@Exception,
		@ExceptionMessage,
		@UserAgent,
		@IPAddress,
		@HttpReferrer,
		@HttpVerb,
		@PathAndQuery,
		GetDate(),
		GetDate(),
		1
	)

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForumGroup_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumForumGroup_CreateUpdateDelete
GO
CREATE       PROCEDURE spForumForumGroup_CreateUpdateDelete
(
	@ForumGroupID	int out,
	@Name		nvarchar(256),
	@Action 	int
)
AS

-- CREATE
IF @Action = 0
BEGIN
	DECLARE @SortOrder int

	SET @SortOrder = ISNULL((SELECT MAX(SortOrder) + 1 FROM tblForumForumGroups), 0)

	-- Create a new forum group
	INSERT INTO 
		tblForumForumGroups 
		(
			Name,
			SortOrder
		)
	VALUES 
		(
			@Name,
			@SortOrder
		)
	
	SET @ForumGroupID = @@IDENTITY
END


-- UPDATE
ELSE IF @Action = 1
BEGIN

	IF EXISTS(SELECT ForumGroupID FROM tblForumForumGroups WHERE ForumGroupID = @ForumGroupID)
	BEGIN
		UPDATE
			tblForumForumGroups
		SET
			Name = @Name
		WHERE
			ForumGroupID = @ForumGroupID
	END

END

-- DELETE
ELSE IF @Action = 2
BEGIN
	DELETE tblForumForumGroups WHERE ForumGroupID = @ForumGroupID
END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForumGroups_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumForumGroups_Get
GO
CREATE  PROCEDURE spForumForumGroups_Get
(
	@SiteID int = 0
)
AS
BEGIN

SELECT 
	*
FROM
	tblForumForumGroups
WHERE
	(SiteID = @SiteID OR SiteID = 0)
			
END


GO


-- sp_helptext spForumForum_CreateUpdateDelete

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_CreateUpdateDelete
GO
CREATE PROCEDURE spForumForum_CreateUpdateDelete
(
	@ForumID	int out,
	@DeleteForum	bit = 0,
	@Name		nvarchar(256) = '',
	@Description	nvarchar(3000) = '',
	@ParentID	int = 0,
	@ForumGroupID	int = 0,
	@IsModerated	bit = 1,
	@DisplayPostsOlderThan	int = 0,
	@IsActive 	bit = 0,
	@EnablePostStatistics bit = 1,
	@EnableAutoDelete bit = 0,
	@EnableAnonymousPosting bit = 0,
	@AutoDeleteThreshold int = 90,
	@SortOrder int = 0
)
AS

-- Are we deleting the forum?
IF @DeleteForum = 1
BEGIN
	-- delete the specified forum and all of its posts
	-- first we must remove all the thread tracking rows
	DELETE 
		tblForumTrackedThreads
	WHERE 
		ThreadID IN (SELECT DISTINCT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID)

	-- we must remove all of the moderators for this forum
	DELETE 
		tblForumModerators
	WHERE 
		ForumID = @ForumID

	-- now we must remove all of the posts
	DELETE 
		tblForumPosts
	WHERE 
		ForumID = @ForumID

	-- finally we can delete the actual forum
	DELETE 
		tblForumForums
	WHERE 
		ForumID = @ForumID

	RETURN
END

-- Are we updating a forum
IF @ForumID > 0
BEGIN
	-- if we are making the forum non-moderated, remove all forum moderators for this forum
	IF @IsModerated = 0
		DELETE 
			tblForumModerators
		WHERE 
			ForumID = @ForumID

	-- Update the forum information
	UPDATE 
		tblForumForums 
	SET
		Name = @Name,
		Description = @Description,
		ParentID = @ParentID,
		ForumGroupID = @ForumGroupID,
		IsModerated = @IsModerated,
		IsActive = @IsActive,
		DaysToView = @DisplayPostsOlderThan,
		EnablePostStatistics = @EnablePostStatistics,
		EnableAutoDelete = @EnableAutoDelete,
		EnableAnonymousPosting = @EnableAnonymousPosting,
		AutoDeleteThreshold = @AutoDeleteThreshold,
		SortOrder = @SortOrder
	WHERE 
		ForumID = @ForumID
END
ELSE
BEGIN

	-- Create a new Forum
	INSERT INTO 
		tblForumForums (
			Name, 
			Description, 
			ParentID, 
			ForumGroupID, 
			IsModerated, 
			DaysToView, 
			IsActive,
			EnablePostStatistics,
			EnableAutoDelete,
			AutoDeleteThreshold
			)
		VALUES (
			@Name,
			@Description,
			@ParentID,
			@ForumGroupID,
			@IsModerated,
			@DisplayPostsOlderThan,
			@IsActive,
			@EnablePostStatistics,
			@EnableAutoDelete,
			@AutoDeleteThreshold
			)
	
	SET @ForumID = @@Identity

END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_GetByPostID' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_GetByPostID
GO
CREATE PROCEDURE spForumForum_GetByPostID
(
	@UserID			int = 0,
	@PostID			int = 0
)
AS

-- Loop up the forum by PostID
DECLARE @ForumID int
SET @ForumID = (SELECT ForumID FROM tblForumPosts WHERE PostID = @PostID)

SELECT @ForumID

-- Return all the forums
SELECT
	F.*,
	LastUserActivity = ISNULL((SELECT LastActivity FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = F.ForumID), '1/1/1797')
FROM
	tblForumForums F
WHERE
	ForumID = @ForumID


-- Return permissions for this user
SELECT
	P.*
FROM
	tblForumForumPermissions P,
	tblForumUsersInRoles R
WHERE
	P.RoleID = R.RoleID AND
	(R.UserID = @UserID OR R.UserID = 0) AND
	P.ForumID = @ForumID


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_GetForumIDByPostID' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_GetForumIDByPostID
GO
CREATE PROCEDURE spForumForum_GetForumIDByPostID
(
	@UserID			int = 0,
	@PostID			int = 0
)
AS

-- Loop up the forum by PostID
DECLARE @ForumID int
SET @ForumID = (SELECT ForumID FROM tblForumPosts WHERE PostID = @PostID)

SELECT @ForumID


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_MarkRead' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_MarkRead
GO
CREATE    procedure spForumForum_MarkRead
(
	@UserID int,
	@ForumID int = 0,
	@ForumGroupID int = 0,
	@MarkAllThreadsRead bit = 0
)
AS
BEGIN
DECLARE @LastReadThread int

	SET NOCOUNT ON

	IF @UserID = 0
		RETURN

	-- Are we marking all forums as read?
	IF @ForumGroupID = 0 AND @ForumID = 0
	BEGIN

		-- 1. Delete any entries for this user
		DELETE tblForumForumsRead WHERE UserID = @UserID
		DELETE tblForumThreadsRead WHERE UserID = @UserID

		-- 2. INSERT into tblForumForumsRead
		INSERT INTO tblForumForumsRead
		SELECT ForumGroupID, ForumID, @UserID, 0, 0, GetDate() FROM tblForumForums F

		RETURN
	END

	-- Are we marking a particular forum group as read?
	IF @ForumGroupID > 0 AND @ForumID = 0
	BEGIN

		-- 1. Delete any entries for this user
		DELETE tblForumForumsRead WHERE UserID = @UserID AND ForumGroupID = @ForumGroupID
		DELETE tblForumThreadsRead WHERE UserID = @UserID AND ForumGroupID = @ForumGroupID

		-- 2. Insert into tblForumForums Read
		INSERT INTO tblForumForumsRead
		SELECT ForumGroupID, ForumID, @UserID, 0, 0, GetDate() FROM tblForumForums F WHERE ForumGroupID = @ForumGroupID

		RETURN
	END

	-- Are we marking an individual forum as read?
	IF @ForumID > 0
	BEGIN
		IF @MarkAllThreadsRead = 1
			IF EXISTS (SELECT UserID FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = @ForumID)
				UPDATE 
					tblForumForumsRead 
				SET 
					NewPosts = 0,
					MarkReadAfter = (SELECT (MostRecentPostID + 1) FROM tblForumForums F WHERE ForumID = @ForumID),
					LastActivity = GetDate()
				WHERE
					UserID = @UserID AND
					ForumID = @ForumID
			ELSE
				INSERT INTO 
					tblForumForumsRead
				SELECT ForumGroupID, ForumID, @UserID, (MostRecentPostID + 1), 0, GetDate() FROM tblForumForums F WHERE ForumID = @ForumID
		ELSE
			IF (SELECT NewPosts FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = @ForumID) = 1
				UPDATE
					tblForumForumsRead							
				SET 
					NewPosts = 0
				WHERE
					UserID = @UserID AND
					ForumID = @ForumID

	END
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_Permission_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_Permission_CreateUpdateDelete
GO
CREATE  procedure spForumForum_Permission_CreateUpdateDelete
(
	@ForumID 	int,
	@RoleID		int,
	@Action		int,
	@View		tinyint = 0,
	@Read		tinyint = 0,
	@Post		tinyint = 0,
	@Reply		tinyint = 0,	
	@Edit		tinyint = 0,
	@Delete		tinyint = 0,
	@Sticky		tinyint = 0,
	@Announce	tinyint = 0,
	@CreatePoll	tinyint = 0,
	@Vote		tinyint = 0,
	@Moderate       tinyint = 0,
	@Attachment     tinyint = 0
)
AS
BEGIN

-- Create
IF @Action = 0
BEGIN

	-- Check if an entry already exists
	IF EXISTS (SELECT ForumID FROM tblForumForumPermissions WHERE ForumID = @ForumID AND RoleID = @RoleID)
		exec spForumForum_Permission_CreateUpdateDelete @ForumID, @RoleID, 1, @View, @Read, @Post, @Reply, @Edit, @Delete, @Sticky, @Announce, @CreatePoll, @Vote, @Moderate
	ELSE
		INSERT INTO 
			tblForumForumPermissions 
		VALUES	(
				@ForumID,
				@RoleID,
				@View,
				@Read,
				@Post,
				@Reply,
				@Edit,
				@Delete,
				@Sticky,
				@Announce,
				@CreatePoll,
				@Vote,
				@Moderate,
				@Attachment
			)
END
-- UPDATE
ELSE IF @Action = 1
BEGIN

	IF NOT EXISTS (SELECT ForumID FROM tblForumForumPermissions WHERE ForumID = @ForumID AND RoleID = @RoleID)
		exec spForumForum_Permission_CreateUpdateDelete @ForumID, @RoleID, 0, @View, @Read, @Post, @Reply, @Edit, @Delete, @Sticky, @Announce, @CreatePoll, @Vote, @Moderate
	ELSE
		UPDATE
			tblForumForumPermissions
		SET
			[View] = 	@View,
			[Read] =	@Read,
			Post =		@Post,
			Reply =		@Reply,
			Edit =		@Edit,
			[Delete] =	@Delete,
			Sticky = 	@Sticky,
			Announce = 	@Announce,
			CreatePoll = 	@CreatePoll,
			Vote =		@Vote,
			Moderate =      @Moderate,
			Attachment =    @Attachment
		WHERE
			ForumID = @ForumID AND
			RoleID = @RoleID

END
ELSE IF @Action = 2
BEGIN
	DELETE tblForumForumPermissions WHERE ForumID = @ForumID AND RoleID = @RoleID
END

END


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_Permissions_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_Permissions_Get
GO
create procedure spForumForum_Permissions_Get
(
	@ForumID int
)
AS
SELECT 
	R.Name,
	P.* 
FROM 
	tblForumForumPermissions P, 
	tblForumRoles R 
WHERE 
	P.RoleID = R.RoleID AND
	P.ForumID = @ForumID





GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_RssPingback_Update' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_RssPingback_Update
GO
create procedure spForumForum_RssPingback_Update (
	@ForumID int,
	@Pingback nvarchar(512),
	@Count int
)
AS
BEGIN

	IF EXISTS (SELECT ForumID FROM tblForumForumPingback WHERE ForumID = @ForumID AND Pingback = @Pingback)
		UPDATE
			tblForumForumPingback
		SET
			[Count] = [Count] + @Count,
			LastUpdated = GetDate()
		WHERE
			ForumID = @ForumID AND
			Pingback = @Pingback
	ELSE
		INSERT INTO
			tblForumForumPingback
		VALUES
			(@ForumID, @Pingback, @Count, GetDate())
			

END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumForum_UpdateSortOrder' 
				AND type = 'P')
   DROP PROCEDURE spForumForum_UpdateSortOrder
GO
CREATE procedure spForumForum_UpdateSortOrder
        ( 
                 @ForumID int, 
                 @MoveUp bit 
        ) 
        AS 
        BEGIN 
        DECLARE @currentSortValue int 
        DECLARE @replaceSortValue int 

        -- Get the current sort order 
        SELECT @currentSortValue = SortOrder FROM tblForumForums WHERE ForumID = @ForumID 

        -- Move the item up or down? 
        IF (@MoveUp = 1) 
          BEGIN 
                IF (@currentSortValue != 1) 
                  BEGIN 
                        SET @replaceSortValue = @currentSortValue - 1 

                        UPDATE tblForumForums SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue 
                        UPDATE tblForumForums SET SortOrder = @replaceSortValue WHERE ForumID = @ForumID 
                  END 
          END 

        ELSE 
          BEGIN 
                IF (@currentSortValue < (SELECT MAX(ForumID) FROM tblForumForums)) 
                BEGIN 
                  SET @replaceSortValue = @currentSortValue + 1 

                  UPDATE tblForumForums SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue 
                  UPDATE tblForumForums SET SortOrder = @replaceSortValue WHERE ForumID = @ForumID 
                END 
          END 
        END 

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForum_ForumsGet' 
				AND type = 'P')
   DROP PROCEDURE spForum_ForumsGet
GO
CREATE              PROCEDURE spForum_ForumsGet
(
	@SiteID			int = 0,
	@UserID			int = 0
)
AS

-- Return all the forums
SELECT
	F.*,
	LastUserActivity = (CASE @UserID
		WHEN 0 THEN '1/1/1797'
		ELSE ( ISNULL( (SELECT LastActivity FROM tblForumForumsRead WHERE UserID = @UserID AND ForumID = F.ForumID), '1/1/1797'))
        END)
FROM
	tblForumForums F
WHERE
	(SiteID = @SiteID OR SiteID = 0) AND
	IsActive = 1


-- Return permissions for this user
SELECT
	P.*
FROM
	tblForumForumPermissions P,
	tblForumUsersInRoles R
WHERE
	P.RoleID = R.RoleID AND
	(R.UserID = @UserID OR R.UserID = 0)



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetEmail' 
				AND type = 'P')
   DROP PROCEDURE spForumGetEmail
GO
CREATE procedure spForumGetEmail
(
	@EmailID	int = 0
)
AS
	IF @EmailID = 0
		SELECT
			*
		FROM 
			tblForumEmails (nolock)
		ORDER BY 
			Description
	ELSE
		SELECT
			*
		FROM 
			tblForumEmails (nolock)
		WHERE
			EmailID = @EmailID
		ORDER BY 
			Description



GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetForumMessages' 
				AND type = 'P')
   DROP PROCEDURE spForumGetForumMessages
GO
CREATE procedure spForumGetForumMessages
(
	@MessageID int = 0
)
AS

IF @MessageID = 0
	SELECT 
		*
	FROM
		tblForumMessages
ELSE
	SELECT 
		*
	FROM
		tblForumMessages
	WHERE
		MessageID = @MessageID


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetForumModerators' 
				AND type = 'P')
   DROP PROCEDURE spForumGetForumModerators
GO
CREATE  PROCEDURE spForumGetForumModerators
(
	@ForumID	int
)
 AS
	-- get a list of forum moderators
	SELECT 
		UserName, EmailNotification, DateCreated
	FROM 
		Moderators (nolock)
	WHERE 
		ForumID = @ForumID OR ForumID = 0




GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetForumSubscriptionType' 
				AND type = 'P')
   DROP PROCEDURE spForumGetForumSubscriptionType
GO
CREATE  procedure spForumGetForumSubscriptionType
(
	@UserID int,
	@ForumID int,
	@SubType int OUTPUT
)
AS
SELECT SubscriptionType FROM tblForumTrackedForums WHERE ForumID=@ForumID AND UserID=@UserID


GO




-- PLePage added 7/14/03
-- Added for Forum Subscription stuff.
IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetForumTrackingEmailsForThread' 
				AND type = 'P')
   DROP PROCEDURE spForumGetForumTrackingEmailsForThread
GO
CREATE    PROCEDURE spForumGetForumTrackingEmailsForThread
(
	@ForumID	int,
	@SubscriptionType	int
)
AS
	-- now, get all of the emails of the users who are tracking this thread
	SELECT
		Email
	FROM 
		Users U (nolock),
		ForumTrackings F
	WHERE
		U.Username = F.Username AND
		F.ForumID = @ForumID AND
		F.subType = @SubscriptionType


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetForumsModeratedByUser' 
				AND type = 'P')
   DROP PROCEDURE spForumGetForumsModeratedByUser
GO
create procedure spForumGetForumsModeratedByUser
(
	@UserName	nvarchar(50)
)
 AS
	-- determine if this user can moderate ALL forums
	IF EXISTS(SELECT NULL FROM Moderators (nolock) WHERE ForumID = 0 AND Username = @UserName)
		SELECT ForumID, ForumName = 'All Forums', EmailNotification, DateCreated FROM Moderators (nolock)
		WHERE ForumID = 0 AND Username = @UserName
	ELSE
		-- get all of the forums moderated by this particular user
		SELECT
			M.ForumID,
			EmailNotification,
			ForumName = F.Name,
			M.DateCreated
		FROM Moderators M (nolock)
			INNER JOIN Forums F (nolock) ON
				F.ForumID = M.ForumID
		WHERE Username = @UserName



GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetForumsNotModeratedByUser' 
				AND type = 'P')
   DROP PROCEDURE spForumGetForumsNotModeratedByUser
GO
CREATE  PROCEDURE spForumGetForumsNotModeratedByUser
(
	@UserName	nvarchar(50)
)
 AS
	-- determine if this user can moderate ALL forums
	IF NOT EXISTS(SELECT NULL FROM Moderators (nolock) WHERE ForumID = 0 AND Username = @UserName)
		-- get all of the forums NOT moderated by this particular user
		SELECT ForumID =  0, ForumName =  'All Forums'
		UNION
		SELECT
			ForumID,
			ForumName = F.Name
		FROM Forums F (nolock) 
		WHERE ForumID NOT IN (SELECT ForumID FROM Moderators (nolock) WHERE Username = @UserName)



GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetMessage' 
				AND type = 'P')
   DROP PROCEDURE spForumGetMessage
GO
create                PROCEDURE spForumGetMessage
(
	@MessageID int
)
 AS
BEGIN
	SELECT
		Title,
		Body
	FROM
		Messages
	WHERE
		MessageID = @MessageID
END




GO





IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetModeratedForums' 
				AND type = 'P')
   DROP PROCEDURE spForumGetModeratedForums
GO
CREATE       PROCEDURE spForumGetModeratedForums
(
	@UserName nvarchar(50)
)
 AS
	-- returns a list of posts awaiting moderation
	-- the posts returned are those that this user can work on
	-- if Moderators.ForumID = 0 for this user, then they can moderate ALL forums
	IF EXISTS(SELECT NULL FROM Moderators (nolock) WHERE UserName=@UserName AND ForumID=0)
		-- return ALL posts awaiting moderation
		SELECT
			ForumID,
			ForumGroupID,
			Name,
			Description,
			DateCreated,
			Moderated,
			DaysToView,
			Active,
			SortOrder
		FROM 
			Forums
		WHERE 	
			Active = 1
		ORDER BY 
			DateCreated
	ELSE
		-- return only those posts in the forum this user can moderate
		SELECT
			ForumID,
			ForumGroupID,
			Name,
			Description,
			DateCreated,
			Moderated,
			DaysToView,
			Active,
			SortOrder

		FROM 
			Forums
		WHERE 
			Active = 1 AND 
			ForumID IN (SELECT ForumID FROM Moderators (nolock) WHERE UserName=@UserName)
		ORDER BY 
			DateCreated



GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetModeratorsForEmailNotification' 
				AND type = 'P')
   DROP PROCEDURE spForumGetModeratorsForEmailNotification
GO
create procedure spForumGetModeratorsForEmailNotification
(
	@PostID	int
)
 AS
	-- get the ForumID
	DECLARE @ForumID int
	SELECT @ForumID = ForumID FROM Posts (nolock) WHERE PostID = @PostID
	SELECT
		U.Username,
		Email
	FROM Users U (nolock)
		INNER JOIN Moderators M (nolock) ON
			M.UserName = U.UserName
	WHERE (M.ForumID = @ForumID OR M.ForumID = 0) AND M.EmailNotification = 1


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetPostRead' 
				AND type = 'P')
   DROP PROCEDURE spForumGetPostRead
GO
CREATE    PROCEDURE spForumGetPostRead
(
	@PostID int,
	@UserName nvarchar (50)
)
 AS
BEGIN
	DECLARE @HasRead bit
	SET @HasRead = 0

	IF EXISTS 
	(
		SELECT
			HasRead
		FROM
			PostsRead
		WHERE
			PostID = @PostID AND
			Username = @UserName
	)
		SET @HasRead = 1

	SELECT HasRead = @HasRead
END


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetSQLSearchResults' 
				AND type = 'P')
   DROP PROCEDURE spForumGetSQLSearchResults
GO
CREATE     PROCEDURE spForumGetSQLSearchResults
(
	@ForumID int,
	@PageSize int,
	@PageIndex int, 
	@SearchTerms nvarchar(500),
	@UserName nvarchar (50) = null
)
AS
	SET NOCOUNT ON

	DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int
	DECLARE @RowsToReturn int
	DECLARE @MoreRecords int
	
	-- First set the rowcount
	SET @RowsToReturn = @PageSize * (@PageIndex + 1)
	SET @MoreRecords = @RowsToReturn + 1
	SET ROWCOUNT @MoreRecords

	-- Set the page bounds
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1

	-- Create a temp table to store the select results
	CREATE TABLE #PageIndex 
	(
		IndexID int IDENTITY (1, 1) NOT NULL,
		PostID int
	)

        -- Are we selecting from a specific forum?
        IF @ForumID > 0 AND @UserName is null
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND ForumID = @ForumID AND ForumID NOT IN (SELECT ForumID from PrivateForums) ORDER BY PostDate Desc
        ELSE IF @ForumID = 0 AND @UserName is null
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND ForumID NOT IN (SELECT ForumID from PrivateForums) ORDER BY PostDate Desc
	ELSE IF @ForumID > 0 AND @UserName is not null
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND ForumID = @ForumID  AND (P.ForumID NOT IN (SELECT ForumID from PrivateForums) OR P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName))) ORDER BY PostDate Desc
	Else
		INSERT INTO #PageIndex (PostID)
		SELECT PostID FROM Posts P WHERE Contains(body, @SearchTerms) AND Approved = 1 AND (P.ForumID NOT IN (SELECT ForumID from PrivateForums) OR P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName))) ORDER BY PostDate Desc

	-- Do we have more records?
        IF (@MoreRecords > (SELECT count(*) FROM #PageIndex))
		SET @MoreRecords = @RowsToReturn

	-- Select the data out of the temporary table
	SELECT
		PageIndex.PostID,
		MoreRecords = @MoreRecords,
		P.ParentID,
		P.ThreadID,
		P.PostLevel,
		P.SortOrder,
		P.UserName,
		P.Subject,
		P.PostDate,
		P.ThreadDate,
		P.Approved,
		P.ForumID,
		F.Name As ForumName,
		Replies = 0,
		P.Body,
		P.TotalViews,
		P.IsLocked,
		HasRead = 0 -- not used
	FROM 
		#PageIndex PageIndex,
		Posts P,
		Forums F
	WHERE 
		P.PostID = PageIndex.PostID AND
		P.ForumID = F.ForumID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

	SET NOCOUNT OFF


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetSearchResults' 
				AND type = 'P')
   DROP PROCEDURE spForumGetSearchResults
GO
CREATE     PROCEDURE spForumGetSearchResults
(
	@SearchTerms	nvarchar(500),
	@Page int,
	@RecsPerPage int,
	@UserName nvarchar(50)
)
 AS
	CREATE TABLE #tmp
	(
		ID int IDENTITY,
		PostID int
	)
	DECLARE @sql nvarchar(1000)
	SET NOCOUNT ON
	SELECT @sql = 'INSERT INTO #tmp(PostID) SELECT PostID ' + 
			'FROM Posts P (nolock) INNER JOIN Forums F (nolock) ON F.ForumID = P.ForumID ' +
			@SearchTerms + ' ORDER BY ThreadDate DESC'
	EXEC(@sql)

	-- ok, all of the rows are inserted into the table.
	-- now, select the correct subset
	DECLARE @FirstRec int, @LastRec int
	SELECT @FirstRec = (@Page - 1) * @RecsPerPage
	SELECT @LastRec = (@Page * @RecsPerPage + 1)
	DECLARE @MoreRecords int
	SELECT @MoreRecords = COUNT(*)  FROM #tmp -- WHERE ID >= @LastRec


	-- Select the data out of the temporary table
	IF @UserName IS NOT NULL
		SELECT
			T.PostID,
			P.ParentID,
			P.ThreadID,
			P.PostLevel,
			P.SortOrder,
			P.UserName,
			P.Subject,
			P.PostDate,
			P.ThreadDate,
			P.Approved,
			P.ForumID,
			F.Name As ForumName,
			MoreRecords = @MoreRecords,
			Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
			P.Body,
			P.TotalViews,
			P.IsLocked,
			HasRead = 0 -- not used
		FROM 
			#tmp T
			INNER JOIN Posts P (nolock) ON
				P.PostID = T.PostID
			INNER JOIN Forums F (nolock) ON
				F.ForumID = P.ForumID
		WHERE 
			T.ID > @FirstRec AND ID < @LastRec AND
			(P.ForumID NOT IN (SELECT ForumID from PrivateForums) OR
			P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName)))
	ELSE
		SELECT
			T.PostID,
			P.ParentID,
			P.ThreadID,
			P.PostLevel,
			P.SortOrder,
			P.UserName,
			P.Subject,
			P.PostDate,
			P.ThreadDate,
			P.Approved,
			P.ForumID,
			F.Name As ForumName,
			MoreRecords = @MoreRecords,
			Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
			P.Body,
			P.TotalViews,
			P.IsLocked,
			HasRead = 0 -- not used
		FROM 
			#tmp T
			INNER JOIN Posts P (nolock) ON
				P.PostID = T.PostID
			INNER JOIN Forums F (nolock) ON
				F.ForumID = P.ForumID
		WHERE 
			T.ID > @FirstRec AND ID < @LastRec AND
			P.ForumID NOT IN (SELECT ForumID from PrivateForums)

	SET NOCOUNT OFF


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetSearchResultsByTextPhrase_FTQ' 
				AND type = 'P')
   DROP PROCEDURE spForumGetSearchResultsByTextPhrase_FTQ
GO
create  PROCEDURE spForumGetSearchResultsByTextPhrase_FTQ (
    @Pattern1 nvarchar(250),
    @ForumID int,
    @Username nvarchar(50)
) AS
    IF @@NESTLEVEL > 1 BEGIN
        INSERT INTO #SearchResults(PostID)
        SELECT PostID
        FROM Posts P (nolock)
        WHERE
            Approved = 1 AND
            (
                @ForumID = 0 OR
                ForumID = @ForumID
            ) AND
            (
                P.ForumID NOT IN (SELECT ForumID FROM PrivateForums) OR
                P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName FROM UsersInRoles WHERE Username = @Username))
            ) AND
            FREETEXT(Body, @Pattern1)
        ORDER BY ThreadDate DESC
    END


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetSearchResultsByText_FTQ' 
				AND type = 'P')
   DROP PROCEDURE spForumGetSearchResultsByText_FTQ
GO
CREATE PROCEDURE spForumGetSearchResultsByText_FTQ (
    @Pattern1 nvarchar(250),
    @ForumID int,
    @Username nvarchar(50)
) AS
    IF @@NESTLEVEL > 1 BEGIN
        INSERT INTO #SearchResults(PostID)
        SELECT PostID
        FROM Posts P (nolock)
        WHERE
            Approved = 1 AND
            (
                @ForumID = 0 OR
                ForumID = @ForumID
            ) AND
            (
                P.ForumID NOT IN (SELECT ForumID FROM PrivateForums) OR
                P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName FROM UsersInRoles WHERE Username = @Username))
            ) AND
            CONTAINS(Body, @Pattern1)
        ORDER BY ThreadDate DESC
    END


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetSearchResultsByUser' 
				AND type = 'P')
   DROP PROCEDURE spForumGetSearchResultsByUser
GO
CREATE    PROCEDURE spForumGetSearchResultsByUser (
    @Page int,
    @RecsPerPage int,
    @ForumID int = 0,
    @UserPattern nvarchar(50),
    @UserName nvarchar(50) = NULL,
    @MoreRecords bit output,
    @Status bit output
) AS


    -- Performance optimizations
    SET NOCOUNT ON
    -- Global declarations
    DECLARE @sql nvarchar(1000)
    DECLARE @FirstRec int, @LastRec int, @MoreRec int

    SET @FirstRec = (@Page - 1) * @RecsPerPage;
    SET @LastRec = (@FirstRec + @RecsPerPage);
    SET @MoreRec = @LastRec + 1;
    SET @MoreRecords = 0;

    CREATE TABLE #SearchResults (
        IndexID int IDENTITY(1,1),
        PostID int
    )

    -- Turn on rowcounting for performance
    SET ROWCOUNT @MoreRec;
    INSERT INTO #SearchResults(PostID)
    SELECT PostID
    FROM Posts P (nolock)
    WHERE
        Approved = 1 AND
        (
            @ForumID = 0 OR
            ForumID = @ForumID
        ) AND
        (
            P.ForumID NOT IN (SELECT ForumID FROM PrivateForums) OR
            P.ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName FROM UsersInRoles WHERE UserName = @UserName))
        ) AND
        0 < ISNULL(PATINDEX(@UserPattern, Username), 1)
    ORDER BY ThreadDate DESC
    IF @@ROWCOUNT > @LastRec SET @MoreRecords = 1
    SET ROWCOUNT 0
    -- Turn off rowcounting

    -- Select the data out of the temporary table
    SELECT
        P.*,
	ForumName = F.Name,
	Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
        HasRead = 0 -- not used
    FROM 
        Posts P (nolock), Forums F, #SearchResults
    WHERE
        P.PostID = #SearchResults.PostID AND
        P.ForumID = F.ForumID AND
        #SearchResults.IndexID > @FirstRec AND
        #SearchResults.IndexID <= @LastRec
    ORDER BY #SearchResults.IndexID ASC

GO



-- PLePage added 7/14/03
-- Added for Forum Subscription stuff.
IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetSubscriptionType' 
				AND type = 'P')
   DROP PROCEDURE spForumGetSubscriptionType
GO
CREATE procedure spForumGetSubscriptionType
(
	@UserID int,
	@ForumID int,
	@SubscriptionType int OUTPUT
)
 AS
select SubscriptionType from tblForumTrackedForums (nolock) where UserID=@UserID AND ForumID=@ForumID



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetThread' 
				AND type = 'P')
   DROP PROCEDURE spForumGetThread
GO
create procedure spForumGetThread
(
	@ThreadID int
) AS
SELECT
	PostID,
	ForumID,
	Subject,
	ParentID,
	ThreadID,
	PostLevel,
	SortOrder,
	PostDate,
	ThreadDate,
	UserName,
	Approved,
	Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	Body
FROM Posts P (nolock)
WHERE Approved = 1 AND ThreadID = @ThreadID
ORDER BY SortOrder


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetThreadByParentID' 
				AND type = 'P')
   DROP PROCEDURE spForumGetThreadByParentID
GO
CREATE    PROCEDURE spForumGetThreadByParentID
(
	@ParentID	int
)
 AS
BEGIN
	SELECT 
		PostID,
		ThreadID,
		ForumID,
		Subject,
		ParentID,
		PostLevel,
		SortOrder,
		PostDate,
		ThreadDate,
		UserName,
		Approved,
		Replies = (SELECT COUNT(*) FROM Posts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
		Body,
		TotalMessagesInThread = 0, -- not used
		TotalViews,
		IsLocked
	FROM
		Posts P
	WHERE
		Approved = 1 AND
		ParentID = @ParentID
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetTop25NewPosts' 
				AND type = 'P')
   DROP PROCEDURE spForumGetTop25NewPosts
GO
CREATE  procedure spForumGetTop25NewPosts
(
	@UserName nvarchar(50)
)
AS

IF @UserName IS NULL
	SELECT TOP 25
		Subject,
		Body,
		P.PostID,
		ThreadID,
		ParentID,
		PostDate = (SELECT Max(PostDate) FROM Posts WHERE P.ThreadID = ThreadID),
		ThreadDate,
		UserName,
		Replies = (SELECT COUNT(*) FROM Posts WHERE P.ThreadID = ThreadID AND PostLevel != 1 AND Approved = 1),
		Body,
		TotalViews,
		IsLocked,
		HasRead = 0,
		MostRecentPostAuthor = (SELECT TOP 1 Username FROM Posts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC),
		MostRecentPostID = (SELECT TOP 1 PostID FROM Posts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC)
	FROM 
		Posts P 
	WHERE 
		PostLevel = 1 AND 
		Approved = 1 AND
		ForumID NOT IN (SELECT ForumID from PrivateForums)
	ORDER BY 
		ThreadDate DESC
ELSE
	SELECT TOP 25 
		Subject,
		Body,
		P.PostID,
		ThreadID,
		ParentID,
		PostDate = (SELECT Max(PostDate) FROM Posts WHERE P.ThreadID = ThreadID),
		ThreadDate,
		UserName,
		Replies = (SELECT COUNT(*) FROM Posts WHERE P.ThreadID = ThreadID AND PostLevel != 1 AND Approved = 1),
		Body,
		TotalViews,
		IsLocked,
		HasRead = 0,
		MostRecentPostAuthor = (SELECT TOP 1 Username FROM Posts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC),
		MostRecentPostID = (SELECT TOP 1 PostID FROM Posts WHERE P.ThreadID = ThreadID AND Approved = 1 ORDER BY PostDate DESC)
	FROM 
		Posts P
	WHERE 
		PostLevel = 1 AND 
		Approved = 1 AND
		(ForumID NOT IN (SELECT ForumID from PrivateForums) OR
		ForumID IN (SELECT ForumID FROM PrivateForums WHERE RoleName IN (SELECT RoleName from UsersInRoles WHERE username = @UserName)))
	ORDER BY 
		ThreadDate DESC



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetTotalNumberOfForums' 
				AND type = 'P')
   DROP PROCEDURE spForumGetTotalNumberOfForums
GO
Create   PROCEDURE spForumGetTotalNumberOfForums
AS

	SELECT
		COUNT (*)
	FROM
		Forums



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetTotalPostCount' 
				AND type = 'P')
   DROP PROCEDURE spForumGetTotalPostCount
GO
CREATE   PROCEDURE spForumGetTotalPostCount
 AS
	SELECT TOP 1 
		TotalPosts 
	FROM 
		tblForumStatistics

GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetTotalPostsForThread' 
				AND type = 'P')
   DROP PROCEDURE spForumGetTotalPostsForThread
GO
CREATE    PROCEDURE spForumGetTotalPostsForThread
(
	@PostID	int
)
 AS
	DECLARE @ThreadID int

	-- Make sure we're working with the threadid
	SELECT 
		@ThreadID = ThreadID
	FROM 
		tblForumPosts
	WHERE
		PostID = @PostID

	-- Get the count of posts for a given thread
	SELECT 
		TotalPostsForThread = COUNT(PostID)
	FROM 
		tblForumPosts (nolock)
	WHERE 
		ThreadID = @ThreadID



GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetUnmoderatedPostStatus' 
				AND type = 'P')
   DROP PROCEDURE spForumGetUnmoderatedPostStatus
GO
CREATE  PROCEDURE spForumGetUnmoderatedPostStatus
(
	@ForumID int = null,
	@UserName nvarchar (50) = null
)
AS
BEGIN
DECLARE @DateDiff int
DECLARE @TotalCount int

	-- Is the user allowed to retrieve this data?
	IF NOT EXISTS(SELECT UserName FROM Moderators WHERE UserName = @UserName)
		RETURN
		
	IF @ForumID = 0
		SET @ForumID = null

	SELECT 
		OldestPostAgeInMinutes = datediff(mi, isnull(min(postdate),getdate()),getdate()),
		TotalPostsInModerationQueue = count(PostID)
	FROM 
		POSTS P 
	WHERE 
		ForumID = isnull(@ForumID,ForumID) AND 
		Approved = 0


END



GO





IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetUserIDByAppToken' 
				AND type = 'P')
   DROP PROCEDURE spForumGetUserIDByAppToken
GO
CREATE         PROCEDURE spForumGetUserIDByAppToken
(
	@AppUserToken varchar(128)
)
AS

SELECT
	U.UserID
FROM 
	tblForumUsers U (nolock)
WHERE 
	U.AppUserToken = @AppUserToken




GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetUserNameFromPostID' 
				AND type = 'P')
   DROP PROCEDURE spForumGetUserNameFromPostID
GO
create procedure spForumGetUserNameFromPostID
(
	@PostID	int
)
 AS
	-- returns who posted a particular post
	SELECT UserName
	FROM Posts (nolock)
	WHERE PostID = @PostID




GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetUsersByName' 
				AND type = 'P')
   DROP PROCEDURE spForumGetUsersByName
GO
CREATE         PROCEDURE spForumGetUsersByName
(
	@PageIndex int,
	@PageSize int,
	@UserNameToFind nvarchar(50)
)
AS

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- Create a temp table to store the select results
CREATE TABLE #PageIndexForUsers 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	UserID int
)	

-- Insert into our temp table
INSERT INTO #PageIndexForUsers (UserID)
SELECT 
	U.UserID 
FROM 
	tblForumUsers U,
	tblForumUserProfile P
WHERE 
	U.UserID = P.UserID AND
	UserAccountStatus = 1 AND 
	EnableDisplayInMemberList = 1 AND
	UserName like '%' + @UserNameToFind + '%'
ORDER BY 
	DateCreated


SELECT
	*,
	IsModerator = (select count(*) from moderators where username = U.UserName)
FROM 
	tblForumUsers U (nolock),
	tblForumUserProfile P,
	#PageIndexForUsers
WHERE 
	U.UserID = #PageIndexForUsers.UserID AND
	#PageIndexForUsers.IndexID > @PageLowerBound AND
	#PageIndexForUsers.IndexID < @PageUpperBound AND
	U.UserID = P.UserID AND
	UserAccountStatus = 1

ORDER BY
	#PageIndexForUsers.IndexID


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumGetVoteResults' 
				AND type = 'P')
   DROP PROCEDURE spForumGetVoteResults
GO
CREATE  procedure spForumGetVoteResults (
	@PostID int
)
AS
BEGIN
  SELECT
	Vote,
	VoteCount
  FROM
	Vote
  WHERE
	PostID = @PostID
END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumImage_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumImage_CreateUpdateDelete
GO
CREATE procedure spForumImage_CreateUpdateDelete
(
    @UserID  int,
    @Content image,
    @ContentType nvarchar(64),
    @ContentSize int,
    @Action  int
)
AS
BEGIN
    DECLARE @ImageID int

    -- Create
    IF @Action = 0 OR @Action = 1
    BEGIN
        -- Remove if already exists from tables: tblForumImages, tblForumUserAvatar
        SET @ImageID = (SELECT ImageID FROM tblForumUserAvatar WHERE UserID = @UserID)
        DELETE tblForumImages WHERE ImageID = @ImageID
        DELETE tblForumUserAvatar WHERE UserID = @UserID

        -- Add new entry
        INSERT INTO tblForumImages VALUES (@ContentSize, @ContentType, @Content, GetDate())
        SET @ImageID = @@Identity
 
        INSERT INTO tblForumUserAvatar VALUES (@UserID, @ImageID)
    END
    ELSE IF @Action = 2
    BEGIN
        -- Remove if already exists from tables: tblForumImages, tblForumUserAvatar
        SET @ImageID = (SELECT ImageID FROM tblForumUserAvatar WHERE UserID = @UserID)
        DELETE tblForumUserAvatar WHERE UserID = @UserID
        DELETE tblForumImages WHERE ImageID = @ImageID
    END
END


GO



-- PLePage added 7/14/03
-- Added for Forum subscription stuff.
IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumIsUserTrackingForum' 
				AND type = 'P')
   DROP PROCEDURE spForumIsUserTrackingForum
GO
CREATE procedure spForumIsUserTrackingForum
(
	@ForumID int,
	@UserID int
)
AS
DECLARE @TrackingForum bit

IF EXISTS(SELECT ForumID FROM tblForumTrackedForums (nolock) WHERE ForumID = @ForumID AND UserID=@UserID)
	SELECT IsUserTrackingPost = 1
ELSE
	SELECT IsUserTrackingPost = 0


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumIsUserTrackingPost' 
				AND type = 'P')
   DROP PROCEDURE spForumIsUserTrackingPost
GO
CREATE procedure spForumIsUserTrackingPost
(
	@ThreadID int,
	@UserName nvarchar(50)
)
AS
DECLARE @TrackingThread bit

IF EXISTS(SELECT ThreadID FROM ThreadTrackings (nolock) WHERE ThreadID = @ThreadID AND UserName=@UserName)
	SELECT IsUserTrackingPost = 1
ELSE
	SELECT IsUserTrackingPost = 0


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumMarkPostAsRead' 
				AND type = 'P')
   DROP PROCEDURE spForumMarkPostAsRead
GO
CREATE        PROCEDURE spForumMarkPostAsRead
(
	@PostID	int,
	@UserName nvarchar (50)
)
 AS
BEGIN

	-- If @UserName is null it is an anonymous user
	IF @UserName IS NOT NULL
	BEGIN
		DECLARE @ForumID int
		DECLARE @PostDate datetime

		-- Mark the post as read
		-- *********************

		-- Only for PostLevel = 1
		IF EXISTS (SELECT PostID FROM tblForumPosts WHERE PostID = @PostID AND PostLevel = 1)
			IF NOT EXISTS (SELECT HasRead FROM PostsRead WHERE UserName = @UserName and PostID = @PostID)
				INSERT INTO PostsRead (UserName, PostID) VALUES (@UserName, @PostID)

	END

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumMessage_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumMessage_CreateUpdateDelete
GO
create procedure spForumMessage_CreateUpdateDelete
(
	@MessageID int,
	@Title NVarChar(1024),
	@Body NVarChar(4000),
	@Action int
)
AS
-- CREATE
IF @Action = 0
BEGIN
	SELECT 'Not Implemented'
END
-- UPDATE
ELSE IF @Action  = 1
BEGIN
	UPDATE
		tblForumMessages
	SET
		Title = @Title,
		Body = @Body
	WHERE
		MessageID = @MessageID
END

-- DELETE
ELSE IF @Action = 2
BEGIN
	SELECT 'Not Implemented'
END	

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_ApprovePost' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_ApprovePost
GO
CREATE         procedure spForumModerate_ApprovePost
(
	@PostID		int,
	@ApprovedBy	int
)
AS
DECLARE @ForumID 	int
DECLARE @ThreadID 	int
DECLARE @PostLevel 	int
DECLARE @UserID		int
DECLARE @IsLocked	bit

-- first make sure that the post is ALREADY non-approved
IF (SELECT IsApproved FROM tblForumPosts (nolock) WHERE PostID = @PostID) = 1
BEGIN
	print 'Post is already approved'
	SELECT 0
	RETURN
END
ELSE
BEGIN

	print 'Post is not approved'

	-- Get details about the thread and forum this post belongs in
	SELECT
		@ForumID = ForumID,
		@ThreadID = ThreadID,
		@PostLevel = PostLevel,
		@UserID	= UserID,
		@IsLocked = IsLocked
	FROM
		tblForumPosts
	WHERE
		PostID = @PostID

	-- Approve the post
	UPDATE 
		tblForumPosts
	SET 
		IsApproved = 1
	WHERE 
		PostID = @PostID

	-- Approved the thread if necessary
	IF @PostLevel = 1
		UPDATE
			tblForumThreads
		SET
			IsApproved = 1
		WHERE
			ThreadID = @ThreadID

	-- Update the user's post count
	exec spForumsystem_UpdateUserPostCount @ForumID, @UserID

	-- Update the forum statistics
	exec spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID

	-- Clean up unnecessary columns in forumsread
	exec spForumsystem_CleanForumsRead @ForumID

	-- update the thread stats
	exec spForumsystem_UpdateThread @ThreadID, @PostID

	-- Update Moderation audit table
	-- Update the ModerationAudit table
	exec spForumsystem_ModerationAction_AuditEntry 1, @ApprovedBy, @PostID

	-- Send back a success code
	SELECT 1
	
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_CheckUser' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_CheckUser
GO
CREATE procedure spForumModerate_CheckUser 
(
	@UserID		int,
	@ForumID	int
)
AS


IF EXISTS(SELECT ForumID FROM tblForumModerators WHERE UserID = @UserID AND ForumID = 0)
  SELECT 1
ELSE
  IF EXISTS (SELECT ForumID FROM tblForumModerators WHERE UserID = @UserID AND ForumID = @ForumID)
    SELECT 1
  ELSE
    SELECT 0

 
GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_DeletePost' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_DeletePost
GO
CREATE        PROCEDURE spForumModerate_DeletePost
(
	@PostID INT,
	@DeletedBy INT,
	@Reason NVARCHAR(1024),
	@DeleteChildPosts BIT = 1
)
AS

DECLARE @IsApproved bit

-- First, get information about the post that is about to be deleted.
SELECT
    @IsApproved = IsApproved
FROM
    tblForumPosts
WHERE
    PostID = @PostID

-- If the post is not approved, permanently delete the post, otherwise, execute tblForumPost_Delete
IF @IsApproved = 0
BEGIN
	
    -- Delete the post.
    DELETE FROM tblForumPosts
    WHERE PostID = @PostID

END	
ELSE
    EXEC spForumsystem_DeletePostAndChildren @PostID = @PostID, @DeleteChildren = @DeleteChildPosts

-- Update Moderation Audit table
exec spForumsystem_ModerationAction_AuditEntry 4, @DeletedBy, @PostID, null, null, @Reason



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_Forum_Roles' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_Forum_Roles
GO
create procedure spForumModerate_Forum_Roles
(
	@ForumID	int
)
AS
SELECT 
	R.RoleID,
	[Name],
	Description 
FROM 
	tblForumForumPermissions P,
	tblForumRoles R
WHERE
	P.RoleID = R.RoleID AND
	Moderate = 1 AND
	ForumID = @ForumID


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_Forums' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_Forums
GO
CREATE procedure spForumModerate_Forums
	(
		@SiteID	int,
		@UserID int
	)
	AS
	BEGIN
	
		SELECT
			ForumID, ParentID ,ForumGroupID, DateCreated, [Description], [Name], NewsgroupName, IsModerated, DaysToView , IsActive ,SortOrder,
			DisplayMask,TotalPosts, TotalThreads, ForumGroupID, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostID,  MostRecentPostSubject,
			MostRecentThreadID, MostRecentThreadReplies, MostRecentPostDate, EnableAutoDelete, EnablePostStatistics,
			AutoDeleteThreshold, EnableAnonymousPosting,
			PostsToModerate = (SELECT Count(PostID) FROM tblForumPosts P WHERE ForumID = F.ForumID AND P.IsApproved = 0),
			LastUserActivity = '1/1/1797'
		FROM
			tblForumForums F
		WHERE
			F.IsActive = 1 AND
			(SELECT Count(PostID) FROM tblForumPosts P WHERE ForumID = F.ForumID AND P.IsApproved = 0) > 0 AND
			(F.SiteID = 0)

	END


GO
IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_PostSet' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_PostSet
GO
CREATE       PROCEDURE spForumModerate_PostSet
(
	@ForumID		int,
	@PageIndex 		int,
	@PageSize 		int,
	@SortBy 		int,
	@SortOrder 		bit,
	@UserID 		int,
	@ReturnRecordCount 	bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @ThreadID int

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	PostID int
)

-- Sort by Post Date
IF @SortBy = 0 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY PostDate

ELSE IF @SortBy = 0 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM Posts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY PostDate DESC

-- Sort by Author
IF @SortBy = 1 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM Posts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY Username

ELSE IF @SortBy = 1 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM Posts P (nolock) WHERE IsApproved = 0 AND ForumID = @ForumID ORDER BY Username DESC

-- Select the individual posts
SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	Username,
	Replies = (SELECT COUNT(*) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
	IsModerator = (SELECT count(*) from tblForumModerators where UserID = @UserID),
	HasRead = 0 -- not used
FROM 
	tblForumPosts P (nolock),
	tblForumThreads T,
	tblForumUsers U,
        tblForumUserProfile UP,
	#PageIndex
WHERE 
	P.PostID = #PageIndex.PostID AND
	P.UserID = U.UserID AND
	T.ThreadID = P.ThreadID AND
        U.UserID = UP.UserID AND
	#PageIndex.IndexID > @PageLowerBound AND
	#PageIndex.IndexID < @PageUpperBound
END



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_Post_Move' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_Post_Move
GO
CREATE    PROCEDURE spForumModerate_Post_Move
(
    @PostID int,
    @MoveToForumID int,
    @MovedBy int
)
AS

DECLARE @ThreadID INT
DECLARE @ForumID INT
DECLARE @IsApproved BIT
DECLARE @PostLevel INT
DECLARE @Notes NVARCHAR(1024)

-- First, get information about the post that is about to be moved.
SELECT
	@ThreadID = ThreadID,
	@ForumID = ForumID,
	@PostLevel = PostLevel,
	@IsApproved = IsApproved
FROM
	tblForumPosts
WHERE
	PostID = @PostID

-- EAD: We only move the post if it is a top level post.
IF @PostLevel = 1
BEGIN
	DECLARE @MoveToIsModerated SMALLINT
	
	-- Get information about the destination forum.
	SELECT 
		@MoveToIsModerated = IsModerated
	FROM 
		tblForumForums
	WHERE 
		ForumID = @MoveToForumID
	
	-- If the post is not already approved, check the moderation status and permissions on the moderator for approved status.
	IF @IsApproved = 0
	BEGIN
		-- If the destination forum requires moderation, make sure the moderator has permission.
		IF @MoveToIsModerated = 1
		BEGIN
			IF EXISTS(
				SELECT
					UserID
				FROM
					tblForumUsersInRoles
				WHERE
					UserID = @MovedBy
					AND RoleID IN (
						SELECT 
							RoleID 
						FROM 
							tblForumForumPermissions
						WHERE 
							ForumID = @ForumID
							AND Moderate = 1 
					)
			)
			BEGIN
				-- The moderator has permissions to move the post and approve it.		
				UPDATE
					tblForumPosts
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID

				UPDATE
					tblForumThreads
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID

				-- approve the post
				EXEC spForumModerate_ApprovePost @PostID, @MovedBy
				
				SET @Notes = 'The post was moved and approved.'
				PRINT @Notes
				SELECT 2
			END
			ELSE BEGIN
				-- The moderator has permissions to move the post but not approve.			
				UPDATE
					tblForumPosts
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID

				UPDATE
					tblForumThreads
				SET 
					ForumID = @MoveToForumID
				WHERE 
					ThreadID = @ThreadID
				
				SET @Notes = 'The post was moved but not approved.'
				PRINT @Notes
				SELECT 1
			END
		END
		ELSE BEGIN
			UPDATE
				tblForumPosts
			SET 
				ForumID = @MoveToForumID
			WHERE 
				ThreadID = @ThreadID

			UPDATE
				tblForumThreads
			SET 
				ForumID = @MoveToForumID
			WHERE 
				ThreadID = @ThreadID

			-- The destination forum is not moderated, approve the post and move the post.
			EXEC spForumModerate_ApprovePost @PostID, @MovedBy
			
			SET @Notes = 'The post was moved and approved.'
			PRINT @Notes
			SELECT 2
		END
	END
	ELSE BEGIN
		-- The post is already approved, move the post.
		UPDATE
			tblForumPosts
		SET 
			ForumID = @MoveToForumID
		WHERE 
			ThreadID = @ThreadID

		UPDATE
			tblForumThreads
		SET 
			ForumID = @MoveToForumID
		WHERE 
			ThreadID = @ThreadID
		
		print 'The approved post was moved.'
		SET @Notes = 'The approved post was moved.'
		SELECT 3
	
	END

	-- Reset the statistics on both forums.
	EXEC spForumsystem_ResetForumStatistics @ForumID
	EXEC spForumsystem_ResetForumStatistics @MoveToForumID
	
	-- Reset the thread statistics on the moved thread.
	EXEC spForumsystem_ResetThreadStatistics @ThreadID
		
	-- Update Moderation Audit table
	EXEC spForumsystem_ModerationAction_AuditEntry 3, @MovedBy, @PostID, null, @ForumID, @Notes

END
ELSE BEGIN
	print 'The post was not moved.'
	SELECT 0
END




GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_Thread_Merge' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_Thread_Merge
GO
CREATE    PROCEDURE spForumModerate_Thread_Merge
(
	@ParentThreadID int,
	@ChildThreadID int,
	@JoinBy int
)
AS
DECLARE @ParentForumID int
DECLARE @LastPostInParent int
DECLARE @PostLevelInParent int
DECLARE @SortOrderInParent int
DECLARE @ChildForumID int
DECLARE @FirstPostInChild int
DECLARE @PostLevelInChild int
DECLARE @SortOrderInChild int
DECLARE @LastPostInChild int

-- Check to ensure we can perform this opertation
IF ((SELECT ThreadID FROM tblForumThreads WHERE ThreadID = @ChildThreadID) = @ParentThreadID)
	RETURN

-- Get details on the parent thread
SELECT TOP 1
	@ParentForumID = ForumID,
	@LastPostInParent = PostID,
	@PostLevelInParent = PostLevel,
	@SortOrderInParent = SortOrder
FROM
	tblForumPosts
WHERE
	ThreadID = @ParentThreadID
ORDER BY
	SortOrder DESC

-- Get details on the child thread
SELECT TOP 1
	@ChildForumID = ForumID,
	@FirstPostInChild = PostID,
	@PostLevelInChild = PostLevel,
	@SortOrderInChild = SortOrder
FROM
	tblForumPosts
WHERE
	ThreadID = @ChildThreadID

-- don't know why this is here
-- Get the last post in the child thread
--SELECT 
--	@LastPostInChild = MostRecentPostID
--FROM
--	tblForumThreads
--WHERE
--	ThreadID = @ChildThreadID

BEGIN TRAN

-- this is now done in the spForumsystem_UpdateThread sproc
-- Update the PostLevel and SortOrder for the Child posts before merging
--UPDATE 
--	tblForumPosts
--SET
--	PostLevel = PostLevel + @PostLevelInParent
--WHERE
--	ThreadID = @ChildThreadID
--
--UPDATE 
--	tblForumPosts
--SET
--	SortOrder = SortOrder + @SortOrderInParent
--WHERE
--	ThreadID = @ChildThreadID


-- Do the Updates
UPDATE
	tblForumPosts
SET
	ThreadID = @ParentThreadID,
	ForumID = @ParentForumID,
	PostLevel = PostLevel + @PostLevelInParent,
	SortOrder = SortOrder + @SortOrderInParent,
	ParentID = @LastPostInParent
WHERE
	ThreadID = @ChildThreadID

-- Now delete all of the old thread info
DELETE FROM 
	tblForumSearchBarrel
WHERE 
	ThreadID = @ChildThreadID

-- Delete all thread tracking data.	
DELETE FROM 
	tblForumTrackedThreads
WHERE 
	ThreadID = @ChildThreadID

-- Cleanup ThreadsRead
DELETE
	tblForumThreadsRead
WHERE
	ThreadID = @ChildThreadID 

-- Delete the child thread
DELETE 
	tblForumThreads
WHERE
	ThreadID = @ChildThreadID

-- Update thread statistics
EXEC spForumsystem_UpdateThread @ParentThreadID, 0

-- Update forum statistics
EXEC spForumsystem_UpdateForum @ParentForumID, @ParentThreadID, @ParentThreadID
EXEC spForumsystem_ResetForumStatistics @ChildForumID

-- Update moderation actions
EXEC spForumsystem_ModerationAction_AuditEntry 7, @JoinBy, @ChildThreadID

COMMIT TRAN



 
GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumModerate_Thread_Split' 
				AND type = 'P')
   DROP PROCEDURE spForumModerate_Thread_Split
GO
CREATE       PROCEDURE spForumModerate_Thread_Split
(
	@PostID INT,
	@MoveToForum INT,
	@SplitBy INT
)
AS
DECLARE @IsSticky BIT
DECLARE @StickyDate DATETIME
DECLARE @IsLocked BIT
DECLARE @NewThreadID INT
DECLARE @OldThreadID INT
DECLARE @UserID INT
DECLARE @PostAuthor NVARCHAR(64)
DECLARE @PostDate DATETIME
DECLARE @EmoticonID INT
DECLARE @TopPostLevel INT
DECLARE @TopSortOrder INT
DECLARE @TotalReplies INT
DECLARE	@MostRecentPostAuthor NVARCHAR(64)
DECLARE	@MostRecentPostAuthorID INT
DECLARE	@MostRecentPostID INT

-- Get details on the post
SELECT 
	@PostDate = PostDate,
	@UserID = UserID,
	@PostAuthor = PostAuthor,
	@IsSticky = 0,			-- shouldn't be a stickie when splitting
	@IsLocked = IsLocked,
	@StickyDate = GetDate(),
	@EmoticonID = EmoticonID,
	@OldThreadID = ThreadID		-- to delete later if no more replies
FROM 
	tblForumPosts 
WHERE 
	PostID = @PostID

BEGIN TRAN

-- Create a new thread by inserting
INSERT tblForumThreads 	
	( ForumID,
	PostDate, 
	UserID, 
	PostAuthor, 
	ThreadDate, 
	MostRecentPostAuthor, 
	MostRecentPostAuthorID, 	
	MostRecentPostID, 
	IsLocked, 
	IsApproved,
	IsSticky, 
	StickyDate, 
	ThreadEmoticonID )
VALUES
	( @MoveToForum, 	-- the forum we are moving to
	@PostDate, 
	@UserID, 
	@PostAuthor,
	@PostDate,
	@PostAuthor,	-- Dummy data until we move all posts below
	@UserID, 	-- Dummy data until we move all posts below
	0,		-- MostRecentPostID, which we don't know yet.
	@IsLocked,
	1,		-- Wouldn't be shown in the forum unless it wasn't approved already.
	@IsSticky,
	@StickyDate,
	@EmoticonID )

SELECT 
	@NewThreadID = @@IDENTITY
FROM
	tblForumThreads

-- Update the post and it's childred (if any) with the new threadid
UPDATE 
	tblForumPosts 
SET 
	ThreadID = @NewThreadID,
	ForumID = @MoveToForum,
	ParentID = @PostID	-- the toplevel post should now reference itself.
--	PostDate = GetDate()	-- We're not going to reset the DATETIME for the posts
WHERE
	ThreadID = @OldThreadID
	AND (PostID = @PostID OR ParentID = @PostID)

-- this is now controlled in the spForumsystem_UpdateThread sproc
-- Fix the PostLevel and SortOrder details of the new thread
--SELECT 
--	@TopPostLevel = PostLevel,
--	@TopSortOrder = SortOrder
--FROM 
--	spForumPosts 
--WHERE 
--	PostID = @PostID
--
--UPDATE 
--	spForumPosts 
--SET 
---	PostLevel = (PostLevel - @TopPostLevel) + 1,
--	SortOrder = (SortOrder - @TopSortOrder) + 1
--WHERE
--	ThreadID = @NewThreadID

-- Update the threads...
EXEC spForumsystem_UpdateThread @NewThreadID, 0
EXEC spForumsystem_UpdateThread @OldThreadID, 0

-- Update forum statistics
EXEC spForumsystem_UpdateForum @MoveToForum, @NewThreadID, @PostID

-- #7. Update moderation actions
EXEC spForumsystem_ModerationAction_AuditEntry 8, @SplitBy, @PostID

COMMIT TRAN



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPost' 
				AND type = 'P')
   DROP PROCEDURE spForumPost
GO
CREATE       PROCEDURE spForumPost
/*

	Procedure for getting basic information on a single post.

*/
(
	@PostID int,
	@UserID int,
	@TrackViews bit
) AS
DECLARE @NextThreadID int
DECLARE @PrevThreadID int
DECLARE @ThreadID int 
DECLARE @ForumID int
DECLARE @SortOrder int
DECLARE @IsApproved bit

SELECT 
	@ThreadID = ThreadID, 
	@ForumID = ForumID, 
	@SortOrder=SortOrder,
	@IsApproved = IsApproved
FROM 
	tblForumPosts (nolock) 
WHERE 
	PostID = @PostID

-- Is the Forum 0 (If so this is a private message and we need to verify the user can view it
IF @ForumID = 0
BEGIN
	IF NOT EXISTS (SELECT UserID FROM tblForumPrivateMessages WHERE UserID = @UserID AND ThreadID = @ThreadID)
		RETURN
END

-- Get the previous and next thread id
EXEC spForumThread_PrevNext @ThreadID, @ForumID, @NextThreadID OUTPUT, @PrevThreadID OUTPUT

DECLARE @TrackingThread bit

IF @TrackViews = 1
BEGIN
	-- Update the counter for the number of times this post is viewed
	UPDATE tblForumPosts SET TotalViews = (TotalViews + 1) WHERE PostID = @PostID
	UPDATE tblForumThreads SET TotalViews = (TotalViews + 1) WHERE ThreadID = @ThreadID
END

-- If @UserID is 0 the user is anonymous
IF @UserID > 0 AND @IsApproved = 1
BEGIN

	-- Mark the post as read
	-- *********************
	IF NOT EXISTS (SELECT ThreadID FROM tblForumThreadsRead WHERE ThreadID = @ThreadID AND UserID = @UserID)
		INSERT INTO tblForumThreadsRead (UserID, ThreadID) VALUES (@UserID, @ThreadID)

END


IF EXISTS(SELECT ThreadID FROM tblForumTrackedThreads (nolock) WHERE ThreadID = @ThreadID AND UserID=@UserID)
	SELECT @TrackingThread = 1
ELSE
	SELECT @TrackingThread = 0

SELECT
	*,
	T.ThreadDate,
	T.StickyDate,
	T.IsLocked,
	T.IsSticky,
	HasRead = 0,
	EditNotes = (SELECT EditNotes FROM tblForumPostEditNotes WHERE PostID = P.PostID),
	IndexInThread = (SELECT Count(PostID) FROM tblForumPosts P1 WHERE IsApproved = 1 AND ThreadID = @ThreadID AND SortOrder <= (SELECT SortOrder FROM tblForumPosts where PostID = @PostID)),
	AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
	IsModerator = (SELECT Count(*) FROM tblForumModerators WHERE UserID = U.UserID),
	Replies = (SELECT COUNT(*) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	PrevThreadID = @PrevThreadID,
	NextThreadID = @NextThreadID,
	UserIsTrackingThread = @TrackingThread
FROM 
	tblForumPosts P,
	tblForumThreads T,
	tblForumUsers U,
	tblForumUserProfile UP
WHERE 
	P.PostID = @PostID AND
	P.ThreadID = T.ThreadID AND
	P.UserID = U.UserID AND
	U.UserID = UP.UserID




GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPostAttachment' 
				AND type = 'P')
   DROP PROCEDURE spForumPostAttachment
GO
create procedure spForumPostAttachment
(
	@PostID int
)
AS
BEGIN

	SELECT
		*
	FROM
		tblForumPostAttachments
	WHERE
		PostID = @PostID

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPostAttachment_Add' 
				AND type = 'P')
   DROP PROCEDURE spForumPostAttachment_Add 
GO
create procedure spForumPostAttachment_Add 
(
	@PostID int,
	@UserID int,
	@ForumID int,
	@Filename nvarchar(256),
	@Content image,
	@ContentType nvarchar(50),
	@ContentSize int
)
AS
BEGIN

	IF EXISTS (SELECT PostID FROM tblForumPostAttachments WHERE PostID = @PostID)
		RETURN

	INSERT INTO 
		tblForumPostAttachments
	(
		PostID,
		ForumID,
		UserID,
		[FileName],
		Content,
		ContentType,
		ContentSize
	)
	VALUES
	(
		@PostID,
		@ForumID,
		@UserID,
		@Filename,
		@Content,
		@ContentType,
		@ContentSize
	)

END




GO


/*
	stored procedure spForumPost_CreateUpdate
	* Procedure now creates new threads here, from an identity column in spForumThreads.
	* To update a Post, ParentID must not equal 0.
	* To delete a post, use spForumModerate_Posts_Move to move it to the Deleted Forum
*/
IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPost_CreateUpdate' 
				AND type = 'P')
   DROP PROCEDURE spForumPost_CreateUpdate
GO
CREATE                     PROCEDURE spForumPost_CreateUpdate
(
	@ForumID int,
	@ParentID int,
	@AllowDuplicatePosts bit,
	@DuplicateIntervalInMinutes int = 0,
	@Subject nvarchar(256),
	@UserID int,
	@PostAuthor nvarchar(64) = null,
	@Body ntext,
	@FormattedBody ntext,
	@EmoticonID int = 0,
	@IsLocked bit,
	@IsSticky bit,
	@StickyDate datetime,
	@PostType int = 0,
	@PostDate datetime = null,
	@UserHostAddress nvarchar(32),
	@PostID int out
) 
AS
SET NOCOUNT ON
DECLARE @MaxSortOrder int
DECLARE @ParentLevel int
DECLARE @ThreadID int
DECLARE @ParentSortOrder int
DECLARE @NextSortOrder int
DECLARE @ApprovedPost bit
DECLARE @ModeratedForum bit
DECLARE @EnablePostStatistics bit
DECLARE @TrackThread bit

-- set the PostDate
IF @PostDate IS NULL
	SET @PostDate = GetDate()

-- set the username
IF @PostAuthor IS NULL
	SELECT 
		@PostAuthor = UserName
	FROM 
		tblForumUsers 
	WHERE 
		UserID = @UserID

-- Do we care about duplicates?
IF @AllowDuplicatePosts = 0
BEGIN
	DECLARE @IsDuplicate bit
	exec spForumsystem_DuplicatePost @UserID, @Body, @DuplicateIntervalInMinutes, @IsDuplicate output

	IF @IsDuplicate = 1
	BEGIN
		SET @PostID = -1	
		RETURN	-- Exit with error code.
	END
END

-- we need to get the ForumID, if the ParentID is not null (there should be a ForumID)
IF @ForumID = 0 AND @ParentID <> 0
	SELECT 
		@ForumID = ForumID
	FROM 
		tblForumPosts (nolock) 
	WHERE 
		PostID = @ParentID

-- Is this forum moderated?
SELECT 
	@ModeratedForum = IsModerated, @EnablePostStatistics = EnablePostStatistics
FROM 
	tblForumForums (nolock)
WHERE 
	ForumID = @ForumID

-- Determine if this post will be approved.
-- If the forum is NOT moderated, then the post will be approved by default.
SET NOCOUNT ON
BEGIN TRAN

IF @ModeratedForum = 0 OR @ForumID = 0
	SELECT @ApprovedPost = 1
ELSE
BEGIN
	-- ok, this is a moderated forum.  Is this user trusted?  If he is, then the post is approved ; else it is not
	SET @ApprovedPost = ( 
		SELECT
			ModerationLevel
		FROM
			tblForumUserProfile (nolock)
		WHERE
			UserID = @UserID )
END


-- EAD: Modifying this sproc to insert directly into tblForumThreads.  We are no longer keying
-- tblForumThreads.ThreadID to be same number as the top PostID for the thread.  This is to allow
-- for the FKs to be put into place.

IF @ParentID = 0	-- parameter indicating this is a top-level post (for a new thread)
BEGIN
	-- First we create a new ThreadID.

	-- check the StickyDate to ensure it's not null
	IF @StickyDate < @PostDate
		SET @StickyDate = @PostDate

	INSERT tblForumThreads 	
		( ForumID,
		PostDate, 
		UserID, 
		PostAuthor, 
		ThreadDate, 
		MostRecentPostAuthor, 
		MostRecentPostAuthorID, 	
		MostRecentPostID, 
		IsLocked, 
		IsApproved,
		IsSticky, 
		StickyDate, 
		ThreadEmoticonID )
	VALUES
		( @ForumID, 
		@PostDate, 
		@UserID, 
		@PostAuthor,
		@PostDate,
		@PostAuthor,
		@UserID, 
		0,	-- MostRecentPostID, which we don't know until after post INSERT below.
		@IsLocked,
		@ApprovedPost,
		@IsSticky,
		@StickyDate,
		@EmoticonID )

	-- Get the new ThreadID
	SELECT 
		@ThreadID = @@IDENTITY
	FROM
		tblForumThreads
		
	-- Now we add the new post
	INSERT tblForumPosts 
		( ForumID, 
		ThreadID, 
		ParentID, 
		PostLevel, 
		SortOrder, 
		Subject, 
		UserID, 
		PostAuthor, 
		IsApproved, 
		IsLocked, 
		Body, 
		FormattedBody, 
		PostType, 
		PostDate, 
		IPAddress, 
		EmoticonID )
	VALUES 
		( @ForumID, 
		@ThreadID, 
		0, 	-- ParentID, which we don't know until after INSERT
		1, 	-- PostLevel, 1 marks start/top/first post in thread.
		1, 	-- SortOrder (not in use at this time)
		@Subject, 
		@UserID, 
		@PostAuthor,
		@ApprovedPost, 
		@IsLocked, 
		@Body, 
		@FormattedBody, 
		@PostType, 
		@PostDate, 
		@UserHostAddress, 
		@EmoticonID )
		

	-- Get the new PostID
	SELECT 
		@PostID = @@IDENTITY
--	FROM
--		tblForumPosts

	-- Update the new Thread with the new PostID
	UPDATE 
		tblForumThreads
	SET 
		MostRecentPostID = @PostID
	WHERE 
		ThreadID = @ThreadID

	-- Update the new Post's ParentID with the new PostID
	UPDATE 
		tblForumPosts
	SET 
		ParentID = @PostID
	WHERE 
		PostID = @PostID

END
ELSE BEGIN	-- @ParentID <> 0 means there is a reply to an existing post

	-- Get the Post Information for what we are replying to
	SELECT 
		@ThreadID = ThreadID,
		@ForumID = ForumID,
		@ParentLevel = PostLevel,
		@ParentSortOrder = SortOrder
	FROM 
		tblForumPosts
	WHERE 
		PostID = @ParentID

	-- Is there another post at the same level or higher?
	SET @NextSortOrder = (
		SELECT 	
			MIN(SortOrder) 
		FROM 
			tblForumPosts 
		WHERE 
			PostLevel <= @ParentLevel 
			AND SortOrder > @ParentSortOrder 
			AND ThreadID = @ThreadID )

	IF @NextSortOrder > 0
	BEGIN
		-- Move the existing posts down
		UPDATE 
			tblForumPosts
		SET 
			SortOrder = SortOrder + 1
		WHERE 
			ThreadID = @ThreadID
			AND SortOrder >= @NextSortOrder

		SET @MaxSortOrder = @NextSortOrder

	END
   	ELSE BEGIN 	-- There are no posts at this level or above
	
		-- Find the highest sort order for this parent
		SELECT 
			@MaxSortOrder = MAX(SortOrder) + 1
		FROM 
			tblForumPosts
		WHERE 
			ThreadID = @ThreadID

	END 

	-- Insert the new post
	INSERT tblForumPosts 
		( ForumID, 
		ThreadID, 
		ParentID, 
		PostLevel, 
		SortOrder, 
		Subject, 
		UserID, 
		PostAuthor, 
		IsApproved, 
		IsLocked, 
		Body, 
		FormattedBody, 
		PostType, 
		PostDate, 
		IPAddress, 
		EmoticonID )
	VALUES 
		( @ForumID, 
		@ThreadID, 
		@ParentID, 
		@ParentLevel + 1, 
		@MaxSortOrder,
		@Subject, 
		@UserID, 
		@PostAuthor, 
		@ApprovedPost, 
		@IsLocked, 
		@Body, 
		@FormattedBody, 
		@PostType, 
		@PostDate, 
		@UserHostAddress, 
		@EmoticonID )


	-- Now check to see if this post is Approved by default.
	-- If so, we go ahead and update the Threads table for the MostRecent items.
	IF @ApprovedPost = 1
	BEGIN		
		-- Grab the new PostID and update the ThreadID's info
		SELECT 
			@PostID = @@IDENTITY 
--		FROM 
--			tblForumPosts

		-- To cut down on overhead, I've elected to update the thread's info
		-- directly from here, without running spForumsystem_UpdateThread since
		-- I already have all of the information that this sproc would normally have to lookup.
		IF @StickyDate < @PostDate
			SET @StickyDate = @PostDate

		UPDATE
			tblForumThreads 	
		SET 
			MostRecentPostAuthor = @PostAuthor,
			MostRecentPostAuthorID = @UserID,
			MostRecentPostID = @PostID,
			TotalReplies = TotalReplies + 1, -- (SELECT COUNT(*) FROM tblForumPosts WHERE ThreadID = @ThreadID AND IsApproved = 1 AND PostLevel > 1),
			IsLocked = @IsLocked,
			StickyDate = @StickyDate,	-- this makes the thread a sticky/announcement, even if it's a reply.
			ThreadDate = @PostDate
		WHERE
			ThreadID = @ThreadID
	END
	ELSE
	BEGIN
		-- Moderated Posts: get the new PostID
		SELECT @PostID = @@IDENTITY 
	END

	-- Clean up ThreadsRead (this should work very well now)
	DELETE
		tblForumThreadsRead
	WHERE
		ThreadID = @ThreadID 
		AND UserID != @UserID
END


-- Update the users tracking for the new post (if needed)
SELECT 
	@TrackThread = EnableThreadTracking 
FROM 
	tblForumUserProfile (nolock) 
WHERE 
	UserID = @UserID

IF @TrackThread = 1
	-- If a row already exists to track this thread for this user, do nothing - otherwise add the row
	IF NOT EXISTS ( SELECT ThreadID FROM tblForumTrackedThreads (nolock) WHERE ThreadID = @ThreadID AND UserID = @UserID )
		INSERT INTO tblForumTrackedThreads 
			( ThreadID, 
			UserID )
		VALUES
			( @ThreadID, 
			@UserID )

COMMIT TRAN
BEGIN TRAN

-- Is this a private post
IF @ForumID = 0
	EXEC spForumPrivateMessages_CreateDelete @UserID, @ThreadID, 0

-- Update the forum statitics
IF @ApprovedPost = 1 AND @ForumID > 0 AND @UserID > 0
BEGIN
	IF @EnablePostStatistics = 1
	BEGIN
		UPDATE 
			tblForumUserProfile 
		SET 
			TotalPosts = TotalPosts + 1 
		WHERE 
			UserID = @UserID
	END

	EXEC spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID
END

 -- Clean up unnecessary records in forumsread
-- EXEC spForumsystem_CleanForumsRead @ForumID

COMMIT TRAN
SET NOCOUNT OFF

SELECT @PostID = @PostID



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPost_ToggleSettings' 
				AND type = 'P')
   DROP PROCEDURE spForumPost_ToggleSettings
GO
CREATE   procedure spForumPost_ToggleSettings
(
	@PostID int,
	@IsAnnouncement bit,
	@IsLocked bit,
	@ModeratorID int
)
AS
BEGIN
	DECLARE @CurrentLockState bit
	DECLARE @CurrentAnnouncementState bit
	
	DECLARE @ThreadID int
	DECLARE @PostLevel int
	SELECT 
		@ThreadID = ThreadID, 
		@PostLevel = PostLevel 
	FROM 
		tblForumPosts 
	WHERE 
		PostID = @PostID

-- Is this a thread
IF @PostLevel =1
BEGIN
	print 'Toggling settings on a thread.'

	-- Get the current state of the thread
	SELECT 
		@CurrentLockState = IsLocked
	FROM 
		tblForumThreads 
	WHERE 
		ThreadID = @ThreadID

	-- Get current is announcement state of the thread
	IF EXISTS (	SELECT 
				ThreadID 
			FROM 
				tblForumThreads
			WHERE
				ThreadID = @ThreadID 
				AND IsSticky = 1
				AND StickyDate > DateAdd( y, 20, GetDate() )
			)

		SET @CurrentAnnouncementState = 1
	ELSE
		SET @CurrentAnnouncementState = 0
		

	-- Is the Post getting locked?
	IF @CurrentLockState != @IsLocked
	BEGIN
		UPDATE
			tblForumThreads
		SET
			IsLocked = @IsLocked
		WHERE
			ThreadID = @ThreadID

		UPDATE
			tblForumPosts
		SET
			IsLocked = @IsLocked
		WHERE
			ThreadID = @ThreadID

		IF @IsLocked = 0
		  exec spForumsystem_ModerationAction_AuditEntry 6, @ModeratorID, @ThreadID
        	ELSE
		  exec spForumsystem_ModerationAction_AuditEntry 5, @ModeratorID, @ThreadID
	END


	-- Is the post an Annoucement
	IF @CurrentAnnouncementState != @IsAnnouncement
		IF @IsAnnouncement = 1
		BEGIN
			UPDATE
				tblForumThreads
			SET
				IsSticky = 1,
				StickyDate = DateAdd(y, 25, ThreadDate)
			WHERE
				ThreadID = @ThreadID
	
			exec spForumsystem_ModerationAction_AuditEntry 16, @ModeratorID, @PostID
	
		END
		ELSE
		BEGIN
			UPDATE
				tblForumThreads
			SET
				IsSticky = 0,
				StickyDate = ThreadDate
			WHERE
				ThreadID = @ThreadID
	
			exec spForumsystem_ModerationAction_AuditEntry 17, @ModeratorID, @PostID
		END
END
ELSE
	print 'Toggling settings on a post.'

	-- Get the current lock state of the thread
	SELECT 
		@CurrentLockState = IsLocked
	FROM 
		tblForumPosts 
	WHERE 
		PostID = @PostID

	-- UPDATE The child posts
	UPDATE
	   	tblForumPosts
	SET
		IsLocked = @IsLocked
	WHERE
		ParentID = @PostID
	
	IF @IsLocked != @CurrentLockState
		IF @IsLocked = 0
		  exec spForumsystem_ModerationAction_AuditEntry 6, @ModeratorID, @PostID
	        ELSE
		  exec spForumsystem_ModerationAction_AuditEntry 5, @ModeratorID, @PostID
END



GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPost_Update' 
				AND type = 'P')
   DROP PROCEDURE spForumPost_Update
GO
CREATE           PROCEDURE spForumPost_Update
(
	@PostID	int,
	@Subject	nvarchar(256),
	@Body		ntext,
	@FormattedBody	ntext,
	@EmoticonID	int = 0,
	@IsSticky 	bit = null,
	@StickyDate 	datetime = null,
	@IsLocked	bit,
	@EditedBy	int,
	@EditNotes	ntext = null
)
AS
	DECLARE @ThreadID int
	DECLARE @PostDate datetime

	-- this sproc updates a post (called from the moderate/admin page)
	UPDATE 
		tblForumPosts 
	SET
		Subject = @Subject,
--		PostDate = GetDate(),	-- This does not get updated.  the original date IS the date!
		Body = @Body,
		FormattedBody = @FormattedBody,
		IsLocked = @IsLocked,
		EmoticonID = @EmoticonID
	WHERE 
		PostID = @PostID

	-- Allow thread to update sticky properties.
	IF (@IsSticky IS NOT NULL) AND (@StickyDate IS NOT NULL)
	BEGIN
		-- Get the thread and postdate this applies to
		SELECT 
			@ThreadID = ThreadID,
			@PostDate = PostDate 
		FROM 
			tblForumPosts 
		WHERE 
			PostID = @PostID

		IF (@StickyDate > '1/1/2000')
		BEGIN
			-- valid date range given
			UPDATE
				tblForumThreads
			SET
				IsSticky = @IsSticky,
				StickyDate = @StickyDate
			WHERE 
				ThreadID = @ThreadID 
		END
		ELSE BEGIN
			-- trying to remove a sticky
			UPDATE
				tblForumThreads
			SET
				IsSticky = @IsSticky,
				StickyDate = @PostDate
			WHERE 
				ThreadID = @ThreadID 		
		END
	END

	IF @EditNotes IS NOT NULL
		IF EXISTS (SELECT PostID FROM tblForumPostEditNotes WHERE PostID = @PostID)
			UPDATE 
				tblForumPostEditNotes
			SET
				EditNotes = @EditNotes
			WHERE	
				PostID = @PostID
		ELSE
			INSERT INTO
				tblForumPostEditNotes
			VALUES
				(@PostID, @EditNotes)

	-- We want to track what happened
	exec spForumsystem_ModerationAction_AuditEntry 2, @EditedBy, @PostID, NULL, NULL, @EditNotes



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPosts_PostSet' 
				AND type = 'P')
   DROP PROCEDURE spForumPosts_PostSet
GO
CREATE         PROCEDURE spForumPosts_PostSet
(
	@PostID	int,
	@PageIndex int,
	@PageSize int,
	@SortBy int,
	@SortOrder bit,
	@UserID int,
	@ReturnRecordCount bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @ThreadID int
DECLARE @ForumID int

-- First set the rowcount
DECLARE @RowsToReturn int
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- Get the ThreadID
SELECT
	@ThreadID = ThreadID,
	@ForumID = ForumID
FROM 
	tblForumPosts 
WHERE 
	PostID = @PostID

-- Is the Forum 0 (If so this is a private message and we need to verify the user can view it
IF @ForumID = 0
BEGIN
	IF NOT EXISTS (SELECT UserID FROM tblForumPrivateMessages WHERE UserID = @UserID AND ThreadID = @ThreadID)
		RETURN
END

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	PostID int
)

-- Sort by Post Date
IF @SortBy = 0 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY PostDate

ELSE IF @SortBy = 0 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY PostDate DESC

-- Sort by Author
IF @SortBy = 1 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY UserID

ELSE IF @SortBy = 1 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY UserID DESC

-- Sort by SortOrder
IF @SortBy = 2 AND @SortOrder = 0
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY SortOrder

ELSE IF @SortBy = 2 AND @SortOrder = 1
    INSERT INTO #PageIndex (PostID)
    SELECT PostID FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID ORDER BY SortOrder DESC


-- Select the individual posts
SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	Username = P.PostAuthor,
	EditNotes = (SELECT EditNotes FROM tblForumPostEditNotes WHERE PostID = P.PostID),
	AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
	Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	IsModerator = (SELECT count(UserID) from tblForumModerators where UserID = @UserID),
	HasRead = 0 -- not used
FROM 
	tblForumPosts P (nolock),
	tblForumThreads T,
	tblForumUsers U,
  tblForumUserProfile UP,
	#PageIndex
WHERE 
	P.PostID = #PageIndex.PostID AND
	P.UserID = U.UserID AND
	T.ThreadID = P.ThreadID AND
  U.UserID = UP.UserID AND
	#PageIndex.IndexID > @PageLowerBound AND
	#PageIndex.IndexID < @PageUpperBound
ORDER BY
	IndexID
END

IF @ReturnRecordCount = 1
  SELECT count(PostID) FROM tblForumPosts (nolock) WHERE IsApproved = 1 AND ThreadID = @ThreadID   

GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumPrivateMessages_CreateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumPrivateMessages_CreateDelete
GO
CREATE  procedure spForumPrivateMessages_CreateDelete
(
	@UserID int,
	@ThreadID int,
	@Action int
)
AS
BEGIN

IF @Action = 0
BEGIN
	-- Does the user already have the ability to see this thread?
	IF EXISTS (SELECT UserID FROM tblForumPrivateMessages WHERE UserID = @UserID and ThreadID = @ThreadID)
		return

	INSERT INTO
		tblForumPrivateMessages
	VALUES
		(
			@UserID,
			@ThreadID
		)

	RETURN
END

IF @Action = 2
BEGIN
	DELETE
		tblForumPrivateMessages
	WHERE
		UserID = @UserID AND
		ThreadID = @ThreadID
	RETURN
END

END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRank_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumRank_CreateUpdateDelete
GO
CREATE procedure spForumRank_CreateUpdateDelete
(
	@RankID				int out,
	@DeleteRank			bit	= 0,
	@RankName			nvarchar(60),
	@PostingCountMin	int,
	@PostingCountMax	int,	
	@RankIconUrl		nvarchar(512)
)
AS

-- are we deleting the rank
IF( @DeleteRank = 1 )
BEGIN

	DELETE tblForumRanks
	WHERE
		RankID	= @RankID

	RETURN
END

-- are we updating the rank
IF( @RankID >  0 )
BEGIN

	UPDATE tblForumRanks SET
		  RankName			= @RankName
		, PostingCountMin	= @PostingCountMin
		, PostingCountMax	= @PostingCountMax
		, RankIconUrl		= @RankIconUrl
	WHERE
		  RankID	= @RankID
		

END
ELSE
BEGIN
	INSERT INTO tblForumRanks (
		RankName, PostingCountMin, PostingCountMax, RankIconUrl
	)
	VALUES( 
		@RankName, @PostingCountMin, @PostingCountMax, @RankIconUrl 
	)

	SET @RankID = @@identity
END

RETURN


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRanks_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumRanks_Get
GO
create procedure spForumRanks_Get
(
	@UserID					int = 0
)
AS
	SELECT r.*
	FROM
		tblForumRanks r


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRemoveForumFromRole' 
				AND type = 'P')
   DROP PROCEDURE spForumRemoveForumFromRole
GO
CREATE procedure spForumRemoveForumFromRole
(
   @ForumID int,
   @Rolename nvarchar(256)
)
AS
IF EXISTS (SELECT ForumID FROM PrivateForums WHERE ForumID=@ForumID AND Rolename=@Rolename)
DELETE FROM
    PrivateForums
WHERE
    ForumID=@ForumID AND Rolename=@Rolename


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRemoveModeratedForumForUser' 
				AND type = 'P')
   DROP PROCEDURE spForumRemoveModeratedForumForUser
GO
create procedure spForumRemoveModeratedForumForUser
(
	@UserName	nvarchar(50),
	@ForumID	int
)
 AS
	-- remove a row from the Moderators table
	DELETE FROM Moderators
	WHERE UserName = @UserName and ForumID = @ForumID



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRemoveUserFromRole' 
				AND type = 'P')
   DROP PROCEDURE spForumRemoveUserFromRole
GO
CREATE procedure spForumRemoveUserFromRole
(
   @UserID	int,
   @RoleID	int
)
AS
IF EXISTS (SELECT UserID FROM tblForumUsersInRoles WHERE UserID = @UserID AND @RoleID = @RoleID)
DELETE FROM
    tblForumUsersInRoles
WHERE
    	UserID	= @UserID
	AND RoleID	= @RoleID


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumReport_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumReport_CreateUpdateDelete
GO
create proc spForumReport_CreateUpdateDelete
(
	  @ReportID			int out
	, @DeleteReport		bit		= 0
	, @ReportName		varchar(20)
	, @Active			bit
	, @ReportCommand	varchar(6500)
	, @ReportScript		ntext
)	
AS

IF( @DeleteReport > 0 )
BEGIN
	DELETE tblForumReports
	WHERE
		ReportID	= @ReportID
	RETURN
END

IF( @ReportID > 0 )
BEGIN
	UPDATE tblForumReports SET
		  ReportName	= @ReportName
		, Active		= @Active
		, ReportCommand	= @ReportCommand
		, ReportScript	= @ReportScript
	WHERE
		ReportID	= @ReportID
END
ELSE
BEGIN	
	INSERT INTO tblForumReports ( 
		ReportName, Active, ReportCommand, ReportScript 
	) VALUES (
		@ReportName, @Active, @ReportCommand, @ReportScript
	)	

	SET @ReportID = @@identity
END
RETURN


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumReports_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumReports_Get
GO
create proc spForumReports_Get
(
	@ReportID	int = 0
)
as
	select
		*
	FROM
		tblForumReports
	WHERE
		ReportID = @ReportID or
		( 
			@ReportID = 0 AND
			1=1
		)


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumReverseTrackingOption' 
				AND type = 'P')
   DROP PROCEDURE spForumReverseTrackingOption 
GO
create procedure spForumReverseTrackingOption 
(
	@UserID int,
	@PostID	int
)
AS
	-- reverse the user's tracking options for a particular thread
	-- first get the threadID of the Post
	DECLARE @ThreadID int
	SELECT @ThreadID = ThreadID FROM tblForumPosts WHERE PostID = @PostID

	IF EXISTS(SELECT ThreadID FROM tblForumTrackedThreads WHERE ThreadID = @ThreadID AND UserID = @UserID)
		-- the user is tracking this thread, delete this row
		DELETE FROM tblForumTrackedThreads
		WHERE ThreadID = @ThreadID AND UserID = @UserID
	ELSE
		-- this user isn't tracking the thread, so add her
		INSERT INTO tblForumTrackedThreads (ThreadID, UserID, DateCreated)
		VALUES(@ThreadID, @UserID, GetDate())


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRole_AddForum' 
				AND type = 'P')
   DROP PROCEDURE spForumRole_AddForum 
GO
CREATE PROCEDURE spForumRole_AddForum
(
   @ForumID int,
   @RoleID int
)
AS
IF NOT EXISTS (SELECT ForumID FROM tblForumForumPermissions WHERE ForumID=@ForumID AND RoleID=@RoleID) AND
    EXISTS (SELECT ForumID FROM tblForumForums WHERE ForumID=@ForumID) AND
    EXISTS (SELECT RoleID FROM tblForumRoles WHERE RoleID=@RoleID)
    BEGIN
        INSERT INTO
            forum_ForumPermissions(ForumID, RoleID)
        VALUES
            (@ForumID, @RoleID)
    END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRole_AddUser' 
				AND type = 'P')
   DROP PROCEDURE spForumRole_AddUser
GO
create procedure spForumRole_AddUser
(
   @UserID int,
   @RoleID int
)
AS
IF NOT EXISTS (SELECT UserID FROM tblForumUsersInRoles WHERE UserID = @UserID AND RoleID = @RoleID)
INSERT INTO
	tblForumUsersInRoles
VALUES
	(@UserID, @RoleID)


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRole_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumRole_Get
GO
create procedure spForumRole_Get
(
	@RoleID int
)
AS
BEGIN
SELECT * FROM tblForumRoles WHERE RoleID = @RoleID
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRoles_AddUser' 
				AND type = 'P')
   DROP PROCEDURE spForumRoles_AddUser
GO
create procedure spForumRoles_AddUser
(
   @UserID int,
   @RoleID int
)
AS
IF NOT EXISTS (SELECT UserID FROM tblForumUsersInRoles WHERE UserID = @UserID AND RoleID = @RoleID)
INSERT INTO
	tblForumUsersInRoles
VALUES
	(@UserID, @RoleID)

GO



-- sp_helptext spForumRoles_CreateUpdateDelete

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRoles_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumRoles_CreateUpdateDelete
GO
CREATE PROCEDURE spForumRoles_CreateUpdateDelete
(
	@RoleID	int out,
	@DeleteRole	bit = 0,
	@Name		nvarchar(256) = '',
	@Description	nvarchar(512) = ''
)
AS

-- Are we deleting the role?
IF @DeleteRole = 1
BEGIN

	-- delete all users in the role
	DELETE 
		tblForumUsersInRoles
	WHERE 
		RoleID = @RoleID

	-- delete all forums using the role
	DELETE 
		tblForumForumPermissions
	WHERE 
		RoleID = @RoleID


	-- finally we can delete the actual role
	DELETE 
		tblForumRoles
	WHERE 
		RoleID = @RoleID

	RETURN
END

-- Are we updating a forum
IF  @RoleID > 0
BEGIN
	-- Update the role
	UPDATE 
		tblForumRoles
	SET
		Name = @Name,
		Description = @Description
	WHERE 
		RoleID = @RoleID
END
ELSE
BEGIN

	-- Create a new Forum
	INSERT INTO 
		tblForumRoles (
			Name, 
			Description
			)
		VALUES (
			@Name,
			@Description
			)
	
	SET @RoleID = @@Identity

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumRoles_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumRoles_Get
GO
CREATE   procedure spForumRoles_Get
(
@UserID int = 0
)
AS
BEGIN

	IF (@UserID = 0)
		SELECT
			*
		FROM
			tblForumRoles
	ELSE
		SELECT DISTINCT
			* 
		FROM 
			tblForumUsersInRoles U,
			tblForumRoles R
		WHERE
			U.RoleID = R.RoleID AND
			UserID = @UserID
END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSearch' 
				AND type = 'P')
   DROP PROCEDURE spForumSearch
GO
CREATE    procedure spForumSearch (
	@SearchSQL nvarchar(4000),
	@RecordCountSQL nvarchar(4000),
	@PageIndex int = 0,
	@PageSize int = 25
)
AS
BEGIN

	DECLARE @StartTime datetime
	DECLARE @RowsToReturn int
	DECLARE @PageLowerBound int
	DECLARE @PageUpperBound int
	DECLARE @Count int

	-- Used to calculate cost of query
	SET @StartTime = GetDate()

	-- Set the rowcount
	SET @RowsToReturn = @PageSize * (@PageIndex + 1)
	SET ROWCOUNT @RowsToReturn

	-- Calculate the page bounds
	SET @PageLowerBound = @PageSize * @PageIndex
	SET @PageUpperBound = @PageLowerBound + @PageSize + 1

	-- Create a temp table to store the results in
	CREATE TABLE #SearchResults
	(
		IndexID int IDENTITY (1, 1) NOT NULL,
		PostID int,
		ForumID int,
		Weight int,
		PostDate datetime
	)

	-- Fill the temp table
	INSERT INTO #SearchResults (PostID, ForumID, Weight, PostDate)
	exec (@SearchSQL)

	-- SELECT actual search results from this table
	SELECT
		P.*,
		U.*,
		T.ThreadDate,
		T.IsLocked,
		T.IsSticky,
		UP.*,
		AttachmentFilename = ISNULL ( (SELECT [FileName] FROM tblForumPostAttachments WHERE PostID = P.PostID), ''),
		Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
		IsModerator = (SELECT count(*) from tblForumModerators where UserID = P.UserID),
		HasRead = 0 -- not used
	FROM 
		tblForumPosts P,
		tblForumUsers U,
		tblForumThreads T,
		tblForumUserProfile UP,
		#SearchResults R
	WHERE
		P.PostID = R.PostID AND
		T.ThreadID = P.ThreadID AND
		U.UserID = P.UserID AND
		P.UserID = UP.UserID AND
		R.IndexID > @PageLowerBound AND
		R.IndexID < @PageUpperBound

	-- Do we need to return a record estimate?
	exec (@RecordCountSQL)

	SELECT Duration = GetDate() - @StartTime
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSearch_Add' 
				AND type = 'P')
   DROP PROCEDURE spForumSearch_Add
GO
CREATE  procedure spForumSearch_Add (
	@WordHash int,
	@Word nvarchar(64),
	@Weight float,
	@PostID int,
	@ThreadID int,
	@ForumID int
)
AS
BEGIN
	IF EXISTS (SELECT WordHash FROM tblForumSearchBarrel WHERE PostID = @PostID AND WordHash = @WordHash)
		UPDATE 
			tblForumSearchBarrel 
		SET
			Weight = @Weight
		WHERE
			WordHash = @WordHash AND
			PostID = @PostID
	ELSE
		INSERT INTO
			tblForumSearchBarrel
			(WordHash, Word, PostID, ThreadID, ForumID, Weight)
		VALUES
			(@WordHash, @Word, @PostID, @ThreadID, @ForumID, @Weight)

	IF EXISTS (SELECT PostID From tblForumPosts WHERE PostID = @PostID AND IsIndexed = 0)
		UPDATE tblForumPosts SET IsIndexed = 1 WHERE PostID = @PostID

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSearch_IgnoreWords' 
				AND type = 'P')
   DROP PROCEDURE spForumSearch_IgnoreWords
GO
CREATE   procedure spForumSearch_IgnoreWords
AS
BEGIN
		SELECT
			WordHash,
			Word
		FROM
			tblForumSearchIgnoreWords
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSearch_IgnoreWords_CreateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumSearch_IgnoreWords_CreateDelete
GO
create procedure spForumSearch_IgnoreWords_CreateDelete
(
	@WordHash int,
	@Word nvarchar (64),
	@Action int
)
AS

-- CREATE
IF @Action = 0
BEGIN
IF NOT EXISTS(SELECT * FROM tblForumSearchIgnoreWords WHERE WordHash = @WordHash)
	INSERT INTO 
		tblForumSearchIgnoreWords
	VALUES
		(@WordHash, @Word)
END

-- DELETE
ELSE IF @Action = 2
BEGIN
	DELETE 
		tblForumSearchIgnoreWords 
	WHERE 
		WordHash = @WordHash

	DELETE
		tblForumSearchBarrel
	WHERE
		WordHash = @WordHash
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSearch_Index' 
				AND type = 'P')
   DROP PROCEDURE spForumSearch_Index
GO
CREATE procedure spForumSearch_Index
(
	@RowCount int = 25
)
AS
BEGIN
SET ROWCOUNT @RowCount

SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	UserName,
	EditNotes = '',
	AttachmentFilename = '',
	Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	IsModerator = 0,
	HasRead = 0 -- not used
FROM 
	tblForumPosts P,
	tblForumUsers U, 
	tblForumThreads T,
	tblForumUserProfile UP,
	tblForumForums F
WHERE 
	F.ForumID = P.ForumID AND 
	P.ThreadID = T.ThreadID AND
	P.UserID = U.UserID AND
        U.UserID = UP.UserID AND
	F.IsSearchable = 1 AND 
	P.IsApproved = 1 AND
	P.IsIndexed = 0
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSearch_PostReindex' 
				AND type = 'P')
   DROP PROCEDURE spForumSearch_PostReindex
GO
CREATE procedure spForumSearch_PostReindex
(
	@RowCount int = 25
)
AS
BEGIN
SET ROWCOUNT @RowCount

SELECT
	*,
	T.IsLocked,
	T.IsSticky,
	UserName,
	EditNotes = '',
	AttachmentFilename = '',
	Replies = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ParentID = P.PostID AND P2.PostLevel != 1),
	IsModerator = 0,
	LastUserActivity = '1/1/1989',
	HasRead = 0 -- not used
FROM 
	tblForumPosts P,
	tblForumUsers U, 
	tblForumThreads T,
	tblForumUserProfile UP,
	tblForumForums F
WHERE 
	F.ForumID = P.ForumID AND 
	P.ThreadID = T.ThreadID AND
	P.UserID = U.UserID AND
        U.UserID = UP.UserID AND
	F.IsActive = 1 AND
	F.IsSearchable = 1 AND 
	P.IsApproved = 1 AND
	P.IsIndexed = 0
ORDER BY
	T.ThreadDate DESC
END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumService_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumService_CreateUpdateDelete
GO
create proc spForumService_CreateUpdateDelete
(
	  @ServiceID				int out
	, @DeleteService			bit = 0
	, @ServiceName				nvarchar(60)
	, @ServiceTypeCode			int
	, @ServiceAssemblyPath		nvarchar(512)
	, @ServiceFullClassName		nvarchar(512)
	, @ServiceWorkingDirectory	nvarchar(512)
)
AS

-- are we deleting the service
IF ( @DeleteService > 0 )
BEGIN

	DELETE tblForumServices
	WHERE
		ServiceID	= @ServiceID

	RETURN
END

IF( @ServiceID > 0 )
BEGIN

	UPDATE tblForumServices SET
		  ServiceName				= @ServiceName
		, ServiceTypeCode			= @ServiceTypeCode
		, ServiceAssemblyPath		= @ServiceAssemblyPath
		, ServiceFullClassName		= @ServiceFullClassName
		, ServiceWorkingDirectory	= @ServiceWorkingDirectory
	WHERE
		ServiceID	= @ServiceID

END
ELSE
BEGIN

	INSERT INTO tblForumServices (
		ServiceName, ServiceTypeCode, ServiceAssemblyPath, ServiceFullClassName, ServiceWorkingDirectory
	) VALUES (
		@ServiceName, @ServiceTypeCode, @ServiceAssemblyPath, @ServiceFullClassName, @ServiceWorkingDirectory
	)

	SET @ServiceID = @@identity

END

RETURN


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumServices_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumServices_Get
GO
create proc spForumServices_Get
(
	@ServiceID int = 0
)
as 
	SELECT
		*
	FROM
		tblForumServices
	WHERE
		ServiceID = @ServiceID or
		(@ServiceID = 0 and 1=1)


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSetForumSubscriptionType' 
				AND type = 'P')
   DROP PROCEDURE spForumSetForumSubscriptionType
GO
CREATE procedure spForumSetForumSubscriptionType
(
	@UserID int,
	@ForumID int,
	@subType int
)
 AS
if (@subType=0)
	DELETE from tblForumTrackedForums where UserID=@UserID and ForumID=@ForumID
ELSE
IF Exists (select SubscriptionType from tblForumTrackedForums (nolock) where UserID=@UserID AND ForumID=@ForumID)
	UPDATE tblForumTrackedForums Set SubscriptionType=@subType where UserID=@UserID and ForumID=@ForumID
ELSE
	INSERT INTO tblForumTrackedForums (UserID, ForumID, SubscriptionType) values (@UserID, @ForumID, @subType)



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSiteSettings_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumSiteSettings_Get
GO
CREATE  PROCEDURE spForumSiteSettings_Get
(
	@Application	nvarchar(512)
)
AS

IF @Application = '*'
	SELECT
		* 
	FROM
		tblForumSiteSettings

ELSE IF NOT EXISTS (SELECT SiteID FROM tblForumSiteSettings WHERE Application = @Application)
	RETURN
ELSE
	SELECT
		* 
	FROM
		tblForumSiteSettings
	WHERE
		Application = @Application



GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSiteSettings_Save' 
				AND type = 'P')
   DROP PROCEDURE spForumSiteSettings_Save
GO
CREATE procedure spForumSiteSettings_Save
(
	@Application 		nvarchar(512),
	@ForumsDisabled		smallint,
	@Settings 		varbinary(8000)
)
AS
BEGIN

	IF EXISTS (SELECT SiteID FROM tblForumSiteSettings WHERE Application = @Application)
		UPDATE
			tblForumSiteSettings
		SET
			ForumsDisabled = @ForumsDisabled,
			Settings = @Settings
		WHERE
			Application = @Application
	ELSE
		INSERT INTO
			tblForumSiteSettings
			(
				Application,
				ForumsDisabled,
				Settings
			)
		VALUES
			(
				@Application,
				@ForumsDisabled,
				@Settings
			)
	
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSite_Statistics' 
				AND type = 'P')
   DROP PROCEDURE spForumSite_Statistics
GO
CREATE              PROCEDURE spForumSite_Statistics 
(
	@UpdateWindow int = 3
)
AS

-- Do we need to update the statistics?
DECLARE @LastUpdate datetime
DECLARE @DateWindow datetime

SET @LastUpdate = ISNULL((SELECT MAX(DateCreated) FROM tblForumstatistics_Site), '1/1/1797')
SET @DateWindow = DATEADD(hh, -@UpdateWindow, GetDate())

if (@LastUpdate <  @DateWindow)
	BEGIN
		exec spForumsystem_UpdateSite
	END

-- SELECT current statistics
SELECT 
	S.*,
	CurrentAnonymousUsers = (SELECT Count(*) FROM tblForumAnonymousUsers),
	MostReadSubject = (SELECT Subject FROM tblForumPosts WHERE PostID = S.MostReadPostID),
	MostViewsSubject = (SELECT Subject FROM tblForumPosts WHERE PostID = S.MostViewsPostID),
	MostActiveSubject = (SELECT Subject FROM tblForumPosts WHERE PostID = S.MostActivePostID),
	MostActiveUser = (SELECT UserName FROM tblForumUsers WHERE UserID = S.MostActiveUserID),
	NewestUser = (SELECT UserName FROM tblForumUsers WHERE UserID = S.NewestUserID)
FROM
	tblForumstatistics_Site S
WHERE
	DateCreated = @LastUpdate

-- SELECT TOP 100 Users
SELECT TOP 100
	U.UserID,
	U.UserName,
	U.PasswordFormat,
	U.Email,
	U.DateCreated,
	U.LastLogin,
	U.LastActivity,
	U.LastAction,
	U.UserAccountStatus,
	U.IsAnonymous,
	S.TotalPosts,
	P.*
FROM
	tblForumstatistics_User S,
	tblForumUsers U,
	tblForumUserProfile P
WHERE
	S.UserID = U.UserID AND
	U.UserID = P.UserID AND
	S.TotalPosts > 0 AND
	P.EnableDisplayInMemberList = 1 AND
	U.UserID > 0
ORDER BY
	S.TotalPosts DESC

-- SELECT top 50 Moderators
SELECT TOP 50
	U.UserID,
	U.UserName,
	U.PasswordFormat,
	U.Email,
	U.DateCreated,
	U.LastLogin,
	U.LastActivity,
	U.LastAction,
	U.UserAccountStatus,
	P.*,
	M.PostsModerated
FROM
	tblForumUsers U,
	tblForumUserProfile P,
	tblForumModerators M
WHERE
	M.UserID = U.UserID AND
	U.UserID = P.UserID AND
	M.PostsModerated > 0
ORDER BY
	PostsModerated DESC

-- SELECT Moderator actions
SELECT
	Description,
	TotalActions
FROM
	tblForumModerationAction
ORDER BY 
	TotalActions DESC 



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSmiley_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumSmiley_CreateUpdateDelete
GO
CREATE proc spForumSmiley_CreateUpdateDelete
(
	  @SmileyID		int out
	, @DeleteSmiley	bit = 0
	, @SmileyCode	nvarchar(20)
	, @SmileyUrl	nvarchar(512)
	, @SmileyText	nvarchar(512)
	, @BracketSafe	bit = 0
)
as

IF( @DeleteSmiley > 0 ) 
BEGIN

	DELETE tblForumSmilies
	WHERE
		SmileyID = @SmileyID

	RETURN
END

IF( @SmileyID > 0 ) 
BEGIN
	UPDATE tblForumSmilies SET
		  SmileyCode	= @SmileyCode
		, SmileyUrl		= @SmileyUrl
		, SmileyText	= @SmileyText
		, BracketSafe	= @BracketSafe
	WHERE
		SmileyID	= @SmileyID
END
ELSE
BEGIN

	INSERT INTO tblForumSmilies (
		SmileyCode, SmileyUrl, SmileyText, BracketSafe
	) VALUES (
		@SmileyCode, @SmileyUrl, @SmileyText, @BracketSafe
	)

	SET @SmileyID = @@identity
END
RETURN



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumSmilies_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumSmilies_Get
GO
create proc spForumSmilies_Get
(
	@SmileyID	int = 0
)
as
	select
		*
	from
		tblForumSmilies
	WHERE
		SmileyID	= @SmileyID OR
		(
			@SmileyID = 0 AND
			1=1
		)



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumStatistics_GetMostActiveUsers_Filtered' 
				AND type = 'P')
   DROP PROCEDURE spForumStatistics_GetMostActiveUsers_Filtered
GO
CREATE PROCEDURE spForumStatistics_GetMostActiveUsers_Filtered (
    @RoleFilter nvarchar(50) = NULL,
    @StartDate datetime = NULL,
    @EndDate datetime = NULL
) AS
    IF @StartDate IS NULL
        IF @EndDate IS NULL SET @StartDate = DATEADD(dd, -1, GETDATE())
        ELSE SET @StartDate = DATEADD(dd, -1, @EndDate)
        
    IF @EndDate IS NULL SET @EndDate = GETDATE()
    IF @StartDate > @EndDate BEGIN
        DECLARE @TempDate datetime;
        SET @TempDate = @StartDate
        SET @StartDate = @EndDate
        SET @EndDate = @TempDate
    END
    
    SELECT TOP 10
        Username, 
        TotalPosts = COUNT(*)
    FROM 
        Posts P 
    WHERE 
        PostDate >= @StartDate AND
        PostDate <= @EndDate AND
        (
            @RoleFilter IS NULL OR
            EXISTS(SELECT NULL FROM UsersInRoles WHERE UserName=P.Username AND RoleName=@RoleFilter)
        )
    GROUP BY P.Username
    ORDER BY TotalPosts DESC



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumStyle_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumStyle_CreateUpdateDelete
GO
create proc spForumStyle_CreateUpdateDelete
(
	  @StyleID				int out
	, @DeleteStyle			bit = 0
	, @StyleName			varchar(30)
	, @StyleSheetTemplate	varchar(30)
	, @BodyBackgroundColor	int
	, @BodyTextColor		int
	, @LinkVisited			int
	, @LinkHover			int
	, @LinkActive			int
	, @RowColorPrimary		int
	, @RowColorSecondary	int
	, @RowColorTertiary		int
	, @RowClassPrimary		varchar(30)
	, @RowClassSecondary	varchar(30)
	, @RowClassTertiary		varchar(30)
	, @HeaderColorPrimary	int
	, @HeaderColorSecondary	int
	, @HeaderColorTertiary	int
	, @HeaderStylePrimary	varchar(30)
	, @HeaderStyleSecondary	varchar(30)
	, @HeaderStyleTertiary	varchar(30)
	, @CellColorPrimary		int
	, @CellColorSecondary	int
	, @CellColorTertiary	int
	, @CellClassPrimary		varchar(30)
	, @CellClassSecondary	varchar(30)
	, @CellClassTertiary	varchar(30)
	, @FontFacePrimary		varchar(30)	
	, @FontFaceSecondary	varchar(30)
	, @FontFaceTertiary		varchar(30)
	, @FontSizePrimary		smallint
	, @FontSizeSecondary	smallint
	, @FontSizeTertiary		smallint
	, @FontColorPrimary		int
	, @FontColorSecondary	int
	, @FontColorTertiary	int
	, @SpanClassPrimary		varchar(30)
	, @SpanClassSecondary	varchar(30)
	, @SpanClassTertiary	varchar(30)
)
AS

IF( @DeleteStyle = 1 ) 
BEGIN
	
	DELETE tblForumStyles
	WHERE
		StyleID	= @StyleID

	RETURN
END

IF( @StyleID > 0 )
BEGIN

	UPDATE tblForumStyles SET
		  StyleName 			= @StyleName
		, StyleSheetTemplate 	= @StyleSheetTemplate
		, BodyBackgroundColor 	= @BodyBackgroundColor
		, BodyTextColor 		= @BodyTextColor
		, LinkVisited 			= @LinkVisited
		, LinkHover 			= @LinkHover
		, LinkActive 			= @LinkActive
		, RowColorPrimary 		= @RowColorPrimary
		, RowColorSecondary 	= @RowColorSecondary
		, RowColorTertiary 		= @RowColorTertiary
		, RowClassPrimary 		= @RowClassPrimary
		, RowClassSecondary 	= @RowClassSecondary
		, RowClassTertiary 		= @RowClassTertiary
		, HeaderColorPrimary 	= @HeaderColorPrimary
		, HeaderColorSecondary	= @HeaderColorSecondary
		, HeaderColorTertiary 	= @HeaderColorTertiary
		, HeaderStylePrimary 	= @HeaderStylePrimary
		, HeaderStyleSecondary 	= @HeaderStyleSecondary
		, HeaderStyleTertiary 	= @HeaderStyleTertiary
		, CellColorPrimary 		= @CellColorPrimary
		, CellColorSecondary	= @CellColorSecondary
		, CellColorTertiary 	= @CellColorTertiary
		, CellClassPrimary 		= @CellClassPrimary
		, CellClassSecondary 	= @CellClassSecondary
		, CellClassTertiary 	= @CellClassTertiary
		, FontFacePrimary 		= @FontFacePrimary
		, FontFaceSecondary 	= @FontFaceSecondary
		, FontFaceTertiary 		= @FontFaceTertiary
		, FontSizePrimary 		= @FontSizePrimary
		, FontSizeSecondary 	= @FontSizeSecondary
		, FontSizeTertiary 		= @FontSizeTertiary
		, FontColorPrimary 		= @FontColorPrimary
		, FontColorSecondary 	= @FontColorSecondary
		, FontColorTertiary 	= @FontColorTertiary
		, SpanClassPrimary 		= @SpanClassPrimary
		, SpanClassSecondary 	= @SpanClassSecondary
		, SpanClassTertiary 	= @SpanClassTertiary
	WHERE
		StyleID = @StyleID


END
ELSE
BEGIN

	INSERT INTO tblForumStyles (
		  StyleName				, StyleSheetTemplate		, BodyBackgroundColor		, BodyTextColor
		, LinkVisited			, LinkHover					, LinkActive				, RowColorPrimary
		, RowColorSecondary		, RowColorTertiary			, RowClassPrimary			, RowClassSecondary
		, RowClassTertiary		, HeaderColorPrimary		, HeaderColorSecondary		, HeaderColorTertiary
		, HeaderStylePrimary	, HeaderStyleSecondary		, HeaderStyleTertiary		, CellColorPrimary
		, CellColorSecondary	, CellColorTertiary			, CellClassPrimary			, CellClassSecondary
		, CellClassTertiary		, FontFacePrimary			, FontFaceSecondary			, FontFaceTertiary
		, FontSizePrimary		, FontSizeSecondary			, FontSizeTertiary			, FontColorPrimary
		, FontColorSecondary	, FontColorTertiary			, SpanClassPrimary			, SpanClassSecondary
		, SpanClassTertiary
	) VALUES (
		  @StyleName			, @StyleSheetTemplate		, @BodyBackgroundColor		, @BodyTextColor		, @LinkVisited			, @LinkHover				, @LinkActive				, @RowColorPrimary
		, @RowColorSecondary	, @RowColorTertiary			, @RowClassPrimary			, @RowClassSecondary
		, @RowClassTertiary		, @HeaderColorPrimary		, @HeaderColorSecondary		, @HeaderColorTertiary
		, @HeaderStylePrimary	, @HeaderStyleSecondary		, @HeaderStyleTertiary		, @CellColorPrimary
		, @CellColorSecondary	, @CellColorTertiary		, @CellClassPrimary			, @CellClassSecondary
		, @CellClassTertiary	, @FontFacePrimary			, @FontFaceSecondary		, @FontFaceTertiary
		, @FontSizePrimary		, @FontSizeSecondary		, @FontSizeTertiary			, @FontColorPrimary
		, @FontColorSecondary	, @FontColorTertiary		, @SpanClassPrimary			, @SpanClassSecondary
		, @SpanClassTertiary
	)

	SELECT @StyleID = @@IDENTITY
END
RETURN


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumStyles_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumStyles_Get
GO
create proc spForumStyles_Get
(
	@StyleID	int = 0
)
as
	select
		*
	from
		tblForumStyles
	WHERE
		StyleID = @StyleID OR
		(
			@StyleID = 0 AND
			1=1
		)



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThread_PrevNext' 
				AND type = 'P')
   DROP PROCEDURE spForumThread_PrevNext
GO
CREATE procedure spForumThread_PrevNext
(
	@ThreadID int,
	@ForumID int,
	@NextThreadID int OUTPUT,
	@PrevThreadID int OUTPUT
)
AS
DECLARE @ThreadDate datetime

SELECT 
	@ThreadDate = ThreadDate 
FROM 
	tblForumThreads
WHERE
	ThreadID = @ThreadID


SELECT TOP 1 
	@PrevThreadID = ThreadID 
FROM 
	tblForumThreads 
WHERE 
	ForumID = @ForumID 
	AND ThreadDate < @ThreadDate 
ORDER BY 
	IsSticky DESC, 
	ThreadDate DESC

IF @@ROWCOUNT < 1
	SELECT @PrevThreadID = 0


SELECT TOP 1 
	@NextThreadID = ThreadID 
FROM 
	tblForumThreads 
WHERE 
	ForumID = @ForumID 
	AND ThreadDate > @ThreadDate 
ORDER BY 
	IsSticky DESC, 
	ThreadDate DESC

IF @@ROWCOUNT < 1
	SELECT @NextThreadID = 0


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThread_Rate' 
				AND type = 'P')
   DROP PROCEDURE spForumThread_Rate
GO
create procedure spForumThread_Rate
(
	@ThreadID int,
	@UserID int,
	@Rating int
)
AS
BEGIN

	IF EXISTS (SELECT ThreadID FROM tblForumPostRating WHERE UserID = @UserID AND ThreadID = @ThreadID)
	BEGIN
		-- Get the User's Current Rating
		DECLARE @CurrentRating int
		SET @CurrentRating = (SELECT Rating FROM tblForumPostRating WHERE UserID = @UserID AND ThreadID = @ThreadID)

		IF @CurrentRating = @Rating
			RETURN

		-- Update the rating
		UPDATE
			tblForumPostRating
		SET 
			Rating = @Rating
		WHERE
			UserID = @UserID AND
			ThreadID = @ThreadID

		-- Update the thread rating
		UPDATE
			tblForumThreads
		SET
			RatingSum = (RatingSum - @CurrentRating) + @Rating
		WHERE
			ThreadID = @ThreadID

	END
	ELSE
	BEGIN
		-- Add an entry into the post rating table
		INSERT INTO
			tblForumPostRating
		VALUES
			(@UserID, @ThreadID, @Rating)

		-- Update the thread rating
		UPDATE
			tblForumThreads
		SET
			RatingSum = RatingSum + @Rating,
			TotalRatings = TotalRatings + 1
		WHERE
			ThreadID = @ThreadID
	END

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThread_Rate_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumThread_Rate_Get
GO
create procedure spForumThread_Rate_Get
(
	@ThreadID int
)
AS
BEGIN
	SELECT
		*
	FROM
		tblForumUsers U,
		tblForumUserProfile UP,
		tblForumPostRating R
	WHERE
		R.UserID = U.UserID AND
		R.ThreadID = @ThreadID AND
		U.UserID = UP.UserID 
END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThreads_GetThreadSet' 
				AND type = 'P')
   DROP PROCEDURE spForumThreads_GetThreadSet
GO
CREATE   procedure spForumThreads_GetThreadSet
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@sqlCount nvarchar(4000),
	@sqlPopulate nvarchar(4000),
	@UserID int,
	@ReturnRecordCount bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalThreads int

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	HasRead bit,
	ThreadID int
)

INSERT INTO #PageIndex (ThreadID, HasRead)
EXEC (@sqlPopulate)

SELECT 
	jT.*,
	HasRead = jPI.HasRead,
	jP.PostID,
	jP.Subject,
	jP.Body,
	UserName = jT.PostAuthor
FROM 
	#PageIndex jPI
	JOIN tblForumThreads jT ON jPI.ThreadID = jT.ThreadID
	JOIN tblForumPosts jP ON jPI.ThreadID = jP.ThreadID
WHERE 
	jPI.IndexID > @PageLowerBound
	AND jPI.IndexID < @PageUpperBound
	AND jP.PostLevel = 1 	-- PostLevel=1 should mean it's a top-level thread starter
ORDER BY
	jPI.IndexID	-- this is the ordering system we're using populated from the @sqlPopulate

DROP TABLE #PageIndex

-- Update that the user has read this forum
IF @UserID > 0
	EXEC spForumForum_MarkRead @UserID, @ForumID

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
	EXEC (@sqlCount)

-- Return the users that the message is to if this
-- is a private message
IF @ForumID = 0
	SELECT 
		U.*,
		UP.*,
		P2.ThreadID 
	FROM
		tblForumPrivateMessages P1, 
		tblForumPrivateMessages P2,
		tblForumUsers U,
		tblForumUserProfile UP
	WHERE 
		P1.UserID = @UserID AND 
		P2.UserID <> @UserID AND 
		P2.UserID = U.UserID AND
		U.UserID = UP.UserID AND
		P1.ThreadID = P2.ThreadID

END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThreads_ThreadSet' 
				AND type = 'P')
   DROP PROCEDURE spForumThreads_ThreadSet
GO
CREATE                  procedure spForumThreads_ThreadSet
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@ThreadsNewerThan Datetime,
	@SortBy int,
	@SortOrder bit,
	@UserID int,
	@UnreadOnly bit,
	@Unanswered bit,
	@ReturnRecordCount bit
)
AS
BEGIN

-- Are we attempting to get unanswered messages?
IF @Unanswered = 1
BEGIN

	exec spForumThreads_ThreadSet_Unanswered @ForumID, @PageIndex, @PageSize, @ThreadsNewerThan, @UserID, @UnreadOnly, @ReturnRecordCount

	RETURN
END

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalThreads int

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

IF (@ReturnRecordCount = 1)
  IF (@UnreadOnly = 1)
    SELECT @TotalThreads = count(T.ThreadID) FROM tblForumThreads T WHERE T.ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan AND IsApproved = 1 AND dbo.sfForumHasReadPost(@UserID, T.ThreadID, T.ForumID) = 0
  ELSE
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan AND IsApproved = 1)

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ThreadID int
)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

-- Sort by Last Post
IF @SortBy = 0 AND @SortOrder = 1 AND @UnreadOnly = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan  AND IsApproved = 1 ORDER BY IsSticky DESC, StickyDate DESC
ELSE IF @SortBy = 0 AND @SortOrder = 1 AND @UnreadOnly = 1
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan  AND IsApproved = 1 AND dbo.sfForumHasReadPost(@UserID, T.ThreadID, T.ForumID) = 0 ORDER BY IsSticky DESC, StickyDate DESC
ELSE IF @SortBy = 0 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND StickyDate >= @ThreadsNewerThan  AND IsApproved = 1 ORDER BY IsSticky, StickyDate

-- Sort by Thread Author
IF @SortBy = 1 AND @SortOrder = 1 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumUsers U WHERE T.ForumID = @ForumID AND T.ThreadDate >= @ThreadsNewerThan AND T.UserID = U.UserID  AND IsApproved = 1 ORDER BY Username
ELSE IF @SortBy = 1 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumUsers U WHERE T.ForumID = @ForumID AND T.ThreadDate >= @ThreadsNewerThan AND T.UserID = U.UserID AND IsApproved = 1 ORDER BY Username DESC

-- Sort by Replies
IF @SortBy = 2 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalReplies
ELSE IF @SortBy = 2 AND @SortOrder = 1 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalReplies DESC

-- Sort by Views
IF @SortBy = 3 AND @SortOrder = 0 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalViews
ELSE IF @SortBy = 3 AND @SortOrder = 1 AND @Unanswered = 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND IsApproved = 1 ORDER BY TotalViews DESC

-- Unanswered Posts
IF @Unanswered = 1 AND @ForumID > 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 AND IsApproved = 1 ORDER BY ThreadDate DESC
ELSE IF @Unanswered = 1
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 AND IsApproved = 1 ORDER BY ThreadDate DESC, F.ForumID DESC

IF @UnreadOnly = 1
	SELECT 
		T.*,
		P.Subject,
		P.Body,
		UserName = T.PostAuthor,
		HasRead =  CASE
			WHEN @UserID = 0 THEN 0
			WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
			END
	FROM 
		tblForumThreads T (nolock),
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ThreadID = PageIndex.ThreadID AND
		P.PostID = T.ThreadID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

ELSE
		SELECT 
		T.*,
		P.Subject,
		P.Body,
		UserName = T.PostAuthor,
		HasRead =  CASE
			WHEN @UserID = 0 THEN 0
			WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
			END
	FROM 
		tblForumThreads T (nolock),
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ThreadID = PageIndex.ThreadID AND
		P.PostID = T.ThreadID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

-- Update that the user has read this forum
IF @UserID > 0
	exec spForumForum_MarkRead @UserID, @ForumID

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
	SELECT @TotalThreads


END


GO



--exec spForumThreads_ThreadSet @ForumId = 42, @PageIndex = 0, @PageSize = 25, @ThreadsNewerThan = 'Jan  1 1753 12:00:00:000AM', @UserID = 0, @UnreadOnly = 0, @SortBy = 3, @SortOrder = 0, @Unanswered = 0, @ReturnRecordCount = 1

--exec spForumThreads_ThreadSet @ForumId = -1, @PageIndex = 0, @PageSize = 25, @ThreadsNewerThan = 'Apr 16 1998 10:26:34:253AM', @UserID = 0, @UnreadOnly = 0, @SortBy = 3, @SortOrder = 0, @Unanswered = 1, @ReturnRecordCount = 1
IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThreads_ThreadSet_Active' 
				AND type = 'P')
   DROP PROCEDURE spForumThreads_ThreadSet_Active
GO
CREATE          procedure spForumThreads_ThreadSet_Active
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@UserID int,
	@TotalReplies int = 1,
	@TotalViews int = 30,
	@ReturnRecordCount bit
)
AS
BEGIN
DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalThreads int

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

IF (@ReturnRecordCount = 1)
	IF @ForumID > 0
		SET @TotalThreads = (select count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews)
	ELSE
		SET @TotalThreads = (select count(T.ThreadID) FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews)

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ThreadID int
)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************
-- Unanswered Posts
IF @ForumID > 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews ORDER BY ThreadDate DESC
ELSE
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND TotalReplies > @TotalReplies AND TotalViews > @TotalViews ORDER BY ThreadDate DESC


SELECT 
	T.*,
	P.Subject,
	P.Body,
	U.UserName,
	HasRead =  CASE
		WHEN @UserID = 0 THEN 0
		WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
		END
FROM 
	tblForumThreads T (nolock),
	tblForumUsers U,
	tblForumPosts P,
	#PageIndex PageIndex
WHERE 
	T.ThreadID = PageIndex.ThreadID AND
	T.UserID = U.UserID AND
	P.PostID = T.ThreadID AND
	PageIndex.IndexID > @PageLowerBound AND
	PageIndex.IndexID < @PageUpperBound
ORDER BY 
	PageIndex.IndexID

-- Update that the user has read this forum
IF @UserID > 0
	exec spForumForum_MarkRead @UserID, @ForumID

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
	SELECT @TotalThreads


END
	

GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumThreads_ThreadSet_Unanswered' 
				AND type = 'P')
   DROP PROCEDURE spForumThreads_ThreadSet_Unanswered
GO
CREATE       procedure spForumThreads_ThreadSet_Unanswered
(
	@ForumID int,
	@PageIndex int, 
	@PageSize int,
	@ThreadsNewerThan Datetime,
	@UserID int,
	@UnreadOnly bit,
	@ReturnRecordCount bit
)
AS
BEGIN

DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalThreads int

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

IF (@ReturnRecordCount = 1)
  IF @UnreadOnly = 1 AND @ForumID > 0
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0)
  ELSE IF @UnreadOnly = 1
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0)
  ELSE IF @UnreadOnly = 0 AND @ForumID > 0
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0)
  ELSE
    SET @TotalThreads = (SELECT count(ThreadID) FROM tblForumThreads WHERE ThreadDate >= @ThreadsNewerThan AND ForumID > 0 AND TotalReplies = 0)

-- Create a temp table to store the select results
CREATE TABLE #PageIndex 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	ThreadID int
)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

IF @ForumID > 0
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads WHERE ForumID = @ForumID AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 ORDER BY ThreadDate DESC
ELSE
    INSERT INTO #PageIndex (ThreadID)
    SELECT ThreadID FROM tblForumThreads T, tblForumForums F WHERE T.ForumID = F.ForumID AND F.ForumID > 0 AND ThreadDate >= @ThreadsNewerThan AND TotalReplies = 0 AND IsSticky = 0 ORDER BY ThreadDate DESC, F.ForumID DESC

IF @UnreadOnly = 1
	-- Get Unread Topics only
		SELECT 
		T.*,
		P.Subject,
		P.Body,
		U.UserName,
		HasRead = 1
	FROM 
		tblForumThreads T (nolock),
		tblForumUsers U,
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ForumID = @ForumID AND 
		T.UserID = U.UserID AND
		P.PostID = T.ThreadID AND
		ThreadDate >= @ThreadsNewerThan AND
		T.ThreadID NOT IN (SELECT PostsRead.PostID FROM PostsRead WHERE PostsRead.UserID = @UserID) AND
		T.ThreadID >= (select MarkReadAfter from ForumsRead where UserID = @UserID and forumid = @ForumID) AND
		T.ThreadID = PageIndex.ThreadID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID
ELSE
	SELECT 
		T.*,
		P.Subject,
		P.Body,
		U.UserName,
		HasRead =  CASE
			WHEN @UserID = 0 THEN 0
			WHEN @UserID > 0 THEN (dbo.sfForumHasReadPost(@UserID, P.PostID, P.ForumID))
			END
	FROM 
		tblForumThreads T (nolock),
		tblForumUsers U,
		tblForumPosts P,
		#PageIndex PageIndex
	WHERE 
		T.ThreadID = PageIndex.ThreadID AND
		T.UserID = U.UserID AND
		T.ThreadID = P.PostID AND
		PageIndex.IndexID > @PageLowerBound AND
		PageIndex.IndexID < @PageUpperBound
	ORDER BY 
		PageIndex.IndexID

-- Update that the user has read this forum
IF @UserID > 0
	exec spForumForum_MarkRead @UserID, @ForumID

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
	SELECT @TotalThreads


END
	

GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumTrackAnonymousUsers' 
				AND type = 'P')
   DROP PROCEDURE spForumTrackAnonymousUsers
GO
CREATE             PROCEDURE spForumTrackAnonymousUsers
(
	@UserID char(36)
)
AS
BEGIN
	-- Does the user already exist?
	IF EXISTS (SELECT UserID FROM AnonymousUsers WHERE UserID = @UserID)
		UPDATE 
			AnonymousUsers
		SET 
			LastLogin = GetDate()
		WHERE
			UserID = @UserID
	ELSE
		INSERT INTO
			AnonymousUsers
			(UserID) 
		VALUES
			(@UserID)
	
	-- Anonymous users also pay tax to clean up table
	DELETE AnonymousUsers WHERE LastLogin < DateAdd(minute, -20, GetDate())	
END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUpdateEmailTemplate' 
				AND type = 'P')
   DROP PROCEDURE spForumUpdateEmailTemplate
GO
CREATE   procedure spForumUpdateEmailTemplate
(
	@EmailID		int,
	@Subject		nvarchar(256),
	@From			nvarchar(128),
	@Message		ntext
)
 AS
	-- Update a particular email message
	UPDATE Emails SET
		FromAddress = @From,
		Subject = @Subject,
		Message = @Message
	WHERE EmailID = @EmailID

GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUpdateForumGroup' 
				AND type = 'P')
   DROP PROCEDURE spForumUpdateForumGroup
GO
CREATE   PROCEDURE spForumUpdateForumGroup
(
	@ForumGroupName		nvarchar(256),
	@ForumGroupID	int
)
 AS
	IF @ForumGroupName IS NULL
		DELETE
			ForumGroups
		WHERE
			ForumGroupID = @ForumGroupID
	ELSE
		-- insert a new forum
		UPDATE 
			ForumGroups 
		SET 
			Name = @ForumGroupName
		WHERE 
			ForumGroupID = @ForumGroupID		





GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUpdateForumGroupSortOrder' 
				AND type = 'P')
   DROP PROCEDURE spForumUpdateForumGroupSortOrder
GO
CREATE procedure spForumUpdateForumGroupSortOrder
(
     @ForumGroupID int,
     @MoveUp bit
)
AS
BEGIN
DECLARE @currentSortValue int
DECLARE @replaceSortValue int

-- Get the current sort order
SELECT @currentSortValue = SortOrder FROM tblForumForumGroups WHERE ForumGroupID = @ForumGroupID

-- Move the item up or down?
IF (@MoveUp = 1)
  BEGIN
    IF (@currentSortValue != 1)
      BEGIN
        SET @replaceSortValue = @currentSortValue - 1

        UPDATE tblForumForumGroups SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue
        UPDATE tblForumForumGroups SET SortOrder = @replaceSortValue WHERE ForumGroupID = @ForumGroupID
      END
  END

ELSE
  BEGIN
    IF (@currentSortValue < (SELECT MAX(ForumGroupID) FROM tblForumForumGroups))
    BEGIN
      SET @replaceSortValue = @currentSortValue + 1

      UPDATE tblForumForumGroups SET SortOrder = @currentSortValue WHERE SortOrder = @replaceSortValue
      UPDATE tblForumForumGroups SET SortOrder = @replaceSortValue WHERE ForumGroupID = @ForumGroupID
    END
  END
END




GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUpdateForumLastCrawlDate' 
				AND type = 'P')
   DROP PROCEDURE spForumUpdateForumLastCrawlDate 
GO
CREATE  procedure spForumUpdateForumLastCrawlDate (
	@ForumID int,
	@LastCrawled datetime,
	@DeepCrawl bit
)
AS
BEGIN
	IF EXISTS (SELECT ForumID FROM tblForumSearchLastCrawled WHERE ForumID = @ForumID)

		-- Are we doing a deep crawl?
		IF @DeepCrawl = 1
			UPDATE 
				tblForumSearchLastCrawled
			SET 
				LastDeepCrawl = @LastCrawled
			WHERE
				ForumID = @ForumID
		ELSE
			UPDATE 
				tblForumSearchLastCrawled
			SET 
				LastShallowCrawl = @LastCrawled
			WHERE
				ForumID = @ForumID
			
	ELSE
		INSERT INTO
			tblForumSearchLastCrawled
		(
			ForumID,
			LastDeepCrawl,
			LastShallowCrawl
		)
		VALUES
		(
			@ForumID,
			@LastCrawled,
			@LastCrawled
		)

END

 
GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUpdateSearchBarrel' 
				AND type = 'P')
   DROP PROCEDURE spForumUpdateSearchBarrel
GO
CREATE   procedure spForumUpdateSearchBarrel (
	@WordHash int,
	@Weight float,
	@PostID int,
	@ThreadID int,
	@ForumID int
)
AS
BEGIN
	IF EXISTS (SELECT WordHash FROM tblForumSearchBarrel WHERE PostID = @PostID AND WordHash = @WordHash)
		UPDATE 
			tblForumSearchBarrel 
		SET
			Weight = @Weight
		WHERE
			WordHash = @WordHash AND
			PostID = @PostID
	ELSE
		INSERT INTO
			tblForumSearchBarrel
			(WordHash, PostID, ThreadID, ForumID, Weight)
		VALUES
			(@WordHash, @PostID, @ThreadID, @ForumID, @Weight)
END


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUserHasPostsAwaitingModeration' 
				AND type = 'P')
   DROP PROCEDURE spForumUserHasPostsAwaitingModeration
GO
create  procedure spForumUserHasPostsAwaitingModeration
(
	@UserName nvarchar(50)
)
AS
BEGIN
	-- Can the user moderate all forums?
	IF EXISTS(SELECT NULL FROM Moderators (nolock) WHERE UserName=@UserName AND ForumID=0)

		-- return ALL posts awaiting moderation
		IF EXISTS(SELECT TOP 1 PostID FROM Posts P (nolock) INNER JOIN Forums F (nolock) ON F.ForumID = P.ForumID WHERE Approved = 0)
		  SELECT 1
		ELSE
		  SELECT 0
	ELSE
		-- return only those posts in the forum this user can moderate
		IF EXISTS (SELECT TOP 1 PostID FROM Posts P (nolock) INNER JOIN Forums F (nolock) ON F.ForumID = P.ForumID WHERE Approved = 0 AND P.ForumID IN (SELECT ForumID FROM Moderators (nolock) WHERE UserName=@UserName))
		  SELECT 1
		ELSE
		  SELECT 0
	
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_Anonymous_Count' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_Anonymous_Count
GO
CREATE  procedure spForumUser_Anonymous_Count
(
	@TimeWindow int,
	@AnonymousUserCount int out
)
AS
BEGIN
DECLARE @StatDate datetime

	-- Clean up the anonymous users table
	DELETE tblForumAnonymousUsers WHERE LastLogin < DateAdd(minute, -@TimeWindow, GetDate())	

	-- Get a count of anonymous users
	SET @AnonymousUserCount = (SELECT count(UserID) FROM tblForumAnonymousUsers)

	-- Do we need to update our forum statistics?
	SET @StatDate = (SELECT MAX(DateCreated) FROM tblForumstatistics_Site)
	IF (SELECT TotalAnonymousUsers FROM tblForumstatistics_Site WHERE DateCreated = @StatDate) < @AnonymousUserCount
		UPDATE
			tblForumstatistics_Site
		SET 
			TotalAnonymousUsers = @AnonymousUserCount
		WHERE
			DateCreated = @StatDate

END



GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_Anonymous_Update' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_Anonymous_Update
GO
CREATE     PROCEDURE spForumUser_Anonymous_Update
(
	@UserID char(36),
	@LastActivity datetime,
	@LastAction nvarchar(1024) = ''
)
AS
BEGIN
	-- Does the user already exist?
	IF EXISTS (SELECT UserID FROM tblForumAnonymousUsers WHERE UserID = @UserID)

		UPDATE 
			tblForumAnonymousUsers
		SET 
			LastLogin = @LastActivity,
			LastAction = @LastAction
		WHERE
			UserID = @UserID

	ELSE

		INSERT INTO
			tblForumAnonymousUsers
			(UserID, LastLogin, LastAction) 
		VALUES
			(@UserID, @LastActivity, @LastAction)

END
 
GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_Avatar' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_Avatar
GO
CREATE procedure spForumUser_Avatar
(
	@UserID	int
)
AS
BEGIN
	IF EXISTS(SELECT UserID FROM tblForumUserAvatar WHERE UserID = @UserID)
		SELECT
			U.UserID,
			U.ImageID,
			Length,
			ContentType,
			Content,
			DateLastUpdated
		FROM
			tblForumImages I,
			tblForumUserAvatar U
		WHERE
			I.ImageID = U.ImageID AND
			U.UserID = @UserID
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_CreateUpdateDelete' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_CreateUpdateDelete
GO
CREATE           procedure spForumUser_CreateUpdateDelete 
(
	@UserID int out,
	@UserName   nvarchar (64) = '',
	@Password   nvarchar (64) = '',
	@Email    nvarchar (128) = '',
	@StringNameValuePairs  varbinary (7500) = 0,
	@UserAccountStatus  smallint = 1,
	@IsAnonymous   smallint = 0,
	@PasswordFormat  int = 1, 
	@PasswordQuestion  nvarchar(256) = '',
	@PasswordAnswer  nvarchar(256) = '',
	@Salt    nvarchar (24) = '',
	@AppUserToken       varchar (128) = '',
	@ForumView   int = 0,
	@TimeZone   float = 0.0,
	@PostRank   binary(1) = 0x0,
	@PostSortOrder   int = 0,
	@IsAvatarApproved   smallint  = 0,
	@ForceLogin   bit   = 0,
	@ModerationLevel  smallint  = 0,
	@EnableThreadTracking  smallint  = 0,
	@EnableDisplayUnreadThreadsOnly smallint  = 0,
	@EnableAvatar    smallint  = 0,
	@EnableDisplayInMemberList  smallint  = 1,
	@EnablePrivateMessages  smallint  = 1,
	@EnableOnlineStatus   smallint  = 1,
	@EnableHtmlEmail   smallint  = 1,
	@Action    int
)
AS

-- this sproc returns various error/success codes
-- a return value of 1 means success
-- a return value of 2 means a dup username
-- a return value of 3 means a dup email address
-- first, we need to check if the username is a dup


-- Are we creating a user?
IF @Action = 0
BEGIN
	IF @IsAnonymous = 1
	BEGIN
		SELECT @UserID = UserID FROM tblForumUsers WHERE UserName = @UserName AND IsAnonymous = 1
		
		-- Check if the anonymous user already exists
		IF @UserID IS NOT NULL
		BEGIN
			SELECT 1
		RETURN
	END

END
	
-- check for username exists
IF EXISTS(SELECT UserName FROM tblForumUsers (nolock) WHERE UserName = @UserName AND IsAnonymous = 0)
	SELECT 2
ELSE
	-- we need to check if the email is a dup
	IF EXISTS(SELECT Email FROM tblForumUsers (nolock) WHERE Email = @Email AND IsAnonymous = 0)
		SELECT 3
	ELSE
	BEGIN
		-- INSERT the user
		INSERT INTO tblForumUsers 
			( UserName, 
			Email, 
			Password, 
			PasswordFormat,
			Salt,
			PasswordQuestion,
			PasswordAnswer,
			UserAccountStatus,
			IsAnonymous,
			AppUserToken )
		VALUES 
			( @UserName, 
			@Email, 
			@Password, 
			@PasswordFormat,
			@Salt,
			@PasswordQuestion,
			@PasswordAnswer, 
			@UserAccountStatus,
			@IsAnonymous,
			@AppUserToken )
		
		-- Get the new userID
		SET @UserID = @@IDENTITY
		
		INSERT INTO tblForumUserProfile
			VALUES
			( @UserID,
			@TimeZone,
			0,
			@PostSortOrder,
			@StringNameValuePairs,
			@PostRank,
			@IsAvatarApproved,
			@ModerationLevel,
			@EnableThreadTracking,
			@EnableDisplayUnreadThreadsOnly,
			@EnableAvatar,
			@EnableDisplayInMemberList,
			@EnablePrivateMessages,
			@EnableOnlineStatus,
			@EnableHtmlEmail )
		
		SELECT 1 -- return Everything's fine status code
	END
	
	-- exit the sproc normally
	RETURN
END

-- Update the user
ELSE IF @Action = 1
BEGIN
	-- First Update the tblForumUsers table
	UPDATE
		tblForumUsers
	SET
		UserName = @UserName,
		Email = @Email,
		UserAccountStatus = @UserAccountStatus,
		ForceLogin = @ForceLogin
	WHERE
		UserID = @UserID
	
	-- Next, update the user's profile
	UPDATE
		tblForumUserProfile
	SET
		TimeZone = @TimeZone,
		PostRank = @PostRank,
		PostSortOrder = @PostSortOrder,
		StringNameValues = @StringNameValuePairs,
		IsAvatarApproved = @IsAvatarApproved,
		ModerationLevel = @ModerationLevel,
		EnableThreadTracking = @EnableThreadTracking,
		EnableDisplayUnreadThreadsOnly = @EnableDisplayUnreadThreadsOnly,
		EnableAvatar = @EnableAvatar,
		EnableDisplayInMemberList = @EnableDisplayInMemberList,
		EnablePrivateMessages = @EnablePrivateMessages,
		EnableOnlineStatus = @EnableOnlineStatus,
		EnableHtmlEmail = @EnableHtmlEmail
	WHERE
		UserID = @UserID

END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_CreateProfile' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_CreateProfile
GO
CREATE           procedure spForumUser_CreateProfile 
(
	@UserID int,
	@StringNameValuePairs  varbinary (7500) = 0,
	@TimeZone   float = 0.0,
	@PostRank   binary(1) = 0x0,
	@PostSortOrder   int = 0,
	@IsAvatarApproved   smallint  = 0,
	@ModerationLevel  smallint  = 0,
	@EnableThreadTracking  smallint  = 0,
	@EnableDisplayUnreadThreadsOnly smallint  = 0,
	@EnableAvatar    smallint  = 0,
	@EnableDisplayInMemberList  smallint  = 1,
	@EnablePrivateMessages  smallint  = 1,
	@EnableOnlineStatus   smallint  = 1,
	@EnableHtmlEmail   smallint  = 1,
	@Action    int
)
AS
BEGIN
	INSERT INTO tblForumUserProfile
		VALUES
		( @UserID,
		@TimeZone,
		0,
		@PostSortOrder,
		@StringNameValuePairs,
		@PostRank,
		@IsAvatarApproved,
		@ModerationLevel,
		@EnableThreadTracking,
		@EnableDisplayUnreadThreadsOnly,
		@EnableAvatar,
		@EnableDisplayInMemberList,
		@EnablePrivateMessages,
		@EnableOnlineStatus,
		@EnableHtmlEmail )
	
	SELECT 1 -- return Everything's fine status code
	
	-- exit the sproc normally
	RETURN
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_Get
GO
CREATE      PROCEDURE spForumUser_Get
(
	@UserID int,
	@UserName nvarchar(64) = '',
	@IsOnline bit = 0,
	@LastAction nvarchar(1024) = ''
)
AS
BEGIN
	
	-- Are we looking up the user by username or ID?
	IF @UserID = 0
	BEGIN
		SELECT
			U.UserID,
			U.Salt,
			U.UserName,
			U.PasswordFormat,
			U.PasswordQuestion,
			U.Email,
			U.DateCreated,
			U.LastLogin,
			U.LastActivity,
			U.LastAction,
			U.UserAccountStatus,
			U.IsAnonymous,
			U.ForceLogin,
			P.*
		FROM
			tblForumUsers U LEFT OUTER JOIN tblForumUserProfile P
			ON U.UserID = P.UserID
		WHERE 	UserName = @UserName
		
		-- Get the Username
		SET @UserID = (SELECT UserID FROM tblForumUsers U WHERE U.UserName = @UserName)
		
	END
	ELSE BEGIN
		-- Looking up the user by ID
	
		-- Get the user details
		SELECT
			U.UserID,
			U.Salt,
			U.UserName,
			U.PasswordFormat,
			U.PasswordQuestion,
			U.Email,
			U.DateCreated,
			U.LastLogin,
			U.LastActivity,
			U.LastAction,
			U.UserAccountStatus,
			U.IsAnonymous,
			U.ForceLogin,
			P.*
		FROM 
			tblForumUsers U LEFT OUTER JOIN tblForumUserProfile P
			ON U.UserID = P.UserID
		WHERE 	U.UserID = @UserID

	END
	
	IF @IsOnline = 1
	BEGIN
		EXEC spForumsystem_UserIsOnline @UserID, @LastAction
	END
	
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_GetByEmail' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_GetByEmail
GO
create procedure spForumUser_GetByEmail
(
	@Email		nvarchar(64)
)
AS
SELECT 
	UserID
FROM
	tblForumUsers
WHERE
	Email = @Email

GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_PasswordAnswer_Change' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_PasswordAnswer_Change
GO
CREATE PROCEDURE spForumUser_PasswordAnswer_Change
(
	@UserID int,
	@PasswordQuestion  nvarchar(256),
	@PasswordAnswer  nvarchar(256)
)
AS
BEGIN
	UPDATE
		tblForumUsers
	SET
		PasswordQuestion = @PasswordQuestion,
		PasswordAnswer = @PasswordAnswer
	WHERE
		UserID = @UserID
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_Password_Change' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_Password_Change
GO
CREATE PROCEDURE spForumUser_Password_Change
(
	@UserID int,
	@PasswordFormat int = 1,
	@NewPassword nvarchar(64),
	@Salt nvarchar(24)
)
AS
BEGIN
	UPDATE
		tblForumUsers
	SET
		[Password] = @NewPassword,
		PasswordFormat = @PasswordFormat,
		Salt = @Salt
	WHERE
		UserID = @UserID
END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUser_ToggleSettings' 
				AND type = 'P')
   DROP PROCEDURE spForumUser_ToggleSettings
GO
CREATE   procedure spForumUser_ToggleSettings
(
	@UserID int,
	@ModerationLevel int,
	@UserAccountStatus int,
	@ForceLogin bit = 0,
	@IsAvatarApproved bit,
	@ModeratorID int
)
AS
BEGIN

	UPDATE 
		tblForumUsers 
	SET
		UserAccountStatus = @UserAccountStatus,
		ForceLogin = @ForceLogin
	WHERE
		UserID = @UserID

	UPDATE
		tblForumUserProfile
	SET
		ModerationLevel = @ModerationLevel,
		IsAvatarApproved = @IsAvatarApproved
	WHERE
		UserID = @UserID

	IF @ModerationLevel = 0
	  exec spForumsystem_ModerationAction_AuditEntry 11, @ModeratorID, null, @UserID
	ELSE	
	  exec spForumsystem_ModerationAction_AuditEntry 10, @ModeratorID, null, @UserID

	IF @UserAccountStatus = 1
	  exec spForumsystem_ModerationAction_AuditEntry 13, @ModeratorID, null, @UserID
	ELSE	
	  exec spForumsystem_ModerationAction_AuditEntry 12, @ModeratorID, null, @UserID

END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUsersInRole_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumUsersInRole_Get
GO
CREATE PROCEDURE spForumUsersInRole_Get
(
	@PageIndex int,
	@PageSize int,
	@SortBy int = 0,
	@SortOrder int = 0,
	@RoleID int,
	@UserAccountStatus smallint = 1,
	@ReturnRecordCount bit = 0
)
AS
BEGIN
DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalUsers int

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Create a temp table to store the select results
CREATE TABLE #PageIndexForUsers 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	UserID int
)	

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
    SET @TotalUsers = (SELECT count(R.UserID) FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID)

-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

-- Sort by Date Joined
IF @SortBy = 0 AND @SortOrder = 1
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY DateCreated
ELSE IF @SortBy = 0 AND @SortOrder = 0
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY DateCreated DESC

-- Sort by username
IF @SortBy = 1 AND @SortOrder = 1
	INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY UserName
ELSE IF @SortBy = 1 AND @SortOrder = 0
	INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY UserName DESC

-- Sort by Last Active
IF @SortBy = 3 AND @SortOrder = 1
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY LastActivity DESC
ELSE IF @SortBy = 3 AND @SortOrder = 0
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY LastActivity

-- Sort by TotalPosts
IF @SortBy = 4 AND @SortOrder = 1
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY TotalPosts DESC
ELSE IF @SortBy = 4 AND @SortOrder = 0
    INSERT INTO #PageIndexForUsers (UserID)
    SELECT U.UserID FROM tblForumUsers U, tblForumUsersInRoles R, tblForumUserProfile P WHERE R.UserID = U.UserID AND R.UserID = P.UserID AND U.UserAccountStatus = @UserAccountStatus AND EnableDisplayInMemberList = 1 AND RoleID = @RoleID ORDER BY TotalPosts

-- Get the user details
SELECT
	*,
	IsModerator = (SELECT Count(*) FROM tblForumModerators WHERE UserID = U.UserID)
FROM 
	tblForumUsers U (nolock),
	tblForumUserProfile P,
	#PageIndexForUsers
WHERE 
	U.UserID = #PageIndexForUsers.UserID AND
	U.UserID = P.UserID AND
	#PageIndexForUsers.IndexID > @PageLowerBound AND
	#PageIndexForUsers.IndexID < @PageUpperBound
ORDER BY
	#PageIndexForUsers.IndexID
END

-- Return the record count if necessary

IF (@ReturnRecordCount = 1)
  SELECT @TotalUsers


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUsers_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumUsers_Get
GO
CREATE         PROCEDURE spForumUsers_Get
(
	@PageIndex int,
	@PageSize int,
	@SortBy int = 0,
	@SortOrder int = 0,
	@UsernameFilter nvarchar(128),
	@FilterIncludesEmailAddress bit = 0,
	@UserAccountStatus smallint = 1,
	@ReturnRecordCount bit = 0,
	@IncludeHiddenUsers bit = 0
)
AS
BEGIN
DECLARE @PageLowerBound int
DECLARE @PageUpperBound int
DECLARE @RowsToReturn int
DECLARE @TotalUsers int

-- Set the page bounds
SET @PageLowerBound = @PageSize * @PageIndex
SET @PageUpperBound = @PageLowerBound + @PageSize + 1

-- First set the rowcount
SET @RowsToReturn = @PageSize * (@PageIndex + 1)
SET ROWCOUNT @RowsToReturn

-- Create a temp table to store the select results
CREATE TABLE #PageIndexForUsers 
(
	IndexID int IDENTITY (1, 1) NOT NULL,
	UserID int
)	

-- Do we need to return a record count?
-- *************************************
IF (@ReturnRecordCount = 1)
  IF ((@UsernameFilter IS NULL) OR (@UsernameFilter = ''))
    SET @TotalUsers = (SELECT count(U.UserID) FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1))
  ELSE
    IF (@FilterIncludesEmailAddress = 0)
      SET @TotalUsers = (SELECT count(U.UserID) FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1) AND UserName LIKE @UsernameFilter)
    ELSE
      SET @TotalUsers = (SELECT count(U.UserID) FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1) AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter))


-- Special case depending on what the user wants and how they want it ordered by
-- *************************************

-- Sort by Date Joined
IF @SortBy = 0 AND @SortOrder = 0
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1) ORDER BY DateCreated
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1) AND UserName LIKE @UsernameFilter ORDER BY DateCreated
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY DateCreated
ELSE IF @SortBy = 0 AND @SortOrder = 1
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY DateCreated DESC
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY DateCreated DESC
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY DateCreated DESC

-- Sort by username
IF @SortBy = 1 AND @SortOrder = 0
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY UserName
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY UserName
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY UserName
ELSE IF @SortBy = 1 AND @SortOrder = 1
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY UserName DESC
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY UserName DESC
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY UserName DESC

-- Sort by Last Active
IF @SortBy = 3 AND @SortOrder = 1
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY LastActivity DESC
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY LastActivity DESC
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY LastActivity DESC
ELSE IF @SortBy = 3 AND @SortOrder = 0
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY LastActivity
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY LastActivity
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY LastActivity

-- Sort by TotalPosts
IF @SortBy = 4 AND @SortOrder = 1
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY TotalPosts DESC
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY TotalPosts DESC
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY TotalPosts DESC
ELSE IF @SortBy = 4 AND @SortOrder = 0
    IF @UsernameFilter IS NULL
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  ORDER BY TotalPosts
    ELSE
	IF @FilterIncludesEmailAddress = 0
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND UserName LIKE @UsernameFilter ORDER BY TotalPosts
	ELSE
	    INSERT INTO #PageIndexForUsers (UserID)
	    SELECT U.UserID FROM tblForumUsers U, tblForumUserProfile P WHERE U.UserID = P.UserID AND UserAccountStatus = @UserAccountStatus AND (EnableDisplayInMemberList = 1 or @IncludeHiddenUsers = 1)  AND (UserName LIKE @UsernameFilter OR Email LIKE @UsernameFilter) ORDER BY TotalPosts

-- Get the user details
SELECT
	*,
	IsModerator = (SELECT Count(*) FROM tblForumModerators WHERE UserID = U.UserID)
FROM 
	tblForumUsers U (nolock),
	tblForumUserProfile P,
	#PageIndexForUsers
WHERE 
	U.UserID = #PageIndexForUsers.UserID AND
	U.UserID = P.UserID AND
	#PageIndexForUsers.IndexID > @PageLowerBound AND
	#PageIndexForUsers.IndexID < @PageUpperBound
ORDER BY
	#PageIndexForUsers.IndexID
END

-- Return the record count if necessary

IF (@ReturnRecordCount = 1)
  SELECT @TotalUsers


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumUsers_Online' 
				AND type = 'P')
   DROP PROCEDURE spForumUsers_Online
GO
CREATE  PROCEDURE spForumUsers_Online
(
	@PastMinutes int
)
AS
BEGIN
	DELETE
		tblForumUsersOnline
	WHERE
		LastActivity < DateAdd(hour, -1, GetDate())
	
	SELECT
		U.UserID,
		U.UserName,
		U.PasswordFormat,
		U.PasswordQuestion,
		U.Email,
		U.DateCreated,
		U.LastLogin,
		U.LastActivity,
		U.LastAction,
		U.UserAccountStatus,
		U.IsAnonymous,
		P.*	--,
--		IsModerator = (select count(*) from tblForumModerators where UserID = U.UserID)
	FROM
		tblForumUsersOnline O
		JOIN tblForumUsers U ON O.UserID = U.UserID
		JOIN tblForumUserProfile P ON U.UserID = P.UserID
	WHERE
		U.IsAnonymous = 0
		AND O.LastActivity > DateAdd(minute, -@PastMinutes, GetDate())
	
	SELECT
		UserID,
		LastActivity = LastLogin,
		LastAction
	FROM
		tblForumAnonymousUsers
END


GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumVote' 
				AND type = 'P')
   DROP PROCEDURE spForumVote
GO
create procedure spForumVote (
  @PostID int,
  @Vote nvarchar(2)
)
AS
IF NOT EXISTS (
    SELECT
        PostID 
    FROM 
        Vote 
    WHERE 
        PostID = @PostID AND Vote = @Vote
)
BEGIN
    -- Transacted insert for download count
    BEGIN TRAN
        INSERT INTO 
            Vote
        VALUES
        (
            @PostID,
            @Vote,
            1
        )
    COMMIT TRAN
END
ELSE
BEGIN
    -- Transacted update for download count
    BEGIN TRAN
        UPDATE 
          Vote
        SET 
          VoteCount  =  VoteCount + 1
        WHERE 
          PostID = @PostID AND
          Vote = @Vote
    COMMIT TRAN
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsecurity_ValidateUser' 
				AND type = 'P')
   DROP PROCEDURE spForumsecurity_ValidateUser
GO
CREATE    procedure spForumsecurity_ValidateUser
(
	@UserName nvarchar(128),
	@Password nvarchar(128)
)
AS
	-- Update the time the user last logged in
	UPDATE 
		tblForumUsers
	SET 
		LastLogin = getdate()
	WHERE 
		UserName = @UserName
		AND Password = @Password
	
	SELECT @@ROWCOUNT 



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsecurity_ValidateUserPasswordAnswer' 
				AND type = 'P')
   DROP PROCEDURE spForumsecurity_ValidateUserPasswordAnswer
GO
CREATE PROCEDURE spForumsecurity_ValidateUserPasswordAnswer
(
	@UserID int,
	@PasswordAnswer nvarchar(256)
)
AS
BEGIN
	SELECT 
		COUNT(UserID) 
	FROM 
		tblForumUsers
	WHERE
		UserID = @UserID
		AND (PasswordAnswer = @PasswordAnswer OR PasswordAnswer = NULL)
END


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_ACE_Get' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_ACE_Get
GO
CREATE procedure spForumsystem_ACE_Get
(
	@ForumID int,
	@UserID int,
	@ACE binary(4) out
)
AS
BEGIN
	SET @ACE = 0x00000000
	SELECT @ACE = convert(int, @ACE) ^ ACE FROM tblForumForumPermissions P, tblForumUserRoles R WHERE P.RoleID = R.RoleID AND R.UserID = @UserID AND P.ForumID = @ForumID
END


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_CleanForumsRead' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_CleanForumsRead
GO
create   procedure spForumsystem_CleanForumsRead
(
	@ForumID int
)
AS
BEGIN
	DELETE
		tblForumForumsRead
	WHERE
		MarkReadAfter = 0 AND
		ForumID = @ForumID
END



GO



IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_DeletePostAndChildren' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_DeletePostAndChildren
GO
CREATE procedure spForumsystem_DeletePostAndChildren
(
    @PostID int,
    @RootPostID int = null,
    @DeleteChildren bit = 1
)
AS

-- Posts are not "deleted", they are moved to ForumID=4.

DECLARE @ThreadID INT
DECLARE @OldThreadID INT
DECLARE @UserID INT
DECLARE @PostAuthor NVARCHAR(64)
DECLARE @ForumID INT
DECLARE @ParentID INT
DECLARE @IsApproved BIT
DECLARE @MostRecentPostAuthor NVARCHAR(64)
DECLARE @MostRecentPostAuthorID NVARCHAR(64)
DECLARE @MostRecentPostID INT
DECLARE @ForumPostStatisticsEnabled BIT
DECLARE @PostDate DATETIME
DECLARE @EmoticonID INT
DECLARE @IsLocked INT
DECLARE @MinPostLevel INT
DECLARE @MinSortOrder INT


-- First, get information about the post that is about to be deleted.
SELECT
	@OldThreadID = ThreadID,
	@UserID = UserID,
	@PostAuthor = PostAuthor,
	@ParentID = ParentID,
	@ForumID = ForumID,
	@IsLocked = IsLocked,
	@IsApproved = IsApproved,
	@PostDate = PostDate,
	@EmoticonID = EmoticonID
FROM
	tblForumPosts
WHERE
	PostID = @PostID


IF @IsApproved = 1 -- AND @RootPostID IS NULL
BEGIN
	-- We create a new thread here because we don't
	-- know if we are deleting a reply, a thread starter, or both.
	INSERT tblForumThreads 	
		( ForumID,
		PostDate, 
		UserID, 
		PostAuthor, 
		ThreadDate, 
		MostRecentPostAuthor, 
		MostRecentPostAuthorID, 	
		MostRecentPostID, 
		IsLocked, 
		IsApproved,
		IsSticky, 
		StickyDate, 
		ThreadEmoticonID )
	VALUES
		( 4, 	-- the Deleted Posts forum
		@PostDate, 
		@UserID, 
		@PostAuthor,
		@PostDate,
		@PostAuthor,
		@UserID, 
		@PostID,	-- MostRecentPostID, which we don't know until after post INSERT below.
		@IsLocked,
		@IsApproved,
		0,	-- Downgrade the thread to a non-sticky.
		@PostDate,
		@EmoticonID )

	-- Get the new ThreadID
	SELECT 
		@ThreadID = @@IDENTITY
	FROM
		tblForumThreads

	-- Move the post to the new thread
        UPDATE 
		tblForumPosts 
	SET 
		ForumID = 4,
		ThreadID = @ThreadID,
		ParentID = @PostID,
		SortOrder = 1,
		PostLevel = 1		-- set as the thread starter
	WHERE 
		PostID = @PostID
	
	-- delete all child posts, unless DeleteChildred is set to 0.
	IF @DeleteChildren = 1 
	BEGIN
		UPDATE
			tblForumPosts
		SET
			ForumID = 4,
			ThreadID = @ThreadID,
			PostLevel = 2,
			SortOrder = 2
		WHERE
			ParentID = @PostID

		-- EAD: quick fix because it was reset above
		-- (set all others to 2 for now)
		UPDATE
			tblForumPosts
		SET
			PostLevel = 1,
			SortOrder = 1
		WHERE
			PostID = @PostID
	END
	ELSE BEGIN
		-- Have to fix the non-deleted child posts, if any, for the ParentID.
		-- Setting them to the top-level post.
		UPDATE
			tblForumPosts
		SET
			ParentID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE ThreadID = @OldThreadID ORDER BY PostID ASC)
		WHERE
			ThreadID = @OldThreadID
	END
    
	-- update the new thread's stats
	SELECT TOP 1
		@MostRecentPostAuthor = PostAuthor,
		@MostRecentPostAuthorID = UserID,
		@MostRecentPostID = PostID
	FROM
		tblForumPosts
	WHERE
		ThreadID = @ThreadID
	ORDER BY
		PostID DESC

	UPDATE
		tblForumThreads
	SET
		MostRecentPostAuthor = @MostRecentPostAuthor, 
		MostRecentPostAuthorID = @MostRecentPostAuthorID, 	
		MostRecentPostID = @MostRecentPostID
	WHERE
		ThreadID = @ThreadID		


	-- If no posts are linked to the OldthreadID, delete the old thread
	IF NOT EXISTS(SELECT ThreadID FROM tblForumPosts WHERE ThreadID = @OldThreadID)
	BEGIN
		-- Delete all thread tracking data.	
		DELETE FROM 
			tblForumSearchBarrel
		WHERE 
			ThreadID = @OldThreadID

		-- Delete all thread tracking data.	
		DELETE FROM 
			tblForumTrackedThreads
		WHERE 
			ThreadID = @OldThreadID

		-- Delete all thread read data.
		DELETE FROM 
			tblForumThreadsRead
		WHERE 
			ThreadID = @OldThreadID

		-- Delete the thread
		DELETE
			tblForumThreads
		WHERE
			ThreadID = @OldThreadID
	END

	-- Decrease the TotalPosts on the user's profile.
	IF (SELECT EnablePostStatistics FROM tblForumForums WHERE ForumID = @ForumID) = 1
		UPDATE 
			tblForumUserProfile
		SET 
			TotalPosts = ISNULL(TotalPosts - (SELECT COUNT(PostID) FROM tblForumPosts WHERE ThreadID = @ThreadID), 0)
		WHERE 
			UserID = @UserID
END

-- Delete from the search index
DELETE 
	tblForumSearchBarrel 
WHERE 
	PostID = @PostID


-- If the post is approved, reset the statistics on the forums and threads table.
IF @IsApproved = 1
BEGIN
	EXEC spForumsystem_ResetThreadStatistics @OldThreadID
	EXEC spForumsystem_ResetThreadStatistics @ThreadID
	EXEC spForumsystem_ResetForumStatistics @ForumID
	EXEC spForumsystem_ResetForumStatistics 4
END


GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_DuplicatePost' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_DuplicatePost
GO
create procedure spForumsystem_DuplicatePost
(
@UserID int,
@Body ntext,
@IntervalInMinutes int = 0,
@IsDuplicate bit out
)
AS

	IF @IntervalInMinutes > 0
		-- Check for duplicates
		IF EXISTS (SELECT TOP 1 PostID FROM tblForumPosts (nolock) WHERE UserID = @UserID AND Body LIKE @Body AND PostDate > DateAdd(minute, -@IntervalInMinutes, GetDate()) )
			SET @IsDuplicate = 1
		ELSE
			SET @IsDuplicate = 0
        ELSE
		-- Check for duplicates
		IF EXISTS (SELECT TOP 1 PostID FROM tblForumPosts (nolock) WHERE UserID = @UserID AND Body LIKE @Body)
			SET @IsDuplicate = 1
		ELSE
			SET @IsDuplicate = 0


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_Import_Forum' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_Import_Forum
GO
CREATE PROCEDURE spForumsystem_Import_Forum
	@ForumID [int],
	@ForumGroupID [int] = 0,
	@ParentID [int] = 0,
	@DateCreated [datetime],
	@IsActive [bit] = 0,
	@IsModerated [bit] = 1,
	@SortOrder [int] = 0,
	@Name [nvarchar](256) = '',
	@Description [nvarchar](3000) = ''
AS
BEGIN

IF EXISTS(SELECT * FROM tblForumForums WHERE ForumID = @ForumID)
	-- Update the forum information
	UPDATE 
		tblForumForums 
	SET
		Name = @Name,
		Description = @Description,
		ParentID = @ParentID,
		DateCreated = @DateCreated,
		ForumGroupID = @ForumGroupID,
		IsModerated = @IsModerated,
		IsActive = @IsActive,
		SortOrder = @SortOrder
	WHERE 
		ForumID = @ForumID
ELSE
BEGIN
	SET IDENTITY_INSERT tblForumForums ON
	INSERT INTO 
		tblForumForums (
			ForumID,
			ForumGroupID, 
			ParentID, 
			DateCreated,
			IsActive,
			IsModerated,
			SortOrder, 
			Name, 
			Description
			)
		VALUES (
			@ForumID,
			@ForumGroupID,
			@ParentID,
			@DateCreated,
			@IsActive,
			@IsModerated,
			@SortOrder,
			@Name,
			@Description
			)
	SET IDENTITY_INSERT tblForumForums OFF

	INSERT INTO 
		tblForumForumPermissions 
		(ForumID, RoleID, [View], [Read], Post, Reply, Edit, [Delete], Sticky, Announce, CreatePoll, Vote, Moderate, Attachment)
	VALUES
		(@ForumID, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0)

END

END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_Import_ForumGroup' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_Import_ForumGroup
GO
create PROCEDURE spForumsystem_Import_ForumGroup
(
	@ForumGroupID int,
	@Name nvarchar(256),
	@SortOrder int
)
AS
BEGIN

	IF EXISTS(SELECT * FROM tblForumForumGroups WHERE ForumGroupID = @ForumGroupID)
		UPDATE
			tblForumForumGroups
		SET 
			Name = @Name,
			SortOrder = @SortOrder
		WHERE
			ForumGroupID = @ForumGroupID
	ELSE
	BEGIN
		SET IDENTITY_INSERT tblForumForumGroups ON
		INSERT INTO
			tblForumForumGroups
			(
				ForumGroupID,
				Name,
				SortOrder
			)
		VALUES
			(
				@ForumGroupID,
				@Name,
				@SortOrder
			)
		SET IDENTITY_INSERT tblForumForumGroups OFF
	END
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_Import_LinkUser' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_Import_LinkUser
GO
CREATE procedure spForumsystem_Import_LinkUser
AS

DECLARE @PA_PostID INT
DECLARE @PA_PostCount INT
DECLARE @PA_PostAuthor VARCHAR(255)
DECLARE @PA_UserID INT

DECLARE @Rows1 INT
DECLARE @FS1 INT

SET @Rows1=0
SET @Rows1 = (SELECT COUNT(PostID) FROM tblForumPosts)

SET @FS1=0
/* ***************************************************************** */
/* Get all Posts and PostAuthors */
/* ***************************************************************** */
DECLARE PostAuthor_Cursor CURSOR FOR
SELECT PostID, PostAuthor FROM tblForumPosts

OPEN PostAuthor_Cursor
FETCH NEXT FROM PostAuthor_Cursor INTO @PA_PostID, @PA_PostAuthor
SET @FS1=@@FETCH_STATUS
 --PRINT @Rows1
WHILE @FS1=0
BEGIN
	--PRINT @FS1
--PRINT 'PostAuthor_Cursor'
	IF(@Rows1>0)
	BEGIN
		IF(@PA_PostAuthor <> '')
		BEGIN
			/* **************************************************************************** */
			/* Get PostAuthor ID's from users table */
			/* **************************************************************************** */
	
			DECLARE PA_UserID_Cursor CURSOR FOR
			Select UserID from tblForumUsers where UserName = @PA_PostAuthor
	
			OPEN PA_UserID_Cursor
			FETCH NEXT FROM PA_UserID_Cursor INTO @PA_UserID
			PRINT '@PA_UserID:' + CONVERT(CHAR(10), @PA_UserID)
			CLOSE PA_UserID_Cursor
			DEALLOCATE PA_UserID_Cursor
		
			IF @PA_UserID is not NULL and @PA_UserID <> 0
				BEGIN
					Update tblForumposts
					Set UserID = @PA_UserID
					Where PostID = @PA_PostID
					
					/* **************************************************************************** */
					/* Now update number of posts for each user */
					/* **************************************************************************** */
					
					DECLARE PA_PostCount_Cursor CURSOR FOR
					Select Count(PostID) as TotalPosts from tblForumPosts where UserID = @PA_UserID
			
					OPEN PA_PostCount_Cursor
					FETCH NEXT FROM PA_PostCount_Cursor INTO @PA_PostCount
					PRINT '@PA_PostCount:' + CONVERT(CHAR(10), @PA_PostCount)

					--Now Update the stats
					Update tblForumuserprofile
					Set TotalPosts = @PA_PostCount
					Where UserID = @PA_UserID

					CLOSE PA_PostCount_Cursor
					DEALLOCATE PA_PostCount_Cursor



				END
	
			--SET @PA_UserID=0
			FETCH NEXT FROM PostAuthor_Cursor INTO @PA_PostID, @PA_PostAuthor
			SET @FS1=@@FETCH_STATUS
	
		END
	END
END
CLOSE PostAuthor_Cursor
DEALLOCATE PostAuthor_Cursor



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_Import_Post' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_Import_Post
GO
CREATE  PROCEDURE spForumsystem_Import_Post (
	@PostID [int],
	@ThreadID [int],
	@ParentID [int],
	@ForumID [int],
	@PostLevel [int],
	@SortOrder [int],
	@PostDate [datetime],
	@IsApproved [bit],
	@Views [int],
	@IsLocked [bit],
	@IsSticky [bit],
	@StickyDate [datetime],
	@UserName [nvarchar] (64),
	@Subject [nvarchar] (256),
	@Body [ntext],
	@FormattedBody [ntext],
	@PostType [int],
	@IPAddress [nvarchar] (32)
)
AS
BEGIN
DECLARE @UserID int

	-- Don't write over data that already exists
/*
	-- Editted by Eric :: we're overwriting to keep the same PostID valid.  Merge comes
	   at a later date.

	IF EXISTS (SELECT PostID FROM tblForumPosts WHERE PostID = @PostID)
		RETURN
	ELSE
*/

	-- Select the user who created the post
	SELECT @UserID = UserID FROM tblForumUsers WHERE UserName = @UserName

	IF @UserID IS NULL
		SET @UserID = 0

	BEGIN TRANSACTION
	-- INSERT into Posts and Threads table

		-- detect if the thread already exists
		IF EXISTS(SELECT ThreadID FROM tblForumThreads WHERE ThreadID = @ThreadID AND ForumID = @ForumID)
		BEGIN
			
			-- found a thread, Check to see if we are making a new deleted post
			IF @ForumID = 4
			BEGIN
	
				-- this is a deleted posts, we create a new thread for each deleted post
				INSERT tblForumThreads 	
					( ForumID,
					PostDate, 
					UserID, 
					PostAuthor, 
					ThreadDate, 
					MostRecentPostAuthor, 
					MostRecentPostAuthorID, 	
					MostRecentPostID, 
					TotalViews,
					IsLocked, 
					IsApproved,
					IsSticky, 
					StickyDate )
				VALUES
					( @ForumID, 
					@PostDate, 
					@UserID, 
					@UserName,
					@PostDate,
					@UserName,
					@UserID, 
					0,	-- MostRecentPostID, which we don't know until after post INSERT below.
					@Views,
					@IsLocked,
					@IsApproved,
					@IsSticky,
					@StickyDate )
	
				-- Get the new ThreadID, which overwrites what we were passed.
				SELECT 
					@ThreadID = @@IDENTITY
				FROM
					tblForumThreads

			END
		END
		ELSE BEGIN
		
			-- we are injecting a threadID outright

			SET IDENTITY_INSERT tblForumThreads ON
			INSERT tblForumThreads 	
				( ThreadID,
				ForumID,
				PostDate, 
				UserID, 
				PostAuthor, 
				ThreadDate, 
				MostRecentPostAuthor, 
				MostRecentPostAuthorID, 	
				MostRecentPostID, 
				TotalViews,
				IsLocked, 
				IsApproved,
				IsSticky, 
				StickyDate )
			VALUES
				( @ThreadID,
				@ForumID, 
				@PostDate, 
				@UserID, 
				@UserName,
				@PostDate,
				@UserName,
				@UserID, 
				0,	-- MostRecentPostID, which we don't know until after post INSERT below.
				@Views,
				@IsLocked,
				@IsApproved,
				@IsSticky,
				@StickyDate )

			SET IDENTITY_INSERT tblForumThreads OFF

		END

		-- finally we insert the post
		SET IDENTITY_INSERT tblForumPosts ON
		INSERT tblForumPosts 
			( PostID,
			ForumID, 
			ThreadID, 
			ParentID, 
			PostLevel, 
			SortOrder, 
			Subject, 
			UserID, 
			PostAuthor, 
			IsApproved, 
			IsLocked, 
			Body, 
			FormattedBody, 
			PostType, 
			PostDate, 
			IPAddress )
		VALUES 
			( @PostID,
			@ForumID, 
			@ThreadID, 
			@ParentID, 	-- ParentID
			@PostLevel, 	-- PostLevel, 1 marks start/top/first post in thread.
			@SortOrder, 	-- SortOrder (not in use at this time)
			@Subject, 
			@UserID, 
			@UserName,
			@IsApproved, 
			@IsLocked, 
			@Body, 
			@FormattedBody, 
			@PostType, 
			@PostDate, 
			@IPAddress )

		SET IDENTITY_INSERT tblForumPosts OFF
	

		-- Update some stats
		IF @IsApproved = 1 AND (@ForumID NOT IN(0, 4))
		BEGIN
			EXEC spForumsystem_UpdateThread @ThreadID, @PostID

			IF (SELECT EnablePostStatistics FROM tblForumForums WHERE ForumID = @ForumID) = 1
				UPDATE tblForumUserProfile SET TotalPosts = TotalPosts + 1 WHERE UserID = @UserID

			EXEC spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID
		END


	COMMIT TRANSACTION
END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_Import_Preperation' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_Import_Preperation
GO
CREATE  PROCEDURE spForumsystem_Import_Preperation
AS
BEGIN
	-- used to prep the database before importing.

	
	BEGIN TRANSACTION
	/*
	DELETE FROM nntp_Posts WHERE ForumID = 10
	DELETE FROM tblForumSearchBarrel WHERE ForumID = 10
	DELETE FROM tblForumPosts WHERE ForumID = 10
	DELETE FROM tblForumThreadsRead WHERE ForumID = 10
	DELETE FROM tblForumThreads WHERE ForumID = 10
	DELETE FROM tblForumForumPermissions WHERE ForumID = 10
	DELETE FROM tblForumForumPingBack WHERE ForumID = 10
	DELETE FROM tblForumForumsRead WHERE ForumID = 10
	DELETE FROM tblForumPostAttachments WHERE ForumID = 10
	DELETE FROM tblForumTrackedForums WHERE ForumID = 10
	DELETE FROM nntp_NewsGroups WHERE ForumID = 10
	DELETE FROM tblForumModerationAudit WHERE ForumID = 10
	DELETE FROM tblForumForums WHERE ForumID = 10
	DELETE FROM tblForumForumGroups WHERE ForumGroupID = 2
	*/

	DELETE FROM nntp_Posts WHERE ForumID > 4
	DELETE FROM tblForumSearchBarrel WHERE ForumID > 4
	DELETE FROM tblForumPosts WHERE ForumID > 4
	DELETE FROM tblForumThreadsRead WHERE ForumID > 4
	DELETE FROM tblForumThreads WHERE ForumID > 4
	DELETE FROM tblForumForumPermissions WHERE ForumID > 4
	DELETE FROM tblForumForumPingBack WHERE ForumID > 4
	DELETE FROM tblForumForumsRead WHERE ForumID > 4
	DELETE FROM tblForumPostAttachments WHERE ForumID > 4
	DELETE FROM tblForumTrackedForums WHERE ForumID > 4
	DELETE FROM nntp_NewsGroups WHERE ForumID > 4
	DELETE FROM tblForumModerationAudit WHERE ForumID > 4
	DELETE FROM tblForumForums WHERE ForumID > 4
	DELETE FROM tblForumForumGroups WHERE ForumGroupID > 1

	COMMIT TRANSACTION
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_Import_User' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_Import_User
GO
CREATE PROCEDURE spForumsystem_Import_User
	@UserID [int],
	@Password [nvarchar](64) = '',
	@Email [nvarchar](128) = '',
	@DateCreated [datetime],
	@UserName [nvarchar](64) = '',
	@AccountStatus [smallint] = 1,
	@TimeZone [int] = 0,
	@EnableAvatar [smallint] = 0,
	@EnableDisplayInMemberList [smallint] = 1,
	@StringNameValuePairs [varbinary](7500) = 0,
	@EnableThreadTracking [smallint] = 0


AS
BEGIN

	-- Are we creating a user? 
	IF EXISTS(SELECT * FROM tblForumUsers WHERE UserID = @UserID)
		RETURN
	ELSE
	BEGIN

			SET IDENTITY_INSERT tblForumUsers ON
			INSERT INTO 
				tblForumUsers 
				(
					UserID,
					UserName, 
					Email, 
					Password, 
					UserAccountStatus,
					DateCreated
				)
			VALUES	
				(
					@UserID,
					@UserName, 
					@Email, 
					@Password, 
					@AccountStatus,
					@DateCreated
				)
			SET IDENTITY_INSERT tblForumUsers OFF

			INSERT INTO
				tblForumUserProfile
				(
					UserID,
					TimeZone,
					EnableAvatar,
					EnableDisplayInMemberList,
					StringNameValues,	
					EnableThreadTracking
				)
			VALUES
				(
					@UserID,
					@TimeZone,
					@EnableAvatar,
					@EnableDisplayInMemberList,
					@StringNameValuePairs,
					@EnableThreadTracking
				)
	END

END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_ModerationAction_AuditEntry' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_ModerationAction_AuditEntry
GO
create procedure spForumsystem_ModerationAction_AuditEntry
(
	@ModerationAction int,
	@ModeratorID int,
	@PostID int = null,
	@UserID int = null,
	@ForumID int = null,
	@Notes nvarchar(1024) = null
)
AS
BEGIN
	INSERT INTO
		tblForumModerationAudit
		(
			ModerationAction,
			PostID,
			UserID,
			ForumID,
			ModeratorID,
			Notes
		)
	VALUES
		(
			@ModerationAction,
			@PostID,
			@UserID,
			@ForumID,
			@ModeratorID,
			@Notes
		)

	UPDATE
		tblForumModerationAction
	SET
		TotalActions = TotalActions + 1
	WHERE
		ModerationAction = @ModerationAction

		
END

GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_ResetForumStatistics' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_ResetForumStatistics
GO
CREATE      procedure spForumsystem_ResetForumStatistics
(
    @ForumID int = 0
)
AS
DECLARE @AutoDelete bit
DECLARE @AutoDeleteThreshold int
DECLARE @ForumCount int
DECLARE @ThreadID int
DECLARE @PostID int
SET @ForumCount = 1

IF @ForumID = 0
    -- Reset the statistics on all of the forums.
    WHILE @ForumCount < (SELECT Max(ForumID) FROM tblForumForums)
    BEGIN
        IF EXISTS(SELECT ForumID FROM tblForumForums WHERE ForumID = @ForumCount)
            EXEC spForumsystem_ResetForumStatistics @ForumCount
            
        SET @ForumCount = @ForumCount + 1
    END
ELSE
    BEGIN
    
	-- Finally, perform any auto-delete
	SELECT 
		@AutoDelete = EnableAutoDelete, 
		@AutoDeleteThreshold = AutoDeleteThreshold 
	FROM 
		tblForumForums
	WHERE
		ForumID = @ForumID

	-- Do we need to cleanup the forum?
	IF @AutoDelete = 1
	BEGIN
		DELETE tblForumThreads WHERE ThreadDate < DateAdd(dd, -@AutoDeleteThreshold, GetDate()) AND LastViewedDate < DateAdd(dd, -@AutoDeleteThreshold, GetDate()) AND StickyDate < DateAdd(dd, -@AutoDeleteThreshold, GetDate()) AND ForumID = @ForumID
		exec sp_recompile 'tblForumThreads'
		exec sp_recompile 'tblForumPosts'
		exec sp_recompile 'tblForumSearchBarrel'
	END

        -- Select the most recent post from the forum.
        SELECT TOP 1
            @ThreadID = ThreadID,
            @PostID = PostID
        FROM 
            tblForumPosts
        WHERE 
            ForumID = @ForumID AND
            IsApproved = 1
        ORDER BY
            PostID DESC
   
        -- If the thread is null, reset the forum statistics.
        IF @ThreadID IS NULL
            UPDATE
                tblForumForums
            SET
                TotalThreads = 0,
                TotalPosts = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID = @ForumID),
                MostRecentPostID = 0,
                MostRecentThreadID = 0,
                MostRecentPostDate = '1/01/1797',
                MostRecentPostAuthorID = 0,
                MostRecentPostSubject = '',
                MostRecentPostAuthor = ''
            WHERE
                ForumID = @ForumID
            
        ELSE
            EXEC spForumsystem_UpdateForum @ForumID, @ThreadID, @PostID

    END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_ResetThreadStatistics' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_ResetThreadStatistics
GO
CREATE   PROCEDURE spForumsystem_ResetThreadStatistics
(
	@ThreadID int
)
AS

DECLARE @PostID int
DECLARE @UserID int
DECLARE @PostDate datetime
DECLARE @PostAuthor varchar(64)
DECLARE @Subject varchar(256)

-- Select the most recent post in the thread.
SELECT TOP 1
	@PostID = PostID,
	@UserID = UserID,
	@PostDate = PostDate,
	@PostAuthor = PostAuthor
FROM
	tblForumPosts
WHERE
	ThreadID = @ThreadID
	AND IsApproved = 1
ORDER BY
	PostID DESC

-- Update the thread.	
UPDATE 
	tblForumThreads
SET
	TotalReplies = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ThreadID = @ThreadID AND IsApproved = 1 AND PostLevel > 1),
	MostRecentPostAuthorID = @UserID,
	MostRecentPostAuthor = @PostAuthor,	
	MostRecentPostID = @PostID
WHERE
	ThreadID = @ThreadID




GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UpdateForum' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UpdateForum
GO
CREATE        procedure spForumsystem_UpdateForum
(
	@ForumID int,
	@ThreadID int,
	@PostID int
)
AS
BEGIN
DECLARE @UserID 		int
DECLARE @PostDate 		datetime
DECLARE @TotalThreads 		int
DECLARE @TotalPostsApproved	int
DECLARE @TotalPosts 		int
DECLARE @Subject		nvarchar(64)
DECLARE @User 			nvarchar(64)

-- Get values necessary to update the forum statistics
SELECT
	@UserID = U.UserID,
	@PostDate = PostDate,
	@TotalPostsApproved = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ForumID = P.ForumID AND P2.IsApproved=1),
	@TotalPosts = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID = P.ForumID),
	@TotalThreads = (SELECT COUNT(P2.PostID) FROM tblForumPosts P2 (nolock) WHERE P2.ForumID = P.ForumID AND P2.IsApproved=1 AND P2.PostLevel=1),
        @Subject = P.Subject,
	@User = PostAuthor
FROM
	tblForumPosts P
	JOIN tblForumUsers U ON U.UserID = P.UserID
WHERE
	PostID = @PostID

-- Do the update within a transaction
BEGIN TRAN

	UPDATE 
		tblForumForums
	SET
		TotalPosts = @TotalPosts,
		TotalThreads = @TotalThreads,
		MostRecentPostID = @PostID,
		MostRecentThreadID = @ThreadID,
		MostRecentPostDate = @PostDate,
		MostRecentPostAuthorID = @UserID,
                MostRecentPostSubject = @Subject,
		MostRecentPostAuthor = @User,
		MostRecentThreadReplies = ISNULL((SELECT TotalReplies FROM tblForumThreads WHERE ThreadID = @ThreadID), 0)
	WHERE
		ForumID = @ForumID

COMMIT TRAN

END

GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UpdateMostActiveUsers' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UpdateMostActiveUsers
GO
CREATE   procedure spForumsystem_UpdateMostActiveUsers
AS
BEGIN
	SET NOCOUNT ON

	DELETE tblForumstatistics_User
	
	INSERT INTO tblForumstatistics_User
	SELECT TOP 100
		U.UserID,
		TotalPosts = ISNULL( (SELECT count(PostID) FROM tblForumPosts WHERE UserID = P.UserID AND PostDate > DateAdd(day, -1, GetDate())), 0)
	FROM
		tblForumUserProfile P,
		tblForumUsers U
	WHERE
		U.UserID = P.UserID AND
		U.IsAnonymous = 0 AND
		U.UserAccountStatus = 1
	ORDER BY
		TotalPosts DESC

	SET NOCOUNT OFF
END


GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UpdateSite' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UpdateSite
GO
CREATE PROCEDURE spForumsystem_UpdateSite 
(
	@UpdateUserPostRank bit = 1,
	@UpdateMostActiveUserList bit = 1
)
AS
	-- Get summary information - Total Users, Total Posts, TotalTopics, DaysPosts, and DaysTopics
	DECLARE @LastDateTimeUpdate datetime
	DECLARE @TotalUsers int
	DECLARE @TotalPosts int
	DECLARE @TotalTopics int
	DECLARE @TotalModerators int
	DECLARE @TotalModeratedPosts int
	DECLARE @NewThreadsInPast24Hours int
	DECLARE @NewPostsInPast24Hours int
	DECLARE @NewUsersInPast24Hours int
	DECLARE @MostViewsPostID int
	DECLARE @MostActivePostID int
	DECLARE @MostReadPostID int
	DECLARE @TotalAnonymousUsers int
	DECLARE @NewestUserID int
	DECLARE @MostActiveUserID int

	SET NOCOUNT ON

	SET @LastDateTimeUpdate = ISNULL( 
					(
						SELECT TOP 1
							DateCreated 
						FROM 
							tblForumstatistics_Site
					), '1/1/1979 12:00:00')

	-- Reset top posters
	IF @UpdateUserPostRank = 1
		exec spForumsystem_UpdateUserPostRank

	IF @UpdateMostActiveUserList = 1
		exec spForumsystem_UpdateMostActiveUsers

	-- Total Anonymous Users
	-- ***********************************************
	SET @TotalAnonymousUsers = ISNULL( 
					(
						SELECT 
							COUNT(UserID) 
						FROM 
							tblForumAnonymousUsers
					), 0 )

	-- Total Moderators, for this site only
	-- ***********************************************
	SET @TotalModerators = ISNULL(
					(
						SELECT 
							COUNT(jUR.UserID) 
						FROM 
							tblForumUsersInRoles jUR
							JOIN tblForumRoles jR ON jR.RoleID = jUR.RoleID
						WHERE 
							jUR.RoleID = 4
					), 0)

	-- Total Moderated Posts
	-- ***********************************************
	SET @TotalModeratedPosts = ISNULL( 
					(
						SELECT 
							COUNT(ModerationAction) 
						FROM 
							tblForumModerationAudit 
						WHERE 
							ModeratedOn >= @LastDateTimeUpdate
					), 0 )

	-- Most "Viewed" thread, by grabbing the first post
	-- ***********************************************
	SET @MostViewsPostID = ISNULL(
					(
						SELECT TOP 1 
							jP1.PostID
						FROM 
							tblForumThreads jT
							JOIN tblForumPosts jP1 ON jP1.ThreadID = jT.ThreadID
							JOIN tblForumForumPermissions jP ON jP.ForumID = jT.ForumID
						WHERE 
							jP.RoleID = 0 AND
							jP.[View] = 0x01 AND
							jT.ThreadDate > DateAdd(d, -3, GetDate()) AND
							jP1.IsApproved = 1 AND
							jT.ForumID > 4		-- excluding PM and hidden forums
						ORDER BY 
							jT.TotalViews DESC
					), 0)


	-- Most "Active" Thread, by grabbing the first post
	-- ***********************************************
	SET @MostActivePostID = ISNULL(
					(
						SELECT TOP 1 
							jP1.PostID
						FROM 
							tblForumThreads jT
							JOIN tblForumPosts jP1 ON jP1.ThreadID = jT.ThreadID
							JOIN tblForumForumPermissions jP ON jP.ForumID = jT.ForumID
						WHERE 
							jP.RoleID = 0 AND
							jP.[View] = 0x01 AND
							jT.ThreadDate > DateAdd(d, -3, GetDate()) AND
							jP1.IsApproved = 1 AND
							jT.ForumID > 4		-- excluding PM and hidden forums
						ORDER BY 
							jT.TotalReplies DESC
					), 0)

	-- Most "Read" thread, by grabbing the first post
	-- ***********************************************
	SET @MostReadPostID = ISNULL(
					(
						SELECT TOP 1 
							jP1.PostID
						FROM 
							tblForumThreads jT
							JOIN tblForumPosts jP1 ON jP1.ThreadID = jT.ThreadID
							JOIN tblForumForumPermissions jP ON jP.ForumID = jT.ForumID
						WHERE 
							jP.RoleID = 0 AND
							jP.[View] = 0x01 AND
							jT.ThreadDate > DateAdd(d, -3, GetDate()) AND
							jP1.IsApproved = 1 AND
							jT.ForumID > 4		-- excluding PM and hidden forums
						ORDER BY 
							( SELECT count(jTR.ThreadID) FROM tblForumThreadsRead jTR WHERE jP1.ThreadID = jTR.ThreadID ) DESC
					), 0)


	-- Most active user
	-- ***********************************************
	SET @MostActiveUserID = ISNULL(
					(
						SELECT TOP 1 
							jU.UserID

						FROM 
							tblForumUsers jU
							JOIN tblForumUserProfile jP ON jP.UserID = jU.UserID
						WHERE
							jP.EnableDisplayInMemberList = 1
						ORDER BY 
							jP.TotalPosts DESC
					), 0)
	-- Newest user
	-- ***********************************************
	SET @NewestUserID = ISNULL(
					(
						SELECT TOP 1 
							 jU.UserID
						FROM 
							tblForumUsers jU
							JOIN tblForumUserProfile jP ON jP.UserID = jU.UserID
						WHERE
							jP.EnableDisplayInMemberList = 1 AND
							jU.UserAccountStatus = 1
						ORDER BY 
							jU.DateCreated DESC
					), 0)


	-- Total Users
	-- ***********************************************
	SET @TotalUsers = ISNULL( 
					(
						SELECT 
							COUNT(UserID) 
						FROM 
							tblForumUserProfile 
						WHERE 
							EnableDisplayInMemberList = 1
					) ,0) 


	-- Total Posts
	-- ***********************************************
	SET @TotalPosts = 	ISNULL( 
					(
						SELECT TOP 1 
							TotalPosts 
						FROM 
							tblForumstatistics_Site
					), 0) +
				 ISNULL( 
					(
						SELECT 
							COUNT(PostID) 
						FROM 
							tblForumPosts 
						WHERE 
							ForumID > 4 AND 
						PostDate >= @LastDateTimeUpdate
					), 0)
	IF @TotalPosts = 0
	BEGIN
		-- there was no previous count.  this is mainly for clean installs
		SET @TotalPosts = (SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID > 4)
	END


	-- Total Topics
	-- ***********************************************
	SET @TotalTopics = 	ISNULL( 
					(
						SELECT TOP 1 
							TotalTopics 
						FROM 
							tblForumstatistics_Site
					), 0) + 
				ISNULL( 
					(
						SELECT 
							COUNT(ThreadID) 
						FROM 
							tblForumThreads 
						WHERE 
							ForumID > 4 AND 
							ThreadDate >= @LastDateTimeUpdate
					), 0)
	IF @TotalTopics = 0
	BEGIN
		-- there was no previous count.  this is mainly for clean installs
		SET @TotalTopics = (SELECT COUNT(ThreadID) FROM tblForumThreads WHERE ForumID > 4)
	END

	-- Total Posts in past 24 hours
	-- ***********************************************
	SET @NewPostsInPast24Hours = ISNULL( 
					(SELECT COUNT(PostID) FROM tblForumPosts WHERE ForumID > 4 And PostDate > DATEADD(dd,-1,getdate())
					), 0)

	-- Total Users in past 24 hours
	-- ***********************************************
	SET @NewUsersInPast24Hours = ISNULL(
						(SELECT COUNT(UserID) FROM tblForumUsers WHERE UserID > 0 And DateCreated > DATEADD(dd,-1,getdate())
					), 0)


	-- Total Topics in past 24 hours
	-- ***********************************************
	SET @NewThreadsInPast24Hours = ISNULL(
						(SELECT COUNT(ThreadID) FROM tblForumThreads WHERE ForumID > 4 AND PostDate > DATEADD(dd,-1,getdate())
					), 0)

	INSERT INTO tblForumstatistics_Site
	SELECT 
		DateCreated = GetDate(),
		TotalUsers = @TotalUsers,
		TotalPosts = @TotalPosts,
		TotalModerators = @TotalModerators,
		TotalModeratedPosts = @TotalModeratedPosts,
		TotalAnonymousUsers = @TotalAnonymousUsers,
		TotalTopics = @TotalTopics,
		DaysPosts = @NewPostsInPast24Hours, -- TODO remove
		DaysTopics = @NewThreadsInPast24Hours, -- TODO remove
		NewPostsInPast24Hours = @NewPostsInPast24Hours,
		NewThreadsInPast24Hours = @NewThreadsInPast24Hours,
		NewUsersInPast24Hours = @NewUsersInPast24Hours,
		MostViewsPostID = @MostViewsPostID,
		MostActivePostID = @MostActivePostID,
		MostActiveUserID = @MostActiveUserID,
		MostReadPostID = @MostReadPostID,
		NewestUserID = @NewestUserID


	SET NOCOUNT OFF


GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UpdateThread' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UpdateThread
GO
CREATE   procedure spForumsystem_UpdateThread (
	@ThreadID int,
	@ReplyPostID int
)
AS
BEGIN
SET NOCOUNT ON
DECLARE @ThreadDate datetime
DECLARE @StickyDate datetime
DECLARE @UserID int
DECLARE @PostAuthor nvarchar(64)
DECLARE @FirstPostID INT

IF @ReplyPostID = 0
	SELECT TOP 1 
		@ReplyPostID = PostID 
	FROM 
		tblForumPosts
	WHERE
		ThreadID = @ThreadID
		AND IsApproved = 1
	ORDER BY
		PostDate DESC


-- Get details about the reply
SELECT 
	@ThreadDate = PostDate, 
	@UserID = UserID, 
	@PostAuthor = PostAuthor 
FROM 
	tblForumPosts 
WHERE 
	PostID = @ReplyPostID

SELECT 
	@StickyDate = StickyDate 
FROM 
	tblForumThreads 
WHERE 
	ThreadID = @ThreadID

IF @StickyDate < @ThreadDate
	SET @StickyDate = @ThreadDate

-- do the mass updates.
UPDATE 
	tblForumThreads
SET
	TotalReplies = (SELECT Count(PostID) FROM tblForumPosts WHERE ThreadID = @ThreadID AND IsApproved = 1 AND PostLevel > 1),
	ThreadDate = @ThreadDate,
	StickyDate = @StickyDate,
	MostRecentPostAuthorID = @UserID,
	MostRecentPostAuthor = @PostAuthor,
	MostRecentPostID = @ReplyPostID
WHERE
	ThreadID = @ThreadID


-- find any lingering ParentIDs that don't match any posts in
-- our thread (from a merge or split action)
SET @FirstPostID = (	SELECT TOP 1 
				PostID 
			FROM 
				tblForumPosts
			WHERE
				ThreadID = @ThreadID
				AND IsApproved = 1
			ORDER BY
				PostDate ASC )

UPDATE
	tblForumPosts
SET
	ParentID = @FirstPostID
WHERE
	ParentID NOT IN (SELECT PostID FROM tblForumPosts WHERE ThreadID = @ThreadID)
	AND ThreadID = @ThreadID


-- fix the PostLevel and SortOrder ordering, by date
-- this could be done better, as it's on a MassScale now.
UPDATE
	tblForumPosts
SET
	PostLevel = 1,
	SortOrder = 1
WHERE
	ThreadID = @ThreadID
	AND PostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE ThreadID = @ThreadID ORDER BY PostID ASC)

UPDATE
	tblForumPosts
SET
	PostLevel = 2,
	SortOrder = SortOrder + 1
WHERE
	ThreadID = @ThreadID
	AND PostID > @ReplyPostID

-- update the EmoticonID, if it's the first post
IF @ReplyPostID = (SELECT TOP 1 PostID FROM tblForumPosts WHERE ThreadID = @ThreadID ORDER BY PostDate ASC)
	UPDATE
		tblForumThreads
	SET
		ThreadEmoticonID = (SELECT EmoticonID FROM tblForumPosts WHERE PostID = @ReplyPostID)
	WHERE
		ThreadID = @ThreadID	


SET NOCOUNT OFF
END



GO

IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UpdateUserPostCount' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UpdateUserPostCount
GO
create  procedure spForumsystem_UpdateUserPostCount
(
	@ForumID 	int,
	@UserID		int
)
AS
BEGIN

	-- Does the forum track post statistics?
	IF (SELECT EnablePostStatistics FROM tblForumForums WHERE ForumID = @ForumID) = 0
		RETURN

	UPDATE tblForumUserProfile SET TotalPosts = TotalPosts + 1 WHERE UserID = @UserID

END

GO




IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UpdateUserPostRank' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UpdateUserPostRank
GO
create      procedure spForumsystem_UpdateUserPostRank
AS
BEGIN
DECLARE @Usercount int
DECLARE @LoopCounter int

	SET NOCOUNT ON

	CREATE Table #PostRank (
	  Rank int IDENTITY (1, 1) NOT NULL,
	  UserID int
	)

	-- Select into temp table
	INSERT INTO #PostRank (UserID)
	SELECT TOP 500
		P.UserID
	FROM
		tblForumUserProfile P
	ORDER BY
		P.TotalPosts DESC

	-- How many users did we select?
	SELECT @Usercount = count(*) FROM #PostRank


	-- First clear all the users
	UPDATE 
		tblForumUserProfile
	SET 
		PostRank = 0x0
	WHERE 
		PostRank > 0

	-- Top 10
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x01
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank < 11)

	-- Top 25
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x02
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 10 AND Rank < 26)


	-- Top 50
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x04
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 25 AND Rank < 51)


	-- Top 75
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x08
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 50 AND Rank < 76)

	-- Top 100
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x10
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 75 AND Rank < 101)

	-- Top 150
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x20
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 100 AND Rank < 151)

	-- Top 200
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x40
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 150 AND Rank < 200)


	-- Top 500
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x80
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 200)

END



GO


IF EXISTS  (SELECT name 
			FROM sysobjects 
			WHERE name = 'spForumsystem_UserIsOnline' 
				AND type = 'P')
   DROP PROCEDURE spForumsystem_UserIsOnline
GO
CREATE    procedure spForumsystem_UserIsOnline (
	@UserID int,
	@LastAction nvarchar(1024)
)
AS
BEGIN
	-- First update the Users table
	UPDATE 
		tblForumUsers 
	SET 
		LastActivity = GetDate(),
		LastAction = @LastAction
	WHERE 
		UserID = @UserID

	-- Now update the lookup table
	IF EXISTS(SELECT UserID FROM tblForumUsersOnline WHERE UserID = @UserID)
		UPDATE tblForumUsersOnline SET LastActivity = GetDate(), LastAction = @LastAction WHERE UserID = @UserID
	ELSE
		INSERT INTO tblForumUsersOnline VALUES (@UserID, GetDate(), @LastAction)


END


GO


