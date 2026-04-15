using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Commands;

public interface IDeleteBook
{
    Task<int> ExecuteAsync(BookModel book);
}
