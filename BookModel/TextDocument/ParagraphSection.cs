namespace BookModel.TextDocument;

public class ParagraphSection : DocumentSection
{
    public IList<CharacterSpan> Spans { get; init; } = new List<CharacterSpan>();
}