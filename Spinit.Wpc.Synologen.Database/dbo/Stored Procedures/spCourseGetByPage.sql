create PROCEDURE spCourseGetByPage
					@mainId INT,
					@languageId INT,
					@locationId INT,
					@cityId INT,
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
				cHeading NTEXT,
				cContactName NVARCHAR(255),
				cLastApplicationDate SMALLDATETIME,
				cCourseStartDate SMALLDATETIME, 
				cCourseEndDate SMALLDATETIME, 
				cPublishStartDate SMALLDATETIME, 
				cPublishEndDate SMALLDATETIME, 
				cCityName NVARCHAR(255),
				cApplications INT,
				cNoOfParticipants INT
			)

			INSERT INTO #TempTable 
			(
				cId,
				cHeading,
				cContactName,
				cLastApplicationDate,
				cCourseStartDate, 
				cCourseEndDate, 
				cPublishStartDate, 
				cPublishEndDate, 
				cCityName,
				cApplications,
				cNoOfParticipants
			)
			EXEC spCourseGetDynamic @mainId, @languageId, @locationId, @cityId, @searchString, @orderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @currentPage * @pageSize
		SELECT @LastRec = (@FirstRec + @pageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
				cId,
				cHeading,
				cContactName,
				cLastApplicationDate,
				cCourseStartDate, 
				cCourseEndDate, 
				cPublishStartDate, 
				cPublishEndDate, 
				cCityName,
				cApplications,
				cNoOfParticipants
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
