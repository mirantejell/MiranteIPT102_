using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Models;
using Domain.Queries;
using MiranteWPF.Commands;

namespace MiranteWPF.ViewModels;

public class AddBookViewModel : BaseViewModel
{
    private readonly IGetAllBooks _getAllBooks;
    private int _bookId;
    private string _title;
    private string _author;
    private string _isbn;
    private int _yearPublished;
    private string _genre;
    private bool _isEditMode;
    private string _searchText;

    public int BookId
    {
        get => _bookId;
        set { _bookId = value; OnPropertyChanged(); }
    }

    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    public string Author
    {
        get => _author;
        set { _author = value; OnPropertyChanged(); }
    }

    public string ISBN
    {
        get => _isbn;
        set { _isbn = value; OnPropertyChanged(); }
    }

    public int YearPublished
    {
        get => _yearPublished;
        set { _yearPublished = value; OnPropertyChanged(); }
    }

    public string Genre
    {
        get => _genre;
        set { _genre = value; OnPropertyChanged(); }
    }

    public bool IsEditMode
    {
        get => _isEditMode;
        set { _isEditMode = value; OnPropertyChanged(); OnPropertyChanged(nameof(ButtonText)); }
    }

    public string ButtonText => IsEditMode ? "Update Book" : "Add Book";

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); FilterBooks(); }
    }

    public ObservableCollection<BookModel> Books { get; set; }
    public ObservableCollection<BookModel> FilteredBooks { get; set; }

    public ICommand AddBookCommand { get; private set; }
    public ICommand UpdateBookCommand { get; private set; }
    public ICommand DeleteBookCommand { get; private set; }
    public ICommand EditBookCommand { get; private set; }

    public AddBookViewModel(IGetAllBooks getAllBooks, AddBookCommand addBookCommand,
        UpdateBookCommand updateBookCommand, DeleteBookCommand deleteBookCommand, EditBookCommand editBookCommand)
    {
        _getAllBooks = getAllBooks;
        Books = new ObservableCollection<BookModel>();
        FilteredBooks = new ObservableCollection<BookModel>();
        AddBookCommand = addBookCommand;
        UpdateBookCommand = updateBookCommand;
        DeleteBookCommand = deleteBookCommand;
        EditBookCommand = editBookCommand;
        LoadBooksAsync();
    }

    public void SetCommands(AddBookCommand addCmd, UpdateBookCommand updateCmd, 
        DeleteBookCommand deleteCmd, EditBookCommand editCmd)
    {
        AddBookCommand = addCmd;
        UpdateBookCommand = updateCmd;
        DeleteBookCommand = deleteCmd;
        EditBookCommand = editCmd;
    }

    public async Task LoadBooksAsync()
    {
        var books = await _getAllBooks.ExecuteAsync();
        Books.Clear();
        foreach (var book in books)
        {
            Books.Add(book);
        }
        FilterBooks();
    }

    private void FilterBooks()
    {
        FilteredBooks.Clear();
        var filtered = string.IsNullOrWhiteSpace(SearchText)
            ? Books
            : Books.Where(b => b.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                              b.Author.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                              b.ISBN.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                              b.Genre.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        foreach (var book in filtered)
        {
            FilteredBooks.Add(book);
        }
    }

    public void ClearForm()
    {
        BookId = 0;
        Title = string.Empty;
        Author = string.Empty;
        ISBN = string.Empty;
        YearPublished = 0;
        Genre = string.Empty;
        IsEditMode = false;
    }
}
