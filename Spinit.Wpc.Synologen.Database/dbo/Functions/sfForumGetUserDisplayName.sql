CREATE   FUNCTION [dbo].[sfForumGetUserDisplayName] (@UserID int)
RETURNS NVARCHAR(100)
AS BEGIN
	DECLARE @userName NVARCHAR(64)
	DECLARE @returnString NVARCHAR(100)
	DECLARE @baseUserId INT

	SELECT @userName = UserName 
		FROM tblForumUsers 	WHERE UserID = @UserID

	SELECT @returnString = 
		'<span class="user-name"><span>' + 
		tblBaseUsers.cFirstName + ' ' + 
		tblBaseUsers.cLastName + 
		',</span><br/><strong>' +
		dbo.sfSynologenGetShopName(tblMemberUserConnection.cMemberId) + 
		'<strong></span>'
		FROM tblBaseUsers
		INNER JOIN tblMemberUserConnection ON tblMemberUserConnection.cUserId = tblBaseUsers.cId
		WHERE cUserName = @userName
	IF (@returnString IS NULL OR LEN(@returnString)=0) BEGIN
		SELECT @returnString = 
			'<span class="user-name"><span>' + 
			tblBaseUsers.cFirstName + ' ' + 
			tblBaseUsers.cLastName + '</span></span>'
			FROM tblBaseUsers
			WHERE cUserName = @userName
	END	
	
	IF (@returnString IS NULL) BEGIN
		SET @returnString = ''
	END
	
	RETURN @returnString
END
