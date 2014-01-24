CREATE PROCEDURE ___spCommerceDeleteProductCategoryRecursive
					@id INT,
					@status INT OUTPUT
	AS	
		BEGIN
			DECLARE @childId INT,
				    @statusRet INT

			-- Examine if there are any childs to remove
			DECLARE get_child CURSOR LOCAL FOR
				SELECT	cId
				FROM	tblCommerceProductCategory
				WHERE	cParent = @id
			
			SELECT @status = 0
			
			OPEN get_child
			FETCH NEXT FROM get_child INTO @childId
			
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
				
					-- Recursive call
					EXECUTE spCommerceDeleteProductCategoryRecursive @childId, @statusRet OUTPUT
				
					IF (@statusRet <> 0)
						BEGIN
							SELECT @status = @statusRet
							CLOSE get_child
							DEALLOCATE get_child							
							RETURN
						END				
						
					FETCH NEXT FROM get_child INTO @childId
				END
		
			CLOSE get_child
			DEALLOCATE get_child
				
			DELETE FROM tblCommerceProductCategoryAttribute
			WHERE cPrdCatId = @id

			DELETE FROM tblCommerceLocationConnection
			WHERE cPrdCatId = @id	
	
			DELETE FROM tblCommerceLanguageConnection
			WHERE cPrdCatId = @id		
			
			DELETE FROM tblCommerceProductProductCategory
			WHERE cPrdCatId = @id
			
			DELETE FROM tblCommerceProductCategory
			WHERE cId = @id	

			SELECT @status = @@ERROR
		END
