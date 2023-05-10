using LanguageExt;

namespace BookModel.TextDocument.Styles;

public class CharacterStyleDefinition : StyleDefinition
{
    public Option<string> FontFamily;
    public Option<bool> Bold;
    public Option<bool> Italics;
    public Option<float> Size;
}