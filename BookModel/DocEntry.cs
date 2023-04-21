namespace BookModel;

public record DocEntry(string Title="(document)") : BinderEntry(Title)
{
    public Document Document = new();
}