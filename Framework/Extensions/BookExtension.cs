using Dapper;
using Domain.Models;

namespace Framework.Extensions;

public static class BookExtension
{
    public static DynamicParameters ToBookDynamicParameters(this BookModel book)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@BookId", book.BookId);
        parameters.Add("@Title", book.Title);
        parameters.Add("@Author", book.Author);
        parameters.Add("@ISBN", book.ISBN);
        parameters.Add("@YearPublished", book.YearPublished);
        parameters.Add("@Genre", book.Genre);
        return parameters;
    }

    public static DynamicParameters ToCreateBookDynamicParameters(this BookModel book)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Title", book.Title);
        parameters.Add("@Author", book.Author);
        parameters.Add("@ISBN", book.ISBN);
        parameters.Add("@YearPublished", book.YearPublished);
        parameters.Add("@Genre", book.Genre);
        return parameters;
    }

    public static DynamicParameters ToDeleteBookDynamicParameters(this BookModel book)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@BookId", book.BookId);
        return parameters;
    }
}
