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


    private static List<BinderEntry> ToBinderEntryList(string act) =>
        act.Select(a => new BinderEntry(a.ToString())).ToList();

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
        Func<List<BinderEntry>, int, List<BinderEntry>> f)
    {
        return f(ToBinderEntryList(act), index)
            .Should()
            .BeEquivalentTo(ToBinderEntryList(exp), o => o.IncludingInternalFields().WithStrictOrdering());
    }
}