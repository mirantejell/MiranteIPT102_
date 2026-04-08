CREATE PROCEDURE [dbo].[CreateStudent]
    @FirstName NVARCHAR(100),
    @LastName  NVARCHAR(100),
    @Age       INT,
    @Course    NVARCHAR(100)
AS
BEGIN
    INSERT INTO StudentTable (FirstName, LastName, Age, Course)
    VALUES (@FirstName, @LastName, @Age, @Course)
END
