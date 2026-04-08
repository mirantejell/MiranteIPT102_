-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MiranteIPT102')
BEGIN
    CREATE DATABASE MiranteIPT102
END
GO

USE MiranteIPT102
GO

-- Create Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Book')
BEGIN
    CREATE TABLE Book (
        BookId INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Author NVARCHAR(100) NOT NULL,
        ISBN NVARCHAR(20) NOT NULL,
        YearPublished INT NOT NULL,
        Genre NVARCHAR(50) NOT NULL
    )
END
GO

-- Create Stored Procedures
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'CreateBook')
    DROP PROCEDURE dbo.CreateBook
GO

CREATE PROCEDURE dbo.CreateBook
    @Title NVARCHAR(200),
    @Author NVARCHAR(100),
    @ISBN NVARCHAR(20),
    @YearPublished INT,
    @Genre NVARCHAR(50)
AS
BEGIN
    INSERT INTO Book (Title, Author, ISBN, YearPublished, Genre)
    VALUES (@Title, @Author, @ISBN, @YearPublished, @Genre)
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'UpdateBook')
    DROP PROCEDURE dbo.UpdateBook
GO

CREATE PROCEDURE dbo.UpdateBook
    @BookId INT,
    @Title NVARCHAR(200),
    @Author NVARCHAR(100),
    @ISBN NVARCHAR(20),
    @YearPublished INT,
    @Genre NVARCHAR(50)
AS
BEGIN
    UPDATE Book
    SET Title = @Title,
        Author = @Author,
        ISBN = @ISBN,
        YearPublished = @YearPublished,
        Genre = @Genre
    WHERE BookId = @BookId
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'DeleteBook')
    DROP PROCEDURE dbo.DeleteBook
GO

CREATE PROCEDURE dbo.DeleteBook
    @BookId INT
AS
BEGIN
    DELETE FROM Book WHERE BookId = @BookId
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'GetAllBooks')
    DROP PROCEDURE dbo.GetAllBooks
GO

CREATE PROCEDURE dbo.GetAllBooks
AS
BEGIN
    SELECT BookId, Title, Author, ISBN, YearPublished, Genre
    FROM Book
    ORDER BY Title
END
GO

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'ReadBookById')
    DROP PROCEDURE dbo.ReadBookById
GO

CREATE PROCEDURE dbo.ReadBookById
    @BookId INT
AS
BEGIN
    SELECT BookId, Title, Author, ISBN, YearPublished, Genre
    FROM Book
    WHERE BookId = @BookId
END
GO
