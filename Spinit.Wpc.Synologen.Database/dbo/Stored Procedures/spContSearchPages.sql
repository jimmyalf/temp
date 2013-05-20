create PROCEDURE spContSearchPages
					@files NVARCHAR (4000),
					@where NVARCHAR (4000)
AS
BEGIN
	DECLARE	@execute NVARCHAR (4000),
			@count1 INT,
			@count2 INT,
			@loop BIT,
			@tmp NVARCHAR (100)
	
	SET @loop = 1
	SET @count1 = 1
	
	SET @count2 = CHARINDEX (',', @files, @count1) 
	
	SET @execute = ''
			
	IF (@count2 <> 0)
		BEGIN
			SET @loop = 1
			WHILE (@loop = 1)
				BEGIN
					SET @tmp = SUBSTRING (@files, @count1, @count2 - @count1)
									
					SET @count1 = @count2 + 1
					SET @count2 = CHARINDEX (',', @files, @count1) 
					IF (@count2 = 0)
						BEGIN
							SET @execute = @execute 
											+ 'SELECT * FROM dbo.' 
											+ @tmp
											+ '()'
							IF (@where IS NOT NULL)
								BEGIN
									SET @execute = @execute 
													+ ' WHERE ' 
													+ @where
								END
							
							SET @tmp = SUBSTRING (@files, @count1, LEN (@files))
							
							IF (LEN (@tmp) != 0)
								BEGIN
									SET @execute = @execute 
													+ 'SELECT * FROM dbo.' 
													+ @tmp
													+ '()'
									IF (@where IS NOT NULL)
										BEGIN
											SET @execute = @execute 
															+ ' WHERE ' 
															+ @where
										END
								END
								
							SET @loop = 0
						END
					ELSE
						BEGIN	
							SET @execute = @execute 
											+ 'SELECT * FROM dbo.' 
											+ @tmp
											+ '()'
							IF (@where IS NOT NULL)
								BEGIN
									SET @execute = @execute 
													+ ' WHERE ' 
													+ @where
								END
											
							SET @execute  = @execute + ' UNION '
						END
				END
		END
			
	EXECUTE (@execute)
END
