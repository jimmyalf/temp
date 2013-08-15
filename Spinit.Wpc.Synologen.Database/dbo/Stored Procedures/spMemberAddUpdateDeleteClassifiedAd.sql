CREATE PROCEDURE spMemberAddUpdateDeleteClassifiedAd
					@type INT,
					@memberId INT = 0,
					@name NVARCHAR(100) = '',
					@email NVARCHAR(100) = '',
					@telephone NVARCHAR(50) = '',
					@heading NVARCHAR(100) = '',
					@description NVARCHAR(512) = '',
					@startdate DATETIME = '',
					@enddate DATETIME = '',
					@active bit = 0,
					@status INT OUTPUT,
					@id INT OUTPUT
					
	AS
	BEGIN	
		DECLARE @stringId INT	
		BEGIN TRANSACTION ADD_UPDATE_DELETE_CLASSIFIEDAD
		IF (@type = 0)
		BEGIN
			INSERT INTO tblMemberClassifiedAds
				(cMemberId, cName, cEmail, cTelephone, cHeading, cDescription, cStartDate, cEndDate, cActive)
			VALUES
				(@memberId, @name, @email, @telephone, @heading, @description, @startdate, @enddate, @active) 
			SELECT @id = @@IDENTITY
		END
		IF (@type = 1)
		BEGIN
			
			UPDATE tblMemberClassifiedAds
			SET 
			cMemberId = @memberid,
			cName = @name,
			cEmail = @email,
			cTelephone = @telephone,
			cHeading = @heading,
			cDescription = @description,
			cStartDate = @startdate,
			cEndDate = @enddate,
			cActive = @active
			WHERE cId = @id
			
		END
		IF (@type = 2)
		BEGIN			
			DELETE FROM tblMemberClassifiedAds
			WHERE cId = @id
		END
		
		SELECT @status = @@ERROR
		IF (@@ERROR <> 0)
		BEGIN
			SELECT @id = 0
			ROLLBACK TRANSACTION ADD_UPDATE_DELETE_CLASSIFIEDAD
			RETURN
		END
									
		IF (@@ERROR = 0)
		BEGIN
			COMMIT TRANSACTION ADD_UPDATE_DELETE_CLASSIFIEDAD
		END
	END
