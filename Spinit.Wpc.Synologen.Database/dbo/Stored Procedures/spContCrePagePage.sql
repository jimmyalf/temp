
CREATE PROCEDURE spContCrePagePage
					@pgeId INT,
					@lnkId INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @dummy INT
			
			DECLARE get_pge CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContPage
				WHERE	cId = @pgeId
				
			DECLARE get_lnk CURSOR LOCAL FOR
				SELECT	1
				FROM	tblContPage
				WHERE	cId = @lnkId
				
			OPEN get_pge
			FETCH NEXT FROM get_pge INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_pge
					DEALLOCATE get_pge
					
					SELECT @status = 0
					RETURN
				END
				
			CLOSE get_pge
			DEALLOCATE get_pge
			
			OPEN get_lnk
			FETCH NEXT FROM get_lnk INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_lnk
					DEALLOCATE get_lnk
					
					SELECT @status = 0
					RETURN
				END
				
			CLOSE get_lnk
			DEALLOCATE get_lnk
			
			INSERT INTO tblContPagePage
				(cPgeId, cLnkId)
			VALUES
				(@pgeId, @lnkId)
				
			SELECT @status = @@ERROR
		END
