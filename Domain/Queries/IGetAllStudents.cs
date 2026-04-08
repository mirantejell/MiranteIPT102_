using Domain.Models;

namespace Domain.Queries;

public interface IGetAllStudents
{
    System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<StudentModel>> ExecuteAsync();
}
