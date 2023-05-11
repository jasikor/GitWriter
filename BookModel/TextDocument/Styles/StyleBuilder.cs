namespace BookModel.TextDocument.Styles;

public class StyleBuilder 
{
    private float _aboveSpacing;
    private float _lineSpacing;
    private CharacterStyle _character;
    private ListStyle _listStyle;
    private float _belowSpacing;


    public DocumentStyle Build()
    {
        return new DocumentStyle() {
            SpacingAbove = _aboveSpacing,
            SpacingBelow = _belowSpacing,
            LineSpacing = _lineSpacing,
            Character = _character,
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
    public StyleBuilder Character(CharacterStyle s)
    {
        _character = s;
        return this;
    }
    
    public StyleBuilder ListStyle(ListStyle s)
    {
        _listStyle = s;
        return this;
    }
}
