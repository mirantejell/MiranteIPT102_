using Dapper;
using Domain.Models;
using Domain.Queries;
using Repository.Interfaces;
using System.Linq;

namespace Framework.Queries;

public class ReadBookById : IReadBookById
{
    private readonly IRepository _repository;

    public ReadBookById(IRepository repository)
    {
        _repository = repository;
    }

    public async System.Threading.Tasks.Task<BookModel> ExecuteAsync(int bookId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@BookId", bookId);
        var result = await _repository.GetDataAsync<BookModel>("dbo.ReadBookById", parameters);
        return result.FirstOrDefault();
    }
}
