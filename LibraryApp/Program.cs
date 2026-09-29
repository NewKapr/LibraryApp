using System.Text;
using LibraryApp.Models;

Console.OutputEncoding = Encoding.UTF8;

Book myBook = new Book();
myBook.Title = "1984";
myBook.Author = "Джордж Оруэлл";
myBook.Year = 1949;
myBook.DisplayInfo();
