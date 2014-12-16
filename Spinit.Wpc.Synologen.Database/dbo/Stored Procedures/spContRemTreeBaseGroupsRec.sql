
CREATE PROCEDURE spContRemTreeBaseGroupsRec
					@parent INT,
					@bseGrpId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@treId INT
					
			DECLARE get_childs CURSOR LOCAL FOR
				SELECT	cId
				FROM	tblContTree
				WHERE	cParent = @parent
				
			OPEN get_childs
			FETCH NEXT FROM get_childs INTO @treId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN

					DELETE FROM	tblContTreeBaseGroups
					WHERE		cTreId = @treId
						AND		cBseGrpId = @bseGrpId
						
					IF (@@ERROR <> 0)
						BEGIN
							CLOSE get_childs
							DEALLOCATE get_childs
							RETURN
						END

					EXECUTE spContRemTreeBaseGroupsRec	@treId, 
														@bseGrpId, 
														@status OUTPUT
														
					IF (@status <> 0)
						BEGIN
							CLOSE get_childs
							DEALLOCATE get_childs
							RETURN
						END
					
					FETCH NEXT FROM get_childs INTO @treId
				END
				
			CLOSE get_childs
			DEALLOCATE get_childs
			
			SELECT @status = @@ERROR
		END
