using LanguageExt;

namespace BookModel.TextDocument.Styles;

public abstract class StyleDefinition
{
    public StyleId Id;
    public string Name { get; set; }
    public Option<StyleId> InheritedFrom;
}