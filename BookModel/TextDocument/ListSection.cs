using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public class ListSection : DocumentSection
{
    public IList<DocumentSection> Sections;
    public Option<StyleId> StyleId;
}