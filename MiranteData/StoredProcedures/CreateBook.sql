CREATE PROCEDURE [dbo].[CreateBook]
    @Title         NVARCHAR(200),
    @Author        NVARCHAR(100),
    @ISBN          NVARCHAR(20),
    @YearPublished INT,
    @Genre         NVARCHAR(50)
AS
BEGIN
    INSERT INTO Book (Title, Author, ISBN, YearPublished, Genre)
    VALUES (@Title, @Author, @ISBN, @YearPublished, @Genre)
END
