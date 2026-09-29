namespace LibraryApp.Models;

public class Magazine : LibraryItem
{
    private int _issueNumber;

    public int IssueNumber
    {
        get => _issueNumber;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Номер выпуска должен быть положительным");
            _issueNumber = value;
        }
    }

    public Magazine(string title, string author, int year, int issueNumber)
        : base(title, author, year)
    {
        IssueNumber = issueNumber;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Журнал: {Title} / {Author} ({Year}) — Выпуск №{IssueNumber}");
    }
}
