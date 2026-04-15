using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Commands;

public interface IUpdateBook
{
    Task<int> ExecuteAsync(BookModel book);
}
