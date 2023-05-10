using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public class CharacterSpan
{
    public FontStyleDefinition Style = new();
    public string Characters;
}