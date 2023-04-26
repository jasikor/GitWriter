namespace BookModel.Binder;

public record DocEntry(string Title="(document)") : BinderEntry(Title)
{
    public Document.Document Document = new();
}