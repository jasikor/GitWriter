namespace BookModel.TextDocument.Styles;

public class StyleBuilder 
{
    private VerticalSpacingStyle _verticalSpacing;
    private LineSpacingStyle _lineSpacing;


    public DocumentStyle Build()
    {
        return new DocumentStyle() {
            VerticalSpacing = _verticalSpacing,
            LineSpacing = _lineSpacing,
        };
    }

    public StyleBuilder VerticalSpacing(VerticalSpacingStyle s)
    {
        _verticalSpacing = s;
        return this;
    }
    public StyleBuilder LineSpacing(LineSpacingStyle s)
    {
        _lineSpacing = s;
        return this;
    }
}
