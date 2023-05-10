namespace BookModel.TextDocument.Styles;

public class StyleBuilder 
{
    private VerticalSpacingStyle _verticalSpacing;
    private LineSpacingStyle _lineSpacing;
    private FontStyle _font;
    private ListStyle _listStyle;


    public DocumentStyle Build()
    {
        return new DocumentStyle() {
            VerticalSpacing = _verticalSpacing,
            LineSpacing = _lineSpacing,
            Font = _font,
            ListStyle = _listStyle,
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
    public StyleBuilder Font(FontStyle s)
    {
        _font = s;
        return this;
    }
    
    public StyleBuilder ListStyle(ListStyle s)
    {
        _listStyle = s;
        return this;
    }
}
