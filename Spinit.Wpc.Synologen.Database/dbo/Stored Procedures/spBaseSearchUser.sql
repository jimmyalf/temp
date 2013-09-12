create PROCEDURE spBaseSearchUser
					@type INT,
					@id INT,
					@username NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cUserName,
					cPassword,
					cFirstName,
					cLastName,
					cEmail,
					cDefaultLocation,
					cActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblBaseUsers
			ORDER BY cUserName ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	cId,
					cUserName,
					cPassword,
					cFirstName,
					cLastName,
					cEmail,
					cDefaultLocation,
					cActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblBaseUsers
			WHERE cUserName = @username
			ORDER BY cUserName ASC
		END
							
	IF @type = 2
		BEGIN
			SELECT	cId,
					cUserName,
					cPassword,
					cFirstName,
					cLastName,
					cEmail,
					cDefaultLocation,
					cActive,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblBaseUsers
			WHERE cId = @id
			ORDER BY cUserName ASC
		END

	SET @status = @@ERROR
			
END
