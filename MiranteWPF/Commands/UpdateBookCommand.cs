using System;
using Domain.Commands;
using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class UpdateBookCommand : BaseCommand
{
    private readonly AddBookViewModel _viewModel;
    private readonly IUpdateBook _updateBook;

    public UpdateBookCommand(AddBookViewModel viewModel, IUpdateBook updateBook)
    {
        _viewModel = viewModel;
        _updateBook = updateBook;
    }

    public override async void Execute(object parameter)
    {
        try
        {
            var book = new BookModel
            {
                BookId = _viewModel.BookId,
                Title = _viewModel.Title,
                Author = _viewModel.Author,
                ISBN = _viewModel.ISBN,
                YearPublished = _viewModel.YearPublished,
                Genre = _viewModel.Genre
            };

            await _updateBook.ExecuteAsync(book);
            await _viewModel.LoadBooksAsync();
            _viewModel.ClearForm();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Error: {ex.Message}");
        }
    }
}
