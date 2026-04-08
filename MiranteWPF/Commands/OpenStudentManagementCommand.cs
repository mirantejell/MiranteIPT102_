using MiranteWPF.Services;

namespace MiranteWPF.Commands;

public class OpenStudentManagementCommand : BaseCommand
{
    private readonly INavigationService _navigationService;

    public OpenStudentManagementCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object parameter)
    {
        _navigationService.Navigate();
    }
}
