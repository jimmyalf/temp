create PROCEDURE spSynologenDisconnectCompanyFromValidationRule
					@validationRuleId INT,
					@companyId INT,
					@status INT OUTPUT
AS BEGIN
	DELETE FROM tblSynologenCompanyValidationRuleConnection
		WHERE cCompanyId = @companyId
		AND cCompanyValidationRuleId = @validationRuleId
END	

IF (@@ERROR = 0) BEGIN
	SELECT @status = 0
END

ELSE BEGIN
	SELECT @status = @@ERROR
END
