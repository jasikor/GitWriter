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