create PROCEDURE spBaseMatchUserAndPassword
					@userName NVARCHAR (100),
					@password NVARCHAR (100),
					@id INT OUTPUT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE	@usrPwd NVARCHAR (50)			
	
	DECLARE get_usr CURSOR LOCAL FOR
		SELECT	cId,
				cPassword
		FROM	tblBaseUsers
		WHERE	cUserName = @userName
		
	OPEN get_usr
	FETCH NEXT FROM get_usr INTO	@id,
									@usrPwd
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_usr
			DEALLOCATE get_usr
					
			SET @status = -1
			RETURN
		END

	CLOSE get_usr
	DEALLOCATE get_usr
	
	IF (((@usrPwd IS NULL) AND (@password IS NOT NULL))
		OR ((@usrPwd IS NOT NULL) AND (@password IS NULL))
		OR ((@usrPwd IS NOT NULL) AND (@password IS NOT NULL)
			AND (@usrPwd != @password)))
		BEGIN
			SET @status = -1
			RETURN
		END
			
	SET @status = 0
END
