using Domain.Commands;
using Domain.Models;
using Framework.Extensions;
using Repository.Interfaces;

namespace Framework.Commands;

public class CreateStudent : ICreateStudent
{
    private readonly IRepository _repository;

    public CreateStudent(IRepository repository)
    {
        _repository = repository;
    }

    public async System.Threading.Tasks.Task<int> ExecuteAsync(StudentModel student)
    {
        return await _repository.SaveDataAsync("dbo.CreateStudent", student.ToCreateStudentDynamicParameters());
    }
}
