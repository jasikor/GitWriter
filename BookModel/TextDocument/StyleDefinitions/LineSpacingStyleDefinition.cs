using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class LineSpacingStyleDefinition : StyleDefinition
{
    public Option<float> Spacing;
}