using System;
using Domain.Commands;
using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class AddStudentCommand : BaseCommand
{
    private readonly AddStudentViewModel _viewModel;
    private readonly ICreateStudent _createStudent;

    public AddStudentCommand(AddStudentViewModel viewModel, ICreateStudent createStudent)
    {
        _viewModel = viewModel;
        _createStudent = createStudent;
    }

    public override async void Execute(object parameter)
    {
        try
        {
            var student = new StudentModel
            {
                FirstName = _viewModel.FirstName,
                LastName = _viewModel.LastName,
                Age = _viewModel.Age,
                Course = _viewModel.Course
            };
            await _createStudent.ExecuteAsync(student);
            await _viewModel.LoadStudentsAsync();
            _viewModel.ClearForm();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Error: {ex.Message}");
        }
    }
}
