namespace BookModel.TextDocument.Styles;

public record DocumentStyle
{
    public float SpacingAbove;
    public float SpacingBelow;
}

public static class DocumentStyleExt
{
    public static DocumentStyle ApplyStyleDefinition(this DocumentStyle ds, VerticalSpacingStyleDefinition definition) =>
        ds with {
            SpacingAbove = definition.SpacingAbove.IfNone(ds.SpacingAbove),
            SpacingBelow = definition.SpacingBelowStyle.Spacing.IfNone(ds.SpacingBelow),
        };
}