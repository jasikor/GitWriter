using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public abstract class DocumentSection
{
    public VerticalSpacingStyleDefinition VerticalSpacing { get; set; } = new();
}