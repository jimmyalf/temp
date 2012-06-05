CREATE PROCEDURE spSynologenGetShopsByPage
					@categoryId INT,
					@contractCustomerId INT,
					@equipmentId INT,
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
				cId INT NOT NULL,
				cShopName NVARCHAR(50) NOT NULL,
				cShopNumber NVARCHAR(50),
				cShopDescription NVARCHAR(500),
				cContactFirstName NVARCHAR(50),
				cContactLastName NVARCHAR(50),
				cEmail NVARCHAR(50),
				cPhone NVARCHAR(50),
				cPhone2 NVARCHAR(50),
				cFax NVARCHAR(50),
				cUrl NVARCHAR(50),
				cMapUrl NVARCHAR(255),
				cAddress NVARCHAR(50),
				cAddress2 NVARCHAR(50),
				cZip NVARCHAR(50),
				cCity NVARCHAR(50),
				cActive BIT NOT NULL,
				cOrganizationNumber NVARCHAR(50),
				cCategoryName NVARCHAR(50),
				cNumberOfMembers INT)

			INSERT INTO #TempTable 
			(
				cId,
				cShopName,
				cShopNumber,
				cShopDescription,
				cContactFirstName,
				cContactLastName,
				cEmail,
				cPhone,
				cPhone2,
				cFax,
				cUrl,
				cMapUrl,
				cAddress,
				cAddress2,
				cZip,
				cCity,
				cActive,
				cOrganizationNumber,
				cCategoryName,
				cNumberOfMembers
			)
			EXEC spSynologenGetShopsDynamic @categoryId, @contractCustomerId,@equipmentId,@SearchString, @OrderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @CurrentPage * @PageSize
		SELECT @LastRec = (@FirstRec + @PageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
				cId,
				cShopName,
				cShopNumber,
				cShopDescription,
				cContactFirstName,
				cContactLastName,
				cEmail,
				cPhone,
				cPhone2,
				cFax,
				cUrl,
				cMapUrl,
				cAddress,
				cAddress2,
				cZip,
				cCity,
				cActive,
				cOrganizationNumber,
				cCategoryName,
				cNumberOfMembers
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
