using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class EditStudentCommand : BaseCommand
{
    private readonly AddStudentViewModel _viewModel;

    public EditStudentCommand(AddStudentViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is StudentModel student)
        {
            _viewModel.StudentId = student.StudentId;
            _viewModel.FirstName = student.FirstName;
            _viewModel.LastName = student.LastName;
            _viewModel.Age = student.Age;
            _viewModel.Course = student.Course;
            _viewModel.IsEditMode = true;
        }
    }
}
