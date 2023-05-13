using BookModel.TextDocument.StyleDefinitions;

namespace BookModel.TextDocument;

public class ListSectionBuilder
{
    private ListSection _listSection = new();

    public ListSection Build() =>
        _listSection;

    public ListSectionBuilder ListStyle(ListStyleDefinition s)
    {
        _listSection.ListStyle = s;
        return this;
    }

    public ListSectionBuilder AddSection(DocumentSection s)
    {
        _listSection.Sections.Add(s);
        return this;
    }

    public ListSectionBuilder ParagraphStyleId(ParagraphStyleDefinitionID id)
    {
        _listSection.ParagraphStyleId = id;
        return this;
    }

    public ListSectionBuilder FirstParagraph(ParagraphSection f)
    {
        _listSection.FirstParagraph = f;
        return this;
    }

    public ListSectionBuilder ListStyleId(ListStyleDefinitionID id)
    {
        _listSection.ListStyleId = id;
        return this;
    }
}