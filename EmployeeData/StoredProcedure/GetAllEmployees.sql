CREATE PROCEDURE [dbo].[GetAllStudents]
AS
BEGIN
    SELECT StudentId, FirstName, LastName, Age, Course
    FROM StudentTable
    ORDER BY LastName, FirstName
END
