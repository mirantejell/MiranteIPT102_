using Domain.Models;

namespace Domain.Commands;

public interface IUpdateStudent
{
    System.Threading.Tasks.Task<int> ExecuteAsync(StudentModel student);
}
