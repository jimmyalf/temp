

CREATE PROCEDURE spForumAddForumToRole
(
   @ForumID int,
   @Rolename nvarchar(256)
)
AS
IF NOT EXISTS (SELECT ForumID FROM PrivateForums WHERE ForumID=@ForumID AND Rolename=@Rolename) AND
    EXISTS (SELECT ForumID FROM Forums WHERE ForumID=@ForumID) AND
    EXISTS (SELECT Rolename FROM UserRoles WHERE Rolename=@Rolename)
    BEGIN
        INSERT INTO
            PrivateForums(ForumID, RoleName)
        VALUES
            (@ForumID, @Rolename)
    END


