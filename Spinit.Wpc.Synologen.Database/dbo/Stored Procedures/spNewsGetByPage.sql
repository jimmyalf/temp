
CREATE PROCEDURE spNewsGetByPage
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR(255),
					@CurrentPage INT,
					@PageSize INT
					
	AS
		BEGIN
			--Create temtable and fill with records matching the search criteria
			CREATE TABLE #TempTable
			(
				ID int IDENTITY PRIMARY KEY,
				cId INT,
				cNewsType SMALLINT,
				cHeading NTEXT, 
				cSummary NTEXT,
				cBody NTEXT,
				cFormatedBody NTEXT,
				cExternalLink NVARCHAR (255),
				cSpotImage INT,
				cSpotHeight INT,
				cSpotWidth INT,
				cSpotAlign INT,
				cStartDate DATETIME,
				cEndDate DATETIME,
				cCreatedBy NVARCHAR(100),
				cCreatedDate DATETIME,
				cEditedBy NVARCHAR(100),
				cEditedDate DATETIME,
				cApprovedBy NVARCHAR(100),
				cApprovedDate DATETIME,
				cLockedBy NVARCHAR(100),
				cLockedDate	DATETIME			
			)

			INSERT INTO #TempTable 
			(
				cId,
				cNewsType,
				cHeading, 
				cSummary,
				cBody,
				cFormatedBody,
				cExternalLink,
				cSpotImage,
				cSpotHeight,
				cSpotWidth,
				cSpotAlign,
				cStartDate,
				cEndDate,
				cCreatedBy,
				cCreatedDate,
				cEditedBy,
				cEditedDate,
				cApprovedBy,
				cApprovedDate,
				cLockedBy,
				cLockedDate
			)
			EXEC spNewsGetNewsDynamic @LocationId, @LanguageId, @CategoryId, @SearchString, @OrderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @CurrentPage * @PageSize
		SELECT @LastRec = (@FirstRec + @PageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
			cId,
			cNewsType,
			cHeading, 
			cSummary,
			cBody,
			cFormatedBody,
			cExternalLink,
			cSpotImage,
			cSpotHeight,
			cSpotWidth,
			cSpotAlign,
			cStartDate,
			cEndDate,
			cCreatedBy,
			cCreatedDate,
			cEditedBy,
			cEditedDate,
			cApprovedBy,
			cApprovedDate,
			cLockedBy,
			cLockedDate
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable 
