using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class Document
{
    public IList<DocumentSection> Items = new List<DocumentSection>();
}