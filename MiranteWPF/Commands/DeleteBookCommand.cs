using System;
using Domain.Commands;
using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class DeleteBookCommand : BaseCommand
{
    private readonly AddBookViewModel _viewModel;
    private readonly IDeleteBook _deleteBook;

    public DeleteBookCommand(AddBookViewModel viewModel, IDeleteBook deleteBook)
    {
        _viewModel = viewModel;
        _deleteBook = deleteBook;
    }

    public override async void Execute(object parameter)
    {
        if (parameter is BookModel book)
        {
            try
            {
                await _deleteBook.ExecuteAsync(book);
                await _viewModel.LoadBooksAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
