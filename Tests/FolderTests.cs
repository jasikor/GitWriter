using System.Collections.Immutable;
using System.Runtime.InteropServices;
using BookModel;
using FluentAssertions;
using FluentAssertions.Collections;

namespace Tests;

public class FolderTests
{
    [Theory]
    [InlineData("Title")]
    [InlineData("Folder")]
    public void Create_SuccessfullyCreates_EmptyFolder_WithTitle(string title) =>
        new Folder(title).Title.Should().Be(title);

    private static ImmutableList<BinderEntry> ToBinderEntryList(string act) =>
        act.Select(a => new BinderEntry(a.ToString())).ToImmutableList();

    [Theory]
    [InlineData("_12", "abc", 0, 0, "_a12", "bc")]
    [InlineData("_12", "abc", 0, 1, "_b12", "ac")]
    [InlineData("_12", "abc", 0, 2, "_c12", "ac")]
    [InlineData("0_2", "abc", 1, 0, "0_a2", "bc")]
    [InlineData("01_", "abc", 2, 0, "01_a", "bc")]
    public void Promote_WorksCorrectly(string root, string subfolder, int subIndex, int promotedIndex, string expRoot,
        string expSubfolder)
    {
        ToFolderWithSubfolder(root, subfolder, subIndex)
            .Promote(subIndex, promotedIndex)
            .Should()
            .BeEquivalentTo(ToFolderWithSubfolder(expRoot, expSubfolder,subIndex),
                o => o.IncludingInternalFields().WithStrictOrdering());
    }

    private static ImmutableList<BinderEntry> ToFolderWithSubfolder(string root, string subfolder, int subIndex)
    {
        var s = new Folder("_") with {Items = ToBinderEntryList(subfolder)};
        return ToBinderEntryList(root).SetItem(subIndex, s);
    }

    [Theory]
    [InlineData("123", 1, "213")]
    [InlineData("123", 2, "132")]
    [InlineData("12", 1, "21")]
    public void MoveUp_SwapsElements_WithCorrectData(string act, int index, string exp) =>
        Move(act, index, exp, FolderExt.MoveUp);

    [Theory]
    [InlineData("123", 0, "123")]
    public void MoveUp_Ignores_Index0(string act, int index, string exp) =>
        Move(act, index, exp, FolderExt.MoveUp);

    [Theory]
    [InlineData("123", 0, "213")]
    [InlineData("123", 1, "132")]
    [InlineData("12", 0, "21")]
    public void MoveDown_SwapsElements_WithCorrectData(string act, int index, string exp) =>
        Move(act, index, exp, FolderExt.MoveDown);

    [Theory]
    [InlineData("123", 2, "123")]
    [InlineData("1234", 3, "1234")]
    public void MoveDown_Ignores_LastElement(string act, int index, string exp) =>
        Move(act, index, exp, FolderExt.MoveDown);

    private static AndConstraint<GenericCollectionAssertions<BinderEntry>> Move(string act, int index, string exp,
        Func<ImmutableList<BinderEntry>, int, ImmutableList<BinderEntry>> f) =>
        f(ToBinderEntryList(act), index)
            .Should()
            .BeEquivalentTo(ToBinderEntryList(exp), o => o.IncludingInternalFields().WithStrictOrdering());
}