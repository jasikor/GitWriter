using BookModel.Binder;
using FluentAssertions;

namespace Tests.Binder;

public class DocEntryTests
{
    [Theory]
    [InlineData("Title")]
    [InlineData("Folder")]
    public void Create_SuccessfullyCreatesDocument_WithTitle(string title) =>
        new DocEntry(title).Title.Should().Be(title);
}