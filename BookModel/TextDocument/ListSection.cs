using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public class ListSection : DocumentSection
{
    public Option<ListStyleDefinitionID> ListStyleId;
    public ListStyleDefinition ListStyle = new();
    public ParagraphSection FirstParagraph = new();
    public IList<DocumentSection> Sections = new List<DocumentSection>();
}