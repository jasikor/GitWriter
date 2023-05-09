using BookModel.TextDocument.Styles;
using LanguageExt;

namespace BookModel.TextDocument;

public abstract class DocumentSection
{
    public VerticalSpacingStyleDefinition Style { get; set; } = new VerticalSpacingStyleDefinition() {
        SpacingAbove = 50,
        SpacingBelowStyle = new SpacingBelowStyleDefinition() {Spacing = 15}
    };
}