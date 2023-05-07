using LanguageExt;

namespace BookModel.TextDocument.Styles;

public class DocumentSectionStyleDefinition : StyleDefinition
{
    public Option<float> SpacingAbove;
    public Option<float> SpacingBelow;
    public Option<float> LineSpacing;
}