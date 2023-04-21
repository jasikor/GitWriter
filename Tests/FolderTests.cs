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

    [Fact]
    public void Add_AddsItem() =>
        new Folder()
            .Add(new Folder())
            .SubFolders.Count()
            .Should().Be(1);


    private static IEnumerable<BinderEntry> ToBinderEntryList(string act) =>
        act.Select(a => new Folder(a.ToString()));

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

    private static AndConstraint<GenericCollectionAssertions<BinderEntry>> Move(string act, int index, string exp,
        Func<IEnumerable<BinderEntry>, int, IEnumerable<BinderEntry>> f)
    {
        return f(ToBinderEntryList(act), index)
            .Should()
            .BeEquivalentTo(ToBinderEntryList(exp), o => o.IncludingInternalFields().WithStrictOrdering());
    }
}