using BookModel.TextDocument.StyleDefinitions;

namespace BookModel.TextDocument;

public class ParagraphSectionBuilder
{
    private ParagraphStyleDefinition _paragraphStyleDefinition;
    private IList<CharacterSpan> _spans = new List<CharacterSpan>();
    private ParagraphStyleDefinitionID _paragraphStyleDefinitionId;

    public ParagraphSection Build() =>
        new ParagraphSection() {
            ParagraphStyle = _paragraphStyleDefinition,
            Spans = _spans,
            ParagraphStyleId = _paragraphStyleDefinitionId,
        };

    public ParagraphSectionBuilder ParagraphStyle(ParagraphStyleDefinition s)
    {
        _paragraphStyleDefinition = s;
        return this;
    }
    public ParagraphSectionBuilder AddCharacterSpan(CharacterSpan s)
    {
        _spans.Add(s);
        return this;
    }

    public ParagraphSectionBuilder ParagraphStyleId(ParagraphStyleDefinitionID id)
    {
        _paragraphStyleDefinitionId = id;
        return this;
    }
}