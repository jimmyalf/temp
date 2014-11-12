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



