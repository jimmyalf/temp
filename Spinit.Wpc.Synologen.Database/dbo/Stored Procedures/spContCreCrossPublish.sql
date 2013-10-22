
CREATE PROCEDURE spContCreCrossPublish
					@treId INT,
					@treCrsId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@dummy INT
			
			DECLARE	chk_master CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContTree
				WHERE	cCrsTpeId = 3
					AND	cId = @treId
					
			DECLARE chk_slave CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContTree
				WHERE	(cCrsTpeId = 2
						OR	cCrsTpeId = 3)
					AND	cId = @treCrsId
					
			OPEN chk_master
			FETCH NEXT FROM chk_master INTO @dummy
			
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					CLOSE chk_master
					DEALLOCATE chk_master
					
					SELECT @status = -13
					RETURN
				END
				
			CLOSE chk_master
			DEALLOCATE chk_master
			
			OPEN chk_slave
			FETCH NEXT FROM chk_slave INTO @dummy
			
			IF (@@FETCH_STATUS <> -1)
				BEGIN
					CLOSE chk_slave
					DEALLOCATE chk_slave
					
					SELECT @status = -13
					RETURN
				END
				
			CLOSE chk_slave
			DEALLOCATE chk_slave
			
			INSERT INTO tblContCrossPublish
				(cTreId, cTreCrsId)
			VALUES
				(@treId, @treCrsId)
				
			UPDATE	tblContTree
			SET		cCrsTpeId = 2
			WHERE	cId = @treId
			
			UPDATE	tblContTree
			SET		cCrsTpeId = 3
			WHERE	cId = @treCrsId
			
			SELECT @status = @@ERROR
		END
