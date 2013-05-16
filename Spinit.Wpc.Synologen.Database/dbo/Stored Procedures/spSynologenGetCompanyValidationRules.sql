create PROCEDURE spSynologenGetCompanyValidationRules
					@validationRuleId INT,
					@companyId INT,			
					@status INT OUTPUT
					
AS BEGIN

	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql= 'SELECT	
					tblSynologenCompanyValidationRule.*,
					tblSynologenCompanyValidationRule.cValidationName + '' ('' + tblSynologenCompanyValidationRule.cValidationDescription + '')'' AS cNameAndDescription
				FROM tblSynologenCompanyValidationRule'

	IF (@companyId IS NOT NULL AND @companyId > 0)
	BEGIN
		SELECT @sql = @sql + 
		' INNER JOIN tblSynologenCompanyValidationRuleConnection ON 
			tblSynologenCompanyValidationRuleConnection.cCompanyValidationRuleId = tblSynologenCompanyValidationRule.cId
		AND tblSynologenCompanyValidationRuleConnection.cCompanyId = @xCompanyId'
	END
	IF (@validationRuleId IS NOT NULL AND @validationRuleId > 0)
	BEGIN
		SELECT @sql = @sql + ' WHERE tblSynologenCompanyValidationRule.cId = @xValidationRuleId'
	END

	SELECT @sql = @sql + ' ORDER BY cOrderId'

	SELECT @paramlist = '@xCompanyId INT, @xValidationRuleId INT'
	EXEC sp_executesql @sql,
						@paramlist,
						@companyId,
						@validationRuleId
				

END
