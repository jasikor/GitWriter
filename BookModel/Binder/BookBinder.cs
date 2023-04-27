namespace BookModel.Binder;

public record BookBinder()
{
    public Folder Root { get; init; } = new("Root");
}