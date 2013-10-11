CREATE FUNCTION sfSynologenGetShopName (@memberId INT)
	RETURNS NVARCHAR (255)
AS
BEGIN
	--DECLARE @length INT
	--SET @length = 50
	DECLARE @retString NVARCHAR(255)
	SELECT @retString = cShopName FROM tblSynologenShop
	INNER JOIN tblSynologenShopMemberConnection ON tblSynologenShopMemberConnection.cSynologenShopId = tblSynologenShop.cId
	WHERE tblSynologenShopMemberConnection.cMemberId = @memberId
	RETURN @retString 
END
