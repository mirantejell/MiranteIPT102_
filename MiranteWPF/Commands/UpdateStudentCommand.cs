using System;
using Domain.Commands;
using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class UpdateStudentCommand : BaseCommand
{
    private readonly AddStudentViewModel _viewModel;
    private readonly IUpdateStudent _updateStudent;

    public UpdateStudentCommand(AddStudentViewModel viewModel, IUpdateStudent updateStudent)
    {
        _viewModel = viewModel;
        _updateStudent = updateStudent;
    }

    public override async void Execute(object parameter)
    {
        try
        {
            var student = new StudentModel
            {
                StudentId = _viewModel.StudentId,
                FirstName = _viewModel.FirstName,
                LastName = _viewModel.LastName,
                Age = _viewModel.Age,
                Course = _viewModel.Course
            };
            await _updateStudent.ExecuteAsync(student);
            await _viewModel.LoadStudentsAsync();
            _viewModel.ClearForm();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Error: {ex.Message}");
        }
    }
}
