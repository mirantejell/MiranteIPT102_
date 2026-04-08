using Domain.Models;

namespace Domain.Queries;

public interface IGetAllBooks
{
    System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<BookModel>> ExecuteAsync();
}
