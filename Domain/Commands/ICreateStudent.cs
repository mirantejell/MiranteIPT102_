using Domain.Models;

namespace Domain.Commands;

public interface ICreateStudent
{
    System.Threading.Tasks.Task<int> ExecuteAsync(StudentModel student);
}
