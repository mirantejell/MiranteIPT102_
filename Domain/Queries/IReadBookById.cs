using Domain.Models;

namespace Domain.Queries;

public interface IReadBookById
{
    System.Threading.Tasks.Task<BookModel> ExecuteAsync(int bookId);
}
