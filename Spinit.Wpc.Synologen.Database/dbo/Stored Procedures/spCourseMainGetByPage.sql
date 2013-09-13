create PROCEDURE spCourseMainGetByPage
					@categoryId INT,
					@searchString NVARCHAR(255),
					@orderBy NVARCHAR(255),
					@currentPage INT,
					@pageSize INT
					
	AS
		BEGIN
			--Create temtable and fill with records matching the search criteria
			CREATE TABLE #TempTable
			(
				ID INT IDENTITY PRIMARY KEY,
				cId INT,
				cName NVARCHAR(250),
				cDescription NTEXT,
				cDetail NTEXT,
				cCourses INT
			)

			INSERT INTO #TempTable 
			(
				cId,
				cName,
				cDescription,
				cDetail,
				cCourses
			)
			EXEC spCourseMainGetDynamic @categoryId, @searchString, @orderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @currentPage * @pageSize
		SELECT @LastRec = (@FirstRec + @pageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
			cId,
			cName,
			cDescription,
			cDetail,
			cCourses
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
