CREATE PROCEDURE [dbo].[UpdateBook]
    @BookId        INT,
    @Title         NVARCHAR(200),
    @Author        NVARCHAR(100),
    @ISBN          NVARCHAR(20),
    @YearPublished INT,
    @Genre         NVARCHAR(50)
AS
BEGIN
    UPDATE Book
    SET Title         = @Title,
        Author        = @Author,
        ISBN          = @ISBN,
        YearPublished = @YearPublished,
        Genre         = @Genre
    WHERE BookId = @BookId
END
