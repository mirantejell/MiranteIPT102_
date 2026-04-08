using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Repository.Interfaces;

namespace Framework.Commands;

public class CreateBook : ICreateBook
{
    private readonly IRepository _repository;

    public CreateBook(IRepository repository)
    {
        _repository = repository;
    }

    public async System.Threading.Tasks.Task<int> ExecuteAsync(BookModel book)
    {
        return await _repository.SaveDataAsync("dbo.CreateBook", book.ToCreateBookDynamicParameters());
    }
}
