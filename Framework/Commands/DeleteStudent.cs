using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Repository.Interfaces;

namespace Framework.Commands;

public class DeleteStudent : IDeleteStudent
{
    private readonly IRepository _repository;

    public DeleteStudent(IRepository repository)
    {
        _repository = repository;
    }

    public async System.Threading.Tasks.Task<int> ExecuteAsync(StudentModel student)
    {
        return await _repository.SaveDataAsync("dbo.DeleteStudent", student.ToDeleteStudentDynamicParameters());
    }
}
