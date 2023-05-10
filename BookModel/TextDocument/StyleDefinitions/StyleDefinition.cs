using LanguageExt;

namespace BookModel.TextDocument.Styles;

public abstract class StyleDefinition
{
    public StyleDefinitionId Id;
    public string Name { get; set; }
    public Option<StyleDefinitionId> InheritedFrom;
}