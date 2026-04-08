using System;
using Domain.Commands;
using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class AddBookCommand : BaseCommand
{
    private readonly AddBookViewModel _viewModel;
    private readonly ICreateBook _createBook;

    public AddBookCommand(AddBookViewModel viewModel, ICreateBook createBook)
    {
        _viewModel = viewModel;
        _createBook = createBook;
    }

    public override async void Execute(object parameter)
    {
        try
        {
            var book = new BookModel
            {
                Title = _viewModel.Title,
                Author = _viewModel.Author,
                ISBN = _viewModel.ISBN,
                YearPublished = _viewModel.YearPublished,
                Genre = _viewModel.Genre
            };

            await _createBook.ExecuteAsync(book);
            await _viewModel.LoadBooksAsync();
            _viewModel.ClearForm();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Error: {ex.Message}");
        }
    }
}
