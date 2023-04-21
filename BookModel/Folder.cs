namespace BookModel;

public record Folder(string Title = "(folder)") : BinderEntry(Title)
{
    public IEnumerable<BinderEntry> SubFolders = new List<BinderEntry>();
}

public static class FolderExt
{
    public static Folder Add(this Folder @this, BinderEntry entry) =>
        @this with {SubFolders = @this.SubFolders.Append(entry)};

    public static IEnumerable<BinderEntry> MoveUp(this IEnumerable<BinderEntry> @this, int index) =>
        index > 0
            ? @this.Swap(index - 1, index)
            : @this;

    private static IEnumerable<BinderEntry> Swap(this IEnumerable<BinderEntry> @this, int index1, int index2)
    {
        var l = @this.ToList();
        (l[index1], l[index2]) = (l[index2], l[index1]);
        return l;
    }
}