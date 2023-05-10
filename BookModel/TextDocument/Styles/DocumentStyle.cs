namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public VerticalSpacingStyle VerticalSpacing;
}

public static class DocumentStyleExt
{
    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds with {
            VerticalSpacing = ds.ApplyVerticalSpacing(definition)
        };

    private static VerticalSpacingStyle ApplyVerticalSpacing(this DocumentStyle ds,
        VerticalSpacingStyleDefinition definition) =>
        ds.VerticalSpacing with {
            Above = definition.Above.IfNone(ds.VerticalSpacing.Above),
            Below = definition.Below.IfNone(ds.VerticalSpacing.Below)
        };

}