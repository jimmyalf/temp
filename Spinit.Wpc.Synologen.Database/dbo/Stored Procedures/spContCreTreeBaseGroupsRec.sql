
CREATE PROCEDURE spContCreTreeBaseGroupsRec
					@parent INT,
					@bseGrpId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@treId INT,
					@treTpeId INT,
					@dummy INT
					
			DECLARE get_childs CURSOR LOCAL FOR
				SELECT	cId,
						cTreTpeId
				FROM	tblContTree
				WHERE	cParent = @parent
				
			OPEN get_childs
			FETCH NEXT FROM get_childs INTO @treId,
											@treTpeId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
					IF ((@treTpeId = 1) OR (@treTpeId = 2) OR (@treTpeId = 3)
						OR (@treTpeId = 6))
						BEGIN
							DECLARE chk_exist CURSOR LOCAL FOR
								SELECT	1
								FROM	tblContTreeBaseGroups
								WHERE	cTreId = @treId
									AND	cBseGrpId = @bseGrpId
								
							OPEN chk_exist
							FETCH NEXT FROM chk_exist INTO @dummy	
					
							IF (@@FETCH_STATUS = -1)
								BEGIN
									INSERT INTO tblContTreeBaseGroups
										(cTreId, cBseGrpId)
									VALUES
										(@treId, @bseGrpId)
								END
								
							CLOSE chk_exist
							DEALLOCATE chk_exist
						END
						
					IF (@@ERROR <> 0)
						BEGIN
							CLOSE get_childs
							DEALLOCATE get_childs
							RETURN
						END

					EXECUTE spContCreTreeBaseGroupsRec	@treId, 
														@bseGrpId, 
														@status OUTPUT
														
					IF (@status <> 0)
						BEGIN
							CLOSE get_childs
							DEALLOCATE get_childs
							RETURN
						END
					
					FETCH NEXT FROM get_childs INTO @treId,
													@treTpeId
				END
				
			CLOSE get_childs
			DEALLOCATE get_childs
			
			SELECT @status = @@ERROR
		END
