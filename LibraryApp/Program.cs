using System.Text;
using LibraryApp.Models;

Console.OutputEncoding = Encoding.UTF8;

var items = new List<LibraryItem>
{
    new Book("1984", "Оруэлл", 1949, 328),
    new Magazine("Science", "Редколлегия", 2023, 5),
    new Book("Анна Каренина", "Толстой", 1877, 850)
};

foreach (var item in items)
{
    item.DisplayInfo();
}
