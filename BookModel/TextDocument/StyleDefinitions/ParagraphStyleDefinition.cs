using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class ParagraphStyleDefinition : StyleDefinition
{
    public Option<float> LineSpacing;
    public Option<float> SpacingBelow;
    public Option<float> SpacingAbove;
    public CharacterStyleDefinition Character = CharacterStyleDefinition.Empty;

    public static readonly ParagraphStyleDefinition Empty = new();
}