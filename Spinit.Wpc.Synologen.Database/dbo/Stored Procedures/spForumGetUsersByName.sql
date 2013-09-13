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


