CREATE PROCEDURE [dbo].[ReadBookById]
    @BookId INT
AS
BEGIN
    SELECT BookId, Title, Author, ISBN, YearPublished, Genre
    FROM Book
    WHERE BookId = @BookId
END
