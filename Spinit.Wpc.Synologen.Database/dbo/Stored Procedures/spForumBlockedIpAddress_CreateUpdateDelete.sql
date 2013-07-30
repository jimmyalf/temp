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


