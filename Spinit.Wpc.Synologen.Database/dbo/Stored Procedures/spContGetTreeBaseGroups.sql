
CREATE PROCEDURE spContGetTreeBaseGroups
					@treId INT,
					@bseGrpId INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@bseGrpId IS NULL)
				BEGIN
					SELECT	cTreId,
							cBseGrpId
					FROM	tblContTreeBaseGroups
					WHERE	cTreId = @treId
				END
			ELSE
				BEGIN
					SELECT	cTreId,
							cBseGrpId
					FROM	tblContTreeBaseGroups
					WHERE	cTreId = @treId
						AND	cBseGrpId = @bseGrpId
				END
			
			SELECT @status = @@ERROR
		END
