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


