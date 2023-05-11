using LanguageExt;

namespace BookModel.TextDocument.StyleDefinitions;

public class ListStyleDefinition : StyleDefinition
{
    public Option<float> Indentation;
    public static readonly ListStyleDefinition Empty = new();

}