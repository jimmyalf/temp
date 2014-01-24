CREATE PROCEDURE spCommerceDeleteProductCategoryRecursive
					@id INT,
					@status INT OUTPUT
	AS	
		BEGIN TRANSACTION
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

			IF (@@ERROR <> 0)
				 BEGIN
				 	ROLLBACK TRANSACTION
			
		    			SELECT @status = @@ERROR
		   		 	RETURN
	 			END

			DELETE FROM tblCommerceLocationConnection
			WHERE cPrdCatId = @id	

			IF (@@ERROR <> 0)
				 BEGIN
				 	ROLLBACK TRANSACTION
			
		    			SELECT @status = @@ERROR
		   		 	RETURN
	 			END
	
			DELETE FROM tblCommerceLanguageConnection
			WHERE cPrdCatId = @id		

			IF (@@ERROR <> 0)
				 BEGIN
				 	ROLLBACK TRANSACTION
			
		    			SELECT @status = @@ERROR
		   		 	RETURN
	 			END
			
			DELETE FROM tblCommerceProductProductCategory
			WHERE cPrdCatId = @id

			IF (@@ERROR <> 0)
				 BEGIN
				 	ROLLBACK TRANSACTION
			
		    			SELECT @status = @@ERROR
		   		 	RETURN
	 			END
			
			DELETE FROM tblCommerceProductCategory
			WHERE cId = @id	

			IF (@@ERROR <> 0)
				 BEGIN
				 	ROLLBACK TRANSACTION
			
		    			SELECT @status = @@ERROR
		   		 	RETURN
	 			END

		COMMIT TRANSACTION
