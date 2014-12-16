CREATE PROCEDURE spCourseAddDeleteApplication
		@action INT=0,
		@id int OUTPUT,
		@courseId INT=0,
		@firstName NVARCHAR(30)='',
		@lastName NVARCHAR(30)='',
		@company NVARCHAR(50)='',
		@orgNr NVARCHAR(50)='',
		@address NVARCHAR(50)='',
		@postCode NVARCHAR(10)='',
		@city NVARCHAR(50)='',
		@country NVARCHAR(50)='',
		@phone NVARCHAR(15)='',
		@mobile NVARCHAR(15)='',
		@email NVARCHAR(30)='',
		@applicationText NVARCHAR(2000)='',
		@nrOfParticipants INT=0,
		@createdDate SMALLDATETIME='',
		@status int OUTPUT

AS

BEGIN TRANSACTION APPLICATION_ADD_DELETE
IF (@action = 0) -- Create
BEGIN
	INSERT INTO tblCourseApplication
		(cCourseId,cFirstName,cLastName,cCompany,cOrgNr,cAddress,cPostCode,cCity,
		 cCountry, cPhone,cMobile,cEmail,cApplicationText,cNrOfParticipants,cCreatedDate) 
	VALUES 
		(@courseId,@firstName,@lastName,@company,@orgNr,@address,@postCode,@city,
		 @country, @phone,@mobile,@email,@applicationText,@nrOfParticipants,GETDATE()) 
	SELECT @id = @@IDENTITY
END
IF (@action = 2) -- Delete
BEGIN
	DELETE FROM tblCourseApplication
	WHERE cId = @id
END

SELECT @status = @@ERROR
IF (@@ERROR <> 0)
BEGIN
	SELECT @id = 0
	ROLLBACK TRANSACTION APPLICATION_ADD_DELETE
	RETURN
END
ELSE
BEGIN
	COMMIT TRANSACTION APPLICATION_ADD_DELETE
END
