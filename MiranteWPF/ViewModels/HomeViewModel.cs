using System.Windows.Input;
using MiranteWPF.Commands;

namespace MiranteWPF.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public ICommand OpenBookManagementCommand { get; }

    public HomeViewModel(OpenBookManagementCommand openBookManagementCommand)
    {
        OpenBookManagementCommand = openBookManagementCommand;
    }
}
