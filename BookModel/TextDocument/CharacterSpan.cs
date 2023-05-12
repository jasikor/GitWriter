using BookModel.TextDocument.StyleDefinitions;
using LanguageExt;

namespace BookModel.TextDocument;

public class CharacterSpan
{
    public Option<CharacterStyleDefinitionID> CharacterStyleDefinitionId;
    public CharacterStyleDefinition CharacterStyle = new();
    public string Characters = string.Empty;
}