CREATE PROCEDURE spSynologenGetMembersByPage
					@type INT,
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@shopId INT,
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
				cMemberId int not null,
				cDescription nvarchar(255),
				cAddress nvarchar(255),
				cZipcode nvarchar(50),
				cCity nvarchar(50),
				cPhone nvarchar(50),
				cFax nvarchar(50),
				cMobile nvarchar(50),
				cEmail nvarchar(50),
				cWww nvarchar(255),
				cVoip nvarchar(50),
				cSkype nvarchar(50),
				cCordless nvarchar(50),
				cBody ntext,
				cOther1 nvarchar(255),
				cOther2 nvarchar(255),
				cOther3 nvarchar(255),
				cContactFirst nvarchar(255),
				cContactLast nvarchar(255),
				cProfilePictureId int,
				cDefaultDirectoryId int,
				cOrgName nvarchar(255) COLLATE FINNISH_SWEDISH_CI_AS,
				cActive int,
				cLanguageId int,
				cCreatedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cCreatedDate datetime NULL ,
				cEditedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cEditedDate datetime NULL ,
				cApprovedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cApprovedDate datetime NULL ,
				cLockedBy nvarchar (100) COLLATE FINNISH_SWEDISH_CI_AS NULL ,
				cLockedDate datetime NULL,
				cFullUserName nvarchar(255),
				cMemberCategoryName NVARCHAR(50)
			)

			INSERT INTO #TempTable 
			(
				cId,
				cMemberId,
				cDescription,
				cAddress,
				cZipcode,
				cCity,
				cPhone,
				cFax,
				cMobile,
				cEmail,
				cWww,
				cVoip,
				cSkype,
				cCordless,
				cBody,
				cOther1,
				cOther2,
				cOther3,
				cContactFirst,
				cContactLast,
				cProfilePictureId,
				cDefaultDirectoryId,
				cOrgName,
				cActive,
				cLanguageId,
				cCreatedBy,
				cCreatedDate,
				cEditedBy,
				cEditedDate,
				cApprovedBy,
				cApprovedDate,
				cLockedBy,
				cLockedDate,
				cFullUserName,
				cMemberCategoryName
			)
			EXEC spSynologenGetMemberDynamic @type, @LocationId, @LanguageId, @CategoryId, @shopId, @SearchString, @OrderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @CurrentPage * @PageSize
		SELECT @LastRec = (@FirstRec + @PageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
			cId,
				cMemberId,
				cDescription,
				cAddress,
				cZipcode,
				cCity,
				cPhone,
				cFax,
				cMobile,
				cEmail,
				cWww,
				cVoip,
				cSkype,
				cCordless,
				cBody,
				cOther1,
				cOther2,
				cOther3,
				cContactFirst,
				cContactLast,
				cProfilePictureId,
				cDefaultDirectoryId,
				cOrgName,
				cActive,
				cLanguageId,
				cCreatedBy,
				cCreatedDate,
				cEditedBy,
				cEditedDate,
				cApprovedBy,
				cApprovedDate,
				cLockedBy,
				cLockedDate,
				cFullUserName,
				cMemberCategoryName
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
