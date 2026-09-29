namespace LibraryApp.Models;

public class Book : LibraryItem
{
    private int _pages;

    public int Pages
    {
        get => _pages;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Количество страниц должно быть положительным");
            _pages = value;
        }
    }

    public Book(string title, string author, int year, int pages)
        : base(title, author, year)
    {
        Pages = pages;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Книга: {Title} / {Author} ({Year}) — {Pages} стр.");
    }

}
