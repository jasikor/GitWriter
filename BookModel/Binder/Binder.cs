namespace BookModel.Binder;

public record Binder
{
    private Binder() => Root = new("Root");

    public Folder Root { get; }

    public static Binder Create() => new();
}