CREATE TABLE [dbo].[tblSynologenShop] (
    [cId]                           INT            IDENTITY (1, 1) NOT NULL,
    [cCategoryId]                   INT            NOT NULL,
    [cShopName]                     NVARCHAR (50)  NOT NULL,
    [cShopNumber]                   NVARCHAR (50)  NULL,
    [cShopDescription]              NVARCHAR (255) NULL,
    [cContactFirstName]             NVARCHAR (50)  NULL,
    [cContactLastName]              NVARCHAR (50)  NULL,
    [cEmail]                        NVARCHAR (50)  NULL,
    [cPhone]                        NVARCHAR (50)  NULL,
    [cPhone2]                       NVARCHAR (50)  NULL,
    [cFax]                          NVARCHAR (50)  NULL,
    [cUrl]                          NVARCHAR (255) NULL,
    [cMapUrl]                       NVARCHAR (255) NULL,
    [cAddress]                      NVARCHAR (50)  NULL,
    [cAddress2]                     NVARCHAR (50)  NULL,
    [cZip]                          NVARCHAR (50)  NULL,
    [cCity]                         NVARCHAR (50)  NULL,
    [cActive]                       BIT            NOT NULL,
    [cGiroId]                       INT            NULL,
    [cGiroNumber]                   NVARCHAR (50)  NULL,
    [cGiroSupplier]                 NVARCHAR (100) NULL,
    [cConcernId]                    INT            NULL,
    [cShopAccess]                   INT            NOT NULL,
    [cOrganizationNumber]           NVARCHAR (50)  NULL,
    [cLatitude]                     DECIMAL (9, 6) NULL,
    [cLongitude]                    DECIMAL (9, 6) NULL,
    [cExternalAccessUsername]       NVARCHAR (50)  NULL,
    [cExternalAccessHashedPassword] NVARCHAR (50)  NULL,
    [cShopGroupId]                  INT            NULL
);





