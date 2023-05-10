using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class ListSection : DocumentSection
{
    public ListStyleDefinition ListStyle = new();
    public ParagraphSection FirstParagraph = new();
    public IList<DocumentSection> Sections = new List<DocumentSection>();
}