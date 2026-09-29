using System.Text;
using LibraryApp.Models;

Console.OutputEncoding = Encoding.UTF8;

try
{
    Book book1 = new Book("1984", "Дж. Оруэлл", 1949);
    Book book2 = new Book("Гарри Поттер", "Дж. Роулинг", 1997);
    Book book3 = new Book("Мастер и Маргарита", "М. Булгаков", 1967);

    book1.DisplayInfo();
    book2.DisplayInfo();
    book3.DisplayInfo();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}

try
{
    Book invalidBook = new Book("1984", "Дж. Оруэлл", -5);
    invalidBook.DisplayInfo();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
