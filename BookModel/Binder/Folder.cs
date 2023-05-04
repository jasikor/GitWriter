namespace BookModel.Binder;

public class Folder : BinderEntry
{
    public IList<BinderEntry> Items { get; init; } = new List<BinderEntry>();
    public Folder(string title) : base(title) { }
}