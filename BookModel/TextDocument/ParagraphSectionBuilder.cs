using BookModel.TextDocument.StyleDefinitions;

namespace BookModel.TextDocument;

public class ParagraphSectionBuilder
{
    private ParagraphSection _paragraphSection = new();

    public ParagraphSection Build() =>
        _paragraphSection;

    public ParagraphSectionBuilder ParagraphStyle(ParagraphStyleDefinition s)
    {
        _paragraphSection.ParagraphStyle = s;
        return this;
    }

    public ParagraphSectionBuilder AddCharacterSpan(CharacterSpan s)
    {
        _paragraphSection.Spans.Add(s);
        return this;
    }

    public ParagraphSectionBuilder ParagraphStyleId(ParagraphStyleDefinitionID id)
    {
        _paragraphSection.ParagraphStyleId = id;
        return this;
    }
}