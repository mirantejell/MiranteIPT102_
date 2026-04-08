using Domain.Models;

namespace Domain.Commands;

public interface ICreateBook
{
    System.Threading.Tasks.Task<int> ExecuteAsync(BookModel book);
}
