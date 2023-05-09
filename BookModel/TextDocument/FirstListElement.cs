using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class FirstListElement
{

    public IList<CharacterSpan> Spans { get; init; } = new List<CharacterSpan>();
}