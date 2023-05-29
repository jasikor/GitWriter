namespace BookModel.Binder;

public record BookBinder()
{
    public IList<Folder> Items { get; init; } = new List<Folder>();
}
