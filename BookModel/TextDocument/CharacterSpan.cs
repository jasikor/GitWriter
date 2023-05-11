using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public class CharacterSpan
{
    public Option<CharacterStyleDefinitionID> CharacterStyleDefinitionId;
    public CharacterStyleDefinition Style = new();
    public string Characters;
}