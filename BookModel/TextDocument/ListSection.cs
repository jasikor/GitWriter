using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class ListSection : DocumentSection
{
    public FirstListElement FirstParagraph = new FirstListElement();
    public IList<DocumentSection> Sections = new List<DocumentSection>();
}