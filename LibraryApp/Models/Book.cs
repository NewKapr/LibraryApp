namespace LibraryApp.Models;

public class Book : LibraryItem, IBorrowable
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
    public bool IsAvailable { get; set; } = true;

    public Book(string title, string author, int year, int pages)
        : base(title, author, year)
    {
        Pages = pages;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Книга: {Title} / {Author} ({Year}) — {Pages} стр.");
    }

    public void Borrow(string borrowerName)
    {
        if (string.IsNullOrWhiteSpace(borrowerName))
            throw new ArgumentException("Имя читателя не может быть пустым", nameof(borrowerName));

        if (IsAvailable)
        {
            IsAvailable = false;
            Console.WriteLine($"Книга '{Title}' выдана пользователю {borrowerName}");
        }
        else
        {
            Console.WriteLine($"Книга '{Title}' уже выдана");
        }
    }

    public void Return()
    {
        IsAvailable = true;
        Console.WriteLine($"Книга '{Title}' возвращена");
    }

}
