using BookModel.TextDocument.Styles;

namespace BookModel.TextDocument;

public enum ListType
{
    Numbered,
    Bulleted
}

public abstract class ListStyle : Style
{
    public ListType Type;
}