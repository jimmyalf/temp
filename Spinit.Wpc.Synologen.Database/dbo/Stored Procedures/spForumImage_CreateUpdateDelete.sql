CREATE procedure spForumImage_CreateUpdateDelete
(
    @UserID  int,
    @Content image,
    @ContentType nvarchar(64),
    @ContentSize int,
    @Action  int
)
AS
BEGIN
    DECLARE @ImageID int

    -- Create
    IF @Action = 0 OR @Action = 1
    BEGIN
        -- Remove if already exists from tables: tblForumImages, tblForumUserAvatar
        SET @ImageID = (SELECT ImageID FROM tblForumUserAvatar WHERE UserID = @UserID)
        DELETE tblForumImages WHERE ImageID = @ImageID
        DELETE tblForumUserAvatar WHERE UserID = @UserID

        -- Add new entry
        INSERT INTO tblForumImages VALUES (@ContentSize, @ContentType, @Content, GetDate())
        SET @ImageID = @@Identity
 
        INSERT INTO tblForumUserAvatar VALUES (@UserID, @ImageID)
    END
    ELSE IF @Action = 2
    BEGIN
        -- Remove if already exists from tables: tblForumImages, tblForumUserAvatar
        SET @ImageID = (SELECT ImageID FROM tblForumUserAvatar WHERE UserID = @UserID)
        DELETE tblForumUserAvatar WHERE UserID = @UserID
        DELETE tblForumImages WHERE ImageID = @ImageID
    END
END


