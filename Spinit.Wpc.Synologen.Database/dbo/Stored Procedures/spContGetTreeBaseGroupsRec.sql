
CREATE PROCEDURE spContGetTreeBaseGroupsRec
					@treId INT,
					@status INT OUTPUT
	AS
		BEGIN
			SELECT	cTreId,
					cBseGrpId
			FROM	sfContGetTreeBaseGroupsRec (@treId)
		
			SELECT @status = @@ERROR
		END
