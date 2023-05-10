namespace BookModel.TextDocument.Styles;

public class StyleBuilder 
{
    private float _aboveSpacing;
    private ParagraphStyle _paragraph;
    private FontStyle _font;
    private ListStyle _listStyle;
    private float _belowSpacing;


    public DocumentStyle Build()
    {
        return new DocumentStyle() {
            AboveSpacing = _aboveSpacing,
            BelowSpacing = _belowSpacing,
            Paragraph = _paragraph,
            Font = _font,
            ListStyle = _listStyle,
        };
    }

    public StyleBuilder AboveSpacing(float s)
    {
        _aboveSpacing = s;
        return this;
    } public StyleBuilder BelowSpacing(float s)
    {
        _belowSpacing = s;
        return this;
    }
    public StyleBuilder ParagraphStyle(ParagraphStyle s)
    {
        _paragraph = s;
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
