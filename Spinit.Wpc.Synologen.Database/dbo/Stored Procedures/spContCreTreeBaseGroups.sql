
CREATE PROCEDURE spContCreTreeBaseGroups
					@treId INT,
					@bseGrpId INT,
					@status INT OUTPUT
	AS
		BEGIN
			INSERT INTO tblContTreeBaseGroups
				(cTreId, cBseGrpId)
			VALUES
				(@treId, @bseGrpId)
				
			SELECT @status = @@ERROR
			
			EXECUTE spContCreTreeBaseGroupsRec @treId, 
											   @bseGrpId, 
											   @status OUTPUT
		END
