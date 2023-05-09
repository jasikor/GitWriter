using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public class ListSection : DocumentSection
{
    public ParagraphSection FirstParagraph = new ParagraphSection();
    public IList<DocumentSection> Sections = new List<DocumentSection>();
}