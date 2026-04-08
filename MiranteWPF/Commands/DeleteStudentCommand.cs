using System;
using Domain.Commands;
using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class DeleteStudentCommand : BaseCommand
{
    private readonly AddStudentViewModel _viewModel;
    private readonly IDeleteStudent _deleteStudent;

    public DeleteStudentCommand(AddStudentViewModel viewModel, IDeleteStudent deleteStudent)
    {
        _viewModel = viewModel;
        _deleteStudent = deleteStudent;
    }

    public override async void Execute(object parameter)
    {
        if (parameter is StudentModel student)
        {
            try
            {
                await _deleteStudent.ExecuteAsync(student);
                await _viewModel.LoadStudentsAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
