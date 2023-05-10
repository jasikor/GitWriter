namespace BookModel.TextDocument.Styles;

public class StyleBuilder 
{
    private VerticalSpacingStyle _verticalSpacing;


    public DocumentStyle Build()
    {
        return new DocumentStyle() {
            VerticalSpacing = _verticalSpacing,
        };
    }

    public StyleBuilder VerticalSpacing(VerticalSpacingStyle s)
    {
        _verticalSpacing = s;
        return this;
    }
}
