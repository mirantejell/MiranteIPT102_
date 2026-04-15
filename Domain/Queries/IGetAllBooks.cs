using Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace Domain.Queries;
public interface IGetAllBooks
{
    Task<IEnumerable<BookModel>> ExecuteAsync();
}
