using LanguageExt;

namespace BookModel.TextDocument.Styles;

public class VerticalSpacingStyleDefinition : StyleDefinition
{
    public Option<float> SpacingAbove;
    public SpacingBelowStyleDefinition SpacingBelowStyle;
    
}

