namespace BookModel.Binder;

public class DocEntry : BinderEntry
{
    public Document.Document Document = new();
    public DocEntry(string title) : base(title) { }
}