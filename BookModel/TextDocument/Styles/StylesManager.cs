using BookModel.TextDocument.StyleDefinitions;
using LanguageExt;

namespace BookModel.TextDocument.Styles;

public interface IStylesManager
{
    DocumentStyle ApplyStyleId(DocumentStyle ds, Option<ParagraphStyleDefinitionID> definitionId);
    DocumentStyle ApplyStyleId(DocumentStyle ds, Option<ListStyleDefinitionID> definitionId);
    DocumentStyle ApplyStyleId(DocumentStyle ds, Option<CharacterStyleDefinitionID> definitionId);
}

public class StylesManager : IStylesManager
{
    private Dictionary<ParagraphStyleDefinitionID, Option<ParagraphStyleDefinition>> _paragraphs =
        new() {
            {ParagraphStyleDefinitionID.Default, Option<ParagraphStyleDefinition>.None}, {
                ParagraphStyleDefinitionID.Heading1,
                new ParagraphStyleDefinition() {Character = new CharacterStyleDefinition() {FontSize = 16}}
            }, {
                ParagraphStyleDefinitionID.Heading2,
                new ParagraphStyleDefinition() {Character = new CharacterStyleDefinition() {FontSize = 14}}
            }, {
                ParagraphStyleDefinitionID.Heading3,
                new ParagraphStyleDefinition() {Character = new CharacterStyleDefinition() {FontSize = 10}}
            }, {
                ParagraphStyleDefinitionID.Title,
                new ParagraphStyleDefinition() {Character = new CharacterStyleDefinition() {FontSize = 20}}
            },
        };

    public DocumentStyle ApplyStyleId(DocumentStyle ds, Option<ParagraphStyleDefinitionID> definitionId) =>
        definitionId.Match(di => ds.ApplyStyleDefinition(FindParagraphStyleDefinition(di)), ds);

    public DocumentStyle ApplyStyleId(DocumentStyle ds, Option<ListStyleDefinitionID> definitionId) =>
        definitionId.Match(di => ds.ApplyStyleDefinition(FindListStyleDefinition(di)), ds);

    public DocumentStyle ApplyStyleId(DocumentStyle ds, Option<CharacterStyleDefinitionID> definitionId) =>
        definitionId.Match(di => ds.ApplyStyleDefinition(FindCharacterStyleDefinition(di)), ds);

    private Option<ParagraphStyleDefinition> FindParagraphStyleDefinition(ParagraphStyleDefinitionID id)
    {
        try {
            return _paragraphs[id];
        }
        catch (KeyNotFoundException) {
            return Option<ParagraphStyleDefinition>.None;
        }
    }

    private Option<ListStyleDefinition> FindListStyleDefinition(ListStyleDefinitionID id)
        => Option<ListStyleDefinition>.None;

    private Option<CharacterStyleDefinition> FindCharacterStyleDefinition(CharacterStyleDefinitionID id)
        => Option<CharacterStyleDefinition>.None;
}