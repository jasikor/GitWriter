using LanguageExt;

namespace BookModel.TextDocument.Styles;

public enum ListType
{
    Numbered,
    Bulleted
}

public abstract class ListStyleDefinition : StyleDefinition
{
    public ListType Type;
    public Option<float> Indentation;
}