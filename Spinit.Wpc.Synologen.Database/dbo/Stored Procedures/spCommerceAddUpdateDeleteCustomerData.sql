CREATE PROCEDURE spCommerceAddUpdateDeleteCustomerData
@action INT, @id INT OUTPUT, @firstName NVARCHAR (100), @lastName NVARCHAR (100), @company NVARCHAR (256), @street NVARCHAR (256), @postCode NVARCHAR (20), @city NVARCHAR (100), @country NVARCHAR (100), @email NVARCHAR (512), @phone NVARCHAR (20), @delStreet NVARCHAR (256), @delPostCode NVARCHAR (20), @delCity NVARCHAR (100), @delCountry NVARCHAR (100), @status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN						
			INSERT INTO tblCommerceCustomerData
				(cFirstName, cLastName, cCompany, cStreet, cPostCode, cCity,
				 cCountry, cEmail, cPhone, cDelStreet, cDelPostCode, cDelCity,
				 cDelCountry, cCreatedDate)
			VALUES
				(@firstName, @lastName, @company, @street, @postCode, @city, 
				 @country, @email, @phone, @delStreet, @delPostCode, @delCity,
				 @delCountry, GETDATE ())
				
			SET @id = @@IDENTITY
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceCustomerData
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
							
			UPDATE	tblCommerceCustomerData
			SET		cFirstName = @firstName,
					cLastName = @lastName,
					cCompany = @company,
					cStreet = @street,
					cPostCode = @postCode,
					cCity = @city,
					cCountry = @country,
					cEmail = @email,
					cPhone = @phone,
					cDelStreet = @delStreet,
					cDelPostCode = @delPostCode,
					cDelCity = @delCity,
					cDelCountry = @delCountry
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceCustomerData
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceCustomerData
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
