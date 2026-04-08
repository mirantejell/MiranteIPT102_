using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace MiranteWPF.Services;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task InitializeAsync()
    {
        await CreateDatabaseIfNotExistsAsync();
        await CreateTableIfNotExistsAsync();
        await CreateStoredProceduresAsync();
        await CreateEmployeeTableIfNotExistsAsync();
        await CreateEmployeeStoredProceduresAsync();
    }

    private async Task CreateDatabaseIfNotExistsAsync()
    {
        var builder = new SqlConnectionStringBuilder(_connectionString);
        var databaseName = builder.InitialCatalog;
        builder.InitialCatalog = "master";

        using var connection = new SqlConnection(builder.ConnectionString);
        await connection.OpenAsync();

        var checkDbSql = $"SELECT database_id FROM sys.databases WHERE Name = '{databaseName}'";
        using var checkCmd = new SqlCommand(checkDbSql, connection);
        var result = await checkCmd.ExecuteScalarAsync();

        if (result == null)
        {
            var createDbSql = $"CREATE DATABASE [{databaseName}]";
            using var createCmd = new SqlCommand(createDbSql, connection);
            await createCmd.ExecuteNonQueryAsync();
        }
    }

    private async Task CreateTableIfNotExistsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var createTableSql = @"
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
            END";

        using var cmd = new SqlCommand(createTableSql, connection);
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task CreateStoredProceduresAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var procedures = new[]
        {
            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'CreateBook')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.CreateBook
                      @Title NVARCHAR(200),
                      @Author NVARCHAR(100),
                      @ISBN NVARCHAR(20),
                      @YearPublished INT,
                      @Genre NVARCHAR(50)
                  AS
                  BEGIN
                      INSERT INTO Book (Title, Author, ISBN, YearPublished, Genre)
                      VALUES (@Title, @Author, @ISBN, @YearPublished, @Genre)
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'UpdateBook')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.UpdateBook
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
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'DeleteBook')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.DeleteBook
                      @BookId INT
                  AS
                  BEGIN
                      DELETE FROM Book WHERE BookId = @BookId
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'GetAllBooks')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.GetAllBooks
                  AS
                  BEGIN
                      SELECT BookId, Title, Author, ISBN, YearPublished, Genre
                      FROM Book
                      ORDER BY Title
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'ReadBookById')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.ReadBookById
                      @BookId INT
                  AS
                  BEGIN
                      SELECT BookId, Title, Author, ISBN, YearPublished, Genre
                      FROM Book
                      WHERE BookId = @BookId
                  END')
              END"
        };

        foreach (var procedureSql in procedures)
        {
            using var cmd = new SqlCommand(procedureSql, connection);
            await cmd.ExecuteNonQueryAsync();
        }
    }

    private async Task CreateEmployeeTableIfNotExistsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'StudentTable')
            BEGIN
                CREATE TABLE StudentTable (
                    StudentId  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    FirstName  NVARCHAR(100) NOT NULL,
                    LastName   NVARCHAR(100) NOT NULL,
                    Age        INT NOT NULL,
                    Course     NVARCHAR(100) NOT NULL
                )
            END";

        using var cmd = new SqlCommand(sql, connection);
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task CreateEmployeeStoredProceduresAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var procedures = new[]
        {
            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'CreateStudent')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.CreateStudent
                      @FirstName NVARCHAR(100), @LastName NVARCHAR(100), @Age INT, @Course NVARCHAR(100)
                  AS BEGIN
                      INSERT INTO StudentTable (FirstName, LastName, Age, Course)
                      VALUES (@FirstName, @LastName, @Age, @Course)
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'UpdateStudent')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.UpdateStudent
                      @StudentId INT, @FirstName NVARCHAR(100), @LastName NVARCHAR(100), @Age INT, @Course NVARCHAR(100)
                  AS BEGIN
                      UPDATE StudentTable
                      SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Course = @Course
                      WHERE StudentId = @StudentId
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'DeleteStudent')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.DeleteStudent
                      @StudentId INT
                  AS BEGIN
                      DELETE FROM StudentTable WHERE StudentId = @StudentId
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'GetAllStudents')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.GetAllStudents
                  AS BEGIN
                      SELECT StudentId, FirstName, LastName, Age, Course
                      FROM StudentTable
                      ORDER BY LastName, FirstName
                  END')
              END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'ReadStudentById')
              BEGIN
                  EXEC('CREATE PROCEDURE dbo.ReadStudentById
                      @StudentId INT
                  AS BEGIN
                      SELECT StudentId, FirstName, LastName, Age, Course
                      FROM StudentTable
                      WHERE StudentId = @StudentId
                  END')
              END"
        };

        foreach (var procedureSql in procedures)
        {
            using var cmd = new SqlCommand(procedureSql, connection);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
