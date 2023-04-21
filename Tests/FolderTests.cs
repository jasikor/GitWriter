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
}