﻿


CREATE PROCEDURE spBaseDisconnectLocationAndComponent
					@locationid INT,
					@componentid INT,
					@status INT OUTPUT
	AS

			BEGIN	
			Select * From tblBaseLocationsComponents	
			Where cLocationId = @locationid and cComponentId = @componentid
			
			if (@@ROWCOUNT > 0) 
				begin
					DELETE FROM tblBaseLocationsComponents
					WHERE cLocationId = @locationid
					AND cComponentId = @componentid
				end
			END	

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END


