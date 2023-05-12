using BookModel.TextDocument.StyleDefinitions;

namespace BookModel.TextDocument;

public class ListSectionBuilder
{
    private ListStyleDefinition _listStyleDefinition;
    private ListStyleDefinitionID _listStyleDefinitionId;
    private IList<DocumentSection> _sections = new List<DocumentSection>();
    private ParagraphSection _firstParagraph;
    private ParagraphStyleDefinitionID _paragraphStyleDefinitionId;

    public ListSection Build() =>
        new ListSection() {
            ListStyle = _listStyleDefinition,
            ListStyleId = _listStyleDefinitionId,
            Sections = _sections,
            FirstParagraph= _firstParagraph,
            ParagraphStyleId = _paragraphStyleDefinitionId,
        };

    public ListSectionBuilder ListStyle(ListStyleDefinition s)
    {
        _listStyleDefinition = s;
        return this;
    }
    public ListSectionBuilder AddSection(DocumentSection s)
    {
        _sections.Add(s);
        return this;
    }

    public ListSectionBuilder ParagraphStyleId(ParagraphStyleDefinitionID id)
    {
        _paragraphStyleDefinitionId = id;
        return this;
    }

    public ListSectionBuilder FirstParagraph(ParagraphSection f)
    {
        _firstParagraph = f;
        return this;
    }
    public ListSectionBuilder ListStyleId(ListStyleDefinitionID id)
    {
        _listStyleDefinitionId = id;
        return this;
    }
}