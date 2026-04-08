using Dapper;
using Domain.Models;
using Domain.Queries;
using Repository.Interfaces;
using System.Linq;

namespace Framework.Queries;

public class ReadStudentById : IReadStudentById
{
    private readonly IRepository _repository;

    public ReadStudentById(IRepository repository)
    {
        _repository = repository;
    }

    public async System.Threading.Tasks.Task<StudentModel> ExecuteAsync(int studentId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StudentId", studentId);
        var result = await _repository.GetDataAsync<StudentModel>("dbo.ReadStudentById", parameters);
        return result.AsList().FirstOrDefault();
    }
}
