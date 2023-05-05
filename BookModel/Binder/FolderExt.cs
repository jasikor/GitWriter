using System.Diagnostics;

namespace BookModel.Binder;

public static class FolderExt
{
    public static Folder MoveUp(this Folder @this, int index) =>
        index > 0
            ? @this.Swap(index - 1, index)
            : @this;

    public static Folder MoveDown(this Folder @this, int index) =>
        index < @this.Items.Count - 1
            ? @this.Swap(index, index + 1)
            : @this;

    public static Folder Promote(this Folder root, int subFolderIndex,
        int promotedIndex)
    {
        var item = RemoveItem(root, subFolderIndex, promotedIndex);
        return root.InsertOrAppend(subFolderIndex, item);
    }

    private static BinderEntry RemoveItem(Folder root, int subFolderIndex, int promotedIndex)
    {
        if (root.Items[subFolderIndex] is Folder sub) {
            var removed = sub.Items[promotedIndex];
            sub.Items.RemoveAt(promotedIndex);
            return removed;
        }

        throw new ArgumentException("Subfolder index should indicate Folder, but it is not.");
    }

    private static Folder InsertOrAppend(this Folder @this, int index,
        BinderEntry item)
    {
        if (index == @this.Items.Count)
            @this.Items.Add(item);
        else
            @this.Items.Insert(index, item);
        return @this;
    }

    public static Folder Demote(this Folder root, int demotedIndex)
    {
        Folder sub = root.Items[demotedIndex - 1] as Folder
                     ?? throw new ArgumentException(
                         "Demoted element should be directly under the Folder, but it is not.");
        var item = root.Items[demotedIndex];
        root.Items.RemoveAt(demotedIndex);
        sub.Items.Add(item);
        return root;
    }

    private static Folder Swap(this Folder @this, int index1, int index2)
    {
        (@this.Items[index1], @this.Items[index2]) = (@this.Items[index2], @this.Items[index1]);
        return @this;
    }
}