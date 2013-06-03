CREATE PROCEDURE spBaseLogout
					 @session NVARCHAR(256),
					 @status int OUTPUT
AS
BEGIN
	SELECT @status = 0

	UPDATE	tblBaseLoginHistory			
	SET		cLogoutTime = GETDATE ()
	WHERE	cSession = @session
	
	SET @status = @@ERROR
END
