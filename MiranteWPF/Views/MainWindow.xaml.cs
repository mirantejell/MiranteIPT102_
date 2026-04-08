using System.Windows;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
