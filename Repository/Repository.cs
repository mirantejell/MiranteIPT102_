using Dapper;
using Microsoft.Data.SqlClient;
using Repository.Interfaces;
using System.Data;

namespace Repository;

public class Repository : IRepository
{
    private readonly string _connectionString;

    public Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async System.Threading.Tasks.Task<int> SaveDataAsync(string storedProcedure, DynamicParameters parameters)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        return await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    public async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<T>> GetDataAsync<T>(string storedProcedure, DynamicParameters parameters)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }
}
