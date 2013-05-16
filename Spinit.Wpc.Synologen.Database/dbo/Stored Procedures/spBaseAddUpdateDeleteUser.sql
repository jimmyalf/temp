create PROCEDURE spBaseAddUpdateDeleteUser
					@action INT,
					@id INT OUTPUT,
					@userName NVARCHAR (100),
					@password NVARCHAR (100),
					@firstName NVARCHAR (100),
					@lastName NVARCHAR (100),
					@email NVARCHAR (512),
					@defaultLocation INT,
					@active BIT,
					@byUserName NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
							
	IF @action = 0
	BEGIN			
		SELECT	1
		FROM	tblBaseUsers
		WHERE	cUserName = @userName
		
		IF @@ROWCOUNT > 0
		BEGIN
			SET @status = -2
			RETURN
		END
		INSERT INTO tblBaseUsers
			(cUserName, cPassword, cFirstName, cLastName, cEmail, cDefaultLocation, cActive,
			 cCreatedBy, cCreatedDate)
		VALUES
			(@username, @password, @firstName, @lastName, @email, @defaultLocation, @active,
			 @byUserName, GETDATE ())

		SET @id = @@IDENTITY

	END
	ELSE IF @action = 1
	BEGIN
		SELECT	1
		FROM	tblBaseUsers
		WHERE	cId = @id
				
		IF @@ROWCOUNT = 0
			BEGIN
				SET @status = -3
				RETURN
			END
		
		IF @userName IS NOT NULL
		BEGIN
			SELECT	1
			FROM	tblBaseUsers
			WHERE	cUserName = @userName
				AND	cId != @id
			
			IF @@ROWCOUNT > 0
			BEGIN
				SET @status = -2
				RETURN
			END			
		END
		
		IF @userName IS NULL
		BEGIN
			SELECT @userName = cUserName
			FROM tblBaseUsers
			WHERE cId = @id
		END
		
		IF @password IS NULL
		BEGIN
			SELECT @password = cPassword
			FROM tblBaseUsers
			WHERE cId = @id
		END
		
		UPDATE	tblBaseUsers
		SET		cUserName = @username,
				cPassword = @password,
				cFirstName = @firstName,
				cLastName = @lastName,
				cEmail = @email,
				cDefaultLocation = @defaultLocation,
				cActive = @active,
				cChangedBy = @byUserName,
				cChangedDate = getDate()
		WHERE	cId = @id
	END
	ELSE IF @action = 2
	BEGIN
		SELECT	1
		FROM	tblBaseUsers
		WHERE	cId = @id
		
		IF @@ROWCOUNT = 0
			BEGIN
				SET @status = -3
				RETURN
			END
			
		DELETE FROM tblBaseUsers
		WHERE		cId = @id
	END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
