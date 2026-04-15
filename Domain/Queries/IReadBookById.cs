using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Queries;

public interface IReadBookById
{
    Task<BookModel> ExecuteAsync(int bookId);
}
