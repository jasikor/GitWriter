namespace BookModel;

public record Folder(string Title = "(folder)") : BinderEntry(Title)
{
    public List<BinderEntry> SubFolders = new List<BinderEntry>();
}

public static class FolderExt
{
    public static Folder Add(this Folder @this, BinderEntry entry) =>
        @this with {SubFolders = @this.SubFolders.Append(entry).ToList()};

    public static List<BinderEntry> MoveUp(this List<BinderEntry> @this, int index) =>
        index > 0
            ? @this.Swap(index - 1, index)
            : @this;

    public static List<BinderEntry> MoveDown(this List<BinderEntry> @this, int index) =>
        index < @this.Count() - 1
            ? @this.Swap(index, index + 1)
            : @this;

    private static List<BinderEntry> Swap(this List<BinderEntry> @this, int index1, int index2)
    {
        var l = @this;
        (l[index1], l[index2]) = (l[index2], l[index1]);
        return l;
    }
}