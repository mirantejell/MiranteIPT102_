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
            using var createCmd = new SqlCommand($"CREATE DATABASE [{databaseName}]", connection);
            await createCmd.ExecuteNonQueryAsync();
        }
    }

    private async Task CreateTableIfNotExistsAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var sql = @"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Book')
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

        using var cmd = new SqlCommand(sql, connection);
        await cmd.ExecuteNonQueryAsync();
    }

    private async Task CreateStoredProceduresAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        var procedures = new[]
        {
            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'CreateBook')
              BEGIN EXEC('CREATE PROCEDURE dbo.CreateBook
                  @Title NVARCHAR(200), @Author NVARCHAR(100), @ISBN NVARCHAR(20), @YearPublished INT, @Genre NVARCHAR(50)
              AS BEGIN
                  INSERT INTO Book (Title, Author, ISBN, YearPublished, Genre)
                  VALUES (@Title, @Author, @ISBN, @YearPublished, @Genre)
              END') END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'UpdateBook')
              BEGIN EXEC('CREATE PROCEDURE dbo.UpdateBook
                  @BookId INT, @Title NVARCHAR(200), @Author NVARCHAR(100), @ISBN NVARCHAR(20), @YearPublished INT, @Genre NVARCHAR(50)
              AS BEGIN
                  UPDATE Book SET Title=@Title, Author=@Author, ISBN=@ISBN, YearPublished=@YearPublished, Genre=@Genre
                  WHERE BookId=@BookId
              END') END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'DeleteBook')
              BEGIN EXEC('CREATE PROCEDURE dbo.DeleteBook @BookId INT AS BEGIN
                  DELETE FROM Book WHERE BookId=@BookId
              END') END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'GetAllBooks')
              BEGIN EXEC('CREATE PROCEDURE dbo.GetAllBooks AS BEGIN
                  SELECT BookId, Title, Author, ISBN, YearPublished, Genre FROM Book ORDER BY Title
              END') END",

            @"IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'ReadBookById')
              BEGIN EXEC('CREATE PROCEDURE dbo.ReadBookById @BookId INT AS BEGIN
                  SELECT BookId, Title, Author, ISBN, YearPublished, Genre FROM Book WHERE BookId=@BookId
              END') END"
        };

        foreach (var proc in procedures)
        {
            using var cmd = new SqlCommand(proc, connection);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
