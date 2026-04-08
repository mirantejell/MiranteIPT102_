CREATE PROCEDURE [dbo].[ReadStudentById]
    @StudentId INT
AS
BEGIN
    SELECT StudentId, FirstName, LastName, Age, Course
    FROM StudentTable
    WHERE StudentId = @StudentId
END
