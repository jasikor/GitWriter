using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class ParagraphSection : DocumentSection
{
    public LineSpacingStyleDefinition LineSpacing = new();
    public IList<CharacterSpan> Spans { get; init; } = new List<CharacterSpan>();
}