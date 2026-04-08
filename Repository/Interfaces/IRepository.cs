using Dapper;

namespace Repository.Interfaces;

public interface IRepository
{
    System.Threading.Tasks.Task<int> SaveDataAsync(string storedProcedure, DynamicParameters parameters);
    System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<T>> GetDataAsync<T>(string storedProcedure, DynamicParameters parameters);
}
