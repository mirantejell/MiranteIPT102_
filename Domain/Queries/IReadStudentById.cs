using Domain.Models;

namespace Domain.Queries;

public interface IReadStudentById
{
    System.Threading.Tasks.Task<StudentModel> ExecuteAsync(int studentId);
}
