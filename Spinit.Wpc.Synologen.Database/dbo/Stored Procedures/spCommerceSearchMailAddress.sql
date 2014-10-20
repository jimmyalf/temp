CREATE PROCEDURE spCommerceSearchMailAddress
					@type INT,
					@id INT,
					@mlId INT,
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cMlId,
					cEmail,
					cIsActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceMailAddress
			ORDER BY cEmail ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cMlId,
					cEmail,
					cIsActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceMailAddress
			WHERE	cMlId = @mlId
			ORDER BY cEmail ASC
		END
							
	IF @type = 2
		BEGIN
			SELECT	cId,
					cMlId,
					cEmail,
					cIsActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceMailAddress
			WHERE	cMlId = @mlId
				AND	cIsActive = 1
			ORDER BY cEmail ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cId,
					cMlId,
					cEmail,
					cIsActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceMailAddress
			WHERE	cId = @id
			ORDER BY cEmail ASC
		END

	SET @status = @@ERROR
			
END
