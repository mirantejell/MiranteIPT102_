using Domain.Models;

namespace Domain.Commands;

public interface IUpdateBook
{
    System.Threading.Tasks.Task<int> ExecuteAsync(BookModel book);
}
