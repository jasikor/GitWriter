using BookModel;
using FluentAssertions;

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
        new Folder().Add(new Folder()).SubFolders.Count().Should().Be(1);

    [Fact]
    public void MoveUp_SwapsElements()
    {
        // Arrange
        var l = new[] {
            new Folder("f1"),
            new Folder("f2"),
            new Folder("f3")
        };

        var exp = new[] {
            new Folder("f2"),
            new Folder("f1"),
            new Folder("f3"),
        };
        // Act
        var actual  = l.MoveUp(1);

        // Assert        
        actual.Should().BeEquivalentTo(exp, o=>o.IncludingInternalFields().WithStrictOrdering());
    }
}