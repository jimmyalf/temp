CREATE FUNCTION sfSynologenGetArticleSPCSAccountNumberForOrderItem (@orderId INT, @articleId INT)
	RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @retValue NVARCHAR(50)
	SELECT @retValue = tblSynologenContractArticleConnection.cSPCSAccountNumber 
	FROM tblSynologenOrder
	INNER JOIN tblSynologenCompany 
		ON tblSynologenOrder.cCompanyId = tblSynologenCompany.cId
	INNER JOIN tblSynologenContractArticleConnection 
		ON tblSynologenCompany.cContractCustomerId = tblSynologenContractArticleConnection.cContractCustomerId
		AND tblSynologenContractArticleConnection.cArticleId = @articleId
	WHERE tblSynologenOrder.cId = @orderId
	RETURN @retValue 
END
