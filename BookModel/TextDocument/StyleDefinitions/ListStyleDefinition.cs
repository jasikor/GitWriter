using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class ListStyleDefinition : StyleDefinition
{
    public Option<float> Indentation;
    public Option<float> SpacingBelowFirstElement;

}