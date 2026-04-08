CREATE PROCEDURE [dbo].[DeleteBook]
    @BookId INT
AS
BEGIN
    DELETE FROM Book WHERE BookId = @BookId
END
