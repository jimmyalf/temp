create PROCEDURE spNewsIntranetGetByPage
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@OnlyPublished BIT,
					@GroupId INT,
					@PublicUserId INT,
					@AuthorId INT,
					@OnlyUnread BIT,
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
				cLockedDate	DATETIME,
				Author NVARCHAR(100),
				AuthorUserId INT,
				IsRead BIT	
				
						
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
				cLockedDate,
				Author,
				AuthorUserId,
				IsRead
			)
			EXEC spNewsIntranetGetNewsDynamic @LocationId, @LanguageId, @CategoryId, 
												@OnlyPublished, @GroupId, @PublicUserId,
												@AuthorId, @OnlyUnread
			
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
			cLockedDate,
			Author,
			AuthorUserId,
			IsRead
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
