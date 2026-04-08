using MiranteWPF.Services;

namespace MiranteWPF.Commands;

public class OpenBookManagementCommand : BaseCommand
{
    private readonly INavigationService _navigationService;

    public OpenBookManagementCommand(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public override void Execute(object parameter)
    {
        _navigationService.Navigate();
    }
}
