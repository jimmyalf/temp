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



