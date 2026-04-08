CREATE PROCEDURE [dbo].[DeleteStudent]
    @StudentId INT
AS
BEGIN
    DELETE FROM StudentTable WHERE StudentId = @StudentId
END
