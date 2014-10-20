CREATE PROCEDURE spSynologenGetContractsByPage
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
				cCode NVARCHAR(50) NULL,
				cName NVARCHAR(50),
				cDescription NVARCHAR(500),
				cAddress NVARCHAR(50),
				cAddress2 NVARCHAR(50),
				cZip NVARCHAR(50),
				cCity NVARCHAR(50),
				cPhone NVARCHAR(50),
				cPhone2 NVARCHAR(50),
				cFax NVARCHAR(50),
				cEmail NVARCHAR(50),
				cActive BIT NOT NULL,
				cNumberOfCompanies INT,
				cNumberOfArticles INT
			)

			INSERT INTO #TempTable 
			(
				cId,
				cCode,
				cName,
				cDescription,
				cAddress,
				cAddress2,
				cZip,
				cCity,
				cPhone,
				cPhone2,
				cFax,
				cEmail,
				cActive,
				cNumberOfCompanies,
				cNumberOfArticles
			)
			EXEC spSynologenGetContractsDynamic @SearchString, @OrderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @CurrentPage * @PageSize
		SELECT @LastRec = (@FirstRec + @PageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
			cId,
			cCode,
			cName,
			cDescription,
			cAddress,
			cAddress2,
			cZip,
			cCity,
			cPhone,
			cPhone2,
			cFax,
			cEmail,
			cActive,
			cNumberOfCompanies,
			cNumberOfArticles
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
