create FUNCTION sfSynologenGetFullUserName (@memberId INT)
	RETURNS NVARCHAR (255)
AS
BEGIN
	--DECLARE @length INT
	--SET @length = 50
	DECLARE @retString NVARCHAR(255)
	SELECT @retString = cFirstName + ' ' + cLastName FROM tblBaseUsers
	INNER JOIN tblMemberUserConnection ON tblMemberUserConnection.cUserId = tblBaseUsers.cId
	WHERE tblMemberUserConnection.cMemberId = @memberId
	RETURN @retString 
END
