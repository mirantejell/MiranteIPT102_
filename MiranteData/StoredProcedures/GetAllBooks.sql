CREATE PROCEDURE [dbo].[GetAllBooks]
AS
BEGIN
    SELECT BookId, Title, Author, ISBN, YearPublished, Genre
    FROM Book
    ORDER BY Title
END
