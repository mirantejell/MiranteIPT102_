using Domain.Models;

namespace Domain.Commands;

public interface IDeleteBook
{
    System.Threading.Tasks.Task<int> ExecuteAsync(BookModel book);
}
