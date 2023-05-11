using BookModel.TextDocument.StyleDefinitions;
using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public abstract class DocumentSection
{
    public Option<ParagraphStyleDefinitionID> ParagraphStyleId;
    public VerticalSpacingStyleDefinition VerticalSpacing { get; set; } = new();
}