namespace BookModel.TextDocument;

public class ParagraphItem : DocumentItem
{
    public IList<CharacterSpan> TextSpans { get; init; } = new List<CharacterSpan>();
}