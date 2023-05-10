using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class FontStyleDefinition : StyleDefinition
{
    public Option<string> Family;
    public Option<float> Size;
}