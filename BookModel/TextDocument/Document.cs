using System.Text;
using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public class Document
{
    public Document(string title = "")
    {
        Title = title;

    }
    public IList<DocumentSection> Items = new List<DocumentSection>();
    public string Title { get; set; }
}