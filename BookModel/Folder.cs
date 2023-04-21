namespace BookModel;

public record Folder(string Title="(folder)") : BinderEntry(Title)
{
    public IEnumerable<BinderEntry> SubFolders = new List<BinderEntry>();
}

public static class FolderExt
{
    public static Folder Add(this Folder @this, BinderEntry entry) =>
        @this with {SubFolders = @this.SubFolders.Append(entry)};
}