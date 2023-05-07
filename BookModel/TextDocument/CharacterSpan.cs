using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public class CharacterSpan
{
    public string Characters;
    public Option<StyleId> StyleId;
}