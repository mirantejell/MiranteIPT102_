CREATE TABLE [dbo].[Book]
(
    [BookId]       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Title]        NVARCHAR(200) NOT NULL,
    [Author]       NVARCHAR(100) NOT NULL,
    [ISBN]         NVARCHAR(20)  NOT NULL,
    [YearPublished] INT          NOT NULL,
    [Genre]        NVARCHAR(50)  NOT NULL
)
