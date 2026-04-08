namespace Domain.Models;

public class BookModel
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int YearPublished { get; set; }
    public string Genre { get; set; }
}
