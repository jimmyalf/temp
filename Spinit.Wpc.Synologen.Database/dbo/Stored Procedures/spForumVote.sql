create procedure spForumVote (
  @PostID int,
  @Vote nvarchar(2)
)
AS
IF NOT EXISTS (
    SELECT
        PostID 
    FROM 
        Vote 
    WHERE 
        PostID = @PostID AND Vote = @Vote
)
BEGIN
    -- Transacted insert for download count
    BEGIN TRAN
        INSERT INTO 
            Vote
        VALUES
        (
            @PostID,
            @Vote,
            1
        )
    COMMIT TRAN
END
ELSE
BEGIN
    -- Transacted update for download count
    BEGIN TRAN
        UPDATE 
          Vote
        SET 
          VoteCount  =  VoteCount + 1
        WHERE 
          PostID = @PostID AND
          Vote = @Vote
    COMMIT TRAN
END


