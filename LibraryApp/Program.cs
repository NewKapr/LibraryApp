using System.Text;
using LibraryApp.Models;
using LibraryApp.Services;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

var library = new Library();
bool running = true;

while (running)
{
    if (!Console.IsInputRedirected && !Console.IsOutputRedirected)
        Console.Clear();

    Console.WriteLine("=== Библиотека ===");
    Console.WriteLine("1. Добавить книгу");
    Console.WriteLine("2. Добавить журнал");
    Console.WriteLine("3. Показать все");
    Console.WriteLine("4. Поиск по автору");
    Console.WriteLine("5. Выдать книгу");
    Console.WriteLine("6. Вернуть книгу");
    Console.WriteLine("7. Современные книги (после 2000)");
    Console.WriteLine("0. Выход");
    Console.Write("Выбор: ");

    string? choice = Console.ReadLine()?.Trim();
    try
    {
        switch (choice)
        {
            case "1": AddBook(library); break;
            case "2": AddMagazine(library); break;
            case "3": ShowAll(library); break;
            case "4": SearchByAuthor(library); break;
            case "5": BorrowBook(library); break;
            case "6": ReturnBook(library); break;
            case "7": ShowModernBooks(library); break;
            case "0":
            case null:
                running = false;
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Ошибка: необходимо ввести целое число.");
    }
    catch (OverflowException)
    {
        Console.WriteLine("Ошибка: число выходит за допустимый диапазон.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
    catch (EndOfStreamException)
    {
        Console.WriteLine("Ввод завершён.");
        running = false;
    }

    if (running && !Console.IsInputRedirected)
    {
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey(intercept: true);
    }
}

static void AddBook(Library library)
{
    string title = ReadText("Название: ");
    string author = ReadText("Автор: ");
    int year = ReadInteger("Год: ");
    int pages = ReadInteger("Страницы: ");
    library.AddItem(new Book(title, author, year, pages));
    Console.WriteLine("Книга добавлена!");
}

static void AddMagazine(Library library)
{
    string title = ReadText("Название: ");
    string author = ReadText("Автор: ");
    int year = ReadInteger("Год: ");
    int issueNumber = ReadInteger("Номер выпуска: ");
    library.AddItem(new Magazine(title, author, year, issueNumber));
    Console.WriteLine("Журнал добавлен!");
}

static void ShowAll(Library library)
{
    var items = library.GetAllItems();
    if (items.Count == 0)
    {
        Console.WriteLine("Библиотека пуста.");
        return;
    }

    foreach (var item in items)
    {
        item.DisplayInfo();
        if (item is IBorrowable borrowable)
            Console.WriteLine(borrowable.IsAvailable ? "Доступна для выдачи" : "Выдана");
    }
}

static void SearchByAuthor(Library library)
{
    string author = ReadText("Автор или часть имени: ");
    var books = library.GetBooksByAuthor(author);
    if (books.Count == 0)
    {
        Console.WriteLine("Книги этого автора не найдены.");
        return;
    }

    books.ForEach(book => book.DisplayInfo());
}

static void BorrowBook(Library library)
{
    var book = FindBook(library);
    if (book is null)
        return;

    IBorrowable borrowable = book;
    borrowable.Borrow("Пользователь");
}

static void ReturnBook(Library library)
{
    var book = FindBook(library);
    if (book is null)
        return;

    IBorrowable borrowable = book;
    if (borrowable.IsAvailable)
    {
        Console.WriteLine("Книга уже находится в библиотеке.");
        return;
    }

    borrowable.Return();
}

static Book? FindBook(Library library)
{
    string title = ReadText("Название книги: ");
    var book = library.GetAllItems()
        .OfType<Book>()
        .FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    if (book is null)
        Console.WriteLine("Книга не найдена.");
    return book;
}

static void ShowModernBooks(Library library)
{
    var titles = library.GetModernBookTitles();
    if (titles.Count == 0)
    {
        Console.WriteLine("Книги после 2000 года не найдены.");
        return;
    }

    titles.ForEach(Console.WriteLine);
}

static string ReadText(string prompt)
{
    Console.Write(prompt);
    string? value = Console.ReadLine();
    if (value is null)
        throw new EndOfStreamException();
    if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentException("Поле не может быть пустым.");
    return value.Trim();
}

static int ReadInteger(string prompt)
{
    return int.Parse(ReadText(prompt));
}
