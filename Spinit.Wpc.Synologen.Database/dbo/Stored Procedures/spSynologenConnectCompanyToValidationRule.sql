create PROCEDURE spSynologenConnectCompanyToValidationRule
					@validationRuleId INT,
					@companyId INT,
					@status INT OUTPUT
AS BEGIN
	INSERT INTO tblSynologenCompanyValidationRuleConnection	(cCompanyId, cCompanyValidationRuleId)
	VALUES 	(@companyId, @validationRuleId)
END	

IF (@@ERROR = 0) BEGIN
	SELECT @status = 0
END

ELSE BEGIN
	SELECT @status = @@ERROR
END
