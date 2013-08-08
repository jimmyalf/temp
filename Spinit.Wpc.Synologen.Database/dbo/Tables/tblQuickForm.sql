CREATE TABLE [dbo].[tblQuickForm] (
    [cId]                         INT             IDENTITY (1, 1) NOT NULL,
    [cFormType]                   INT             NOT NULL,
    [cName]                       NVARCHAR (255)  NOT NULL,
    [cContent]                    NTEXT           NOT NULL,
    [cMailSubject]                NVARCHAR (255)  NULL,
    [cMailTo]                     NVARCHAR (1000) NULL,
    [cMailFromDefault]            NVARCHAR (255)  NULL,
    [cReturnUrl]                  NVARCHAR (255)  NULL,
    [cComitText]                  NVARCHAR (255)  NULL,
    [cEmailConfirmationBody]      NTEXT           NULL,
    [cConfirmationEmailActivated] BIT             NULL,
    CONSTRAINT [PK_tblQuickForm] PRIMARY KEY CLUSTERED ([cId] ASC)
);

