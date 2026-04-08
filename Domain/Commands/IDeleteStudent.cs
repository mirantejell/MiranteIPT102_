using Domain.Models;

namespace Domain.Commands;

public interface IDeleteStudent
{
    System.Threading.Tasks.Task<int> ExecuteAsync(StudentModel student);
}
