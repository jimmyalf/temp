


CREATE PROCEDURE spBaseRemoveLocation
					@locationid INT,
					@status INT OUTPUT
	AS
		Delete From tblBaseLocationsLanguages
		WHERE cLocationId = @locationid
		
		Delete From tblBaseLocationsComponents
		WHERE cLocationId = @locationid
	
		DELETE FROM tblBaseLocations
		WHERE cId = @locationid
			
		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END


