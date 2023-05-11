namespace BookModel.TextDocument.Styles;

public class StyleBuilder 
{
    private float _aboveSpacing;
    private float _lineSpacing;
    private CharacterStyle _characterStyle;
    private ListStyle _listStyle;
    private float _belowSpacing;


    public DocumentStyle Build()
    {
        return new DocumentStyle() {
            SpacingAbove = _aboveSpacing,
            SpacingBelow = _belowSpacing,
            LineSpacing = _lineSpacing,
            CharacterStyle = _characterStyle,
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
    public StyleBuilder LineSpacing(float s)
    {
        _lineSpacing = s;
        return this;
    }
    public StyleBuilder CharacterStyle(CharacterStyle s)
    {
        _characterStyle = s;
        return this;
    }
    
    public StyleBuilder ListStyle(ListStyle s)
    {
        _listStyle = s;
        return this;
    }
}
