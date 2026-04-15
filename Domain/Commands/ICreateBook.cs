using Domain.Models;
using System.Threading.Tasks;
namespace Domain.Commands;

public interface ICreateBook
{
   Task<int> ExecuteAsync(BookModel book);
}
