CREATE PROCEDURE spCommerceAddUpdateDeleteMailAddress
					@action INT,
					@id INT OUTPUT,
					@mlId INT,
					@email NVARCHAR (512),
					@isActive BIT,
					@userName NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN	
			SELECT	@dummy = 1
			FROM	tblQuickMailMail
			WHERE	cId = @mlId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -21
					RETURN
				END
			
			INSERT INTO tblCommerceMailAddress
				(cMlId, cEmail, cIsActive, cCreatedBy, cCreatedDate)
			VALUES
				(@mlId, @email, @isActive, @userName, GETDATE ())
				
			SET @id = @@IDENTITY
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceMailAddress
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			SELECT	@dummy = 1
			FROM	tblQuickMailMail
			WHERE	cId = @mlId
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -21
					RETURN
				END

			UPDATE	tblCommerceMailAddress
			SET		cMlId = @mlId,
					cEmail = @email,
					cIsActive = @isActive,
					cChangedBy = @userName,
					cChangedDate = GETDATE ()
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceMailAddress
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceMailAddress
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
