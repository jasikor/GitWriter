using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class ParagraphStyleDefinition : StyleDefinition
{
    public Option<float> LineSpacing;
    public Option<float> SpacingBelow;
    public Option<float> SpacingAbove;
    public Option<CharacterStyleDefinition> Character;

}