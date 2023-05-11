using System.Diagnostics;
using BookModel.TextDocument.StyleDefinitions;

namespace BookModel.TextDocument.Styles;

public class StylesManager
{
    private Dictionary<ParagraphStyleDefinitionID, ParagraphStyleDefinition> _paragraphs =
        new() {
            {ParagraphStyleDefinitionID.Default, ParagraphStyleDefinition.Empty},
            {ParagraphStyleDefinitionID.Heading1, new() {Font = new() {Size = 16}}},
            {ParagraphStyleDefinitionID.Heading2, new() {Font = new() {Size = 14}}},
            {ParagraphStyleDefinitionID.Heading3, new() {Font = new() {Size = 10}}},
            {ParagraphStyleDefinitionID.Title, new() {Font = new() {Size = 20}}},
        };

    public DocumentStyle Apply(DocumentStyle ds, StyleDefinitionId definitionId) =>
        definitionId switch {
            ParagraphStyleDefinitionID pd => ds.Apply(FindParagraphStyleDefinition(pd)),
            ListStyleDefinitionID ld => ds.Apply(FindListStyleDefinition(ld)),
            FontStyleDefinitionID fd => ds.Apply(FindFontStyleDefinition(fd)),
            _ => throw new UnreachableException()
        };

    private ParagraphStyleDefinition FindParagraphStyleDefinition(ParagraphStyleDefinitionID id)
    {
        try {
            return _paragraphs[id];
        }
        catch (KeyNotFoundException) {
            return _paragraphs[ParagraphStyleDefinitionID.Default];
        }
    }

    private ListStyleDefinition FindListStyleDefinition(ListStyleDefinitionID id)
        => ListStyleDefinition.Empty;

    private FontStyleDefinition FindFontStyleDefinition(FontStyleDefinitionID id)
        => FontStyleDefinition.Empty;
}