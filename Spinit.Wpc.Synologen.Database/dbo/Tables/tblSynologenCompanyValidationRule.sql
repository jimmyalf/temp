CREATE TABLE [dbo].[tblSynologenCompanyValidationRule] (
    [cId]                    INT            IDENTITY (1, 1) NOT NULL,
    [cOrderId]               INT            NULL,
    [cValidationName]        NVARCHAR (50)  NOT NULL,
    [cValidationDescription] NVARCHAR (255) NULL,
    [cControlToValidate]     NVARCHAR (100) NOT NULL,
    [cValidationType]        INT            NOT NULL,
    [cValidationRegex]       NVARCHAR (255) NULL,
    [cErrorMessage]          NVARCHAR (255) NOT NULL,
    CONSTRAINT [PK_tblSynologenCompanyValidationRule] PRIMARY KEY CLUSTERED ([cId] ASC)
);

