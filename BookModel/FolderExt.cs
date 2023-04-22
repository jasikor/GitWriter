using System.Collections.Immutable;
using System.Diagnostics;

namespace BookModel;

public static class FolderExt
{
    public static ImmutableList<BinderEntry> MoveUp(this ImmutableList<BinderEntry> @this, int index) =>
        index > 0
            ? @this.Swap(index - 1, index)
            : @this;

    public static ImmutableList<BinderEntry> MoveDown(this ImmutableList<BinderEntry> @this, int index) =>
        index < @this.Count - 1
            ? @this.Swap(index, index + 1)
            : @this;

    public static ImmutableList<BinderEntry> Promote(this ImmutableList<BinderEntry> root, int subFolderIndex,
        int promotedIndex) =>
        root[subFolderIndex] switch {
            Folder sub =>
                root
                    .SetItem(subFolderIndex, sub with {Items = sub.Items.RemoveAt(promotedIndex)})
                    .InsertOrAppend(subFolderIndex + 1, sub.Items[promotedIndex]),
            _ => throw new UnreachableException(),
        };

    public static ImmutableList<BinderEntry> Demote(this ImmutableList<BinderEntry> root, int demotedIndex) =>
        root[demotedIndex - 1] switch {
            Folder sub =>
                root.RemoveAt(demotedIndex)
                    .SetItem(demotedIndex - 1, sub with {Items = sub.Items.Add(root[demotedIndex])}),
            _ => throw new UnreachableException(),
        };

    private static ImmutableList<BinderEntry> InsertOrAppend(this ImmutableList<BinderEntry> @this, int index,
        BinderEntry item) =>
        index == @this.Count
            ? @this.Add(item)
            : @this.Insert(index, item);


    private static ImmutableList<BinderEntry> Swap(this ImmutableList<BinderEntry> @this, int index1, int index2) =>
        @this
            .SetItem(index1, @this[index2])
            .SetItem(index2, @this[index1]);
}