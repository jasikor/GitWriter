using System.Collections.Immutable;

namespace BookModel;

public record Folder(string Title = "(folder)") : BinderEntry(Title)
{
    public ImmutableList<BinderEntry> SubFolders = ImmutableList<BinderEntry>.Empty;
}

public static class FolderExt
{
    public static ImmutableList<BinderEntry> MoveUp(this ImmutableList<BinderEntry> @this, int index) =>
        index > 0
            ? @this.Swap(index - 1, index)
            : @this;

    public static ImmutableList<BinderEntry> MoveDown(this ImmutableList<BinderEntry> @this, int index) =>
        index < @this.Count() - 1
            ? @this.Swap(index, index + 1)
            : @this;

    private static ImmutableList<BinderEntry> Swap(this ImmutableList<BinderEntry> @this, int index1, int index2) =>
        @this
            .SetItem(index1, @this[index2])
            .SetItem(index2, @this[index1]);
}