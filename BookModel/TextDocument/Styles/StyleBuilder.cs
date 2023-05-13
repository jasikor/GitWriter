namespace BookModel.TextDocument.Styles;

public class StyleBuilder
{
    private DocumentStyle _documentStyle = new();


    public DocumentStyle Build()
    {
        return _documentStyle;
    }

    public StyleBuilder AboveSpacing(float s)
    {
        _documentStyle = _documentStyle with {SpacingAbove = s};
        return this;
    }

    public StyleBuilder BelowSpacing(float s)
    {
        _documentStyle = _documentStyle with {SpacingBelow = s};
        return this;
    }

    public StyleBuilder LineSpacing(float s)
    {
        _documentStyle = _documentStyle with {LineSpacing = s};
        return this;
    }

    public StyleBuilder CharacterStyle(CharacterStyle s)
    {
        _documentStyle = _documentStyle with {CharacterStyle = s};
        return this;
    }

    public StyleBuilder ListStyle(ListStyle s)
    {
        _documentStyle = _documentStyle with {ListStyle = s};
        return this;
    }
}