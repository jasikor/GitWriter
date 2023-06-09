using System.Diagnostics;

namespace BookModel.Binder;

public static class BinderExt
{
    public static BinderEntry MoveUp(this BinderEntry entry, int index) =>
        index > 0
            ? entry.Swap(index - 1, index)
            : entry;

    public static BinderEntry MoveDown(this BinderEntry entry, int index) =>
        index < entry.Items.Count - 1
            ? entry.Swap(index, index + 1)
            : entry;

    public static BinderEntry Promote(this BinderEntry entry, int subFolderIndex,
        int promotedIndex)
    {
        var item = RemoveItem(entry, subFolderIndex, promotedIndex);
        return entry.InsertOrAppend(subFolderIndex, item);
    }

    private static BinderEntry RemoveItem(BinderEntry entry, int subFolderIndex, int promotedIndex)
    {
        var sub = entry.Items[subFolderIndex];
        var removed = sub.Items[promotedIndex];
        sub.Items.RemoveAt(promotedIndex);
        return removed;
    }

    private static BinderEntry InsertOrAppend(this BinderEntry entry, int index,
        BinderEntry item)
    {
        if (index == entry.Items.Count)
            entry.Items.Add(item);
        else
            entry.Items.Insert(index, item);
        return entry;
    }

    public static BinderEntry Demote(this BinderEntry entry, int demotedIndex)
    {
        BinderEntry sub = entry.Items[demotedIndex - 1] as BinderEntry
                          ?? throw new ArgumentException(
                              "Demoted element should be directly under the BinderEntry, but it is not.");
        var item = entry.Items[demotedIndex];
        entry.Items.RemoveAt(demotedIndex);
        sub.Items.Add(item);
        return entry;
    }

    private static BinderEntry Swap(this BinderEntry entry, int index1, int index2)
    {
        var f = entry.Items.Select(i => i).ToList();
        (f[index1], f[index2]) = (f[index2], f[index1]);
        return new BinderEntry(entry.Title) {Items = f};
    }
}