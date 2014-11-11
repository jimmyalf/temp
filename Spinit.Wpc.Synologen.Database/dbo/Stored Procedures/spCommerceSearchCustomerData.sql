CREATE PROCEDURE spCommerceSearchCustomerData
					@type INT,
					@id INT,
					@name NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cFirstName,
					cLastName,
					cCompany,
					cStreet,
					cPostCode,
					cCity,
					cCountry,
					cEmail,
					cPhone,
					cDelStreet,
					cDelPostCode,
					cDelCity,
					cDelCountry,
					cCreatedDate
			FROM	tblCommerceCustomerData
			ORDER BY cLastName ASC, cFirstName ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cFirstName,
					cLastName,
					cCompany,
					cStreet,
					cPostCode,
					cCity,
					cCountry,
					cEmail,
					cPhone,
					cDelStreet,
					cDelPostCode,
					cDelCity,
					cDelCountry,
					cCreatedDate
			FROM	tblCommerceCustomerData
			WHERE	cFirstName LIKE '%' + @name + '%'
				OR	cLastName LIKE '%' + @name + '%'
			ORDER BY cLastName ASC, cFirstName ASC
		END

	IF @type = 2
		BEGIN
			SELECT	cId,
					cFirstName,
					cLastName,
					cCompany,
					cStreet,
					cPostCode,
					cCity,
					cCountry,
					cEmail,
					cPhone,
					cDelStreet,
					cDelPostCode,
					cDelCity,
					cDelCountry,
					cCreatedDate
			FROM	tblCommerceCustomerData
			WHERE	cId = @id
			ORDER BY cLastName ASC, cFirstName ASC
		END

	SET @status = @@ERROR
			
END
