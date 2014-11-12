create procedure spForumSearch_IgnoreWords_CreateDelete
(
	@WordHash int,
	@Word nvarchar (64),
	@Action int
)
AS

-- CREATE
IF @Action = 0
BEGIN
IF NOT EXISTS(SELECT * FROM tblForumSearchIgnoreWords WHERE WordHash = @WordHash)
	INSERT INTO 
		tblForumSearchIgnoreWords
	VALUES
		(@WordHash, @Word)
END

-- DELETE
ELSE IF @Action = 2
BEGIN
	DELETE 
		tblForumSearchIgnoreWords 
	WHERE 
		WordHash = @WordHash

	DELETE
		tblForumSearchBarrel
	WHERE
		WordHash = @WordHash
END


