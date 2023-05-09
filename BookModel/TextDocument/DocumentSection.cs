using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public abstract class DocumentSection
{
    public DocumentSectionStyleDefinition Style { get; set; } = new DocumentSectionStyleDefinition() {
        SpacingAbove = 50,
        SpacingBelow = 15
    };
}