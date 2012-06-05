CREATE procedure spForumRemoveForumFromRole
(
   @ForumID int,
   @Rolename nvarchar(256)
)
AS
IF EXISTS (SELECT ForumID FROM PrivateForums WHERE ForumID=@ForumID AND Rolename=@Rolename)
DELETE FROM
    PrivateForums
WHERE
    ForumID=@ForumID AND Rolename=@Rolename


