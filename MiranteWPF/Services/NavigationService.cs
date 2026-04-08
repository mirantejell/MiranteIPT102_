using System;
using MiranteWPF.Stores;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Services;

public class NavigationService : INavigationService
{
    private readonly NavigationStore _navigationStore;
    private readonly Func<BaseViewModel> _createViewModel;

    public NavigationService(NavigationStore navigationStore, Func<BaseViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}
