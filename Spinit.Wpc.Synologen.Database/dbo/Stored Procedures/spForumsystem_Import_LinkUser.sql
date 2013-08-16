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



