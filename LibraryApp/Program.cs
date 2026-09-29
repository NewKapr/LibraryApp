using System.Text;
using LibraryApp.Models;

Console.OutputEncoding = Encoding.UTF8;

var book = new Book("1984", "Оруэлл", 1949, 328);
IBorrowable borrowable = book;
borrowable.Borrow("Анна");
borrowable.Borrow("Иван");
borrowable.Return();
