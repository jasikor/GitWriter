namespace BookModel.Binder;

public class BinderEntry
{
    public BinderEntry(string title) => Title = title;
    public string Title { get; set; } 
    public IList<BinderEntry> Items { get; init; } = new List<BinderEntry>();
}