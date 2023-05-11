using System.Diagnostics;
using System.Text;
using BookModel.TextDocument.StyleDefinitions;
using LanguageExt;

namespace BookModel.TextDocument.Styles;

public class StylesManager
{
    private Dictionary<ParagraphStyleDefinitionID, ParagraphStyleDefinition> _paragraphs =
        new() {
            {ParagraphStyleDefinitionID.Default, ParagraphStyleDefinition.Empty},
            {ParagraphStyleDefinitionID.Heading1, new() {Character = new() {FontSize = 16}}},
            {ParagraphStyleDefinitionID.Heading2, new() {Character = new() {FontSize = 14}}},
            {ParagraphStyleDefinitionID.Heading3, new() {Character = new() {FontSize = 10}}},
            {ParagraphStyleDefinitionID.Title, new() {Character = new() {FontSize = 20}}},
        };

    public DocumentStyle Apply(DocumentStyle ds, Option<ParagraphStyleDefinitionID> definitionId) =>
        definitionId.Match(di => ds.Apply(FindParagraphStyleDefinition(di)), ds);
 public DocumentStyle Apply(DocumentStyle ds, Option<ListStyleDefinitionID> definitionId) =>
        definitionId.Match(di => ds.Apply(FindListStyleDefinition(di)), ds);

    public DocumentStyle Apply(DocumentStyle ds, StyleDefinitionId definitionId) =>
        definitionId switch {
            ParagraphStyleDefinitionID pd => ds.Apply(FindParagraphStyleDefinition(pd)),
            ListStyleDefinitionID ld => ds.Apply(FindListStyleDefinition(ld)),
            CharacterStyleDefinitionID fd => ds.Apply(FindCharacterStyleDefinition(fd)),
            _ => throw new UnreachableException()
        };

    private ParagraphStyleDefinition FindParagraphStyleDefinition(ParagraphStyleDefinitionID id)
    {
        try {
            return _paragraphs[id];
        }
        catch (KeyNotFoundException) {
            return ParagraphStyleDefinition.Empty;
        }
    }

    private ListStyleDefinition FindListStyleDefinition(ListStyleDefinitionID id)
        => ListStyleDefinition.Empty;

    private CharacterStyleDefinition FindCharacterStyleDefinition(CharacterStyleDefinitionID id)
        => CharacterStyleDefinition.Empty;
}