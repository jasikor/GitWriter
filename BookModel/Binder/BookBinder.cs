namespace BookModel.Binder;

public record BookBinder()
{
    public IList<BinderEntry> Items { get; init; } = new List<BinderEntry>();
}
