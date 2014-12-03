create VIEW vwNewsIntranetNews
AS
SELECT     dbo.tblNews.*, dbo.tblBaseUsers.cFirstName + ' ' + dbo.tblBaseUsers.cLastName AS Author, tblBaseUsers.cId AS AuthorUserId
FROM         dbo.tblNews LEFT OUTER JOIN
                      dbo.tblBaseUsers ON dbo.tblNews.cCreatedBy = dbo.tblBaseUsers.cUserName
