
CREATE PROCEDURE spCampaignGetByPage
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
				cId int not null,
				cName nvarchar(50),
				cHeading nvarchar(255),
				cDescription nvarchar(255),
				cCampaignSpot int,
				cSpotHeight int,
				cSpotWidth int,
				cCampaignType int,
				cThumbsRows int,
				cThumbsColumns int,
				cThumbsHeight int,
				cThumbsWidth int,
				cListRowsPerPage int,
				cActive bit,
				cStartDate datetime NULL ,
				cEndDate datetime NULL ,
				cCreatedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cCreatedDate datetime NULL ,
				cEditedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cEditedDate datetime NULL ,
				cApprovedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cApprovedDate datetime NULL ,
				cLockedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cLockedDate datetime NULL  		
			)

			INSERT INTO #TempTable 
			(
				cId,
				cName,
				cHeading,
				cDescription,
				cCampaignSpot,
				cSpotHeight,
				cSpotWidth,
				cCampaignType,
				cThumbsRows,
				cThumbsColumns,
				cThumbsHeight,
				cThumbsWidth,
				cListRowsPerPage,
				cActive,
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
			EXEC spCampaignGetCampaignsDynamic @LocationId, @LanguageId, @CategoryId, @SearchString, @OrderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @CurrentPage * @PageSize
		SELECT @LastRec = (@FirstRec + @PageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
			cId,
				cName,
				cHeading,
				cDescription,
				cCampaignSpot,
				cSpotHeight,
				cSpotWidth,
				cCampaignType,
				cThumbsRows,
				cThumbsColumns,
				cThumbsHeight,
				cThumbsWidth,
				cListRowsPerPage,
				cActive,
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
