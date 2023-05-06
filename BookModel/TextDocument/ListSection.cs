using LanguageExt;

namespace BookModel.TextDocument;



public class ListSection : DocumentSection
{
    public IList<DocumentSection> Sections;
    public Option<ListStyle> Style;
}