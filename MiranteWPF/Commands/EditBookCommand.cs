using Domain.Models;
using MiranteWPF.ViewModels;

namespace MiranteWPF.Commands;

public class EditBookCommand : BaseCommand
{
    private readonly AddBookViewModel _viewModel;

    public EditBookCommand(AddBookViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public override void Execute(object parameter)
    {
        if (parameter is BookModel book)
        {
            _viewModel.BookId = book.BookId;
            _viewModel.Title = book.Title;
            _viewModel.Author = book.Author;
            _viewModel.ISBN = book.ISBN;
            _viewModel.YearPublished = book.YearPublished;
            _viewModel.Genre = book.Genre;
            _viewModel.IsEditMode = true;
        }
    }
}
