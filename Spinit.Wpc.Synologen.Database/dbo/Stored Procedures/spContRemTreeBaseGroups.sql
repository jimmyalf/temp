
CREATE PROCEDURE spContRemTreeBaseGroups
					@treId INT,
					@bseGrpId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DELETE FROM	tblContTreeBaseGroups
			WHERE		cTreId = @treId
				AND		cBseGrpId = @bseGrpId
				
			EXECUTE spContRemTreeBaseGroupsRec @treId, 
											   @bseGrpId, 
											   @status OUTPUT				
			
			SELECT @status = @@ERROR
		END	
