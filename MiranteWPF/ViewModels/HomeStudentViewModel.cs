using System.Windows.Input;
using MiranteWPF.Commands;

namespace MiranteWPF.ViewModels;

public class HomeStudentViewModel : BaseViewModel
{
    public ICommand OpenStudentManagementCommand { get; }

    public HomeStudentViewModel(OpenStudentManagementCommand openStudentManagementCommand)
    {
        OpenStudentManagementCommand = openStudentManagementCommand;
    }
}
