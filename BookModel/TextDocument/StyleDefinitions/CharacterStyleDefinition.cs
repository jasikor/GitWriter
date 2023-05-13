using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class CharacterStyleDefinition : StyleDefinition
{
    public Option<string> FontFamily;
    public Option<float> FontSize;
}