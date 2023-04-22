using System.Collections.Immutable;

namespace BookModel;

public record Folder(string Title = "(folder)") : BinderEntry(Title)
{
    public ImmutableList<BinderEntry> Items = ImmutableList<BinderEntry>.Empty;
}