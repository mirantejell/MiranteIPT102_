using System.Windows.Input;
using MiranteWPF.Commands;

namespace MiranteWPF.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand OpenBookManagementCommand { get; }
    public ICommand OpenStudentManagementCommand { get; }

    public HomeViewModel(OpenBookManagementCommand openBookManagementCommand,
        OpenStudentManagementCommand openStudentManagementCommand)
    {
        OpenBookManagementCommand = openBookManagementCommand;
        OpenStudentManagementCommand = openStudentManagementCommand;
    }
}
