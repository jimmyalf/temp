CREATE PROCEDURE spSynologenGetOrdersByPage
					@contractId INT,
					@statusId INT,
					@settlementId INT,
					@createDateStartLimit SMALLDATETIME,
					@createDateStopLimit SMALLDATETIME,
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
				cCompanyUnit NVARCHAR(255),
				cCustomerFirstName NVARCHAR(50),
				cCustomerLastName NVARCHAR(50),
				cCustomerName NVARCHAR(100),
				cPersonalIdNumber NVARCHAR(50),
				cPhone NVARCHAR(50),
				cEmail NVARCHAR(50),
				cInvoiceNumber BIGINT,
				cStatusId INT,
				cCompanyName NVARCHAR(50),
				cContractName NVARCHAR(50),
				cStatusName NVARCHAR(50), 
				cCreatedDate SMALLDATETIME,
				cSalesPersonName NVARCHAR(255),
				cSalesPersonShopName NVARCHAR(255))

			INSERT INTO #TempTable (
				cId,
				cCompanyUnit,
				cCustomerFirstName,
				cCustomerLastName,
				cCustomerName,
				cPersonalIdNumber,
				cPhone,
				cEmail,
				cInvoiceNumber,
				cStatusId,
				cCompanyName,
				cContractName,
				cStatusName,
				cCreatedDate,
				cSalesPersonName,
				cSalesPersonShopName)
			EXEC spSynologenGetOrdersDynamic @contractId, @statusId, @settlementId, @createDateStartLimit, @createDateStopLimit, @SearchString, @OrderBy
			
		END

		--Create variable to identify the first and last record that should be selected
		DECLARE @FirstRec int, @LastRec int
		SELECT @FirstRec = @CurrentPage * @PageSize
		SELECT @LastRec = (@FirstRec + @PageSize + 1)

		--Select one page of data based on the record numbers above
		SELECT 
				cId,
				cCompanyUnit,
				cCustomerFirstName,
				cCustomerLastName,
				cCustomerName,
				cPersonalIdNumber,
				cPhone,
				cEmail,
				cInvoiceNumber,
				cStatusId,
				cCompanyName,
				cContractName,
				cStatusName,
				cCreatedDate,
				cSalesPersonName,
				cSalesPersonShopName
		FROM 
		#TempTable
		WHERE 
		ID > @FirstRec 
		AND
		ID < @LastRec
		
		--Return the total number of records 
		SELECT COUNT(*) FROM #TempTable
