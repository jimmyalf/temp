create FUNCTION [sfSynologenGetArticleVATForOrderItem] (@orderId INT, @articleId INT)
	RETURNS BIT
AS
BEGIN
	--DECLARE @length INT
	--SET @length = 50
	DECLARE @retValue BIT
	SELECT @retValue = tblSynologenContractArticleConnection.cNoVat 
	FROM tblSynologenOrder
	INNER JOIN tblSynologenCompany 
		ON tblSynologenOrder.cCompanyId = tblSynologenCompany.cId
	INNER JOIN tblSynologenContractArticleConnection 
		ON tblSynologenCompany.cContractCustomerId = tblSynologenContractArticleConnection.cContractCustomerId
		AND tblSynologenContractArticleConnection.cArticleId = @articleId
	WHERE tblSynologenOrder.cId = @orderId
	RETURN @retValue 
END
