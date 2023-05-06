using LanguageExt;

namespace BookModel.TextDocument;

public enum ListType
{
    Numbered,
    Bulleted
}

public class ListItem : DocumentItem 
{
    public ListType ListType;
    public Option<ListStyle> Style;
}