using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Commands;
using Domain.Queries;
using Framework.Commands;
using Framework.Queries;
using MiranteWPF.Commands;
using MiranteWPF.Services;
using MiranteWPF.Stores;
using MiranteWPF.ViewModels;
using MiranteWPF.Views;
using Repository.Interfaces;

namespace MiranteWPF;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSingleton<IRepository>(new Repository.Repository(connectionString));

        // Book services
        services.AddSingleton<ICreateBook, Framework.Commands.CreateBook>();
        services.AddSingleton<IUpdateBook, Framework.Commands.UpdateBook>();
        services.AddSingleton<IDeleteBook, Framework.Commands.DeleteBook>();
        services.AddSingleton<IGetAllBooks, Framework.Queries.GetAllBooks>();
        services.AddSingleton<IReadBookById, Framework.Queries.ReadBookById>();

        services.AddSingleton<NavigationStore>();
        services.AddSingleton<DatabaseInitializer>(sp => new DatabaseInitializer(connectionString));

        services.AddSingleton<INavigationService>(sp => new NavigationService(
            sp.GetRequiredService<NavigationStore>(),
            () => sp.GetRequiredService<HomeViewModel>()));

        services.AddSingleton<OpenBookManagementCommand>(sp => new OpenBookManagementCommand(
            new NavigationService(
                sp.GetRequiredService<NavigationStore>(),
                () => sp.GetRequiredService<AddBookViewModel>())));

        services.AddSingleton<HomeViewModel>(sp =>
            new HomeViewModel(
                sp.GetRequiredService<OpenBookManagementCommand>()));

        services.AddSingleton<AddBookViewModel>(sp =>
        {
            var viewModel = new AddBookViewModel(
                sp.GetRequiredService<IGetAllBooks>(),
                null, null, null, null);

            var addCmd = new AddBookCommand(viewModel, sp.GetRequiredService<ICreateBook>());
            var updateCmd = new UpdateBookCommand(viewModel, sp.GetRequiredService<IUpdateBook>());
            var deleteCmd = new DeleteBookCommand(viewModel, sp.GetRequiredService<IDeleteBook>());
            var editCmd = new EditBookCommand(viewModel);

            viewModel.SetCommands(addCmd, updateCmd, deleteCmd, editCmd);
            return viewModel;
        });

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var dbInitializer = _serviceProvider.GetRequiredService<DatabaseInitializer>();
        await dbInitializer.InitializeAsync();

        var navigationService = _serviceProvider.GetRequiredService<INavigationService>();
        navigationService.Navigate();

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
