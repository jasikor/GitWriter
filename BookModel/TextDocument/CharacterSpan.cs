using BookModel.TextDocument.StyleDefinitions;
using LanguageExt;

namespace BookModel.TextDocument;

public class CharacterSpan
{
    public Option<CharacterStyleDefinitionID> CharacterStyleDefinitionId;
    public Option<CharacterStyleDefinition> CharacterStyle;
    public string Characters = string.Empty;
}