CREATE TABLE [dbo].[tblSynologenCompanyValidationRuleConnection] (
    [cCompanyId]               INT NOT NULL,
    [cCompanyValidationRuleId] INT NOT NULL,
    CONSTRAINT [PK_tblSynologenCompanyValidationRuleConnection] PRIMARY KEY CLUSTERED ([cCompanyId] ASC, [cCompanyValidationRuleId] ASC),
    CONSTRAINT [FK_tblSynologenCompanyValidationRuleConnection_tblSynologenCompany] FOREIGN KEY ([cCompanyId]) REFERENCES [dbo].[tblSynologenCompany] ([cId]),
    CONSTRAINT [FK_tblSynologenCompanyValidationRuleConnection_tblSynologenCompanyValidationRule] FOREIGN KEY ([cCompanyValidationRuleId]) REFERENCES [dbo].[tblSynologenCompanyValidationRule] ([cId])
);

