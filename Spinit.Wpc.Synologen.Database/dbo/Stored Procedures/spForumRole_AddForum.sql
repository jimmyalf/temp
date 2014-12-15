CREATE PROCEDURE spForumRole_AddForum
(
   @ForumID int,
   @RoleID int
)
AS
IF NOT EXISTS (SELECT ForumID FROM tblForumForumPermissions WHERE ForumID=@ForumID AND RoleID=@RoleID) AND
    EXISTS (SELECT ForumID FROM tblForumForums WHERE ForumID=@ForumID) AND
    EXISTS (SELECT RoleID FROM tblForumRoles WHERE RoleID=@RoleID)
    BEGIN
        INSERT INTO
            forum_ForumPermissions(ForumID, RoleID)
        VALUES
            (@ForumID, @RoleID)
    END


