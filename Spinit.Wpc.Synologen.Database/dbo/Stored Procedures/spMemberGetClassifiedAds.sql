

CREATE PROCEDURE spMemberGetClassifiedAds
					@type INT,
					@adid INT,
					@memberid INT,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
					SELECT	*
					FROM	tblMemberClassifiedAds
					WHERE cId = @adid								
			END
			IF (@type = 1)
				BEGIN				
					SELECT	*
					FROM	tblMemberClassifiedAds
					WHERE cMemberId = @memberid	
					ORDER BY cStartDate DESC	
				END
			IF (@type = 2)
				BEGIN				
					SELECT	*
					FROM	tblMemberClassifiedAds
					WHERE cActive = 1 
					AND (cStartDate <= getDate() OR cStartDate IS NULL)
					AND (cEndDate >= getDate() OR cEndDate IS NULL) 
					ORDER BY cStartDate DESC	
				END
			IF (@type = 3)
				BEGIN				
					SELECT	*
					FROM	tblMemberClassifiedAds
					ORDER BY cStartDate DESC	
				END
			SELECT @status = @@ERROR
		END
