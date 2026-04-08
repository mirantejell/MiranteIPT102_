using Dapper;
using Domain.Models;
using Domain.Queries;
using Repository.Interfaces;

namespace Framework.Queries;

public class GetAllBooks : IGetAllBooks
{
    private readonly IRepository _repository;

    public GetAllBooks(IRepository repository)
    {
        _repository = repository;
    }

    public async System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<BookModel>> ExecuteAsync()
    {
        return await _repository.GetDataAsync<BookModel>("dbo.GetAllBooks", new DynamicParameters());
    }
}
