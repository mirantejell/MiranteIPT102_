using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Models;
using Domain.Queries;
using MiranteWPF.Commands;

namespace MiranteWPF.ViewModels;

public class AddStudentViewModel : BaseViewModel
{
    private readonly IGetAllStudents _getAllStudents;
    private int _studentId;
    private string _firstName;
    private string _lastName;
    private int _age;
    private string _course;
    private bool _isEditMode;
    private string _searchText;

    public int StudentId
    {
        get => _studentId;
        set { _studentId = value; OnPropertyChanged(); }
    }

    public string FirstName
    {
        get => _firstName;
        set { _firstName = value; OnPropertyChanged(); }
    }

    public string LastName
    {
        get => _lastName;
        set { _lastName = value; OnPropertyChanged(); }
    }

    public int Age
    {
        get => _age;
        set { _age = value; OnPropertyChanged(); }
    }

    public string Course
    {
        get => _course;
        set { _course = value; OnPropertyChanged(); }
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set { _isEditMode = value; OnPropertyChanged(); OnPropertyChanged(nameof(ButtonText)); }
    }

    public string ButtonText => IsEditMode ? "Update Student" : "Add Student";

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); FilterStudents(); }
    }

    public ObservableCollection<StudentModel> Students { get; set; }
    public ObservableCollection<StudentModel> FilteredStudents { get; set; }

    public ICommand AddStudentCommand { get; private set; }
    public ICommand UpdateStudentCommand { get; private set; }
    public ICommand DeleteStudentCommand { get; private set; }
    public ICommand EditStudentCommand { get; private set; }

    public AddStudentViewModel(IGetAllStudents getAllStudents, AddStudentCommand addStudentCommand,
        UpdateStudentCommand updateStudentCommand, DeleteStudentCommand deleteStudentCommand,
        EditStudentCommand editStudentCommand)
    {
        _getAllStudents = getAllStudents;
        Students = new ObservableCollection<StudentModel>();
        FilteredStudents = new ObservableCollection<StudentModel>();
        AddStudentCommand = addStudentCommand;
        UpdateStudentCommand = updateStudentCommand;
        DeleteStudentCommand = deleteStudentCommand;
        EditStudentCommand = editStudentCommand;
        LoadStudentsAsync();
    }

    public void SetCommands(AddStudentCommand addCmd, UpdateStudentCommand updateCmd,
        DeleteStudentCommand deleteCmd, EditStudentCommand editCmd)
    {
        AddStudentCommand = addCmd;
        UpdateStudentCommand = updateCmd;
        DeleteStudentCommand = deleteCmd;
        EditStudentCommand = editCmd;
    }

    public async Task LoadStudentsAsync()
    {
        var students = await _getAllStudents.ExecuteAsync();
        Students.Clear();
        foreach (var s in students)
            Students.Add(s);
        FilterStudents();
    }

    private void FilterStudents()
    {
        FilteredStudents.Clear();
        var filtered = string.IsNullOrWhiteSpace(SearchText)
            ? Students
            : Students.Where(s =>
                s.FirstName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                s.LastName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                s.Course.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (var s in filtered)
            FilteredStudents.Add(s);
    }

    public void ClearForm()
    {
        StudentId = 0;
        FirstName = string.Empty;
        LastName = string.Empty;
        Age = 0;
        Course = string.Empty;
        IsEditMode = false;
    }
}
