using System.Diagnostics;

namespace BookModel.Binder;

public static class FolderExt
{
    public static Folder MoveUp(this Folder folder, int index) =>
        index > 0
            ? folder.Swap(index - 1, index)
            : folder;

    public static Folder MoveDown(this Folder folder, int index) =>
        index < folder.Items.Count - 1
            ? folder.Swap(index, index + 1)
            : folder;

    public static Folder Promote(this Folder folder, int subFolderIndex,
        int promotedIndex)
    {
        var item = RemoveItem(folder, subFolderIndex, promotedIndex);
        return folder.InsertOrAppend(subFolderIndex, item);
    }

    private static BinderEntry RemoveItem(Folder folder, int subFolderIndex, int promotedIndex)
    {
        if (folder.Items[subFolderIndex] is Folder sub) {
            var removed = sub.Items[promotedIndex];
            sub.Items.RemoveAt(promotedIndex);
            return removed;
        }

        throw new ArgumentException("Subfolder index should indicate Folder, but it is not.");
    }

    private static Folder InsertOrAppend(this Folder folder, int index,
        BinderEntry item)
    {
        if (index == folder.Items.Count)
            folder.Items.Add(item);
        else
            folder.Items.Insert(index, item);
        return folder;
    }

    public static Folder Demote(this Folder folder, int demotedIndex)
    {
        Folder sub = folder.Items[demotedIndex - 1] as Folder
                     ?? throw new ArgumentException(
                         "Demoted element should be directly under the Folder, but it is not.");
        var item = folder.Items[demotedIndex];
        folder.Items.RemoveAt(demotedIndex);
        sub.Items.Add(item);
        return folder;
    }

    private static Folder Swap(this Folder folder, int index1, int index2)
    {
        var f = folder.Items.Select(i=>i).ToList();
        (f[index1], f[index2]) = (f[index2], f[index1]);
        return new Folder(folder.Title) {Items = f};
    }

}