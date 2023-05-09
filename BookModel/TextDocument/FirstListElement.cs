using System.Net.Http.Headers;
using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class FirstListElement
{
    public FirstListStyleDefinition Style = new();

    public IList<CharacterSpan> Spans { get; init; } = new List<CharacterSpan>();
}