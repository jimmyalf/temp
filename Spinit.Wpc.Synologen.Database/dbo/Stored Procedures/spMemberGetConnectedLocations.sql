

CREATE PROCEDURE spMemberGetConnectedLocations
					@memberid INT,
					@status INT OUTPUT
	AS
		BEGIN
				BEGIN
					SELECT	*
					FROM	tblBaseLocations tbl, tblMemberLocationConnection tmlc
					WHERE tmlc.cMemberId = @memberid
					AND tmlc.cLocationId = tbl.cId
				END
			SELECT @status = @@ERROR
		END
