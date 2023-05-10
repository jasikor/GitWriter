using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class ParagraphSection : DocumentSection
{
    public ParagraphStyleDefinition ParagraphStyle = new();
    public IList<CharacterSpan> Spans { get; init; } = new List<CharacterSpan>();
}