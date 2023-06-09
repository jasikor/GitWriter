using System.Collections.Immutable;
using BookModel.Binder;
using FluentAssertions;
using FluentAssertions.Collections;

namespace Tests.Binder;

public class BinderExtTests
{
    [Theory]
    [InlineData("Title")]
    [InlineData("Folder")]
    public void Create_SuccessfullyCreates_EmptyFolder_WithTitle(string title) =>
        new Folder(title).Title.Should().Be(title);

    private static IList<BinderEntry> ToBinderEntryList(string act) =>
        act.Select(a => new BinderEntry(a.ToString())).ToList();

    [Theory]
    [InlineData("_12", "abc", 0, 0, "_a12", "bc")]
    [InlineData("_12", "abc", 0, 1, "_b12", "ac")]
    [InlineData("_12", "abc", 0, 2, "_c12", "ab")]
    [InlineData("0_2", "abc", 1, 0, "0_a2", "bc")]
    [InlineData("01_", "abc", 2, 0, "01_a", "bc")]
    public void Promote_WorksCorrectly(string root, string subfolder, int subIndex, int promotedIndex, string expRoot,
        string expSubfolder)
    {
        ToEntryWithSubfolder(root, subfolder, subIndex)
            .Promote(subIndex, promotedIndex)
            .Should()
            .Be(ToEntryWithSubfolder(expRoot, expSubfolder, subIndex));
    }

    [Theory]
    [InlineData("_12", "abc", 0, 1, "_2", "abc1")]
    [InlineData("_1", "abc", 0, 1, "_", "abc1")]
    [InlineData("0_2", "abc", 1, 2, "0_", "abc2")]
    [InlineData("0_2", "", 1, 2, "0_", "2")]
    public void Demote_WorksCorrectly(string root, string subfolder, int subIndex, int demotedIndex, string expRoot,
        string expSubfolder) =>
        ToEntryWithSubfolder(root, subfolder, subIndex)
            .Demote(demotedIndex)
            .Should()
            .Be(ToEntryWithSubfolder(expRoot, expSubfolder, subIndex));

    private static BinderEntry ToEntryWithSubfolder(string root, string subfolder, int subIndex)
    {
        var sub = ToBinderEntry(subfolder);
        var r = ToBinderEntryList(root);
        r[subIndex] = sub;
        return new BinderEntry("_") {Items = r};
    }

    private static BinderEntry ToBinderEntry(string subfolder) => new BinderEntry("_") {Items = ToBinderEntryList(subfolder)};

    [Theory]
    [InlineData("123", 1, "213")]
    [InlineData("123", 2, "132")]
    [InlineData("12", 1, "21")]
    public void MoveUp_SwapsElements_WithCorrectData(string act, int index, string exp) =>
        Move(act, index, exp, BinderExt.MoveUp);

    [Theory]
    [InlineData("123", 0, "123")]
    public void MoveUp_Ignores_Index0(string act, int index, string exp) =>
        Move(act, index, exp, BinderExt.MoveUp);

    [Theory]
    [InlineData("123", 0, "213")]
    [InlineData("123", 1, "132")]
    [InlineData("12", 0, "21")]
    public void MoveDown_SwapsElements_WithCorrectData(string act, int index, string exp) =>
        Move(act, index, exp, BinderExt.MoveDown);

    [Theory]
    [InlineData("123", 2, "123")]
    [InlineData("1234", 3, "1234")]
    public void MoveDown_Ignores_LastElement(string act, int index, string exp) =>
        Move(act, index, exp, BinderExt.MoveDown);

    private static void Move(string act, int index, string exp, Func<BinderEntry, int, BinderEntry> f) =>
        f(ToBinderEntry(act), index)
            .Should()
            .BeEquivalentTo(ToBinderEntry(exp),
                o => o.IncludingInternalFields().WithStrictOrdering());
}