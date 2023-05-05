using BookModel.TextDocument;
using FluentAssertions;

namespace Tests.Document;

public class DocumentPersistenceTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(10000)]
    public void Load_WorksCorrectly(int noOfLines)
    {
        // Arrange
        Func<IEnumerable<string>> read = () => 
            Enumerable.Range(0, noOfLines)
            .Select(i => i.ToString())
            .ToList();

        var exp = read()
            .Select(i => new Paragraph(){Line =  i.ToString()});
        // Act
        var sut = new BookModel.TextDocument.Document().Load(read);

        // Assert        
        sut.Items.Should()
            .BeEquivalentTo(exp, o => o.IncludingInternalFields().WithStrictOrdering());

    }
}